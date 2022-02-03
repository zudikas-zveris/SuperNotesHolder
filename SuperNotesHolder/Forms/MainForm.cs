using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SuperNotesHolder.Controllers;
using SuperNotesHolder.Models;
using Newtonsoft.Json;
using SuperNotesHolder.Forms;
using SuperNotesHolder.Utils;
using static SuperNotesHolder.NotePreviewControl;

namespace SuperNotesHolder
{
    public partial class MainForm : Form
    {
        public enum DockType
        {
            None = 0,
            Top = 1,
            Left = 2,
            Right = 3,
            Bottom = 4
        }

        private const int SNAP_DIST = 50;
        private const int DOCK_LINE_WIDTH = 3;


        private int normalHeight;
        private int normalWidth;
        private int normalTop;
        private int normalLeft;
        private bool isExpanded;
        private bool preventCollapse;
        private DockType dockLocation;

        private NotesController notesController;

        public MainForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            HotKeyManager.MainForm = this;

            noteEditControl.closeButton.Click += (s, e) => {
                preventCollapse = true;
                if (MessageBox.Show("Are you sure you want to delete selected items?", "Delete selected", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RemoveNote(noteEditControl.Note);
                    notesController.DeleteNote(noteEditControl.Note);
                    noteEditControl.Note = null;
                    CloseNoteEditControl();
                }
                preventCollapse = false;
            };
            noteEditControl.okButton.Click += (s, e) =>
            {
                UpdateNote(noteEditControl.Note);
                notesController.DeleteNote(noteEditControl.Note);
                notesController.AddNote(noteEditControl.Note);                
                notesController.SaveNotes();
                noteEditControl.Note = null;
                CloseNoteEditControl();
            };

            normalHeight = Height;
            normalWidth = Width;
            normalTop = Top;
            normalLeft = Left;
            dockLocation = DockType.None;

            isExpanded = true;
            preventCollapse = false;

            notesController = new NotesController();
            notesController.LoadNotes();
            FillNotes();            
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                if (!isExpanded)
                {
                    // turn on WS_EX_TOOLWINDOW style bit
                    cp.ExStyle |= 0x80;
                }
                    

                return cp;
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            LoadMainFormSettings();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveMainFormSettings();
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            Screen scn = Screen.FromPoint(this.Location);
            Rectangle screenRect = scn.Bounds;

            if (this.Top <= screenRect.Top || DoSnap(this.Top, screenRect.Top))
            {
                this.Top = screenRect.Top;                
            }

            if (this.Left <= screenRect.Left || DoSnap(this.Left, screenRect.Left))
            {
                this.Left = screenRect.Left;
            }

            if (this.Right >= screenRect.Right || DoSnap(screenRect.Right, this.Right))
            {
                this.Left = screenRect.Right - this.Width;
            }

            if (this.Top != screenRect.Top && (this.Bottom >= screenRect.Bottom || DoSnap(screenRect.Bottom, this.Bottom)))
            {
                this.Top = screenRect.Bottom - this.Height;
            }

            
            if (this.Top == screenRect.Top)
            {
                dockLocation = DockType.Top;
            }
            else if(this.Left == screenRect.Left)
            {
                dockLocation = DockType.Left;
            }
            else if (this.Right == screenRect.Right)
            {
                dockLocation = DockType.Right;
            }
            else if (this.Bottom == screenRect.Bottom)
            {
                dockLocation = DockType.Bottom;
            }
            else
            {
                dockLocation = DockType.None;
            }
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            FormExpand(false);            
        }

        private void MainForm_MouseEnter(object sender, EventArgs e)
        {
            FormExpand(true);
            collapseTimer.Start();
        }

        private void addNoteButton_MouseLeave(object sender, EventArgs e)
        {
            //FormExpand(false);
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            preventCollapse = true;
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            preventCollapse = false;
            if (WindowState == FormWindowState.Normal && isExpanded)
            {
                normalHeight = Height;
                normalWidth = Width;
                normalTop = Top;
                normalLeft = Left;                
            }
        }

        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal && isExpanded)
            {
                normalHeight = Height;
                normalWidth = Width;
                normalTop = Top;
                normalLeft = Left;
            }
        }

        private void collapseTimer_Tick(object sender, EventArgs e)
        {            
            Point pt = PointToClient(Cursor.Position);

            int corY = Height - ClientSize.Height;
            int corX = Width - ClientSize.Width;

            if (pt.X >= -corX && pt.Y >= -corY && pt.X <= Width && pt.Y <= Height)
            {
                // inside
                //Console.WriteLine("inside");
            }
            else
            {                
                var ctrl = FindFocusedControl(this);
                
                if (ctrl == addNoteButton)
                {                    
                    this.ActiveControl = null;
                }

                if (!ContainsFocus || (ContainsFocus && ctrl == null))
                {
                    FormExpand(false);
                    collapseTimer.Stop();
                }

            }
        }

        private void addNoteButton_Click(object sender, EventArgs e)
        {
            Note note = notesController.NewNote();
            ShowNoteEditControl(note);
            AddNote(note);
        }

        private void FormExpand(bool value)
        {
            if (isExpanded && preventCollapse && !value)
                return;

            isExpanded = value;

            if (value)
            {
                if (WindowState == FormWindowState.Normal)
                {                    
                    FormBorderStyle = FormBorderStyle.Sizable;                    
                    this.Padding = new Padding(0);
                    BackColor = SystemColors.Control;
                    Height = normalHeight;
                    Width = normalWidth;
                    Left = normalLeft;
                    Top = normalTop;
                    addNoteButton.Focus();
                }                
            }
            else
            {
                if (dockLocation == DockType.None)
                    return;

                this.Visible = false;
                this.AutoScrollPosition = new Point(0, 0);
                FormBorderStyle = FormBorderStyle.None;                                

                if (dockLocation == DockType.Top)
                {                    
                    this.Padding = new Padding(0, DOCK_LINE_WIDTH, 0,0);
                    Height = DOCK_LINE_WIDTH;
                }
                else if (dockLocation == DockType.Left) 
                {
                    this.Padding = new Padding(DOCK_LINE_WIDTH, 0, 0, 0);
                    Width = DOCK_LINE_WIDTH;
                    Screen scn = Screen.FromPoint(this.Location);
                    this.Left = scn.Bounds.Left;
                }
                else if (dockLocation == DockType.Right)
                {
                    this.Padding = new Padding(DOCK_LINE_WIDTH, 0, 0, 0);
                    Width = DOCK_LINE_WIDTH;
                    Screen scn = Screen.FromPoint(this.Location);
                    this.Left = scn.Bounds.Right - Width;
                }
                else if (dockLocation == DockType.Bottom)
                {
                    this.Padding = new Padding(0, DOCK_LINE_WIDTH, 0, 0);
                    Height = DOCK_LINE_WIDTH;
                    Screen scn = Screen.FromPoint(this.Location);
                    this.Top = scn.Bounds.Bottom - Height;
                }
                Application.DoEvents();

                this.Visible = true;
                BackColor = Color.Red;

            }
        }

        private bool DoSnap(int pos, int edge)
        {
            int delta = pos - edge;
            return delta > 0 && delta <= SNAP_DIST;
        }

        private void FillNotes()
        {
            notesPanel.Controls.Clear();

            foreach (Note note in notesController.Notes)
            {
                AddNote(note);
            }

            InitHotKeys();
        }

        private void ClearNotes()
        {
            for (int i = notesPanel.Controls.Count - 1; i >= 0; i--)
            {
                Control ctrl = notesPanel.Controls[i];
                notesPanel.Controls.RemoveAt(i);
                ctrl.Dispose();
            }
        }

        private void AddNote(Note note)
        {
            NotePreviewControl notes = new NotePreviewControl();

            notes.Dock = DockStyle.Top;
            notes.Note = note;
            notes.deleteButton.Click += (s, e) =>
            {
                preventCollapse = true;
                if (MessageBox.Show("Are you sure you want to delete selected items?", "Delete selected", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    notesController.DeleteNote(note);
                    notesPanel.Controls.Remove(notes);
                }
                preventCollapse = false;
            };

            notes.textControl.DoubleClick += (s, e) =>
            {
                ShowNoteEditControl(note);
            };

            notes.OnMoveUp += (s, e) =>
            {
                notesController.MoveUpNote(((MoveUpEventArgs)e).Note);
                FillNotes();
            };

            notesPanel.Controls.Add(notes);
        }

        private void UpdateNote(Note note)
        {
            for (int i = notesPanel.Controls.Count - 1; i >= 0; i--)
            {
                NotePreviewControl ctrl = notesPanel.Controls[i] as NotePreviewControl;
                if (ctrl.Note == note)
                {
                    ctrl.Note = note;
                    return;
                }
            }
        }

        private void RemoveNote(Note note)
        {
            for (int i = notesPanel.Controls.Count - 1; i >= 0; i--)
            {
                NotePreviewControl ctrl = notesPanel.Controls[i] as NotePreviewControl;
                if (ctrl.Note == note)
                {
                    notesPanel.Controls.RemoveAt(i);
                    ctrl.Dispose();
                }                
            }
        }

        private void LoadMainFormSettings()
        {
            MainFormSettings settings = JsonConvert.DeserializeObject<MainFormSettings>(Properties.Settings.Default.MainFormSettings);

            if (settings == null)
                return;

            WindowState = settings.WindowState;
            normalTop = settings.Top;
            normalLeft = settings.Left;
            normalWidth = settings.Width;
            normalHeight = settings.Height;
            dockLocation = settings.DockLocation;

            if (WindowState == FormWindowState.Normal)
            {
                Top = settings.Top;
                Left = settings.Left;
                Width = settings.Width;
                Height = settings.Height;
            }
        }

        private void SaveMainFormSettings()
        {
            MainFormSettings settings = new MainFormSettings()
            {
                WindowState = WindowState,
                Left = normalLeft,
                Top = normalTop,
                Width = normalWidth,
                Height = normalHeight,
                DockLocation = dockLocation
            };

            Properties.Settings.Default.MainFormSettings = JsonConvert.SerializeObject(settings);
            Properties.Settings.Default.Save();
        }

        private void ShowNoteEditControl(Note note)
        {            
            InitEditorHotkeys();

            noteEditControl.Note = note;
            noteEditControl.BringToFront();
            noteEditControl.Focus();
        }

        private void CloseNoteEditControl()
        {
            noteEditControl.SendToBack();
            InitHotKeys();
            addNoteButton.Focus();            
        }

        private void InitHotKeys()
        {
            HotKeyManager.RemoveHotKeys();
            HotKeyManager.AddHotKey(DeleteSelected, Keys.Delete);
        }

        private void InitEditorHotkeys()
        {
            HotKeyManager.RemoveHotKeys();

            // register the hotkeys with the form
            HotKeyManager.AddHotKey(noteEditControl.OpenSearch, Keys.F, true);
            HotKeyManager.AddHotKey(noteEditControl.CloseSearch, Keys.Escape);
            HotKeyManager.AddHotKey(noteEditControl.FormatAsXml, Keys.X, true, true, true);
            HotKeyManager.AddHotKey(noteEditControl.FormatAsJson, Keys.J, true, true, true);

            //HotKeyManager.AddHotKey(this, OpenFindDialog, Keys.F, true, false, true);
            //HotKeyManager.AddHotKey(this, OpenReplaceDialog, Keys.R, true);
            //HotKeyManager.AddHotKey(this, OpenReplaceDialog, Keys.H, true);
            //HotKeyManager.AddHotKey(this, Uppercase, Keys.U, true);
            //HotKeyManager.AddHotKey(this, Lowercase, Keys.L, true);
            //HotKeyManager.AddHotKey(this, ZoomIn, Keys.Oemplus, true);
            //HotKeyManager.AddHotKey(this, ZoomOut, Keys.OemMinus, true);
            //HotKeyManager.AddHotKey(this, ZoomDefault, Keys.D0, true);

        }

        private void ClearHotKeys()
        {
            HotKeyManager.RemoveHotKeys();
        }

        private void DeleteSelected()
        {
            preventCollapse = true;

            if (MessageBox.Show("Are you sure you want to delete selected items?", "Delete selected", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = notesPanel.Controls.Count - 1; i >= 0; i--)
                {
                    NotePreviewControl ctrl = (NotePreviewControl)notesPanel.Controls[i];
                    if (ctrl.Selected)
                    {
                        notesController.DeleteNote(ctrl.Note);
                        notesPanel.Controls.RemoveAt(i);
                        ctrl.Dispose();
                    }

                }
            }

            preventCollapse = false;
        }

        private Control FindFocusedControl(Control control)
        {
            var container = control as IContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as IContainerControl;
            }
            return control;
        }


    }
}

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

        private int normalHeight;
        private int normalWidth;
        private int normalTop;
        private int normalLeft;

        private DockType dockLocation;

        private NotesController notesController;

        public MainForm()
        {
            InitializeComponent();

            normalHeight = Height;
            normalWidth = Width;
            normalTop = Top;
            normalLeft = Left;
            dockLocation = DockType.None;            

            notesController = new NotesController();
            notesController.LoadNotes();
            FillNotes();
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

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                normalHeight = Height;
                normalWidth = Width;
                normalTop = Top;
                normalLeft = Left;
            }
        }

        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
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
                
            }
            else
            {
                if (!ContainsFocus)
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
        }



        private void FormExpand(bool value)
        {
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
                }                
            }
            else
            {
                if (dockLocation == DockType.None)
                    return;

                this.AutoScrollPosition = new Point(0, 0);
                FormBorderStyle = FormBorderStyle.None;
                //this.Padding = new Padding(10);
                BackColor = Color.Red;

                if (dockLocation == DockType.Top)
                {                    
                    this.Padding = new Padding(0,10,0,0);
                    Height = 5;
                }
                else if (dockLocation == DockType.Left) 
                {
                    this.Padding = new Padding(10, 0, 0, 0);
                    Width = 5;
                    Screen scn = Screen.FromPoint(this.Location);
                    this.Left = scn.Bounds.Left;
                }
                else if (dockLocation == DockType.Right)
                {
                    this.Padding = new Padding(10, 0, 0, 0);
                    Width = 5;
                    Screen scn = Screen.FromPoint(this.Location);
                    this.Left = scn.Bounds.Right - Width;
                }
                else if (dockLocation == DockType.Bottom)
                {
                    this.Padding = new Padding(0, 10, 0, 0);
                    Height = 5;
                    Screen scn = Screen.FromPoint(this.Location);
                    this.Top = scn.Bounds.Bottom - Height;
                }

            }
        }

        private bool DoSnap(int pos, int edge)
        {
            int delta = pos - edge;
            return delta > 0 && delta <= SNAP_DIST;
        }

        private void FillNotes()
        {
            foreach(Note note in notesController.Notes)
            {
                NotePreviewControl notes = new NotePreviewControl();

                notes.Dock = DockStyle.Top;
                notes.Note = note;
                notes.deleteButton.Click += (s, e) =>
                {
                    notesController.DeleteNote(note);
                    notesPanel.Controls.Remove(notes);
                };
                notes.textControl.DoubleClick += (s, e) =>
                {
                    ShowNoteEditControl(note);
                };
                notesPanel.Controls.Add(notes);
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
            for (int i = notesPanel.Controls.Count - 1; i >= 0; i--)
            {
                Control ctrl = notesPanel.Controls[i];
                notesPanel.Controls.RemoveAt(i);
                ctrl.Dispose();
            }
                
            //notesPanel.Controls.Clear();

            NoteEditControl editCtrl = new NoteEditControl(this);            
            editCtrl.Note = note;
            editCtrl.BringToFront();
            editCtrl.Dock = DockStyle.Fill;

            editCtrl.closeButton.Click += (s, e) => {
                notesController.DeleteNote(note);
                this.notesPanel.Controls.Remove(editCtrl);
                FillNotes();
                editCtrl.Close();
            };

            editCtrl.okButton.Click += (s, e) =>
            {
                notesController.SaveNotes();
                this.notesPanel.Controls.Remove(editCtrl);
                FillNotes();
                editCtrl.Close();
            };

            this.notesPanel.Controls.Add(editCtrl);
        }
    }
}

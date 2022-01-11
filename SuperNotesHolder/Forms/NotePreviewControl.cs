using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperNotesHolder.Models;
using SuperNotesHolder.Utils;

namespace SuperNotesHolder
{
    public partial class NotePreviewControl : UserControl
    {
        public delegate void SelectionChangeHandler(object sender, EventArgs e);
        public event SelectionChangeHandler OnSelectionChanged;

        public delegate void MoveUpHandler(object sender, EventArgs e);
        public event MoveUpHandler OnMoveUp;

        private bool selected;

        public bool Selected {
            get { return selected; }
            set {
                selected = value;
                if (selected)
                {
                    textControl.BackColor = ThemeManager.Theme.GetColor("preview.textControl.selectBackColor");
                    textControl.ForeColor = ThemeManager.Theme.GetColor("preview.textControl.selectForeColor");
                }
                else
                {
                    textControl.BackColor = ThemeManager.Theme.GetColor("preview.textControl.backColor");
                    textControl.ForeColor = ThemeManager.Theme.GetColor("preview.textControl.foreColor");
                }

                if (OnSelectionChanged == null) return;
                SelectionChangedEventArgs args = new SelectionChangedEventArgs(selected);
                OnSelectionChanged(this, args);
            }
        }

        private Note note;

        public Note Note {
            get { return note; }
            set {
                note = value;
                SetText(note.Text);
                timeLabel.Text = note.TimeStamp.ToString("yyyy-MM-dd HH:mm");
            }
        }

        public NotePreviewControl()
        {
            InitializeComponent();
            
            Selected = false;
        }


        public void SetText(string text)
        {            
            textControl.Text = text;         
        }

        private void textControl_Click(object sender, EventArgs e)
        {
            Selected = !Selected;
        }

        private void textControl_MouseEnter(object sender, EventArgs e)
        {
            if (!Selected)
            {
                textControl.BackColor = ThemeManager.Theme.GetColor("preview.textControl.hoverBackColor");
                textControl.ForeColor = ThemeManager.Theme.GetColor("preview.textControl.hoverForeColor");
            }               
        }

        private void textControl_MouseLeave(object sender, EventArgs e)
        {
            if (!Selected)
            {
                textControl.BackColor = ThemeManager.Theme.GetColor("preview.textControl.backColor");
                textControl.ForeColor = ThemeManager.Theme.GetColor("preview.textControl.foreColor");
            }
        }



        public class SelectionChangedEventArgs : EventArgs
        {
            public bool Selected { get; private set; }

            public SelectionChangedEventArgs(bool selected)
            {
                Selected = selected;
            }
        }

        public class MoveUpEventArgs : EventArgs
        {
            public Note Note { get; private set; }

            public MoveUpEventArgs(Note note)
            {
                Note = note;
            }
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnMoveUp == null) return;
            MoveUpEventArgs args = new MoveUpEventArgs(Note);
            OnMoveUp(this, args);
        }
    }
}

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
        public bool Selected { get; set; }

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
            //textControl.ReadOnly = false;
            textControl.Text = text;
            //textControl.ReadOnly = true;
        }

        private void textControl_Click(object sender, EventArgs e)
        {
            Selected = !Selected;
            if (Selected)
            {
                textControl.BackColor = ThemeManager.Theme.GetColor("preview.textControl.selectBackColor");
                textControl.ForeColor = ThemeManager.Theme.GetColor("preview.textControl.selectForeColor");
            }
            else
            {
                textControl.BackColor = ThemeManager.Theme.GetColor("preview.textControl.backColor");
                textControl.ForeColor = ThemeManager.Theme.GetColor("preview.textControl.foreColor");
            }                
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
    }
}

using SuperNotesHolder.Controls;

namespace SuperNotesHolder
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            SuperNotesHolder.Models.Note note1 = new SuperNotesHolder.Models.Note();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.collapseTimer = new System.Windows.Forms.Timer(this.components);
            this.addNoteButton = new System.Windows.Forms.Button();
            this.notesPanel = new SuperNotesHolder.Controls.SelectablePanel();
            this.noteEditControl = new SuperNotesHolder.Forms.NoteEditControl();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // collapseTimer
            // 
            this.collapseTimer.Tick += new System.EventHandler(this.collapseTimer_Tick);
            // 
            // addNoteButton
            // 
            this.addNoteButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addNoteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addNoteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.addNoteButton.Location = new System.Drawing.Point(2, 2);
            this.addNoteButton.Name = "addNoteButton";
            this.addNoteButton.Size = new System.Drawing.Size(483, 39);
            this.addNoteButton.TabIndex = 2;
            this.addNoteButton.Text = "Click to add note";
            this.addNoteButton.UseVisualStyleBackColor = true;
            this.addNoteButton.Click += new System.EventHandler(this.addNoteButton_Click);
            this.addNoteButton.MouseLeave += new System.EventHandler(this.addNoteButton_MouseLeave);
            // 
            // notesPanel
            // 
            this.notesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notesPanel.AutoScroll = true;
            this.notesPanel.Location = new System.Drawing.Point(2, 42);
            this.notesPanel.Name = "notesPanel";
            this.notesPanel.Size = new System.Drawing.Size(482, 566);
            this.notesPanel.TabIndex = 5;
            this.notesPanel.TabStop = true;
            // 
            // noteEditControl
            // 
            this.noteEditControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.noteEditControl.Location = new System.Drawing.Point(2, 42);
            this.noteEditControl.Name = "noteEditControl";
            note1.FilePath = null;
            note1.Local = true;
            note1.Pos = 0;
            note1.RenderType = ScintillaNET.Lexer.Null;
            note1.Text = null;
            note1.TimeStamp = new System.DateTime(2019, 10, 1, 15, 54, 37, 963);
            this.noteEditControl.Note = note1;
            this.noteEditControl.Size = new System.Drawing.Size(483, 566);
            this.noteEditControl.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(486, 609);
            this.Controls.Add(this.addNoteButton);
            this.Controls.Add(this.notesPanel);
            this.Controls.Add(this.noteEditControl);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Super Notes Holder";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
            this.MouseEnter += new System.EventHandler(this.MainForm_MouseEnter);
            this.Move += new System.EventHandler(this.MainForm_Move);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Timer collapseTimer;
        private System.Windows.Forms.Button addNoteButton;
        private SelectablePanel notesPanel;
        private Forms.NoteEditControl noteEditControl;
    }
}


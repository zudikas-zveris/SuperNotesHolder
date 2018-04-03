namespace SuperNotesHolder.Forms
{
    partial class NoteEditControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoteEditControl));
            this.textControl = new ScintillaNET.Scintilla();
            this.topBarPanel = new System.Windows.Forms.Panel();
            this.xmlButton = new System.Windows.Forms.Button();
            this.jsonButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.PanelSearch = new System.Windows.Forms.Panel();
            this.BtnNextSearch = new System.Windows.Forms.Button();
            this.BtnPrevSearch = new System.Windows.Forms.Button();
            this.BtnCloseSearch = new System.Windows.Forms.Button();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.notificationPanel = new System.Windows.Forms.Panel();
            this.notificationCloseButton = new System.Windows.Forms.Button();
            this.notificationTextBox = new System.Windows.Forms.RichTextBox();
            this.topBarPanel.SuspendLayout();
            this.PanelSearch.SuspendLayout();
            this.notificationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // textControl
            // 
            this.textControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textControl.Location = new System.Drawing.Point(0, 32);
            this.textControl.Name = "textControl";
            this.textControl.ScrollWidth = 1;
            this.textControl.Size = new System.Drawing.Size(322, 410);
            this.textControl.TabIndex = 1;
            this.textControl.TextChanged += new System.EventHandler(this.textControl_TextChanged);
            this.textControl.Validated += new System.EventHandler(this.textControl_Validated);
            // 
            // topBarPanel
            // 
            this.topBarPanel.Controls.Add(this.xmlButton);
            this.topBarPanel.Controls.Add(this.jsonButton);
            this.topBarPanel.Controls.Add(this.okButton);
            this.topBarPanel.Controls.Add(this.closeButton);
            this.topBarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBarPanel.Location = new System.Drawing.Point(0, 0);
            this.topBarPanel.Name = "topBarPanel";
            this.topBarPanel.Size = new System.Drawing.Size(322, 32);
            this.topBarPanel.TabIndex = 2;
            // 
            // xmlButton
            // 
            this.xmlButton.Location = new System.Drawing.Point(99, 3);
            this.xmlButton.Name = "xmlButton";
            this.xmlButton.Size = new System.Drawing.Size(41, 24);
            this.xmlButton.TabIndex = 5;
            this.xmlButton.Text = "XML";
            this.xmlButton.UseVisualStyleBackColor = true;
            this.xmlButton.Click += new System.EventHandler(this.xmlButton_Click);
            // 
            // jsonButton
            // 
            this.jsonButton.Location = new System.Drawing.Point(45, 3);
            this.jsonButton.Name = "jsonButton";
            this.jsonButton.Size = new System.Drawing.Size(48, 24);
            this.jsonButton.TabIndex = 4;
            this.jsonButton.Text = "Json";
            this.jsonButton.UseVisualStyleBackColor = true;
            this.jsonButton.Click += new System.EventHandler(this.jsonButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(3, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(36, 24);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(290, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(29, 24);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "X";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // PanelSearch
            // 
            this.PanelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelSearch.BackColor = System.Drawing.Color.White;
            this.PanelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelSearch.Controls.Add(this.BtnNextSearch);
            this.PanelSearch.Controls.Add(this.BtnPrevSearch);
            this.PanelSearch.Controls.Add(this.BtnCloseSearch);
            this.PanelSearch.Controls.Add(this.TxtSearch);
            this.PanelSearch.Location = new System.Drawing.Point(30, 34);
            this.PanelSearch.Name = "PanelSearch";
            this.PanelSearch.Size = new System.Drawing.Size(292, 40);
            this.PanelSearch.TabIndex = 11;
            this.PanelSearch.Visible = false;
            // 
            // BtnNextSearch
            // 
            this.BtnNextSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNextSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNextSearch.ForeColor = System.Drawing.Color.White;
            this.BtnNextSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnNextSearch.Image")));
            this.BtnNextSearch.Location = new System.Drawing.Point(233, 4);
            this.BtnNextSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnNextSearch.Name = "BtnNextSearch";
            this.BtnNextSearch.Size = new System.Drawing.Size(25, 30);
            this.BtnNextSearch.TabIndex = 9;
            this.BtnNextSearch.Tag = "Find next (Enter)";
            this.BtnNextSearch.UseVisualStyleBackColor = true;
            this.BtnNextSearch.Click += new System.EventHandler(this.BtnNextSearch_Click);
            // 
            // BtnPrevSearch
            // 
            this.BtnPrevSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrevSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevSearch.ForeColor = System.Drawing.Color.White;
            this.BtnPrevSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnPrevSearch.Image")));
            this.BtnPrevSearch.Location = new System.Drawing.Point(205, 4);
            this.BtnPrevSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnPrevSearch.Name = "BtnPrevSearch";
            this.BtnPrevSearch.Size = new System.Drawing.Size(25, 30);
            this.BtnPrevSearch.TabIndex = 8;
            this.BtnPrevSearch.Tag = "Find previous (Shift+Enter)";
            this.BtnPrevSearch.UseVisualStyleBackColor = true;
            this.BtnPrevSearch.Click += new System.EventHandler(this.BtnPrevSearch_Click);
            // 
            // BtnCloseSearch
            // 
            this.BtnCloseSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCloseSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCloseSearch.ForeColor = System.Drawing.Color.White;
            this.BtnCloseSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnCloseSearch.Image")));
            this.BtnCloseSearch.Location = new System.Drawing.Point(261, 4);
            this.BtnCloseSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnCloseSearch.Name = "BtnCloseSearch";
            this.BtnCloseSearch.Size = new System.Drawing.Size(25, 30);
            this.BtnCloseSearch.TabIndex = 7;
            this.BtnCloseSearch.Tag = "Close (Esc)";
            this.BtnCloseSearch.UseVisualStyleBackColor = true;
            this.BtnCloseSearch.Click += new System.EventHandler(this.BtnClearSearch_Click);
            // 
            // TxtSearch
            // 
            this.TxtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.Location = new System.Drawing.Point(10, 6);
            this.TxtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(189, 25);
            this.TxtSearch.TabIndex = 6;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.TxtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // notificationPanel
            // 
            this.notificationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.notificationPanel.BackColor = System.Drawing.Color.White;
            this.notificationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.notificationPanel.Controls.Add(this.notificationTextBox);
            this.notificationPanel.Controls.Add(this.notificationCloseButton);
            this.notificationPanel.Location = new System.Drawing.Point(17, 341);
            this.notificationPanel.Name = "notificationPanel";
            this.notificationPanel.Size = new System.Drawing.Size(300, 98);
            this.notificationPanel.TabIndex = 12;
            this.notificationPanel.Visible = false;
            // 
            // notificationCloseButton
            // 
            this.notificationCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.notificationCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.notificationCloseButton.ForeColor = System.Drawing.Color.White;
            this.notificationCloseButton.Image = ((System.Drawing.Image)(resources.GetObject("notificationCloseButton.Image")));
            this.notificationCloseButton.Location = new System.Drawing.Point(272, -1);
            this.notificationCloseButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.notificationCloseButton.Name = "notificationCloseButton";
            this.notificationCloseButton.Size = new System.Drawing.Size(25, 30);
            this.notificationCloseButton.TabIndex = 8;
            this.notificationCloseButton.Tag = "Close (Esc)";
            this.notificationCloseButton.UseVisualStyleBackColor = true;
            this.notificationCloseButton.Click += new System.EventHandler(this.notificationCloseButton_Click);
            // 
            // notificationTextBox
            // 
            this.notificationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.notificationTextBox.Location = new System.Drawing.Point(9, 34);
            this.notificationTextBox.Name = "notificationTextBox";
            this.notificationTextBox.Size = new System.Drawing.Size(287, 62);
            this.notificationTextBox.TabIndex = 9;
            this.notificationTextBox.Text = "";
            // 
            // NoteEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.notificationPanel);
            this.Controls.Add(this.PanelSearch);
            this.Controls.Add(this.textControl);
            this.Controls.Add(this.topBarPanel);
            this.Name = "NoteEditControl";
            this.Size = new System.Drawing.Size(322, 442);
            this.topBarPanel.ResumeLayout(false);
            this.PanelSearch.ResumeLayout(false);
            this.PanelSearch.PerformLayout();
            this.notificationPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal ScintillaNET.Scintilla textControl;
        private System.Windows.Forms.Panel topBarPanel;
        internal System.Windows.Forms.Button xmlButton;
        internal System.Windows.Forms.Button jsonButton;
        internal System.Windows.Forms.Button okButton;
        internal System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Panel PanelSearch;
        private System.Windows.Forms.Button BtnNextSearch;
        private System.Windows.Forms.Button BtnPrevSearch;
        private System.Windows.Forms.Button BtnCloseSearch;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Panel notificationPanel;
        private System.Windows.Forms.Button notificationCloseButton;
        private System.Windows.Forms.RichTextBox notificationTextBox;
    }
}

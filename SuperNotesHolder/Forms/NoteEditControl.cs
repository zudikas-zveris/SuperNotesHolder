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
using ScintillaNET;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.IO;
using SuperNotesHolder.Utils;

namespace SuperNotesHolder.Forms
{
    public partial class NoteEditControl : UserControl
    {

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;


        private Note note;


        public Note Note
        {
            get { return note; }
            set
            {
                note = value;
                if (note == null) note = new Note();
                textControl.Text = note.Text;
                textControl.Lexer = note.RenderType;
                textControl.SetProperty("fold", "1");
                textControl.SetProperty("fold.compact", "1");
                textControl.SetProperty("fold.html", "1");
            }
        }


        public NoteEditControl()
        {
            InitializeComponent();

            // INITIAL VIEW CONFIG
            textControl.WrapMode = WrapMode.None;
            textControl.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors();
            InitSyntaxColoring();

            // NUMBER MARGIN
            InitNumberMargin();

            // BOOKMARK MARGIN
            //InitBookmarkMargin();

            // CODE FOLDING MARGIN
            InitCodeFolding();

            // DRAG DROP
            InitDragDropFile();

            // remove conflicting hotkeys from scintilla
            textControl.ClearCmdKey(Keys.Control | Keys.F);
            textControl.ClearCmdKey(Keys.Control | Keys.R);
            textControl.ClearCmdKey(Keys.Control | Keys.H);
            textControl.ClearCmdKey(Keys.Control | Keys.L);
            textControl.ClearCmdKey(Keys.Control | Keys.U);
            
        }

        public void Close()
        {
            HotKeyManager.RemoveHotKeys();
            this.Dispose();
        }



        private void textControl_TextChanged(object sender, EventArgs e)
        {
            Note.Text = textControl.Text;
        }

        private void jsonButton_Click(object sender, EventArgs e)
        {
            FormatAsJson();
        }

        private void xmlButton_Click(object sender, EventArgs e)
        {
            FormatAsXml();
        }

        private void textControl_Validated(object sender, EventArgs e)
        {
            Note.Text = textControl.Text;
        }

        private void notificationCloseButton_Click(object sender, EventArgs e)
        {
            notificationTextBox.Text = "";
            notificationPanel.Visible = false;
        }



        private void InitColors()
        {
            textControl.SetSelectionBackColor(true, IntToColor(0x114D9C));
            textControl.CaretForeColor = Color.White;
        }

        private void InitSyntaxColoring()
        {

            // Configure the default style
            textControl.StyleResetDefault();
            textControl.Styles[Style.Default].Font = "Consolas";
            textControl.Styles[Style.Default].Size = 10;
            textControl.Styles[Style.Default].BackColor = IntToColor(0x212121);
            textControl.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            textControl.StyleClearAll();

            
            // Configure the CPP (C#) lexer styles
            textControl.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            textControl.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            textControl.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            textControl.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            textControl.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            textControl.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            textControl.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            textControl.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            textControl.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            textControl.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            textControl.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            textControl.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            textControl.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            textControl.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            textControl.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            textControl.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            // Set the Styles
            //textControl.StyleResetDefault();
            //// I like fixed font for XML
            //textControl.Styles[Style.Default].Font = "Courier";
            //textControl.Styles[Style.Default].Size = 10;
            //textControl.StyleClearAll();
            //textControl.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
            //textControl.Styles[Style.Xml.Entity].ForeColor = Color.Red;
            //textControl.Styles[Style.Xml.Comment].ForeColor = Color.Green;
            //textControl.Styles[Style.Xml.Tag].ForeColor = Color.Blue;
            //textControl.Styles[Style.Xml.TagEnd].ForeColor = Color.Blue;
            //textControl.Styles[Style.Xml.DoubleString].ForeColor = Color.DeepPink;
            //textControl.Styles[Style.Xml.SingleString].ForeColor = Color.DeepPink;

            textControl.Lexer = Lexer.Null;
            //textControl.Lexer = Lexer.Xml;
            //TextArea.Lexer = Lexer.Cpp;

            textControl.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            textControl.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

        }

        private void InitNumberMargin()
        {

            textControl.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            textControl.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            textControl.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            textControl.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = textControl.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            textControl.MarginClick += (object sender, MarginClickEventArgs e) =>
            {
                if (e.Margin == BOOKMARK_MARGIN)
                {
                    // Do we have a marker for this line?
                    const uint mask = (1 << BOOKMARK_MARKER);
                    var line = textControl.Lines[textControl.LineFromPosition(e.Position)];
                    if ((line.MarkerGet() & mask) > 0)
                    {
                        // Remove existing bookmark
                        line.MarkerDelete(BOOKMARK_MARKER);
                    }
                    else
                    {
                        // Add bookmark
                        line.MarkerAdd(BOOKMARK_MARKER);
                    }
                }
            };
        }

        private void InitBookmarkMargin()
        {

            //TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = textControl.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = textControl.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding()
        {
            textControl.Lexer = Lexer.Json;            


            textControl.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            textControl.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            textControl.SetProperty("fold", "1");
            textControl.SetProperty("fold.compact", "1");
            textControl.SetProperty("fold.html", "1");
            

            // Configure a margin to display folding symbols
            textControl.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            textControl.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            textControl.Margins[FOLDING_MARGIN].Sensitive = true;
            textControl.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                textControl.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                textControl.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            textControl.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            textControl.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            textControl.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            textControl.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            textControl.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            textControl.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            textControl.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            textControl.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        public void InitDragDropFile()
        {

            textControl.AllowDrop = true;
            textControl.DragEnter += delegate (object sender, DragEventArgs e) {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            };
            textControl.DragDrop += delegate (object sender, DragEventArgs e) {

                // get file drop
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {

                    Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                    if (a != null)
                    {

                        string path = a.GetValue(0).ToString();

                        LoadDataFromFile(path);

                    }
                }
            };

        }



        





        #region Main Menu Commands NENAUDOJAMA
/*
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    LoadDataFromFile(openFileDialog.FileName);
            //}
        }



        private void findDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFindDialog();
        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenReplaceDialog();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.SelectAll();
        }

        private void selectLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Line line = TextArea.Lines[TextArea.CurrentLine];
            TextArea.SetSelection(line.Position + line.Length, line.Position);
        }

        private void clearSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.SetEmptySelection(0);
        }

        private void indentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Indent();
        }

        private void outdentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Outdent();
        }

        private void uppercaseSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uppercase();
        }

        private void lowercaseSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lowercase();
        }

        private void wordWrapToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            // toggle word wrap
            //wordWrapItem.Checked = !wordWrapItem.Checked;
            //TextArea.WrapMode = wordWrapItem.Checked ? WrapMode.Word : WrapMode.None;
        }

        private void indentGuidesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // toggle indent guides
            //indentGuidesItem.Checked = !indentGuidesItem.Checked;
            //TextArea.IndentationGuides = indentGuidesItem.Checked ? IndentView.LookBoth : IndentView.None;
        }

        private void hiddenCharactersToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // toggle view whitespace
            //hiddenCharactersItem.Checked = !hiddenCharactersItem.Checked;
            //TextArea.ViewWhitespace = hiddenCharactersItem.Checked ? WhitespaceMode.VisibleAlways : WhitespaceMode.Invisible;
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        private void zoom100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomDefault();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.FoldAll(FoldAction.Contract);
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextArea.FoldAll(FoldAction.Expand);
        }


        #endregion

        #region Uppercase / Lowercase

        private void Lowercase()
        {

            // save the selection
            int start = TextArea.SelectionStart;
            int end = TextArea.SelectionEnd;

            // modify the selected text
            TextArea.ReplaceSelection(TextArea.GetTextRange(start, end - start).ToLower());

            // preserve the original selection
            TextArea.SetSelection(start, end);
        }

        private void Uppercase()
        {

            // save the selection
            int start = TextArea.SelectionStart;
            int end = TextArea.SelectionEnd;

            // modify the selected text
            TextArea.ReplaceSelection(TextArea.GetTextRange(start, end - start).ToUpper());

            // preserve the original selection
            TextArea.SetSelection(start, end);
        }

        #endregion

        #region Indent / Outdent

        private void Indent()
        {
            // we use this hack to send "Shift+Tab" to scintilla, since there is no known API to indent,
            // although the indentation function exists. Pressing TAB with the editor focused confirms this.
            GenerateKeystrokes("{TAB}");
        }

        private void Outdent()
        {
            // we use this hack to send "Shift+Tab" to scintilla, since there is no known API to outdent,
            // although the indentation function exists. Pressing Shift+Tab with the editor focused confirms this.
            GenerateKeystrokes("+{TAB}");
        }

        private void GenerateKeystrokes(string keys)
        {
            //HotKeyManager.Enable = false;
            //TextArea.Focus();
            //SendKeys.Send(keys);
            //HotKeyManager.Enable = true;
        }

        #endregion

        #region Zoom

        private void ZoomIn()
        {
            TextArea.ZoomIn();
        }

        private void ZoomOut()
        {
            TextArea.ZoomOut();
        }

        private void ZoomDefault()
        {
            TextArea.Zoom = 0;
        }


        #endregion

       

        #region Utils

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }












*/
        #endregion
        


        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        public void InvokeIfNeeded(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        public void FormatAsJson()
        {
            try
            {
                textControl.Text = JValue.Parse(textControl.Text).ToString(Formatting.Indented);
                textControl.Lexer = Lexer.Json;                
                Note.RenderType = Lexer.Json;
            }
            catch (Exception ex)
            {
                notificationPanel.Visible = true;
                notificationTextBox.Text = ex.Message;
                //MessageBox.Show("Parse error!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        public void FormatAsXml()
        {
            try
            {
                XDocument doc = XDocument.Parse(textControl.Text);
                textControl.Text = doc.ToString();
                textControl.Lexer = Lexer.Xml;                
                Note.RenderType = Lexer.Xml;
            }
            catch (Exception ex)
            {
                notificationPanel.Visible = true;
                notificationTextBox.Text = ex.Message;
                //MessageBox.Show("Parse error!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataFromFile(string path)
        {
            if (File.Exists(path))
            {                
                textControl.Text = File.ReadAllText(path);
            }
        }


        #region Quick Search Bar

        bool SearchIsOpen = false;

        public void OpenSearch()
        {

            SearchManager.SearchBox = TxtSearch;
            SearchManager.TextArea = textControl;

            if (!SearchIsOpen)
            {
                SearchIsOpen = true;
                InvokeIfNeeded(delegate ()
                {
                    PanelSearch.Visible = true;
                    TxtSearch.Text = SearchManager.LastSearch;
                    TxtSearch.Focus();
                    TxtSearch.SelectAll();
                });
            }
            else
            {
                InvokeIfNeeded(delegate ()
                {
                    TxtSearch.Focus();
                    TxtSearch.SelectAll();
                });
            }
        }

        public void CloseSearch()
        {
            if (SearchIsOpen)
            {
                SearchIsOpen = false;
                InvokeIfNeeded(delegate ()
                {
                    PanelSearch.Visible = false;
                    //CurBrowser.GetBrowser().StopFinding(true);
                });
            }
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            CloseSearch();
        }

        private void BtnPrevSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(false, false);
        }

        private void BtnNextSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(true, false);
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchManager.Find(true, true);
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (HotKeyManager.IsHotkey(e, Keys.Enter))
            {
                SearchManager.Find(true, false);
            }
            if (HotKeyManager.IsHotkey(e, Keys.Enter, true) || HotKeyManager.IsHotkey(e, Keys.Enter, false, true))
            {
                SearchManager.Find(false, false);
            }
        }

        #endregion

        #region Find & Replace Dialog

        private void OpenFindDialog()
        {

        }

        private void OpenReplaceDialog()
        {


        }

        #endregion

    }
}

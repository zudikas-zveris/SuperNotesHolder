using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperNotesHolder.Models
{
    public class Note
    {
        public enum NoteRenderType
        {
            String,
            Json,
            Xml,
            JavaScript
        }

        public int Pos { get; set; }
        public DateTime TimeStamp { get; set; }
        public ScintillaNET.Lexer RenderType { get; set; }
        public string Text { get; set; }
        public bool Local { get; set; }
        public string FilePath { get; set; }
        
        public Note()
        {
            RenderType = ScintillaNET.Lexer.Null;
            TimeStamp = DateTime.Now;
            Local = true;
        }
    }
}

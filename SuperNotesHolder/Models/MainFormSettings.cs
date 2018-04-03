using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperNotesHolder.Models
{
    public class MainFormSettings
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public MainForm.DockType DockLocation { get; set; }
        public FormWindowState WindowState { get; set; }
    }
}

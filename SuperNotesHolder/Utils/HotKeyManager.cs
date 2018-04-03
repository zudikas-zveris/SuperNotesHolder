using SuperNotesHolder.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperNotesHolder.Utils
{
    public class HotKeyManager
    {
        List<KeyEventHandler> delegates = new List<KeyEventHandler>();

        public bool Enable = true;

        public MainForm MainForm { get; set; }

        public void AddHotKey(NoteEditControl editControl, Action function, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
        {
            MainForm = editControl.MainForm;

            editControl.MainForm.KeyPreview = true;


            KeyEventHandler keyEventHdl = delegate (object sender, KeyEventArgs e)
            {
                if (IsHotkey(e, key, ctrl, shift, alt))
                {
                    function();
                }
            };

            editControl.MainForm.KeyDown += keyEventHdl;
            delegates.Add(keyEventHdl);

        }

        public bool IsHotkey(KeyEventArgs eventData, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
        {
            return eventData.KeyCode == key && eventData.Control == ctrl && eventData.Shift == shift && eventData.Alt == alt;
        }

        public void RemoveHotKeys()
        {
            foreach (KeyEventHandler eh in delegates)
            {
                MainForm.KeyDown -= eh;
            }
            delegates.Clear();
        }


    }
}

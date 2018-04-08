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
        private List<KeyEventHandler> delegates = new List<KeyEventHandler>();

        private static HotKeyManager instance;
        private Form mainForm;

        private HotKeyManager()
        {

        }

        public static HotKeyManager Default
        {
            get
            {
                if (instance == null)
                    instance = new HotKeyManager();

                return instance;
            }
        }

        public static Form MainForm
        {
            get { return Default.mainForm; }
            set { Default.mainForm = value; }
        }

        public static void AddHotKey(Action function, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
        {
            Default.mainForm.KeyPreview = true;            

            KeyEventHandler keyEventHdl = delegate (object sender, KeyEventArgs e)
            {
                if (IsHotkey(e, key, ctrl, shift, alt))
                {
                    function();
                }
            };

            Default.mainForm.KeyDown += keyEventHdl;
            Default.delegates.Add(keyEventHdl);

        }

   
        public static void RemoveHotKeys()
        {
            foreach (KeyEventHandler eh in Default.delegates)
            {
                Default.mainForm.KeyDown -= eh;
            }

            Default.delegates.Clear();
        }

        public static bool IsHotkey(KeyEventArgs eventData, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
        {
            return eventData.KeyCode == key && eventData.Control == ctrl && eventData.Shift == shift && eventData.Alt == alt;
        }


    }
}

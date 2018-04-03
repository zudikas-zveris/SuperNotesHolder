using Newtonsoft.Json;
using SuperNotesHolder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperNotesHolder.Controllers
{
    public class NotesController
    {
        public List<Note> Notes { get; set; }


        public void LoadNotes()
        {
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SuperNotesHolder", "data.json");

            if (File.Exists(fileName))
            {
                string notesJson = File.ReadAllText(fileName);

                Notes = JsonConvert.DeserializeObject<List<Note>>(notesJson);
            }
            else
            {
                Notes = new List<Note>();
            }
                
        }

        public void SaveNotes()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SuperNotesHolder");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var fileName = Path.Combine(folder, "data.json");
            
            string notesJson = JsonConvert.SerializeObject(Notes);

            File.WriteAllText(fileName, notesJson);            
        }

        internal Note NewNote()
        {
            Note note = new Note();
            Notes.Add(note);
            SaveNotes();
            return note;
        }

        internal void DeleteNote(Note note)
        {
            Notes.Remove(note);
            SaveNotes();
        }
    }
}

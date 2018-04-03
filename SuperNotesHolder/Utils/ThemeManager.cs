using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Drawing;

namespace SuperNotesHolder.Utils
{
    public class ThemeManager
    {
        private static ThemeManager instance;        

        private JObject theme;

        private ThemeManager() {

            string themeFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Themes", Properties.Settings.Default.Theme + ".json");
            theme = JObject.Parse(File.ReadAllText(themeFile));
        }

        public static ThemeManager Theme
        {
            get
            {
                if (instance == null)
                {
                    instance = new ThemeManager();
                }
                return instance;
            }
        }


        public Color GetColor(string name)
        {
            string color = theme.Property(name).Value.ToString();
            return ColorTranslator.FromHtml(color);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistractMe
{
    public class SettingsManager
    {
        private string settingsPath;
        public SettingsManager()
        {
            settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DistractMe", "settings.json");
        }
        public UserSettings Get()
        {
            if (File.Exists(settingsPath))
            {
                using (StreamReader sr = new StreamReader(settingsPath))
                {
                    return JsonConvert.DeserializeObject<UserSettings>(sr.ReadToEnd());
                }
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(settingsPath));
                UserSettings settings = new UserSettings();
                settings.DistractionInterval = 15;
                settings.BlockInterval = 30;
                settings.Messages = new string[]
                {
                    "Drink some water",
                    "look around",
                    "stretch a little bit"
                };
                SaveSettings(settings);
                return settings;
            }
        }
        public void SaveSettings(UserSettings settings)
        {
            File.WriteAllText(settingsPath, JsonConvert.SerializeObject(settings));
        }
    }
}

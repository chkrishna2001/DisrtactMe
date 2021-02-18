using Newtonsoft.Json;
using System;
using System.IO;

namespace DistractMe
{
    public class SettingsManager
    {
        private string settingsPath;
        public SettingsManager()
        {
            settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DistractMe", "settings.json");
            if (!File.Exists(settingsPath))
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
            }
        }
        public UserSettings Get()
        {
            using (StreamReader sr = new StreamReader(settingsPath))
            {
                return JsonConvert.DeserializeObject<UserSettings>(sr.ReadToEnd());
            }
        }
        public void SaveSettings(UserSettings settings)
        {
            File.WriteAllText(settingsPath, JsonConvert.SerializeObject(settings));
        }
    }
}

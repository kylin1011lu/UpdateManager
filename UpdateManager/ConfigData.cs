using System;
using System.Configuration;

namespace UpdateManager
{
    class ConfigData
    {
        static public string KEY_RELEASEPATH ="CONFIG_DATA_KEY_RELEASEPATH";
        static public string KEY_PLATFORMPATH = "CONFIG_DATA_KEY_PLATFORMPATH";
        static public string KEY_PLATFORMLIST = "CONFIG_DATA_KEY_PLATFORMLIST";
        static public string KEY_FRAMEPATH = "CONFIG_DATA_KEY_FRAMEPATH";

        static public string GetStringForKey(string key,string defaultvalue)
        {
            string result = "";
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var appSettings = configFile.AppSettings.Settings;
                result = appSettings[key] == null ? defaultvalue : appSettings[key].Value;
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return result;
            }
        }

        static public void SetStringForKey(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}

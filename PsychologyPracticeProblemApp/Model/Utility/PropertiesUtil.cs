using Microsoft.Maui.ApplicationModel.DataTransfer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.Model.Utility;
/* TODO:
 *XLogin -> save password
 *XCreate Account
 *XFigure out how to save/load mobile files
 * 
 * 
 * SETTINGS:
 *XToggle Dark Mode
 *XChange DataSet Count
 *XChange DataSet Min / Max
 *XChange History Count
 * Set new password
 * 
 * ADMIN SETTINGS:
 * Clear global history
 * Clear users database
 * View global stats
 * Users:
 *  Delete user
 *  Clear history
 *  Login as user
 * 
 */
class PropertiesUtil {
    private static String propFile = "settings.prop";
    public static bool StayLoggedIn { get; set; } = false;
    public static bool DarkMode { get; set; } = false;
    public static String SavedUser { get; set; } = "";
    public static String SavedPassword { get; set; } = "";
    public static int DatasetCount { get; set; } = 10;
    public static int DatasetMin { get; set; } = 0;
    public static int DatasetMax { get; set; } = 10;
    public static int HistoryCount { get; set; } = 10;
    public static async void Save()
    {
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), propFile);
        using(var writer = File.CreateText(fileName))
        {
            await writer.WriteLineAsync(DataToFile("StayLoggedIn",  StayLoggedIn.ToString()));
            await writer.WriteLineAsync(DataToFile("DarkMode",      DarkMode.ToString()));
            await writer.WriteLineAsync(DataToFile("SavedUser",     SavedUser));
            await writer.WriteLineAsync(DataToFile("SavedPassword", SavedPassword));
            await writer.WriteLineAsync(DataToFile("DatasetCount",  DatasetCount.ToString()));
            await writer.WriteLineAsync(DataToFile("DatasetMin",    DatasetMin.ToString()));
            await writer.WriteLineAsync(DataToFile("DatasetMax",    DatasetMax.ToString()));
            await writer.WriteLineAsync(DataToFile("HistoryCount",  HistoryCount.ToString()));
        }
    }
    private static String DataToFile(string propName, string val)
    {
        return propName + "=" + val;
    }
    public static void UpdateTheme()
    {
        App.Current.UserAppTheme = DarkMode ? AppTheme.Dark : AppTheme.Light;
    }
    public static async Task Load()
    {
        var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), propFile);

        if(fileName == null || !File.Exists(fileName)) return;
        Debug.WriteLine(fileName);
        using(var reader = new StreamReader(fileName, true))
        {
            string line;
            while((line = await reader.ReadLineAsync()) != null)
            {
                Debug.WriteLine(line);
                string[] parts = line.Split('=');
                switch(parts[0])
                {
                    case "StayLoggedIn":    StayLoggedIn    = bool.Parse(parts[1]); break;
                    case "DarkMode":        DarkMode        = bool.Parse(parts[1]); break;
                    case "SavedUser":       SavedUser       = parts[1]; break;
                    case "SavedPassword":   SavedPassword   = parts[1]; break;
                    case "DatasetCount":    DatasetCount    = int.Parse(parts[1]); break;
                    case "DatasetMin":      DatasetMin      = int.Parse(parts[1]); break;
                    case "DatasetMax":      DatasetMax      = int.Parse(parts[1]); break;
                    case "HistoryCount":    HistoryCount    = int.Parse(parts[1]); break;
                }
            }
        }
        UpdateTheme();
    }
}

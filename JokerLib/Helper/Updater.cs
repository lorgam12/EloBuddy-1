
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Version = System.Version;

namespace JokerLib.Helper
{
    public static class Updater
    {
        public static void CheckUpdates(string Path, string AddonName)
        {
            string RawVersion = new WebClient().DownloadString("https://raw.githubusercontent.com/DownsecAkr/" + Path + "/master/" + Assembly.GetExecutingAssembly().GetName().Name + "/Properties/AssemblyInfo.cs");
            var Try = new Regex(@"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]").Match(RawVersion);
            if (Try.Success)
            {
                if (new Version(string.Format("{0}.{1}.{2}.{3}", Try.Groups[1], Try.Groups[2], Try.Groups[3], Try.Groups[4])) > Assembly.GetExecutingAssembly().GetName().Version)
                {
                    Logger.Notification(AddonName + " - Outdated", "You have an older version please update :D", 15000);
                }
                else
                {
                    Logger.Notification(AddonName + " - Loaded", "Enjoy free elo! Keppo");
                }
            }
        }

        public static void CheckLibraryUpdates()
        {
            string RawVersion = new WebClient().DownloadString("https://raw.githubusercontent.com/DownsecAkr/JokerLib/master/" + Assembly.GetExecutingAssembly().GetName().Name + "/Properties/AssemblyInfo.cs");
            var Try = new Regex(@"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]").Match(RawVersion);
            if (Try.Success)
            {
                if (new Version(string.Format("{0}.{1}.{2}.{3}", Try.Groups[1], Try.Groups[2], Try.Groups[3], Try.Groups[4])) > Assembly.GetExecutingAssembly().GetName().Version)
                {
                    Logger.Notification("Library - Outdated", "You have an older version please update :D", 15000);
                }
                else
                {
                    Logger.Notification("Library - Updated", "Keppo");
                }
            }
        }

        public static void CheckUpdates(string Path, string AddonName, string Github)
        {
            string RawVersion = new WebClient().DownloadString("https://raw.githubusercontent.com/" + Github +"/" + Path + "/master/" + Assembly.GetExecutingAssembly().GetName().Name + "/Properties/AssemblyInfo.cs");
            var Try = new Regex(@"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]").Match(RawVersion);
            if (Try.Success)
            {
                if (new Version(string.Format("{0}.{1}.{2}.{3}", Try.Groups[1], Try.Groups[2], Try.Groups[3], Try.Groups[4])) > Assembly.GetExecutingAssembly().GetName().Version)
                {
                    Logger.Notification(AddonName + " - Outdated", "You have an older version please update :D", 15000);
                }
                else
                {
                    Logger.Notification(AddonName + " - Loaded", "Enjoy free elo! Keppo");
                }
            }
        }
    }
}

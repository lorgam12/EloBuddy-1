using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Disable_All_Drawings
{
    class Program
    {
        private static Menu Principal;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Principal = MainMenu.AddMenu("Disable Drawings", "Disable Drawings");
            Principal.Add("Disable", new CheckBox("Disable Drawings ?"));

            if (Principal["Disable"].Cast<CheckBox>().CurrentValue)
            {
                Hacks.DisableDrawings = true;
            }
            else
            {
                Hacks.DisableDrawings = false;
            }
        }
    }
}

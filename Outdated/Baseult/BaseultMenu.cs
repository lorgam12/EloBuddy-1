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

namespace Baseult
{
    class BaseultMenu
    {
        public static Menu Principal, Settings;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("BaseUlt", "BaseUlt");
            Principal.AddLabel("Credits iRaxe");

            Settings = Principal.AddSubMenu("Settings", "Settings");
            Settings.Add("UseBaseUlt", new CheckBox("Use Baseult ?"));
            Settings.AddSeparator(2);
            Settings.Add("ShowEnemies", new CheckBox("Show Enemy Recalls"));
            Settings.Add("ShowAllies", new CheckBox("Show Ally Recalls"));
            Settings.AddSeparator(1);
            Settings.AddSeparator(2);
            Settings.AddLabel("Use BaseUlt for:");
            foreach(var x in EntityManager.Heroes.Enemies)
            {
                Settings.Add("Ult/" + x.ChampionName, new CheckBox(x.ChampionName));
            }
        }

        public static bool CheckBox(Menu m, string s)
        {
            return m[s].Cast<CheckBox>().CurrentValue;
        }

        public static int Slider(Menu m, string s)
        {
            return m[s].Cast<Slider>().CurrentValue;
        }

    }
}

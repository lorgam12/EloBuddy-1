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

namespace Iron_Inquisitor_Kayle
{
    class KayleMenu
    {
        public static Menu Principal, Combo, Manage, Laneclear, Jungleclear, Lasthit, Flee, Misc, Draw;

        public static void Init()
        {
            Principal = MainMenu.AddMenu("Kayle", "Kayle");

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("Q", new CheckBox("Use Q"));
            Combo.Add("E", new CheckBox("Use E"));

            Manage = Principal.AddSubMenu("Manage", "Manage");
            Manage.AddLabel("Ally Manager");
            Manage.AddSeparator();
            foreach (var Ally in EntityManager.Heroes.Allies.Where(x => x.Hero != Champion.Kayle))
            {
                Manage.Add(Ally.ChampionName + "/W", new CheckBox("Use W"));
                Manage.Add(Ally.ChampionName + "/R", new CheckBox("Use R"));
            }
            Manage.Add("MinWAlly", new Slider("Min Health: {0}% To use heal in ally", 40, 10, 80));
            Manage.Add("MinRAlly", new Slider("Min Health: {0}% To use ult in ally", 15, 5, 30));
            Manage.AddSeparator(2);
            Manage.AddLabel("Kayle Manager");
            Manage.Add("MinW", new Slider("Min Health: {0}% To use heal in yourself", 50, 10, 80));
            Manage.Add("MinR", new Slider("Min Health: {0}% To use ult in yourself", 15, 5, 45));
            Manage.AddSeparator();
            Manage.Add("Order", new ComboBox("Priority:", 0, "Kayle > Ally", "Ally > Kayle"));

            Laneclear = Principal.AddSubMenu("Laneclear", "Laneclear");
            Laneclear.Add("Q", new CheckBox("Use Q"));
            Laneclear.Add("E", new CheckBox("Use E"));

            Jungleclear = Principal.AddSubMenu("Jungleclear", "Jungleclear");
            Jungleclear.Add("Q", new CheckBox("Use Q"));
            Jungleclear.Add("E", new CheckBox("Use E"));

            Lasthit = Principal.AddSubMenu("Lasthit", "Lasthit");
            Lasthit.Add("Q", new CheckBox("Use Q"));

            Flee = Principal.AddSubMenu("Flee", "Flee");
            Flee.Add("W", new CheckBox("Use W"));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("SkinHack", new CheckBox("SkinHack?", false));
            Misc.Add("SkinID", new Slider("Skin ID: {0}", 8, 0, 8));
            Misc.Add("Reset", new KeyBind("Reset (Skin Bug):", false, KeyBind.BindTypes.HoldActive, 'T'));

            Draw = Principal.AddSubMenu("Draw", "Draw");
            Draw.Add("Q", new CheckBox("Draw Q"));
            Draw.Add("W", new CheckBox("Draw W"));
            Draw.Add("E", new CheckBox("Draw E"));
            Draw.Add("R", new CheckBox("Draw R"));
        }

        public static bool CheckBox(Menu m, string s)
        {
            return m[s].Cast<CheckBox>().CurrentValue;
        }

        public static int Slider(Menu m, string s)
        {
            return m[s].Cast<Slider>().CurrentValue;
        }

        public static bool Keybind(Menu m, string s)
        {
            return m[s].Cast<KeyBind>().CurrentValue;
        }

        public static int ComboBox(Menu m, string s)
        {
            return m[s].Cast<ComboBox>().SelectedIndex;
        }
    }
}

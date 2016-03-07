using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;

namespace Pool_Party_Graves
{
    class GravesMenu
    {
        public static Menu Principal, Combo, Laneclear, Jungleclear, Flee, Misc, Spells, Draw;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("Pool Party Graves", "Graves");
            Principal.AddLabel("Pool Party Graves v" + Assembly.GetExecutingAssembly().GetName().Version);
            Principal.AddSeparator(2);
            Principal.AddLabel("Good game !");

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("UseQCombo", new CheckBox("Use Q"));
            Combo.Add("UseWCombo", new CheckBox("Use W"));
            Combo.Add("UseECombo", new CheckBox("Use E"));
            Combo.Add("UseRCombo", new CheckBox("Use R"));

            Laneclear = Principal.AddSubMenu("Laneclear", "Laneclear");
            Laneclear.Add("UseQLane", new CheckBox("Use Q"));
            Laneclear.Add("UseQLaneMin", new Slider("Use Q if hit {0} minions", 3, 1, 5));
            Laneclear.Add("UseELane", new CheckBox("Use E for reload"));

            Jungleclear = Principal.AddSubMenu("Jungleclear", "Jungleclear");
            Jungleclear.Add("UseQJG", new CheckBox("Use Q"));
            Jungleclear.Add("UseEJG", new CheckBox("Use E for reload"));

            Flee = Principal.AddSubMenu("Flee", "Flee");
            Flee.Add("UseEFlee", new CheckBox("Use E"));
            Flee.Add("UseWFlee", new CheckBox("Use W"));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("Skin", new CheckBox("Skinhack ?", false));
            Misc.Add("SkinID", new Slider("Skin ID: {0}", 5, 0, 6));
            Misc.Add("Interrupter", new CheckBox("Interrupter ?"));
            Misc.Add("Gapcloser", new CheckBox("Gapcloser ?"));

            Draw = Principal.AddSubMenu("Drawing", "Drawing");
            Draw.Add("DrawQ", new CheckBox("Draw Q"));
            Draw.Add("DrawW", new CheckBox("Draw W"));
            Draw.Add("DrawE", new CheckBox("Draw E"));
            Draw.Add("DrawR", new CheckBox("Draw R"));
            Draw.Add("DrawR2", new CheckBox("Draw R Extended"));
            Draw.Add("DrawDMG", new CheckBox("Draw Combo Damage"));
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
    }
}

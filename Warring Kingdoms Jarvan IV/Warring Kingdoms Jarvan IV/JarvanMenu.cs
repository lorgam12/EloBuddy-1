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
using System.Reflection;

namespace Warring_Kingdoms_Jarvan_IV
{
    class JarvanMenu
    {
        public static Menu Principal, Combo, Laneclear, Jungleclear, Flee, Misc, Draw;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("Jarvan IV", "Jarvan");
            Principal.AddLabel("Warring Kingdoms Jarvan v" + Assembly.GetExecutingAssembly().GetName().Version);
            Principal.AddSeparator(2);
            Principal.AddLabel("Good game !");

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("UseQCombo", new CheckBox("Use Q"));
            Combo.Add("UseWCombo", new CheckBox("Use W"));
            Combo.Add("UseECombo", new CheckBox("Use E"));
            Combo.Add("UseRCombo", new CheckBox("Use R"));
            Combo.AddSeparator(2);
            Combo.Add("AutoRCombo", new Slider("Auto R if {0} Enemies", 3, 1, 5));
            Combo.Add("UseRComboKey", new KeyBind("Press Key To use R", false, KeyBind.BindTypes.HoldActive, 'T'));

            Laneclear = Principal.AddSubMenu("Laneclear", "Laneclear");
            Laneclear.Add("UseQLane", new CheckBox("Use Q"));
            Laneclear.Add("UseQLaneMin", new Slider("Use Q if hit {0} minions", 3, 1, 5));

            Jungleclear = Principal.AddSubMenu("Jungleclear", "Jungleclear");
            Jungleclear.Add("UseQJG", new CheckBox("Use Q"));
            Jungleclear.Add("UseWJG", new CheckBox("Use W"));
            Jungleclear.Add("UseEJG", new CheckBox("Use E"));

            Flee = Principal.AddSubMenu("Flee", "Flee");
            Flee.Add("UseEQFlee", new CheckBox("Use E+Q"));

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
            Draw.Add("DrawDMG", new CheckBox("Draw combo damage"));
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

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

namespace Hextech_Annie
{
    class AnnieMenu
    {
        public static Menu Principal, Combo, Laneclear, Jungleclear, Lasthit, Misc, Draw;

        public static void Init()
        {
            Principal = MainMenu.AddMenu("Annie", "Annie");
            Principal.AddLabel("Prediction:");
            Principal.Add("WPred", new Slider("W Hitchance: {0}%", 80, 20, 100));
            Principal.Add("RPred", new Slider("R Hitchance: {0}%", 80, 20, 100));

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("Q", new CheckBox("Use Q"));
            Combo.Add("W", new CheckBox("Use W"));
            Combo.Add("E", new CheckBox("Auto Stack Passive (E)"));
            Combo.Add("R", new CheckBox("Use R"));
            Combo.AddSeparator(2);
            Combo.Add("Only", new CheckBox("Only use the ult if stun enemies"));
            Combo.Add("Flash", new KeyBind("Flash + Ult", false, KeyBind.BindTypes.HoldActive, 'H'));
            Combo.AddSeparator();
            Combo.Add("Mode", new ComboBox("Pilot Mode:", 0, "Focuses on the nearest enemy", "Focuses on the enemy with the lowest HP"));

            Laneclear = Principal.AddSubMenu("Laneclear", "Laneclear");
            Laneclear.Add("Q", new CheckBox("Use Q"));
            Laneclear.Add("W", new CheckBox("Use W"));
            Laneclear.Add("Exception", new CheckBox("Don't use spells to farm if have stun"));

            Jungleclear = Principal.AddSubMenu("Jungleclear", "Jungleclear");
            Jungleclear.Add("Q", new CheckBox("Use Q"));
            Jungleclear.Add("W", new CheckBox("Use W"));

            Lasthit = Principal.AddSubMenu("Lasthit", "Lasthit");
            Lasthit.Add("Q", new CheckBox("Use Q"));
            Lasthit.Add("Exception", new CheckBox("Don't use Q to farm if have stun"));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("SkinHack", new CheckBox("SkinHack?", false));
            Misc.Add("SkinID", new Slider("SkinID: {0}", 10, 0, 10));
            Misc.Add("Reset", new KeyBind("Reset (Skin Bug):", false, KeyBind.BindTypes.HoldActive, 'T'));
            Misc.AddSeparator(2);
            Misc.Add("Gapcloser", new CheckBox("Gapcloser?"));
            Misc.Add("Interrupter", new CheckBox("Interrupter?"));

            Draw = Principal.AddSubMenu("Draw", "Draw");
            Draw.Add("Q", new CheckBox("Draw Q"));
            Draw.Add("W", new CheckBox("Draw W"));
            Draw.Add("R", new CheckBox("Draw R"));
            Draw.Add("Flash", new CheckBox("Draw Flash + Ult"));
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
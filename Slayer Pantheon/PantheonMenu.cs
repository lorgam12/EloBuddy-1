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

namespace Slayer_Pantheon
{
    class PantheonMenu
    {
        public static Menu Principal, Combo, Laneclear, Jungleclear, Lasthit, Misc, Draw;

        public static void Init()
        {
            Principal = MainMenu.AddMenu("Pantheon", "Pantheon");
            Principal.AddLabel("Prediction:");
            Principal.Add("EPred", new Slider("Hitchance E: {0}", 80, 20, 100));

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("Q", new CheckBox("Use Q"));
            Combo.Add("W", new CheckBox("Use W"));
            Combo.Add("E", new CheckBox("Use E"));

            Laneclear = Principal.AddSubMenu("Laneclear", "Laneclear");
            Laneclear.Add("Q", new CheckBox("Use Q"));
            Laneclear.Add("E", new CheckBox("Use E"));

            Jungleclear = Principal.AddSubMenu("Jungleclear", "Jungleclear");
            Jungleclear.Add("Q", new CheckBox("Use Q"));
            Jungleclear.Add("W", new CheckBox("Use W"));
            Jungleclear.Add("E", new CheckBox("Use E"));

            Lasthit = Principal.AddSubMenu("Lasthit", "Lasthit");
            Lasthit.Add("Q", new CheckBox("Use Q"));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("SkinHack", new CheckBox("SkinHack?", false));
            Misc.Add("SkinID", new Slider("Skin ID: {0}", 7, 0, 7));
            Misc.Add("Reset", new KeyBind("Reset (Skin Bug):", false, KeyBind.BindTypes.HoldActive, 'T'));
            Misc.AddSeparator(2);
            Misc.Add("Gapcloser", new CheckBox("Gapcloser?"));
            Misc.Add("Interrupter", new CheckBox("Interrupter?"));

            Draw = Principal.AddSubMenu("Draw", "Draw");
            Draw.Add("Q", new CheckBox("Draw Q"));
            Draw.Add("W", new CheckBox("Draw W"));
            Draw.Add("E", new CheckBox("Draw E"));
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

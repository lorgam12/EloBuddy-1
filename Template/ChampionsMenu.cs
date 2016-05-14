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

namespace Template
{
    class ChampionsMenu
    {
        public static Menu Principal, Combo, Laneclear, Jungleclear, Lasthit, Flee, Misc, Draw;

        public static void Init()
        {
            Principal = MainMenu.AddMenu("Champions", "Champions");
            Principal.AddLabel("Prediction:");
            Principal.Add("QPred", new Slider("Q Hitchance: {0}", 80, 20, 100));
            Principal.Add("WPred", new Slider("W Hitchance: {0}", 80, 20, 100));

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("Q", new CheckBox("Use Q"));
            Combo.Add("W", new CheckBox("Use W"));
            Combo.Add("E", new CheckBox("Use E"));
            Combo.Add("R", new CheckBox("Use R"));

            Laneclear = Principal.AddSubMenu("Laneclear", "Laneclear");
            Laneclear.Add("Q", new CheckBox("Use Q"));
            Laneclear.Add("W", new CheckBox("Use W"));

            Jungleclear = Principal.AddSubMenu("Jungleclear", "Jungleclear");
            Jungleclear.Add("Q", new CheckBox("Use Q"));
            Jungleclear.Add("W", new CheckBox("Use W"));
            Jungleclear.Add("E", new CheckBox("Use E"));

            Lasthit = Principal.AddSubMenu("Lasthit", "Lasthit");
            Lasthit.Add("Q", new CheckBox("Use Q"));

            Flee = Principal.AddSubMenu("Flee", "Flee");
            Flee.Add("E", new CheckBox("Use E"));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("SkinHack", new CheckBox("SkinHack?"));
            Misc.Add("SkinID", new Slider("Skin ID: {0}", 0, 0, 0));
            Misc.Add("Reset", new KeyBind("Reset (Skin Bug):", false, KeyBind.BindTypes.HoldActive, 'T'));
            Misc.AddSeparator(2);
            Misc.Add("Gapcloser", new CheckBox("Gapcloser?"));
            Misc.Add("Interrupter", new CheckBox("Interrupter?"));

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

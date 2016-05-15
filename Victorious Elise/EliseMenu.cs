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

namespace Victorious_Elise
{
    class EliseMenu
    {
        public static Menu Principal, Combo, Laneclear, Jungleclear, Lasthit, Flee, Misc, Draw;

        public static void Init()
        {
            Principal = MainMenu.AddMenu("Elise", "Elise");
            Principal.AddLabel("Prediction:");
            Principal.Add("WPred", new Slider("W Hitchance: {0}%", 80, 20, 100));
            Principal.Add("EPred", new Slider("E Hitchance: {0}%", 80, 20, 100));

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.AddLabel("Human Form:");
            Combo.Add("Q", new CheckBox("Use Q"));
            Combo.Add("W", new CheckBox("Use W"));
            Combo.Add("E", new CheckBox("Use E"));
            Combo.AddSeparator(2);
            Combo.AddLabel("Spider Form:");
            Combo.Add("Q2", new CheckBox("Use Q"));
            Combo.Add("W2", new CheckBox("Use W"));
            Combo.Add("E2", new CheckBox("Use E"));
            Combo.Add("E2Ex", new Slider("Use E Min {0} Range", 550, 200, 700));
            Combo.AddSeparator(2);
            Combo.Add("Switch", new CheckBox("Auto switch form (Spider and Human)"));
            Combo.Add("Delay", new Slider("Humanizer (Delay):", 300, 0, 500));

            Laneclear = Principal.AddSubMenu("Laneclear", "Laneclear");
            Laneclear.AddLabel("Human Form:");
            Laneclear.Add("Q", new CheckBox("Use Q"));
            Laneclear.Add("W", new CheckBox("Use W"));
            Laneclear.AddSeparator(2);
            Laneclear.AddLabel("Spider Form:");
            Laneclear.Add("Q2", new CheckBox("Use Q"));
            Laneclear.Add("W2", new CheckBox("Use W"));

            Jungleclear = Principal.AddSubMenu("Jungleclear", "Jungleclear");
            Jungleclear.AddLabel("Human Form:");
            Jungleclear.Add("Q", new CheckBox("Use Q"));
            Jungleclear.Add("W", new CheckBox("Use W"));
            Jungleclear.Add("E", new CheckBox("Use E"));
            Jungleclear.AddSeparator(2);
            Jungleclear.AddLabel("Spider Form:");
            Jungleclear.Add("Q2", new CheckBox("Use Q"));
            Jungleclear.Add("W2", new CheckBox("Use W"));

            Lasthit = Principal.AddSubMenu("Lasthit", "Lasthit");
            Lasthit.AddLabel("Human Form:");
            Lasthit.Add("Q", new CheckBox("Use Q"));
            Lasthit.AddLabel("Spider Form:");
            Lasthit.Add("Q2", new CheckBox("Use Q"));

            Flee = Principal.AddSubMenu("Flee", "Flee");
            Flee.AddLabel("Human Form:");
            Flee.Add("E", new CheckBox("Use E"));
            Flee.AddSeparator(2);
            Flee.AddLabel("Spider Form:");
            Flee.Add("E2", new CheckBox("Use E"));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("Gapcloser", new CheckBox("Gapcloser?"));
            Misc.Add("Interrupter", new CheckBox("Interrupter?"));
            Misc.AddSeparator(2);
            Misc.Add("SkinHack", new CheckBox("SkinHack?", false));
            Misc.Add("SkinID", new Slider("Skin ID: {0}", 2, 0, 4));
            Misc.Add("Reset", new KeyBind("Reset (Skin Bug)", false, KeyBind.BindTypes.HoldActive, 'T'));

            Draw = Principal.AddSubMenu("Draw", "Draw");
            Draw.AddLabel("Human Form:");
            Draw.Add("Q", new CheckBox("Draw Q"));
            Draw.Add("W", new CheckBox("Draw W"));
            Draw.Add("E", new CheckBox("Draw E"));
            Draw.AddSeparator(2);
            Draw.AddLabel("Spider Form:");
            Draw.Add("Q2", new CheckBox("Draw Q"));
            Draw.Add("E2", new CheckBox("Draw E"));
            Draw.AddSeparator(2);
            Draw.Add("Cooldowns", new CheckBox("Draw Cooldowns Skills"));
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

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
using System.Net;
using Version = System.Version;
using System.Text.RegularExpressions;

namespace Soul_Reaver_Draven
{
    class DravenMenu
    {
        public static Menu Principal, Combo, Axes, Laneclear, Jungleclear, Flee, Misc, Draw;

        public static void Init()
        {
            Principal = MainMenu.AddMenu("Draven", "Draven");
            Principal.AddLabel("Prediction:");
            Principal.Add("EPred", new Slider("E Hitchance: {0}%", 80, 20, 100));
            Principal.Add("RPred", new Slider("R Hitchance: {0}%", 80, 20, 100));

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("Q", new CheckBox("Use Q"));
            Combo.Add("E", new CheckBox("Use E"));
            Combo.Add("R", new CheckBox("Use R"));

            Axes = Principal.AddSubMenu("Axes", "Axes");
            Axes.Add("Mode", new ComboBox("Axes Catch Mode:", 0, "Cursor Range", "Player Range"));
            Axes.Add("Range", new Slider("Set Catch Range: {0}", 600, 250, 1000));
            Axes.AddSeparator();
            Axes.Add("Pick", new ComboBox("Pick Axes Mode:", 1, "Combo Mode", "Always", "Manual"));
            Axes.Add("Delay", new Slider("Humanizer: {0} Delay", 250, 0, 350));

            Laneclear = Principal.AddSubMenu("Laneclear", "Laneclear");
            Laneclear.Add("Q", new CheckBox("Use Q"));

            Jungleclear = Principal.AddSubMenu("Jungleclear", "Jungleclear");
            Jungleclear.Add("Q", new CheckBox("Use Q"));
            Jungleclear.Add("E", new CheckBox("Use E"));

            Flee = Principal.AddSubMenu("Flee", "Flee");
            Flee.Add("W", new CheckBox("Use W"));
            Flee.Add("E", new CheckBox("Use E"));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("SkinHack", new CheckBox("SkinHack?", false));
            Misc.Add("SkinID", new Slider("Skin ID: {0}", 1, 0, 6));
            Misc.Add("Reset", new KeyBind("Reset (Skin Bug):", false, KeyBind.BindTypes.HoldActive, 'T'));
            Misc.AddSeparator(2);
            Misc.Add("Interrupter", new CheckBox("Interrupter?"));
            Misc.Add("Gapcloser", new CheckBox("Gapcloser?"));

            Draw = Principal.AddSubMenu("Draw", "Draw");
            Draw.Add("E", new CheckBox("Draw E"));
            Draw.Add("Axes", new CheckBox("Draw Axes"));
            Draw.Add("Catch", new CheckBox("Draw Catch Range"));
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

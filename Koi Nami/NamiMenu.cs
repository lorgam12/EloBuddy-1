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

namespace Koi_Nami
{
    class NamiMenu
    {
        public static Menu Principal, Combo, Ally, Misc, Draw;

        public static void Init()
        {
            Principal = MainMenu.AddMenu("Nami", "Nami");
            Principal.AddLabel("Prediction:");
            Principal.Add("QPred", new Slider("Q Hitchance: {0}%", 80, 20, 100));
            Principal.Add("RPred", new Slider("R Hitchance: {0}%", 80, 20, 100));

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("Q", new CheckBox("Use Q"));
            Combo.Add("R", new CheckBox("Use R"));
            Combo.Add("AutoR", new Slider("Auto R if hit {0} enemies", 3, 0, 5));

            Ally = Principal.AddSubMenu("Ally", "Ally");
            foreach(var x in EntityManager.Heroes.Allies)
            {
                Ally.Add(x.ChampionName + "/W", new CheckBox("Use W To Heal (" + x.ChampionName + ")"));
                Ally.Add(x.ChampionName + "/E", new CheckBox("Use E Before AA (" + x.ChampionName + ")"));
            }
            Ally.Add("Min", new Slider("Min Health: {0}% To Heal", 40, 10, 80));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("SkinHack", new CheckBox("SkinHack?", false));
            Misc.Add("SkinID", new Slider("Skin ID: {0}", 2, 0, 3));
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

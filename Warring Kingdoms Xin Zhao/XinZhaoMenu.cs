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
using SharpDX;

namespace Warring_Kingdoms_Xin_Zhao
{
    class XinZhaoMenu
    {
        public static Menu Principal, Combo, Laneclear, Jungleclear, Flee, Misc, Draw;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("XinZhao", "XinZhao");
            Principal.AddLabel("Warring Kingdoms XinZhao v" + Assembly.GetExecutingAssembly().GetName().Version);
            Principal.AddSeparator(2);
            Principal.AddLabel("Good game !");

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("UseQCombo", new CheckBox("Use Q"));
            Combo.Add("UseWCombo", new CheckBox("Use W"));
            Combo.Add("UseECombo", new CheckBox("Use E"));
            Combo.Add("UseEComboMin", new Slider("Use E Min {0} Range", 300, 200, 500));
            Combo.Add("UseRCombo", new CheckBox("Use R"));
            Combo.AddSeparator(2);
            Combo.Add("AutoRCombo", new Slider("Auto R if {0} Enemies", 3, 1, 5));
            Combo.Add("UseRComboKey", new KeyBind("Press Key To use R", false, KeyBind.BindTypes.HoldActive, 'T'));

            Laneclear = Principal.AddSubMenu("Laneclear", "Laneclear");
            Laneclear.Add("UseQLane", new CheckBox("Use Q"));
            Laneclear.Add("UseWLane", new CheckBox("Use W"));
            Laneclear.Add("UseELane", new CheckBox("Use E"));
            Laneclear.Add("UseELaneMin", new Slider("Use E Min {0} Range", 300, 200, 500));

            Jungleclear = Principal.AddSubMenu("Jungleclear", "Jungleclear");
            Jungleclear.Add("UseQJG", new CheckBox("Use Q"));
            Jungleclear.Add("UseWJG", new CheckBox("Use W"));
            Jungleclear.Add("UseEJG", new CheckBox("Use E"));
            Jungleclear.Add("UseEJGMin", new Slider("Use E Min {0} Range", 300, 200, 500));

            Flee = Principal.AddSubMenu("Flee", "Flee");
            Flee.Add("UseEQFlee", new CheckBox("Use E"));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("Skin", new CheckBox("Skinhack ?", false));
            Misc.Add("SkinID", new Slider("Skin ID: {0}", 5, 0, 6));
            Misc.Add("Interrupter", new CheckBox("Interrupter ?"));

            Draw = Principal.AddSubMenu("Drawing", "Drawing");
            Draw.Add("DrawE", new CheckBox("Draw E"));
            Draw.Add("DrawR", new CheckBox("Draw R"));
            //Draw.Add("DrawDMG", new CheckBox("Draw combo damage"));
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

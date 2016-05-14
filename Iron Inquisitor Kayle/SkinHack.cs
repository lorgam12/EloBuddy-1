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
    class SkinHack
    {
        public static void Init()
        {
            if (KayleMenu.CheckBox(KayleMenu.Misc, "SkinHack"))
            {
                Player.Instance.SetSkinId(KayleMenu.Slider(KayleMenu.Misc, "SkinID"));
            }

            if (KayleMenu.Keybind(KayleMenu.Misc, "Reset"))
            {
                Player.Instance.SetModel(Player.Instance.ChampionName);
            }
        }
    }
}

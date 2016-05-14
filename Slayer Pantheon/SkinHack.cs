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
    class SkinHack
    {
        public static void Init()
        {
            if (PantheonMenu.CheckBox(PantheonMenu.Misc, "SkinHack"))
            {
                Player.Instance.SetSkinId(PantheonMenu.Slider(PantheonMenu.Misc, "SkinID"));
            }

            if (PantheonMenu.Keybind(PantheonMenu.Misc, "Reset"))
            {
                Player.Instance.SetModel(Player.Instance.ChampionName);
            }
        }
    }
}

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
    class SkinHack
    {
        public static void Init()
        {
            if(NamiMenu.CheckBox(NamiMenu.Misc, "SkinHack"))
            {
                Player.Instance.SetSkinId(NamiMenu.Slider(NamiMenu.Misc, "SkinID"));
            }

            if(NamiMenu.Keybind(NamiMenu.Misc, "Reset"))
            {
                Player.Instance.SetModel(Player.Instance.ChampionName);
            }
        }
    }
}

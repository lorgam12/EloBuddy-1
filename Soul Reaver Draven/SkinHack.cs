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

namespace Soul_Reaver_Draven
{
    class SkinHack
    {
        public static void Init()
        {
            if(DravenMenu.CheckBox(DravenMenu.Misc, "SkinHack"))
            {
                Player.Instance.SetSkinId(DravenMenu.Slider(DravenMenu.Misc, "SkinID"));
            }

            if(DravenMenu.Keybind(DravenMenu.Misc, "Reset"))
            {
                Player.Instance.SetModel(Player.Instance.ChampionName);
            }
        }
    }
}

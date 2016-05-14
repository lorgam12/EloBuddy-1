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

namespace Iron_Inquisitor_Kayle.Modes
{
    class Flee
    {
        public static void Init()
        {
            if (Kayle.W.IsReady() && KayleMenu.CheckBox(KayleMenu.Flee, "W"))
            {
                Kayle.W.Cast(Player.Instance);
            }
        }
    }
}

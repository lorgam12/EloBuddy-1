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

namespace Championship_Riven
{
    class Damages : Extensions
    {
        public static float Combo(Obj_AI_Base T)
        {
            float D = 0;

            if (Riven.Q.IsReady())
                D += QDMG(T) * 3 - Riven.CountQ;

            if (Riven.W.IsReady())
                D += WDMG(T);

            if (Riven.R.IsReady())
                D += RDMG(T);

            D += PDMG(T);

            return D;
        }

        public static float Dot(Obj_AI_Base T)
        {
            return Player.Instance.GetSummonerSpellDamage(T, DamageLibrary.SummonerSpells.Ignite);
        }
    }
}

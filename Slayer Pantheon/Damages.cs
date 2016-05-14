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
    class Damages
    {
        public static float QDamage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 65, 105, 145, 185, 225 }[Pantheon.Q.Level - 1] + 1.4f * Player.Instance.FlatPhysicalDamageMod) * ((T.Health / T.MaxHealth < 0.15) ? 2 : 1);
        }

        public static float WDamage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 50, 75, 100, 125, 150 }[Pantheon.W.Level - 1] + 1 * Player.Instance.TotalMagicalDamage);
        }

        public static float EDamagE(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 13, 23, 33, 43, 53 }[Pantheon.E.Level - 1] + 0.6f * Player.Instance.FlatPhysicalDamageMod) * ((T is AIHeroClient) ? 2 : 1);
        }
    }
}

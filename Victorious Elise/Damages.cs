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

namespace Victorious_Elise
{
    class Damages
    {
        public static float QDamage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 40, 75, 110, 145, 180 }[Elise.Q.Level - 1] + (0.08f + 0.03f / 100 * Player.Instance.TotalMagicalDamage) * T.Health);
        }

        public static float Q2Damage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 60, 100, 140, 180, 220 }[Elise.Q2.Level - 1] + (0.08f + 0.03f / 100 * Player.Instance.TotalMagicalDamage) * (T.MaxHealth - T.Health));
        }

        public static float WDamage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 75, 125, 175, 225, 275 }[Elise.W.Level - 1] + 0.08f * Player.Instance.TotalMagicalDamage);
        }
    }
}

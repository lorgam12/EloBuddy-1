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

namespace Hextech_Annie
{
    class Damages
    {
        public static float QDamage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 80, 115, 150, 185, 220 }[Annie.Q.Level - 1] + 0.8f * Player.Instance.TotalMagicalDamage);
        }

        public static float WDamage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 70, 115, 160, 205, 250 }[Annie.W.Level - 1] + 0.85f * Player.Instance.TotalMagicalDamage);
        }

        public static float RDamage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 150, 275, 400 }[Annie.Q.Level - 1] + new float[] { 10, 15, 20 }[Annie.E.Level - 1] + new float[] { 50, 75, 100 }[Annie.R.Level - 1] + (0.65f + 0.1f + 0.15f) * Player.Instance.TotalMagicalDamage);
        }
    }
}

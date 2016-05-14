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
    class Damages
    {
        public static float QDamage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 45, 55, 65, 75, 85 }[Draven.Q.Level - 1] / 100 * Player.Instance.BaseAttackDamage + Player.Instance.FlatPhysicalDamageMod);
        }

        public static float EDamage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 70, 105, 140, 175, 210 }[Draven.E.Level - 1] + 0.05f * Player.Instance.FlatPhysicalDamageMod);
        }

        public static float RDamage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 175, 275, 375 }[Draven.R.Level - 1] + 1.1f * Player.Instance.FlatPhysicalDamageMod);
        }

        public static float PDamage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Physical, 0.45f + (Player.Instance.BaseAttackDamage + Player.Instance.FlatPhysicalDamageMod));
        }
    }
}

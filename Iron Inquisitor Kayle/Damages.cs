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
    class Damages
    {
        public static float QDamage(Obj_AI_Base T)
        {
            return T.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 60, 110, 160, 210, 260 }[Kayle.Q.Level - 1] + 1 * Player.Instance.FlatPhysicalDamageMod + 0.6f * Player.Instance.TotalMagicalDamage);
        }
    }
}
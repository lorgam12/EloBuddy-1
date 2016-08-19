using EloBuddy;

namespace JokerLib.Helper
{
    public static class Target
    {
        public static bool IsValid(Obj_AI_Base T)
        {
            if (T.HasBuffOfType(BuffType.PhysicalImmunity) || T.HasBuffOfType(BuffType.SpellImmunity) || T.IsZombie || T.IsInvulnerable || T.HasBuffOfType(BuffType.Invulnerability) || T.HasBuff("kindredrnodeathbuff") || T.HasBuffOfType(BuffType.SpellShield))
            {
                return false;
            }

            return true;
        }
    }
}

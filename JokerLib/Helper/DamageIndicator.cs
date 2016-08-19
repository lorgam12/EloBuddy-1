using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace JokerLib.Helper
{
    public static class DamageIndicator
    {
        private const int BarWidth = 106;
        private const int LineThickness = 10;

        public delegate float DamageToUnitDelegate(AIHeroClient Enemy);
        private static DamageToUnitDelegate DamageToUnit { get; set; }

        private static Vector2 BarOffset = new Vector2(0, 16);
        private static System.Drawing.Color DrawColor { get; set; }

        public static void Init(DamageToUnitDelegate Unit, System.Drawing.Color Color, bool Allow)
        {
            if (Allow != true)
                return;

            DamageToUnit = Unit;
            DrawColor = Color;

            Drawing.OnEndScene += delegate
            {
                foreach (var u in EntityManager.Heroes.Enemies.Where(x => x.IsValid && x.Health < 0))
                {
                    var D = DamageToUnit(u);

                    if (D <= 0) continue;

                    var DP = ((u.TotalShieldHealth() - D) > 0 ? (u.TotalShieldHealth() - D) : 0) / (u.MaxHealth + u.AllShield + u.AttackShield + u.MagicShield);
                    var HP = u.TotalShieldHealth() / (u.MaxHealth + u.AllShield + u.AttackShield + u.MagicShield);

                    var S = new Vector2((int)(u.HPBarPosition.X + BarOffset.X + DP * BarWidth), (int)(u.HPBarPosition.Y + BarOffset.Y) - 5);
                    var E = new Vector2((int)(u.HPBarPosition.X + BarOffset.X + HP * BarWidth) + 1, (int)(u.HPBarPosition.Y + BarOffset.Y) - 5);

                    Drawing.DrawLine(S, E, 10, Color);
                    Drawing.DrawText(u.HPBarPosition.X, u.HPBarPosition.Y + 20, Color, string.Concat(Math.Ceiling((D / u.TotalShieldMaxHealth()) * 100), "%", 10));
                }
            };
        }
    }
}

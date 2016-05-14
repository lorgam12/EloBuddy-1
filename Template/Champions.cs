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

namespace Template
{
    class Champions
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Active E;
        public static Spell.Active R;

        public static void Init()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 0, SkillShotType.Linear, 250, int.MaxValue, 0);
            W = new Spell.Skillshot(SpellSlot.W, 0, SkillShotType.Circular, 250, int.MaxValue, 0);
            E = new Spell.Active(SpellSlot.E, 0);
            R = new Spell.Active(SpellSlot.R, 0);

            Game.OnUpdate += Game_OnUpdate;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Modes.Combo.Init();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                Modes.Laneclear.Init();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                Modes.Jungleclear.Init();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                Modes.Lasthit.Init();
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (sender.IsEnemy || sender.IsValid)
            {
                if (ChampionsMenu.CheckBox(ChampionsMenu.Misc, "Interrupter"))
                {
                    if (sender.IsValidTarget(Q.Range))
                    {
                        var QPred = Q.GetPrediction(sender);

                        if (QPred.HitChancePercent >= ChampionsMenu.Slider(ChampionsMenu.Principal, "QPred"))
                        {
                            Q.Cast(QPred.UnitPosition);
                        }
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsEnemy || sender.IsValid)
            {
                if(ChampionsMenu.CheckBox(ChampionsMenu.Misc, "Gapcloser"))
                {
                    if(sender.IsValidTarget(Q.Range))
                    {
                        var QPred = Q.GetPrediction(sender);

                        if(QPred.HitChancePercent >= ChampionsMenu.Slider(ChampionsMenu.Principal, "QPred"))
                        {
                            Q.Cast(QPred.UnitPosition);
                        }
                    }
                }
            }
        }
    }
}

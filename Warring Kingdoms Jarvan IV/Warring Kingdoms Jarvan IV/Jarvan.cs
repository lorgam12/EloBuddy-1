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

namespace Warring_Kingdoms_Jarvan_IV
{
    class Jarvan
    {
        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Targeted R;
        public static GameObject Estandarte = null;

        public static void Load()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 770, SkillShotType.Linear, 600, int.MaxValue, 70);
            W = new Spell.Active(SpellSlot.W, 520);
            E = new Spell.Skillshot(SpellSlot.E, 840, SkillShotType.Circular, 500, int.MaxValue, 70);
            R = new Spell.Targeted(SpellSlot.R, 650);

            Game.OnTick += Game_OnTick;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            DamageIndicator.Initialize(GetTotalDamage);
        }

        public static float GetTotalDamage(AIHeroClient T)
        {
            float Damage = 0f;

            if (T != null)
            {
                if (Q.IsReady())
                {
                    Damage += Player.Instance.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 70, 115, 160, 205, 250 }[Q.Level] + 1.2f * Player.Instance.TotalAttackDamage);
                }

                if (E.IsReady())
                {
                    Damage += Player.Instance.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 60, 105, 150, 195, 240 }[E.Level] + 0.8f * Player.Instance.TotalMagicalDamage);

                }

                if (R.IsReady())
                {
                    Damage += Player.Instance.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 200, 325, 450 }[R.Level] + 1.5f * Player.Instance.TotalAttackDamage);

                }

                return Damage;
            }

            return Damage;
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!sender.IsValid || sender.IsDead || sender.IsMe || sender.IsAlly)
                return;

            if (sender == null)
                return;

            if(e.End == Player.Instance.Position)
            {
                foreach(var Allys in EntityManager.Heroes.Allies.Where(x => x.IsValidTarget(E.Range)).OrderBy(x => x.HealthPercent))
                {
                    E.Cast(Allys.Position);
                    Q.Cast(Allys.Position);
                }
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (!sender.IsValid || sender.IsAlly || sender.IsMe)
                return;

            if (sender == null)
                return;

            if(e.DangerLevel == DangerLevel.High)
            {
                var EPred = E.GetPrediction(sender);
                var QPred = Q.GetPrediction(sender);

                if(EPred.HitChance >= HitChance.High && QPred.HitChance >= HitChance.High)
                {
                    E.Cast(sender);
                    Q.Cast(sender);
                }
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (JarvanMenu.CheckBox(JarvanMenu.Misc, "Skin"))
            {
                Player.SetSkinId(JarvanMenu.Slider(JarvanMenu.Misc, "SkinID"));
            }

            if (JarvanMenu.Keybind(JarvanMenu.Combo, "UseRComboKey") && R.IsReady())
            {
                var Target = TargetSelector.GetTarget(R.Range, DamageType.Physical);

                if(Target != null)
                {
                    R.Cast(Target);
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var QTarget = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                var WTarget = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                var ETarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                var RTarget = TargetSelector.GetTarget(R.Range, DamageType.Physical);

                if(E.IsReady() && Q.IsReady())
                {
                    if (!QTarget.IsValidTarget(E.Range) && !JarvanMenu.CheckBox(JarvanMenu.Combo, "UseQCombo") && !JarvanMenu.CheckBox(JarvanMenu.Combo, "UseECombo"))
                        return;

                    if (QTarget == null)
                        return;

                    var Vec = QTarget.ServerPosition - Player.Instance.Position;
                    var Behind = E.GetPrediction(ETarget).CastPosition + Vector3.Normalize(Vec) * 100;

                    if(E.IsReady())
                    {
                        E.Cast(Behind);
                    }

                    if(Q.IsReady())
                    {
                        Q.Cast(Behind);
                    }
                   
                }else if(QTarget.IsValidTarget(Q.Range) && JarvanMenu.CheckBox(JarvanMenu.Combo, "UseQCombo"))
                {
                    if (QTarget == null)
                        return;

                    var QPred = Q.GetPrediction(QTarget);

                    if(QPred.HitChance >= HitChance.Medium)
                    {
                        Q.Cast(QPred.UnitPosition);
                    }
                }

                if(W.IsReady() && JarvanMenu.CheckBox(JarvanMenu.Combo, "UseWCombo"))
                {
                    if (WTarget == null)
                        return;

                    if(WTarget.IsValidTarget(W.Range))
                    {
                        Core.DelayAction(() => W.Cast(), 1000);
                    }
                }

                if(R.IsReady() && JarvanMenu.CheckBox(JarvanMenu.Combo, "UseRCombo"))
                {
                    if (RTarget == null)
                        return;

                    if(RTarget.CountEnemiesInRange(R.Range) >= JarvanMenu.Slider(JarvanMenu.Combo, "AutoRCombo"))
                    {
                        if(RTarget.IsValidTarget(R.Range))
                        {
                            R.Cast(RTarget);
                        }
                    }
                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                if (!JarvanMenu.CheckBox(JarvanMenu.Flee, "UseEQFlee"))
                    return;

                if(E.IsReady() && Q.IsReady())
                {
                    E.Cast(Game.CursorPos.Distance(Player.Instance.Position) > E.Range ? Player.Instance.Position.Extend(Game.CursorPos, E.Range - 1).To3D() : Game.CursorPos);
                    Q.Cast(Game.CursorPos.Distance(Player.Instance.Position) > Q.Range ? Player.Instance.Position.Extend(Game.CursorPos, Q.Range - 1).To3D() : Game.CursorPos);
                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => !x.IsDead && x.IsValidTarget(Q.Range));
                var Minions = EntityManager.MinionsAndMonsters.GetLineFarmLocation(EntityManager.MinionsAndMonsters.EnemyMinions, Q.Width, (int) Q.Range);

                if (Minion == null)
                    return;

                if(Q.IsReady() && JarvanMenu.CheckBox(JarvanMenu.Laneclear, "UseQLane"))
                {
                    if (Minions.HitNumber >= JarvanMenu.Slider(JarvanMenu.Laneclear, "UseQLaneMin"))
                    {
                        Q.Cast(Minions.CastPosition);
                    }
                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderBy(x => x.MaxHealth).FirstOrDefault(x => x.IsValidTarget(Q.Range));

                if (Minion == null)
                    return;

                if(JarvanMenu.CheckBox(JarvanMenu.Jungleclear, "UseQJG") && JarvanMenu.CheckBox(JarvanMenu.Jungleclear, "UseEJG"))
                {
                    if(E.IsReady() && Q.IsReady())
                    {
                        E.Cast(Minion);
                        Q.Cast(Minion);
                    }else if(!E.IsReady())
                    {
                        Q.Cast(Minion);
                    }
                }
            }
        }
    }
}

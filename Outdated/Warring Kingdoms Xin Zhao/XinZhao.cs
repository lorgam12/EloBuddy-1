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

namespace Warring_Kingdoms_Xin_Zhao
{
    class XinZhao
    {
        public static Spell.Active Q;
        public static Spell.Active W;
        public static Spell.Targeted E;
        public static Spell.Active R;

        public static void Load()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 650);
            R = new Spell.Active(SpellSlot.R, 500);

            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
            Game.OnUpdate += Game_OnUpdate;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (sender.IsAlly || sender.IsMe || sender.IsDead)
                return;

            if(sender.IsValidTarget(R.Range))
            {
                if(e.DangerLevel == DangerLevel.High)
                {
                    R.Cast();
                }
            }
        }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                if (target.IsValidTarget(Player.Instance.GetAutoAttackRange()))
                {
                    if (W.IsReady() || XinZhaoMenu.CheckBox(XinZhaoMenu.Combo, "UseWCombo"))
                    {
                        W.Cast();
                    }

                    if (Q.IsReady() || XinZhaoMenu.CheckBox(XinZhaoMenu.Combo, "UseQCombo"))
                    {
                        Q.Cast();
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                if (target.IsValidTarget(Player.Instance.GetAutoAttackRange()))
                {
                    if (W.IsReady() || XinZhaoMenu.CheckBox(XinZhaoMenu.Laneclear, "UseWLane"))
                    {
                        W.Cast();
                    }

                    if (Q.IsReady() || XinZhaoMenu.CheckBox(XinZhaoMenu.Laneclear, "UseQLane"))
                    {
                        Q.Cast();
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                if (target.IsValidTarget(Player.Instance.GetAutoAttackRange()))
                {
                    if (W.IsReady() || XinZhaoMenu.CheckBox(XinZhaoMenu.Jungleclear, "UseWJG"))
                    {
                        W.Cast();
                    }

                    if (Q.IsReady() || XinZhaoMenu.CheckBox(XinZhaoMenu.Jungleclear, "UseQJG"))
                    {
                        Q.Cast();
                    }
                }
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if (XinZhaoMenu.CheckBox(XinZhaoMenu.Misc, "Skin"))
            {
                Player.SetSkinId(XinZhaoMenu.Slider(XinZhaoMenu.Misc, "SkinID"));
            }

            if (XinZhaoMenu.Keybind(XinZhaoMenu.Combo, "UseRComboKey") && R.IsReady())
            {
                var Target = TargetSelector.GetTarget(R.Range, DamageType.Physical);

                if (Target != null || Target.IsValidTarget(R.Range))
                {
                    R.Cast();
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Minions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.IsValid || !x.IsDead).OrderBy(x => x.IsValidTarget(E.Range));
                var Minion = EntityManager.MinionsAndMonsters.GetLineFarmLocation(Minions, E.Range, (int)E.Range);

                if (Minions == null)
                    return;

                if(E.IsReady() || XinZhaoMenu.CheckBox(XinZhaoMenu.Laneclear, "UseELane"))
                {
                    if (Player.Instance.Position.Distance(Minion.CastPosition) >= XinZhaoMenu.Slider(XinZhaoMenu.Laneclear, "UseELaneMin"))
                    {
                        E.Cast(Minion.CastPosition);
                    }
                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                var Monster = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderBy(x => x.MaxHealth).FirstOrDefault(x => x.IsValidTarget(E.Range));

                if (Monster == null)
                    return;

                if (E.IsReady() || XinZhaoMenu.CheckBox(XinZhaoMenu.Jungleclear, "UseEJG"))
                {
                    if (Player.Instance.Position.Distance(Monster) >= XinZhaoMenu.Slider(XinZhaoMenu.Jungleclear, "UseEJGMin"))
                    {
                        E.Cast(Monster);
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                if(R.IsReady() || XinZhaoMenu.CheckBox(XinZhaoMenu.Combo, "UseRCombo"))
                {
                    if(Player.Instance.Position.CountEnemiesInRange(R.Range) > XinZhaoMenu.Slider(XinZhaoMenu.Combo, "AutoRCombo"))
                    {
                        R.Cast();
                    }
                }

                if (E.IsReady() || XinZhaoMenu.CheckBox(XinZhaoMenu.Combo, "UseECombo"))
                {
                    var Target = TargetSelector.GetTarget(E.Range + Player.Instance.GetAutoAttackRange(), DamageType.Physical);

                    if (Target == null)
                        return;

                    if (Player.Instance.Position.Distance(Target) >= XinZhaoMenu.Slider(XinZhaoMenu.Combo, "UseEComboMin"))
                    {
                        E.Cast(Target);
                    }
                }
            }
        }
    }
}

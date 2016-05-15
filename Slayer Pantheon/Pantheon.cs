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

using System.Reflection;
using System.Net;
using Version = System.Version;
using System.Text.RegularExpressions;

namespace Slayer_Pantheon
{
    class Pantheon
    {
        public static Spell.Targeted Q;
        public static Spell.Targeted W;
        public static Spell.Skillshot E;

        public static void Init()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Targeted(SpellSlot.W, 600);
            E = new Spell.Skillshot(SpellSlot.E, 400, SkillShotType.Cone, 250, 800, 70);

            Check();
            Game.OnUpdate += Game_OnUpdate;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
            {
                if (args.Slot == SpellSlot.E)
                {
                    Orbwalker.DisableMovement = true;
                    Core.DelayAction(() => Orbwalker.DisableMovement = false, 1550);
                }
            }
        }

        public static void Check()
        {
            string RawVersion = new WebClient().DownloadString("https://raw.githubusercontent.com/DownsecAkr/EloBuddy/master/" + Assembly.GetExecutingAssembly().GetName().Name + "/Properties/AssemblyInfo.cs");
            var Try = new Regex(@"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]").Match(RawVersion);
            if (!Try.Success)
            {
                if (new Version(string.Format("{0}.{1}.{2}.{3}", Try.Groups[1], Try.Groups[2], Try.Groups[3], Try.Groups[4])) > Assembly.GetExecutingAssembly().GetName().Version)
                {
                    Chat.Print("<font color ='#042722'> This version is outdated </font> <font color='#ff0000'>" + Assembly.GetExecutingAssembly().GetName().Version + " </font>");
                }
                else
                {
                    Chat.Print("<font color ='#042722'> Thanks for using </font> <font color='#00530a'>" + Assembly.GetExecutingAssembly().GetName().Name + " </font>");
                }
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            SkinHack.Init();

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
            if (sender.IsEnemy && sender.IsValid)
            {
                if (PantheonMenu.CheckBox(PantheonMenu.Misc, "Interrupter"))
                {
                    if (sender.IsValidTarget(W.Range))
                    {
                        if (W.IsReady())
                        {
                            W.Cast(sender);
                        }
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if(sender.IsEnemy && sender.IsValid)
            {
                if (PantheonMenu.CheckBox(PantheonMenu.Misc, "Gapcloser"))
                {
                    if (sender.IsValidTarget(W.Range))
                    {
                        if (W.IsReady())
                        {
                            W.Cast(sender);
                        }
                    }
                }
            }
        }
    }
}

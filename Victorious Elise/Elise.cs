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

namespace Victorious_Elise
{
    class Elise
    {
        public static Spell.Targeted Q;
        public static Spell.Skillshot W;
        public static Spell.Skillshot E;
        public static Spell.Active R;
        public static Spell.Targeted Q2;
        public static Spell.Active W2;
        public static Spell.Targeted E2;

        public static void Init()
        {
            // Human
            Q = new Spell.Targeted(SpellSlot.Q, 625);
            W = new Spell.Skillshot(SpellSlot.W, 950, SkillShotType.Linear, 250, 1000, 100);
            E = new Spell.Skillshot(SpellSlot.E, 1075, SkillShotType.Linear, 250, 1600, 60);
            E.AllowedCollisionCount = 0;

            // Spider
            Q2 = new Spell.Targeted(SpellSlot.Q, 475);
            W2 = new Spell.Active(SpellSlot.W);
            E2 = new Spell.Targeted(SpellSlot.E, 750);

            R = new Spell.Active(SpellSlot.R);

            Check();
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Game.OnUpdate += Game_OnUpdate;
            Obj_AI_Base.OnProcessSpellCast += Cooldowns.Obj_AI_Base_OnProcessSpellCast;
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

        public static bool CheckForm()
        {
            if(Player.Instance.Spellbook.GetSpell(SpellSlot.Q).Name == "EliseHumanQ" || Player.Instance.Spellbook.GetSpell(SpellSlot.W).Name == "EliseHumanW" || Player.Instance.Spellbook.GetSpell(SpellSlot.E).Name == "EliseHumanE")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if(sender.IsEnemy && sender.IsValid)
            {
                if(EliseMenu.CheckBox(EliseMenu.Misc, "Interrupter"))
                {
                    if(CheckForm())
                    {
                        if(E.IsReady())
                        {
                            var EPred = E.GetPrediction(sender);

                            if(EPred.HitChancePercent >= EliseMenu.Slider(EliseMenu.Principal, "EPred"))
                            {
                                E.Cast(EPred.UnitPosition);
                            }
                        }
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if(sender.IsEnemy && sender.IsValid)
            {
                if (EliseMenu.CheckBox(EliseMenu.Misc, "Gapcloser"))
                {
                    if (CheckForm())
                    {
                        if (E.IsReady())
                        {
                            var EPred = E.GetPrediction(sender);

                            if (EPred.HitChancePercent >= EliseMenu.Slider(EliseMenu.Principal, "EPred"))
                            {
                                E.Cast(EPred.UnitPosition);
                            }
                        }
                    }
                }
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            Cooldowns.Init();
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

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                Modes.Flee.Init();
            }
        }
    }
}

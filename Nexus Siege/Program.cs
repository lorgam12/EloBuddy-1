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

namespace Nexus_Siege
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static Menu Info;
        private static Spell.Skillshot Destruction;

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Info = MainMenu.AddMenu("Nexus Siege", "Nexus Siege");
            Info.AddLabel("Author: DownsecAkr");
            Info.AddSeparator(2);
            Info.Add("Active", new CheckBox("Auto cast Beam of destruction?"));

            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            CheckSlot();

            if (Info["Active"].Cast<CheckBox>().CurrentValue)
            {
                var Target = TargetSelector.GetTarget(Destruction.Range, DamageType.True);

                if (Player.HasBuff("SiegeLaserAffix"))
                {
                    if (Target != null)
                    {
                        if (Destruction.GetPrediction(Target).HitChance >= HitChance.High)
                        {
                            Destruction.Cast(Destruction.GetPrediction(Target).CastPosition);
                        }
                    }
                }
            }
        }

        private static void CheckSlot()
        {
            if (Player.GetSpell(SpellSlot.Summoner1).Name == "SiegeLaserAffix")
            {
                Destruction = new Spell.Skillshot(SpellSlot.Summoner1, 3100, SkillShotType.Linear, 240, 1200, 80);
            }else if (Player.GetSpell(SpellSlot.Summoner2).Name == "SiegeLaserAffix")
            {
                Destruction = new Spell.Skillshot(SpellSlot.Summoner2, 3100, SkillShotType.Linear, 240, 1200, 80);
            }
        }
    }
}
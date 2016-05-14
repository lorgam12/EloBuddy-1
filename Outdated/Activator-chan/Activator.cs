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

using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Version = System.Version;

namespace Activator_chan
{
    class Activator
    {
        //https://github.com/Phandaros/EloBuddy/blob/master/KaPoppy/Helper.cs

        public static void CheckForUpdates()
        {
            string RawVersion = new WebClient().DownloadString("https://raw.githubusercontent.com/DownsecAkr/EloBuddy/master/" + Assembly.GetExecutingAssembly().GetName().Name + "/Properties/AssemblyInfo.cs");
            var Try = new Regex(@"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]").Match(RawVersion);
            if (Try.Success)
            {
                if (new Version(string.Format("{0}.{1}.{2}.{3}", Try.Groups[1], Try.Groups[2], Try.Groups[3], Try.Groups[4])) > Assembly.GetExecutingAssembly().GetName().Version)
                {
                    Chat.Print("There is a newer version that " + Assembly.GetExecutingAssembly().GetName().Name + ", please update");
                }
            }
        }

        public static void Load()
        {
            CheckForUpdates();
            Items.Load();
            Spells.Load();
            ActivatorMenu.Load();
            Game.OnTick += Game_OnTick;
            Obj_AI_Base.OnProcessSpellCast += AIHeroClient_OnProcessSpellCast;
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
        }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            if (Items.HydraRavenous.IsReady() || Items.HydraRavenous.IsOwned())
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "HydraRavenous"))
                    return;

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                {
                    var Enemy = target as AIHeroClient;

                    if (!Enemy.IsEnemy)
                        return;

                    if(Enemy.HealthPercent <= ActivatorMenu.Slider(ActivatorMenu.Offensive, "HydraRavenous/01"))
                    {
                        Items.HydraRavenous.Cast();
                    }
                }
            }

            if (Items.HydraTitanic.IsReady() || Items.HydraTitanic.IsOwned())
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "HydraTitanic"))
                    return;

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                {
                    var Enemy = target as AIHeroClient;

                    if (!Enemy.IsEnemy)
                        return;

                    if (Enemy.HealthPercent <= ActivatorMenu.Slider(ActivatorMenu.Offensive, "HydraTitanic/01"))
                    {
                        Items.HydraTitanic.Cast();
                    }
                }
            }

            if (Items.Tiamat.IsReady() || Items.Tiamat.IsOwned())
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Offensive, "Tiamat"))
                    return;

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                {
                    var Enemy = target as AIHeroClient;

                    if (!Enemy.IsEnemy)
                        return;

                    if (Enemy.HealthPercent <= ActivatorMenu.Slider(ActivatorMenu.Offensive, "Tiamat/01"))
                    {
                        Items.Tiamat.Cast();
                    }
                }
            }
        }

        private static void AIHeroClient_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if (sender.IsAlly || !args.Target.IsMe || sender.IsMe)
                return;

            if (sender == null)
                return;

            var Enemy = (AIHeroClient)sender;

            foreach (var x in SpellsDatabase.CheckSpells)
            {
                if (Enemy.Hero == x.Champ)
                {
                    if (args.SData.Name == x.Name)
                    {
                        if (ActivatorMenu.CheckBox(ActivatorMenu.Summoner, "Barrier/01"))
                        {
                            Spells.UseBarrier();
                        }

                        if (ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Zhonya/" + x.Slot) && ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Seraph/" + x.Slot))
                        {
                            var Delay = (int)x.DelayX * 1000;

                            if (Items.Zhonya.IsReady())
                            {
                                Core.DelayAction(() => Items.Zhonya.Cast(), Delay);
                            }
                            else if (Items.Seraph.IsReady())
                            {
                                Core.DelayAction(() => Items.Seraph.Cast(), Delay);
                            }
                        }
                    }
                    else
                    {
                        if (args.Slot == x.Slot)
                        {
                            if (ActivatorMenu.CheckBox(ActivatorMenu.Defensive, "Zhonya/" + x.Slot))
                            {
                                var Delay = (int)x.DelayX * 1000;

                                if (Items.Zhonya.IsReady())
                                {
                                    Core.DelayAction(() => Items.Zhonya.Cast(), Delay);
                                }
                                else if (Items.Seraph.IsReady())
                                {
                                    Core.DelayAction(() => Items.Seraph.Cast(), Delay);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            try
            {
                Items.UseHealthPotion();
            }
            catch(Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error HealthPotion");
            }

            try
            {
                Items.UseBiscuit();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Biscuit");
            }

            try
            {
                Items.UseRefillablePotion();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Refillable Potion");
            }

            try
            {
                Items.UseHuntersPotion();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Hunters Potion");
            }

            try
            {
                Items.UseCorruptingPotion();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Corrupting Potion");
            }

            try
            {
                if (!Spells.CheckSmite())
                    return;

                Spells.UseSmite();
            }
            catch(Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error smite");
            }

            try
            {
                if (!Spells.CheckCleanse())
                    return;

                Spells.UseCleanse();
            }
            catch(Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error cleanse");
            }

            try
            {
                if (!Spells.CheckHeal())
                    return;

                Spells.UseHeal();
            }
            catch(Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Heal");
            }

            /*
            try
            {
                if (!Spells.CheckIgnite())
                    return;

                Spells.UseIgnite();
            }
            catch(Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Ignite");
            }
            */

            try
            {
                Items.UseYoumu();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Youmu");
            }

            try
            {
                Items.UseTiamat();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Tiamat");
            }

            try
            {
                Items.UseSolari();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Solari");
            }

            try
            {
                Items.UseQss2();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Qss2");
            }

            try
            {
                Items.UseQss();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Qss");
            }

            try
            {
                Items.UseMuramana();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Muramana");
            }

            try
            {
                Items.UseMikael();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Mikael");
            }

            try
            {
                Items.UseHydraTitanic();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Hydra Titanic");
            }

            try
            {
                Items.UseHydraRavenous();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Hydra Ravenous");
            }

            try
            {
                Items.UseFaceMountain();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Face Mountain");
            }

            try
            {
                Items.UseBladeKing();
            }
            catch (Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Blade King");
            }
        }
    }
}

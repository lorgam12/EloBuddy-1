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

namespace Activator_chan
{
    class Activator
    {
        public static void Load()
        {
            Items.Load();
            Spells.Load();
            ActivatorMenu.Load();
            Game.OnTick += Game_OnTick;
            Obj_AI_Base.OnProcessSpellCast += AIHeroClient_OnProcessSpellCast;
        }

        private static void AIHeroClient_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if (sender.IsAlly || !args.Target.IsMe || sender.IsMe)
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
                Spells.UseHeal();
            }
            catch(Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Heal");
            }

            try
            {
                Spells.UseIgnite();
            }
            catch(Exception)
            {
                if (!ActivatorMenu.CheckBox(ActivatorMenu.Principal, "ChatDebug"))
                    return;

                Chat.Print("Error Ignite");
            }

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

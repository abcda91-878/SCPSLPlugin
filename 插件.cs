using Exiled.API.Features;
using System.Collections.Generic;
using System;
using PlayerRoles;
using PlayerEvent = Exiled.Events.Handlers.Player;
using ServerEvent = Exiled.Events.Handlers.Server;
using Exiled.API.Features.Doors;
using Exiled.Events.EventArgs.Player;
using MEC;
using Exiled.Events.EventArgs.Server;

namespace test
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "滚木插件";
        public override string Author => "青枫社区服主";
        public override void OnEnabled()
        {
            Log.Info("加载成功！");
            base.OnEnabled();
        }
    }
    public class scp181
    {
        public static void 注册事件()
        {
            PlayerEvent.InteractingDoor += PlayerOpenDoor;
            PlayerEvent.InteractingLocker += PlayerOpenLoker;
            PlayerEvent.DroppingItem += PlayerDropItem;
            PlayerEvent.Hurting += PlayerHurt;
            PlayerEvent.Died += OnPlayerDied;
            ServerEvent.EndingRound += OnRoundEnd;
        }

        public static void 注销事件()
        {
            PlayerEvent.InteractingDoor -= PlayerOpenDoor;
            PlayerEvent.InteractingLocker -= PlayerOpenLoker;
            PlayerEvent.DroppingItem -= PlayerDropItem;
            PlayerEvent.Hurting -= PlayerHurt;
            PlayerEvent.Died -= OnPlayerDied;
            ServerEvent.EndingRound -= OnRoundEnd;
        }
        public static List<int> SCP181id = new List<int>();
        private static bool SCP181技能是否可用 = true;
        private static bool SkillIsActive = true;

        private static int SkillColldown = 70;
        private const float DamageReductionPercentage = 0.5f; // 减伤百分比
        private const float Skillcooldown = 30f; // 技能冷却时间（秒）
        private const float DamageReductionDuration = 10f; // 减伤持续时间（秒）



        public static void SpawnSCP181(Player player)
        {
            if (player.Role.Type != RoleTypeId.ClassD)
            {
                player.Role.Set(RoleTypeId.ClassD);
            }
            player.AddItem(ItemType.Coin);
            SCP181id.Add(player.Id);
            player.CustomInfo = "SCP181";
        }
        private static void PlayerOpenDoor(InteractingDoorEventArgs ev)
        {
            if (SCP181id.Contains(ev.Player.Id) && !ev.Door.IsLocked)
            {
                if (new Random().Next(0, 100) > 60)
                {
                    ev.Door.IsOpen = true;
                }
            }
        }
        private static void PlayerOpenLoker(InteractingLockerEventArgs ev)
        {
            if (SCP181id.Contains(ev.Player.Id))
            {
                if (new Random().Next(0, 100) > 80)
                {
                    ev.IsAllowed = true;
                    Timing.CallDelayed(10, () =>
                    {
                        SkillIsActive = false;
                    });
                }
            }
        }
        private static void PlayerDropItem(DroppingItemEventArgs ev)
        {
            if (!SCP181id.Contains(ev.Player.Id) || ev.Item.Type != ItemType.Coin)
                return;

            var item = ev.Item;
            if (item != null && item.Type == ItemType.Coin)
            {
                SCP181技能是否可用 = false;

                ev.IsAllowed = false;

                Timing.CallDelayed(10, () =>
                {
                    SkillIsActive = false;
                });
                Timing.CallDelayed(Skillcooldown, () =>
                {
                    SCP181技能是否可用 = true;
                });

                if (SCP181技能是否可用)
                {
                    SkillIsActive = true;
                }
                else
                {
                    return;
                }
            }
        }
        private static void PlayerHurt(HurtingEventArgs ev)
        {
            if (!SCP181id.Contains(ev.Player.Id))
                return;
            if (SkillIsActive == true)
            {
                ev.Amount *= (1 - 30 / 100f); // 减少伤害
            }
            if (SkillIsActive == false)
            {
                return;
            }

        }
        private static void OnRoundEnd(EndingRoundEventArgs _)
        {
            SkillIsActive = false;
            SCP181id.Clear();
            SCP181技能是否可用 = true;

        }
        private static void OnPlayerDied(DiedEventArgs ev)
        {
            if (SCP181id.Contains(ev.Player.Id))
            {
                SkillIsActive |= false;
                SCP181id.Clear();
                SCP181技能是否可用 = true;
            }

        }
    }
}
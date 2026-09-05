using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;
using Exiled.API.Features;
using PlayerRoles;

namespace test
{
    public static class SpawnSys
    {
        private static Player player1;

        public static IEnumerator<float> RenWuFenPei()
        {
            // 等待 1 秒再执行
            yield return Timing.WaitForSeconds(1f);

            // 获取当前所有玩家为列表
            List<Player> players = Player.List.ToList();
            List<Player> players1 = players;
            int d = 0;
            foreach (Player player in players1)
            {
                switch (player1.Role.Type)
                {
                    case RoleTypeId.ClassD:
                        d++;
                        switch (d)
                        {
                            case 1:
                                scp181.SpawnSCP181(player1);
                                break;
                        }
                        break;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    class KingUnitMove : IChessUnityMoving
    {
        static readonly int[] kingXaxisMove = { -1, 0 , 1 , -1 , 1 , -1 , 0 , 1};
        static readonly int[] kingYaxisMove = { -1 , -1 , -1 , 0 ,0 , 1 , 1 , 1};
        public List<Vector2Int> GetPossibleCordintaes(Vector2Int startPos,Vector2Int size, bool[,] grid)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            for (int i = 0; i < 8; i++)
            {
                Vector2Int newCords = new Vector2Int(startPos.x + kingXaxisMove[i], startPos.y + kingYaxisMove[i]);
                if (newCords.x >= 0 && newCords.x < size.x && newCords.y >= 0 && newCords.y < size.y)
                {
                    if (grid[newCords.x, newCords.y] == false)
                    {
                        result.Add(newCords);
                    }
                }
            }
            return result;
        }
    }
}

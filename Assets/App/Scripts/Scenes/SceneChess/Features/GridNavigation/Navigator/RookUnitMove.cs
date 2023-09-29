using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    class RookUnitMove : IChessUnityMoving
    {
        static readonly int[] rookgXaxisMove = { -1 , 1 , 0 , 0};
        static readonly int[] rookgYaxisMove = { 0 , 0 , -1 , 1};
        public List<Vector2Int> GetPossibleCordintaes(Vector2Int startPos,Vector2Int size, bool[,] grid)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            bool[] lockedDirection = new bool[8];
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (lockedDirection[j])
                        continue;
                    Vector2Int newCords = new Vector2Int(startPos.x + rookgXaxisMove[j] * i, startPos.y + rookgYaxisMove[j] * i);
                    if (newCords.x >= 0 && newCords.x < size.x && newCords.y >= 0 && newCords.y < size.y)
                    {
                        if (grid[newCords.x, newCords.y] == true)
                        {
                            lockedDirection[j] = true;
                            continue;
                        }
                        result.Add(newCords);
                    }
                }
            }
            return result;
        }
        
    }
}

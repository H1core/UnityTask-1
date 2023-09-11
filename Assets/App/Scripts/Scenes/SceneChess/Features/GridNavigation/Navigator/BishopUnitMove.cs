using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    class BishopUnitMove : IChessUnityMoving
    {
        private static int[] bishopXaxisMove = { 1,1,-1,-1};
        private static int[] bishopYaxisMove = { 1, -1, 1, -1 };
        public List<Vector2Int> GetPossibleCordintaes(Vector2Int startPos , bool[,] grid)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            bool[] lockedDirection = new bool[4];
            for (int i = 1; i < 8; i++) {
                for (int j = 0; j < 4; j++) {
                    if (lockedDirection[j])
                        continue;
                    Vector2Int newCords = new Vector2Int(startPos.x + bishopXaxisMove[j] * i, startPos.y + bishopYaxisMove[j] * i);
                    if (newCords.x >= 0 && newCords.x <= 7 && newCords.y >= 0 && newCords.y <= 7)
                    {
                        if(grid [newCords.x,newCords.y] == true)
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

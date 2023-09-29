using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    class HorseUnitMove : IChessUnityMoving
    {
        static readonly int[] horseXaxisMove = { -2 , -2 , -1 , -1 , 1, 1 , 2 ,2};
        static readonly int[] horseYaxisMove = { -1, 1, -2, 2, -2, 2, -1, 1 };
        public List<Vector2Int> GetPossibleCordintaes(Vector2Int startPos , Vector2Int size, bool[,] grid)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            for (int i = 0; i < 8; i++)
            {
                Vector2Int newCords = new Vector2Int(startPos.x + horseXaxisMove[i], startPos.y + horseYaxisMove[i]);
                if (newCords.x >= 0 && newCords.x < size.x && newCords.y >= 0 && newCords.y < size.y)
                {
                    if(grid[newCords.x,newCords.y] == false)
                    {
                        result.Add(newCords);
                    }
                }
            }
            return result;
        }
    }
}

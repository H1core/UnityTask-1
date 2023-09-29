using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    class PonUnitMove : IChessUnityMoving
    {
        public int color = -1;
        public List<Vector2Int> GetPossibleCordintaes(Vector2Int startPos,Vector2Int size, bool[,] grid)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            Vector2Int newCords = new Vector2Int(startPos.x , startPos.y + color);
            if (newCords.x >= 0 && newCords.x < size.x && newCords.y >= 0 && newCords.y < size.y)
            {
                if (grid[newCords.x, newCords.y] == false)
                {
                    result.Add(newCords);
                }
            }
            return result;
        }
    }
}

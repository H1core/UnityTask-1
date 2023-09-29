using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    public interface IChessUnityMoving
    {
        List<UnityEngine.Vector2Int> GetPossibleCordintaes(UnityEngine.Vector2Int startPos , UnityEngine.Vector2Int size, bool[,] grid);
    }
}

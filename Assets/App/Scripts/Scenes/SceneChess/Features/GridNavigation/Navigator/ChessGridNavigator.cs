using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    public class ChessGridNavigator : IChessGridNavigator
    {
        IChessUnityMoving chessUnityMoving;

        public List<Vector2Int> FindPath(ChessUnitType unit, Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            int[,] field = new int[8, 8];
            bool[,] Cells = new bool[8, 8];
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    field[i, j] = 2147483647;
                    Cells[i, j] = false;
                }
                
            }
            foreach(var piece in grid.Pieces)
            {
                Cells[piece.CellPosition.x, piece.CellPosition.y] = true;
            }
            Cells[from.x, from.y] = false;
            field[from.x, from.y] = 0;
            switch (unit)
            {
                case ChessUnitType.Knight: { chessUnityMoving = new HorseUnitMove(); break; }
                case ChessUnitType.Bishop: { chessUnityMoving = new BishopUnitMove(); break; }
                case ChessUnitType.Queen: { chessUnityMoving = new QueenUnitMove(); break; }
                case ChessUnitType.King: { chessUnityMoving = new KingUnitMove(); break; }
                case ChessUnitType.Rook: { chessUnityMoving = new RookUnitMove(); break; }
                case ChessUnitType.Pon: { chessUnityMoving = new PonUnitMove(); var t = chessUnityMoving as PonUnitMove; t.color = grid.Get(from).PieceModel.Color == ChessUnitColor.White ? -1 : 1; break; }
                default: { chessUnityMoving = new HorseUnitMove(); break; }
            }
            
            //Создания массива "очков" -> в каждой позиции массива нахоидтся число , являющимся кол-вом ходов до этой клетки
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            queue.Enqueue(from);
            while (queue.Count > 0)
            {
                var position = queue.Dequeue();
                foreach (var nextPosition in chessUnityMoving.GetPossibleCordintaes(position, Cells))
                {
                    if (field[nextPosition.x, nextPosition.y] > field[position.x, position.y] + 1)
                    {
                        field[nextPosition.x, nextPosition.y] = field[position.x, position.y] + 1;
                        queue.Enqueue(nextPosition);
                    }
                }
            }
            if(field[to.x, to.y] == 2147483647)
            {
                return null;
            }
            //Восстановление пути используя массив
            List<Vector2Int> result = new List<Vector2Int>();
            Vector2Int recoveryPos = to;

            if (chessUnityMoving as PonUnitMove != null) {
                (chessUnityMoving as PonUnitMove).ReverseColor();
            }

            while (field[recoveryPos.x, recoveryPos.y] > 0)
            {
                result.Add(recoveryPos);
                foreach(var previousPosition in chessUnityMoving.GetPossibleCordintaes(recoveryPos, Cells))
                {
                    if(field[recoveryPos.x, recoveryPos.y] == field[previousPosition.x,previousPosition.y] + 1)
                    {
                        recoveryPos = previousPosition;
                        break;
                    }
                }

            }
            result.Reverse();
            return result;
        }
    }
}
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
        
        private IChessUnityMoving getRequiredInterface(ChessUnitType unit)
        {
            switch (unit)
            {
                case ChessUnitType.Knight: { return new HorseUnitMove();}
                case ChessUnitType.Bishop: { return new BishopUnitMove();}
                case ChessUnitType.Queen: { return new QueenUnitMove(); }
                case ChessUnitType.King: { return new KingUnitMove();}
                case ChessUnitType.Rook: { return new RookUnitMove();}
                case ChessUnitType.Pon: { return new PonUnitMove();}
            }
            return null;
        }
        private void CalculatePath(ref List<Vector2Int>[,] field, ref bool[,] Cells, ref Vector2Int from, ref ChessGrid grid)
        {
            Cells[from.x, from.y] = true;
            field[from.x, from.y].Add(from);
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            queue.Enqueue(from);
            while (queue.Count > 0)
            {
                var position = queue.Dequeue();
                foreach (var nextPosition in chessUnityMoving.GetPossibleCordintaes(position, grid.Size, Cells))
                {
                    if (Cells[nextPosition.x, nextPosition.y] == false)
                    {
                        Cells[nextPosition.x, nextPosition.y] = true;
                        foreach (var pos in field[position.x, position.y])
                            field[nextPosition.x, nextPosition.y].Add(pos);
                        field[nextPosition.x, nextPosition.y].Add(nextPosition);
                        queue.Enqueue(nextPosition);
                    }
                }
            }
        }
        private void initArrays(ref List<Vector2Int>[,] field, ref bool[,] Cells , ref ChessGrid grid)
        {
            for (int i = 0; i < grid.Size.x; i++)
            {
                for (int j = 0; j < grid.Size.y; j++)
                {
                    field[i, j] = new List<Vector2Int>();
                    Cells[i, j] = false;
                }
            }
            foreach (var piece in grid.Pieces)
                Cells[piece.CellPosition.x, piece.CellPosition.y] = true;
        }
        public List<Vector2Int> FindPath(ChessUnitType unit, Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            List<Vector2Int>[,] field = new List<Vector2Int>[grid.Size.x, grid.Size.y];
            bool[,] Cells = new bool[grid.Size.x, grid.Size.y];

            initArrays(ref field, ref Cells, ref grid);

            chessUnityMoving = getRequiredInterface(unit);

            CalculatePath(ref field, ref Cells, ref from, ref grid);

            if (field[to.x, to.y].Count == 0)
                return null;
            else
                return field[to.x, to.y];
        }
    }
}
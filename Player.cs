using System;
using System.Collections.Generic;
using static System.Console;

namespace GameDesign
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string id) : base(id) { }

        public override List<Move> UndoMove(Board board)
        {
            if (playerMoveList.Count > 0)
            {
                int index = playerMoveList.Count - 1;
                Move lastMove = playerMoveList[index];
                redoMovelist.Add(lastMove);
                playerMoveList.RemoveAt(index);
                board.table[lastMove.row][lastMove.col] = null; // 用 null 表示空位
                WriteLine("Undo move successfully!");
                return redoMovelist;
            }
            WriteLine("Undo move unsuccessfully!");
            return new List<Move>();
        }

        public override string ToString() => "Human player";
    }

    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string id) : base(id) { }

        public override List<Move> UndoMove(Board board)
        {
            if (playerMoveList.Count > 0)
            {
                int index = playerMoveList.Count - 1;
                Move lastMove = playerMoveList[index];
                redoMovelist.Add(lastMove);
                playerMoveList.RemoveAt(index);
                board.table[lastMove.row][lastMove.col] = null; // 用 null 表示空位
                WriteLine("Undo move successfully!");
                return redoMovelist;
            }
            WriteLine("Undo move unsuccessfully!");
            return new List<Move>();
        }

        public override string ToString() => "Computer player";
    }

    public class Player
    {
        public string ID { get; set; }
        public Move move { get; set; } = new Move();
        public int SOSCount { get; set; } = 0;

        public List<Player> playerList { get; set; } = new List<Player>();
        public List<string> symbolTrack { get; set; } = new List<string>();
        public List<Move> playerMoveList { get; set; } = new List<Move>();
        public List<Move> redoMovelist { get; set; } = new List<Move>();

        public Player(string id)
        {
            ID = id;
        }

        public virtual Move GetMove()
        {
            return playerMoveList.Count > 0 ? playerMoveList[^1] : null;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Player other) return false;
            return this.ID == other.ID;
        }

        public override int GetHashCode() => ID.GetHashCode();

        public virtual bool validMoveCheck() => true;

        public virtual List<Move> UndoMove(Board board) => new List<Move>();

        public virtual void RedoMove(Board board, Game GAME, string symbol)
        {
            if (redoMovelist.Count > 0)
            {
                int index = redoMovelist.Count - 1;
                Move redoMove = redoMovelist[index];

                if (board != null &&
                    redoMove.row >= 0 && redoMove.row < board.table.Length &&
                    redoMove.col >= 0 && redoMove.col < board.table[0].Length)
                {
                    board.table[redoMove.row][redoMove.col] = symbol;
                    WriteLine("Redo your move successfully!");
                    redoMovelist.RemoveAt(index);
                    playerMoveList.Add(redoMove);
                }
                else
                {
                    WriteLine("Invalid redo move!");
                }
            }
            else
            {
                WriteLine("Redo your move unsuccessfully!");
            }
        }
    }
}

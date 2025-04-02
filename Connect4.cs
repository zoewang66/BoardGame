using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace GameDesign
{
    public class Connect4Game : Game
    {
        private string symbol1;
        private string symbol2;
        private List<string> symbolList;

        private Player inactivePlayer;

        private bool isUndo = false;
        private bool isRedo = false;

        private string input = "";

        // 为了演示，给电脑落子用的随机数生成器
        private Random random = new Random();

        public Connect4Game(Player p1, Player p2) : base(p1, p2)
        {
            symbolList = new List<string>() { "O", "X", " " };
            symbol1 = symbolList[0];
            symbol2 = symbolList[1];

            helpMessage = "Welcome to Connect Four Game!\nWe are here to help you to know the rules!\n"
                          + "Here is the rule:\nEach player forms an unbroken chain of four pieces horizontally, vertically, or diagonally.\n"
                          + "Who forms the unbroken chain first, who wins the game.\n";

            WriteLine(" ");
            getStart();
            activePlayer = player1;
            inactivePlayer = player2;
            WriteLine(" ");
            Run();
        }

        public override void getStart()
        {
            WriteLine(helpMessage);

            WriteLine("1. Start a new game");
            WriteLine("2. Load game");
            string choice = ReadLine();

            if (choice == "2")
            {
                LoadGameProgress("gameprogress.json", this);
            }
            else
            {
                board = new Board(6, 7);
                modelChoose();
            }
        }

        public override void Run()
        {
            int count = 0;
            while (!board.IsTableFilled())
            {
                if (count < 2)
                {
                    PlayertTypeMove();
                    ++count;
                }
                else
                {
                    if (activePlayer is HumanPlayer)
                    {
                        // 允许连续进行撤销/重做操作，直到用户选择新落子
                        while (chooseRedoUndo())
                        {
                            // 持续处理撤销/重做
                        }
                        ++count;
                    }
                    else if (activePlayer is ComputerPlayer)
                    {
                        PlayertTypeMove();
                        ++count;
                    }
                }

                // 检查胜利条件
                if (count > 6)
                {
                    if (checkWin(symbol1))
                    {
                        WriteLine("{0} wins!", player1);
                        break;
                    }
                    else if (checkWin(symbol2))
                    {
                        WriteLine("{0} wins!", player2);
                        break;
                    }
                }

                switchPlayer();
            }
        }

        // 根据玩家类型决定如何落子
        public void PlayertTypeMove()
        {
            if (player1 is HumanPlayer && player1.Equals(activePlayer))
            {
                WriteLine("Player1 (Human): ");
                MakeMove(symbol1);
                activePlayer.playerMoveList = player1.playerMoveList;
            }
            else if (player2 is HumanPlayer && player2.Equals(activePlayer))
            {
                WriteLine("Player2 (Human): ");
                MakeMove(symbol2);
                activePlayer.playerMoveList = player2.playerMoveList;
            }
            else if (player2 is ComputerPlayer && player2.Equals(activePlayer))
            {
                WriteLine("Player2 (Computer) is thinking...");
                // 调用自动落子方法
                ComputerMakeMove(symbol2);
                activePlayer.playerMoveList = player2.playerMoveList;
            }
        }

        // 人类玩家落子逻辑（读控制台输入）
        public override List<Move> MakeMove(string symbol)
        {
            int col = 0;
            int row = board.Row;
            Write("Please enter the column (1 - 7), or Q to save and quit >> ");
            input = ReadLine();

            if (input == "Q")
            {
                SaveAndExit();
            }
            else if (int.TryParse(input, out col) && col >= 1 && col <= 7)
            {
                // 找到该列最底部的空位
                while (row > 0 && board.table[row][col] != null)
                {
                    row--;
                }

                if (row == 0)
                {
                    WriteLine("That column is full, please choose another one!");
                    return MakeMove(symbol);
                }

                // 在 (row, col) 落子
                activePlayer.playerMoveList.Add(new Move(row, col));
                board.table[row][col] = symbol;
                getSymbol();
                board.GenerateTable();
            }
            else
            {
                WriteLine("Invalid entry, please re-enter your move!");
                return MakeMove(symbol);
            }

            return activePlayer.playerMoveList;
        }

        // 电脑自动落子逻辑（随机列）
        private void ComputerMakeMove(string symbol)
        {
            while (true)
            {
                // 随机从 1 到 board.Col 之间选列
                int col = random.Next(1, board.Col + 1);
                int row = board.Row;

                // 找到该列最底部的空位
                while (row > 0 && board.table[row][col] != null)
                {
                    row--;
                }

                // 如果 row == 0，说明该列满了，换列重试
                if (row == 0)
                {
                    continue;
                }

                // 否则在 (row, col) 落子
                activePlayer.playerMoveList.Add(new Move(row, col));
                board.table[row][col] = symbol;
                getSymbol();
                board.GenerateTable();
                break; // 跳出循环，结束电脑本次落子
            }
        }

        public override string getSymbol()
        {
            string chosen = symbol1;
            if (player2.Equals(activePlayer)) chosen = symbol2;
            activePlayer.symbolTrack.Add(chosen);
            return chosen;
        }

        // 撤销/重做菜单
        public bool chooseRedoUndo()
        {
            Write("Undo (U), Redo (R), New Move (N), Quit (Q): ");
            string input = ReadLine().ToUpper();

            if (input == "N")
            {
                // 直接让玩家进行新落子
                PlayertTypeMove();
                return false; // 跳出 while
            }
            else if (input == "Q")
            {
                SaveAndExit();
                return false; // 不会执行到这里
            }
            else if (input == "U")
            {
                activePlayer.UndoMove(board);
                board.GenerateTable();
                return true; // 继续停留在撤销/重做菜单
            }
            else if (input == "R")
            {
                string symbol = activePlayer.symbolTrack.LastOrDefault() ?? "X";
                activePlayer.RedoMove(board, this, symbol);
                board.GenerateTable();
                return true; // 继续停留在撤销/重做菜单
            }
            else
            {
                WriteLine("Invalid input, try again.");
                return chooseRedoUndo();
            }
        }

        public override Player switchPlayer()
        {
            if (activePlayer.ID == player1.ID)
            {
                activePlayer = player2;
                inactivePlayer = player1;
            }
            else
            {
                activePlayer = player1;
                inactivePlayer = player2;
            }
            return activePlayer;
        }

        // 判断胜利
        public bool checkWin(string symbol)
        {
            return checkHorizontalWin(symbol) || CheckVerticalWin(symbol) || CheckDiagonalWin(symbol);
        }

        private bool checkHorizontalWin(string symbol)
        {
            for (int i = 1; i < board.table.Length; ++i)
            {
                for (int j = 1; j <= board.table[i].Length - 4; ++j)
                {
                    if (Enumerable.Range(0, 4).All(offset => board.table[i][j + offset] == symbol))
                        return true;
                }
            }
            return false;
        }

        private bool CheckVerticalWin(string symbol)
        {
            for (int i = 1; i <= board.table.Length - 4; ++i)
            {
                for (int j = 1; j < board.table[i].Length; ++j)
                {
                    if (Enumerable.Range(0, 4).All(offset => board.table[i + offset][j] == symbol))
                        return true;
                }
            }
            return false;
        }

        private bool CheckDiagonalWin(string symbol)
        {
            for (int i = 1; i <= board.table.Length - 4; ++i)
            {
                for (int j = 1; j <= board.table[i].Length - 4; ++j)
                {
                    if (Enumerable.Range(0, 4).All(offset => board.table[i + offset][j + offset] == symbol) ||
                        Enumerable.Range(0, 4).All(offset => board.table[i + 3 - offset][j + offset] == symbol))
                        return true;
                }
            }
            return false;
        }
    }
}

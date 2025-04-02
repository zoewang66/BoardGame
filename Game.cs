using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using static System.Console;

namespace GameDesign
{
    public abstract class Game
    {
        public string helpMessage { get; set; } = string.Empty;
        public string[][] table { get; set; } = Array.Empty<string[]>();
        public Player player1 { get; set; } = new Player("1");
        public Player player2 { get; set; } = new Player("2");
        public Player activePlayer { get; set; } = new Player("1");
        public Board board { get; set; } = new Board(6, 7);
        public Piece piece { get; set; } = new Piece();
        public Move move { get; set; } = new Move();
        public static string model = "1";
        public List<Player> playerList { get; set; } = new List<Player>();

        private Game game;
        public Game GAME
        {
            get => game;
            set => game = value;
        }

        public Game(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;
            playerList = new List<Player> { p1, p2 };
        }

        public virtual void getStart() { }
        public virtual void Run() { }

        public virtual string getSymbol()
        {
            if (activePlayer.symbolTrack.Count > 0)
                return activePlayer.symbolTrack.Last();
            return "X";
        }

        public virtual Player switchPlayer() => null;

        public void SaveAndExit()
        {
            SaveGameProgress("gameprogress.json");
            Environment.Exit(0);
        }

        public void LoadGameProgress(string fileName, Game gameInstance)
        {
            if (!File.Exists(fileName)) return;

            string json = File.ReadAllText(fileName);
            GameProgress? gameProgress = JsonSerializer.Deserialize<GameProgress>(json);

            if (gameProgress?.Table != null)
            {
                board.table = gameProgress.Table;
                board.GenerateTable();

                if (!string.IsNullOrEmpty(gameProgress.SelectedModel))
                {
                    model = gameProgress.SelectedModel == "Human VS Human" ? "1" : "2";

                    if (model == "1")
                    {
                        gameInstance.player1 = new HumanPlayer("1");
                        gameInstance.player2 = new HumanPlayer("2");
                    }
                    else
                    {
                        gameInstance.player1 = new HumanPlayer("1");
                        gameInstance.player2 = new ComputerPlayer("2");
                    }
                    gameInstance.playerList = new List<Player> { gameInstance.player1, gameInstance.player2 };
                }
            }
            else
            {
                WriteLine("Error: Loaded game progress is invalid or incomplete.\n");
            }
        }

        public void SaveGameProgress(string fileName)
        {
            var gameProgress = new GameProgress
            {
                Table = board.table,
                SelectedModel = model == "1" ? "Human VS Human" : "Human VS Computer"
            };

            string json = JsonSerializer.Serialize(gameProgress);
            File.WriteAllText(fileName, json);
        }

        public List<Player> modelChoose()
        {
            const string MODEL1 = "Human VS Human";
            const string MODEL2 = "Human VS Computer";

            Write("If you want to play with Human, enter 1; with Computer, enter 2 >> ");
            model = ReadLine()?.Trim() ?? "1";

            while (model != "1" && model != "2")
            {
                WriteLine("Invalid input. Please enter 1 or 2.");
                model = ReadLine()?.Trim() ?? "1";
            }

            if (model == "1")
            {
                player1 = new HumanPlayer("1");
                player2 = new HumanPlayer("2");
                WriteLine("You are in the Model of {0}!", MODEL1);
            }
            else
            {
                player1 = new HumanPlayer("1");
                player2 = new ComputerPlayer("2");
                WriteLine("You are in the Model of {0}!", MODEL2);
            }

            playerList = new List<Player> { player1, player2 };
            WriteLine("Player list: {0}, {1}\n", player1, player2);
            return playerList;
        }

        public virtual List<Move> MakeMove(string symbol) => activePlayer.playerMoveList;
        public virtual List<Move> MakeMove() => activePlayer.playerMoveList;
        public virtual List<Move> SysMakeMove() => activePlayer.playerMoveList;
    }

    public class GameProgress
    {
        public string[][] Table { get; set; } = Array.Empty<string[]>();
        public string SelectedModel { get; set; } = "";
    }
}

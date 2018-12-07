using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    static class Program
    {
        static Player[] players;
        static Player currentTurnPlayer;
        static void Main ( string[] args )
        {
            bool keepPlaying = true;
            string userInput = "";
            players = new Player[2];
            
            while (keepPlaying)
            {
                Console.Clear();
                Console.Write(PrintMenu());
                userInput = Console.ReadLine().ToLower();

                switch (userInput)
                {
                    case "n":
                    case "1":
                        NewGame();
                        break;
                    case "h":
                    case "2":
                        DisplayHelp();
                        break;
                    case "e":
                    case "q":
                    case "3":
                        keepPlaying = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input. Please provide an option from the menu.\n");
                        Console.Write("press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static string PrintMenu()
        {
            StringBuilder menu = new StringBuilder();

            menu.Append(gameTitle);
            menu.Append("+ Main Menu +\n");
            menu.Append("1. [N]ew Game\n");
            menu.Append("2. [H]ow to Play\n");
            menu.Append("3. [E]xit\n");
            menu.Append("-> ");

            return menu.ToString();
        }

        static private void NewGame()
        {
            Game game = new Game();
            players[0] = new Player();
            players[1] = new Player();
            currentTurnPlayer = players[1];

            SetUpPlayers(players[0], players[1]);

            while (!game.TestForWinner())
            {
                // We swap turns before taking it so that if there is a winner,
                // it's the current turn player.
                SwapTurns(currentTurnPlayer);
                TakeTurn(currentTurnPlayer, game);
            }

            StringBuilder victory = new StringBuilder();
            victory.Append(gameTitle);
            victory.Append($"CONGRATULATIONS {currentTurnPlayer.Name.ToUpper()}!\n");
            victory.Append("You won the game!\n\n");
            victory.Append("press any key to continue...");

            Console.Clear();
            Console.Write(victory.ToString());
            Console.ReadKey();
        }

        private static void DrawGame(Game game, Player currentTurnPlayer)
        {
            StringBuilder gameState = new StringBuilder();

            gameState.Append(gameTitle);
            gameState.Append($"{currentTurnPlayer.Name}'s turn.\n");
            gameState.Append(game.GetBoardState());

            Console.Clear();
            Console.Write(gameState);
        }

        private static void TakeTurn(Player player, Game game)
        {
            bool piecePlaced = false;

            while ( !piecePlaced )
            {
                DrawGame( game, player );
                Console.WriteLine( "Please enter the number of the column you wish to place a piece in: " );
                Console.Write( "-> " );

                if ( int.TryParse( Console.ReadKey().KeyChar.ToString(), out int column ) )
                    piecePlaced = game.DropPiece( player.Symbol, column - 1 );
            }

        }

        private static void SwapTurns(Player player)
        {
            if (player == players[0])
                currentTurnPlayer = players[1];
            else
                currentTurnPlayer = players[0];
        }

        private static void SetUpPlayers(Player player1, Player player2)
        {
            StringBuilder message = new StringBuilder();

            message.Append(gameTitle);
            message.Append("+ PLAYER 1 SETUP +\n");
            message.Append("What is Player 1's name? \n");
            message.Append("-> ");

            Console.Clear();
            Console.Write(message);

            player1.Name = Console.ReadLine();
            player1.Symbol = 'X';

            message.Clear();
            message.Append(gameTitle);
            message.Append("+ PLAYER 2 SETUP +\n");
            message.Append("What is Player 2's name? \n");
            message.Append("-> ");

            Console.Clear();
            Console.Write(message);

            player2.Name = Console.ReadLine();
            player2.Symbol = '+';
        }

        static private void DisplayHelp()
        {
            StringBuilder message = new StringBuilder();

            message.Append("Columns are assigned numbers 1 - 7.\n");
            message.Append("Players take turns selecting a column to play a piece into.\n");
            message.Append("Simply enter the number of the desired column.\n\n");
            message.Append("Pieces are always played in the next available slot in a column,\n");
            message.Append("starting from the bottom of the play area.\n\n");
            message.Append("The game is over when one player gets four of their symbols in a row.\n");
            message.Append("The symbols can be matched vertically, horizontally or diagonally in either direction.\n\n");
            message.Append("press any key to continue...");

            Console.Clear();
            Console.Write(message.ToString());
            Console.ReadKey();
        }

        private const string gameTitle = "---=== Connect Four ===---\n\n";
    }
}

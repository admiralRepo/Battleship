using System;
using System.Text;

namespace BattleShip {

    public struct Coordinate {
        public Coordinate (int row, int col) {
            this.Row = row;
            this.Col = col;
        }
        public int Row { get; set; }
        public int Col { get; set; }
    }
    public class Game {
        public int CounterPlayer1Hit { get; set; }
        public int CounterPlayer2Hit { get; set; }
        public string[, ] Board1 { get; set; }
        public string[, ] Board2 { get; set; }
        Game () {
            this.Board1 = new string[boardSize, boardSize].SetBoardToEmpty ();
            this.Board2 = new string[boardSize, boardSize].SetBoardToEmpty ();
            this.CounterPlayer1Hit = 0;
            this.CounterPlayer2Hit = 0;
        }
        const int boardSize = 8;
        public int WinCount {
            get {
                int sum = 0;
                for (int x = shipCount; x > 0; x--)
                    sum += x;
                return sum;
            }
        }
        const int shotCount = 5;
        const int shipCount = 3;
        int? winner = null;
        public static void Main (String[] args) {
            Game game = new Game ();
            Console.WriteLine ("Hello Player 1");
            game.Board1 = game.PlaceShips (game.Board1);
            Console.WriteLine ("Now its turn of player 2. Hit enter when ready");
            Console.ReadLine ();
            Console.WriteLine ("\n\n\nHello Player 2");
            game.Board2 = game.PlaceShips (game.Board2);
            while (game.CounterPlayer1Hit < game.WinCount && game.CounterPlayer2Hit < game.WinCount) {
                game.GetUserGuesses (shotCount, game.Board2, true);
                if (game.CounterPlayer1Hit >= game.WinCount) break;
                game.GetUserGuesses (shotCount, game.Board1, false);
            }
            int winner = game.CounterPlayer1Hit == game.WinCount ? 1 : 2;
            Console.WriteLine ("Player" + winner + " won!");
        }

        /// Gets one coordinate from User Input
        public Coordinate GetCoord () {
            int colIndex = -1;
            int rowIndex = -1;
            while (colIndex > boardSize - 1 || colIndex < 0 || rowIndex > boardSize - 1 || rowIndex < 0) {
                Console.WriteLine ("Enter coordinate in the format of A1 : ");
                string coord = Console.ReadLine ();
                colIndex = coord[0].ToString ().ToUpper ().GetColumnFromLetter ();
                rowIndex = Convert.ToInt32 (coord[1].ToString ()) - 1;
            }
            return new Coordinate (rowIndex, colIndex);
        }

        /// Places one user ship by User input to given board assuming user will enter begin/end coordinates in a straight line without going over the ship length
        public string[, ] PlaceOneShip (String[, ] board, int size) {
            Console.WriteLine ("Place " + size + " size ship, Enter begin coordinate");
            Coordinate coordStart = GetCoord ();
            //Coordinate coordStart = new Coordinate (3, 3);
            board[coordStart.Row, coordStart.Col] = "P";
            // 2 size ship 
            if (size > 1) {
                Console.WriteLine ("Place " + size + " size ship, Enter end coordinate");
                var coordEnd = GetCoord ();
                //var coordEnd = new Coordinate (3, 6);
                int smallCol = Math.Min (coordStart.Col, coordEnd.Col);
                int bigCol = Math.Max (coordStart.Col, coordEnd.Col);
                int smallRow = Math.Min (coordStart.Row, coordEnd.Row);
                int bigRow = Math.Max (coordStart.Row, coordEnd.Row);
                //                if ((bigCol - smallCol + 1 != size) || (bigRow - smallRow + 1 != size)) {
                //                  Console.WriteLine(bigCol.ToString()+","+smallCol.ToString()+","+bigRow.ToString()+","+smallRow.ToString());
                //                Console.WriteLine ("Invalid coordinates");
                //              Console.WriteLine ("size" + size.ToString ());
                //        } else
                board[coordEnd.Row, coordEnd.Col] = "P";
                // 3+ size ships
                if (size > 2) {
                    if (coordEnd.Row == coordStart.Row) {
                        for (int x = smallCol; x < bigCol; x++) {
                           // Console.WriteLine ("Coordinates:" + coordEnd.Row.ToString () + "" + x.ToString ());
                            board[coordEnd.Row, x] = "P";
                        }
                    } else if (coordEnd.Col == coordStart.Col) {
                        for (int y = smallRow; y < bigRow; y++) {
                           // Console.WriteLine ("Coordinates:" + "" + y.ToString () + coordEnd.Col.ToString ());
                            board[y, coordEnd.Col] = "P";
                        }
                    } else {
                        Console.WriteLine ("Invalid coordinates");
                    }
                }
            }
            Console.WriteLine("You placed your ships as :");
            board.PrintBoard ();
            return board;
        }

        /// Places user ships by User input to given board assuming user will enter begin/end coordinates correctly
        public String[, ] PlaceShips (string[, ] board) {
            for (int x = 1; x <= shipCount; x++)
                board = PlaceOneShip (board, x);
            return board;
        }

        // GEt user guesses and return new board + win status. Assumes user doesnt enter same coordinates again
        public void GetUserGuesses (int shots, String[, ] board, bool isPlayer1Guessing) {

            if (isPlayer1Guessing)
                Console.WriteLine ("------------------\n\n\nYour turn Player 1");
            else Console.WriteLine ("------------------\n\n\nYour turn Player 2");

            Console.WriteLine ("Enemy board last  :");
            board.PrintBoard (false);

            Console.WriteLine ("");
            for (int x = 0; x < shots && CounterPlayer1Hit < WinCount && CounterPlayer2Hit < WinCount; x++) {
                Console.WriteLine ("Shot " + (x + 1));
                var guessCoord = GetCoord ();
                int rowIndex = guessCoord.Row;
                int colIndex = guessCoord.Col;
                if (board[rowIndex, colIndex] == "P") {
                    Console.WriteLine ("You hit the enemy ship!");
                    board[rowIndex, colIndex] = "X";
                    if (isPlayer1Guessing)
                        CounterPlayer1Hit++;
                    else CounterPlayer2Hit++;
                } else {
                    Console.WriteLine ("You missed!");
                    board[rowIndex, colIndex] = "0";
                }
                board.PrintBoard (false);
            }
        }
    }
}
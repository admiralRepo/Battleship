using System;
using System.Text;

namespace BattleShip {
    public static class BoardUtils {

        /// Sets all cells to empty of given baord
        public static string[, ] SetBoardToEmpty (this string[, ] board) {
            for (int x = 0; x < board.GetLength (0); x++) {
                for (int y = 0; y < board.GetLength (0); y++) {
                    board[x, y] = ".";
                }
            }
            return board;
        }

        /// prints given board 
        public static void PrintBoard (this string[, ] board, bool forSelf = true) {
            Console.WriteLine (" ABCDEFGH");
            for (int r = 0; r < board.GetLength (0); r++) {
                var row = new StringBuilder ();
                row.Append ((r + 1) + "");
                for (int c = 0; c < board.GetLength (0); c++) {
                    if (forSelf) row.Append (board[r, c]);
                    else row.Append (board[r, c] == "X" || board[r, c] == "0" ? board[r, c] : "?");
                }
                Console.WriteLine (row.ToString ());
            }
        }

        /// Covert column char to index
        public static int GetColumnFromLetter (this String letter) {
            int col = -1;
            switch (letter) {
                case "A":
                    col = 0;
                    break;
                case "B":
                    col = 1;
                    break;
                case "C":
                    col = 2;
                    break;
                case "D":
                    col = 3;
                    break;
                case "E":
                    col = 4;
                    break;
                case "F":
                    col = 5;
                    break;
                case "G":
                    col = 6;
                    break;
                case "H":
                    col = 7;
                    break;
                default:
                    break;
            }
            return col;
        }

    }
}
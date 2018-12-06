using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class Game
    {
        private char[][] board;
        private int[] nextFreePerColumn;

        public Game()
        {
            board = new char[7][];
            nextFreePerColumn = new int[7];

            for (int i = 0; i < 7; i++)
                board[i] = new char[6];

            ResetBoard();
        }

        /// <summary>
        /// Resets all slots in the board and the next free slot value for each column.
        /// </summary>
        public void ResetBoard()
        {
            for (int i = 0; i < 7; i++)
            {
                // reset all slots in a column to the default
                for (int j = 0; j < 6; j++)
                    board[i][j] = '.';

                // reset the next free slot for the current column
                nextFreePerColumn[i] = 0;
            }
        }

        /// <summary>
        /// Adds a new piece to the next free slot in the specified column.
        /// </summary>
        /// <param name="piece">The character for the current turn player</param>
        /// <param name="column">Which column you wish to drop a piece into</param>
        /// <returns>Whether a piece was successfully dropped.</returns>
        public bool DropPiece(char piece, int column)
        {
            if (nextFreePerColumn[column] >= 5)
                return false;

            board[column][nextFreePerColumn[column]] = piece;
            nextFreePerColumn[column]++;
            return true;
        }

        /// <summary>
        /// Converts the board to a form that can be easily drawn to screen.
        /// </summary>
        /// <returns>A string representation of the current board.</returns>
        public string GetBoardState()
        {
            StringBuilder boardState = new StringBuilder();

            boardState.Append( " 1 2 3 4 5 6 7 \n" );
            boardState.Append(" _ _ _ _ _ _ _ \n");
            boardState.Append($"|{board[0][5]}|{board[1][5]}|{board[2][5]}|{board[3][5]}|{board[4][5]}|{board[5][5]}|{board[6][5]}|\n");
            boardState.Append($"|{board[0][4]}|{board[1][4]}|{board[2][4]}|{board[3][4]}|{board[4][4]}|{board[5][4]}|{board[6][4]}|\n");
            boardState.Append($"|{board[0][3]}|{board[1][3]}|{board[2][3]}|{board[3][3]}|{board[4][3]}|{board[5][3]}|{board[6][3]}|\n");
            boardState.Append($"|{board[0][2]}|{board[1][2]}|{board[2][2]}|{board[3][2]}|{board[4][2]}|{board[5][2]}|{board[6][2]}|\n");
            boardState.Append($"|{board[0][1]}|{board[1][1]}|{board[2][1]}|{board[3][1]}|{board[4][1]}|{board[5][1]}|{board[6][1]}|\n");
            boardState.Append($"|{board[0][0]}|{board[1][0]}|{board[2][0]}|{board[3][0]}|{board[4][0]}|{board[5][0]}|{board[6][0]}|\n");
            boardState.Append(" - - - - - - - \n");

            return boardState.ToString();
        }

        /// <summary>
        /// Verifies if there is a winner on the current board.
        /// </summary>
        /// <returns>Returns true if there is a winner.</returns>
        public bool TestForWinner()
        {
            // Since we have to have 4 in a row, 
            // we only have to run tests for all directions
            // on the first 3 rows.
            // For the last 3 row, we only test horizontally.
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i][j] != '.') //don't test "empty" fields
                        if (CheckAll(i, j))
                            return true;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    if (board[i][j] != '.') //don't test "empty" fields
                        if (CheckHorizontal(i, j))
                            return true;
                }
            }

            return false;
        }

        private bool CheckAll(int column, int slot)
        {
            if (CheckVertical(column, slot))
                return true;

            if (column < 4) //Past here a win is impossible
                if (CheckHorizontal(column, slot))
                    return true;

            if (column < 4) //Past here a win is impossible
            {
                if (CheckDiagonal(column, slot, true))
                    return true;
            }

            if (column > 2) //Past here a win is impossible
            {
                if (CheckDiagonal(column, slot, false))
                    return true;
            }

            return false;
        }

        private bool CheckHorizontal(int column, int slot)
        {
            char testChar = board[column][slot];

            // We exit with false as soon as we find a character that doesn't match.
            for (int i = 1; i < 4; i++)
                if (testChar != board[column + i][slot])
                    return false;

            // if all 4 characters match, we return true.
            return true;
        }

        private bool CheckVertical(int column, int slot)
        {
            char testChar = board[column][slot];

            for (int i = 1; i < 4; i++)
                if (testChar != board[column][slot + i])
                    return false;

            return true;
        }

        private bool CheckDiagonal(int column, int slot, bool toTheRight)
        {
            char testChar = board[column][slot];

            if (toTheRight)
            {
                for (int i = 1; i < 4; i++)
                    if (testChar != board[column + i][slot + i])
                        return false;

                return true;
            }
            else
            {
                for (int i = 1; i < 4; i++)
                    if (testChar != board[column - i][slot + i])
                        return false;

                return true;
            }
        }
    }
}

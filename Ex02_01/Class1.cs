using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;

namespace Ex02_01
{
    class fourInRowFront
    {
        private fourInRowBackend gameBackend;

        public fourInRowFront(int i_numRows, int i_numColumns)
        {
            gameBackend = new fourInRowBackend(i_numRows, i_numColumns);
        }

        private void PrintGameBoard()
        {
            Console.WriteLine("FOUR IN A ROW - (ARBA BE SHORA)");
            Console.WriteLine("  ------------------------\n");
            Console.WriteLine(gameBackend.CreateStringFromBoard());
        }

        static void runFourInRowConsoleAI(int i_numberOfRows, int numberOfColumns) { }
        static void runFourInRowConsole2Users(int i_numberOfRows, int i_numberOfColumns)
        {
            fourInRowFront game = new fourInRowFront(i_numberOfRows,i_numberOfColumns);
            string x;
            bool foundWinner = false;
            do
            {
                Screen.Clear();
                game.PrintGameBoard();
                Console.WriteLine("Enter the  column you and to insert your ");
                x = Console.ReadLine();// validation 
                game.gameBackend.turnManager(int.Parse(x) - 1);//fix it to check if posible 

                if (game.gameBackend.checkIfWin(int.Parse(x) - 1))
                {
                    foundWinner = true;
                    Console.Clear();
                    game.PrintGameBoard();
                    Console.WriteLine("\nwe have winner! ");
                }
                else if (game.gameBackend.isEvenGame())
                {
                    foundWinner = true;
                    Console.Clear();
                    game.PrintGameBoard(); // dup code fixx itt
                    Console.WriteLine("\nwe have tie! \n");
                }
                game.gameBackend.changePlayerTurn();

            } while (!foundWinner);
        }

        static int[] GetValidBoardSizeFromUser()
        {
            int[] boardSize = new int[2]; 
            bool isValidInput = false;

            do
            {
                Console.WriteLine("Hello, please enter the size of the board (4x4 to 8x8):");
                Console.WriteLine("Number of rows:");
                string numberOfRowsFromUser = Console.ReadLine();
                Console.WriteLine("Number of columns:");
                string numberOfColumnsFromUser = Console.ReadLine();

                if (int.TryParse(numberOfColumnsFromUser, out int numberOfColumns) &&
                    int.TryParse(numberOfRowsFromUser, out int numberOfRows) &&
                    numberOfColumns >= 4 && numberOfColumns <= 8 &&
                    numberOfRows >= 4 && numberOfRows <= 8)
                {
                    boardSize[0] = numberOfRows;
                    boardSize[1] = numberOfColumns;
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter valid numbers between 4 and 8 for both columns and rows.");
                }

            } while (!isValidInput);

            return boardSize;
        }

        static int GetValidTypeOfGameFromUser()
        {
            bool isValidInput = false;
            int typeOfGame;

            do
            {
                Console.WriteLine("For AI press 1, to play against other user press 2:");
                string stringtypeOfGame = Console.ReadLine();

                if (int.TryParse(stringtypeOfGame, out typeOfGame) && (typeOfGame == 1 || typeOfGame == 2))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter 1 to play against AI or 2 to play against another user.");
                }

            } while (!isValidInput);

            return typeOfGame;
        }

        static void runFourInRowConsole()
        {
            int[] boardSize = GetValidBoardSizeFromUser();
            int userGameChoice = GetValidTypeOfGameFromUser();

            if (userGameChoice == 1)
            {
                runFourInRowConsoleAI(boardSize[0], boardSize[1]);
            }
            else if (userGameChoice == 2)
            {
                runFourInRowConsole2Users(boardSize[0], boardSize[1]);
            }
        }

        static void Main()
        {
            runFourInRowConsole();
        }
    }

    class fourInRowBackend
    {
        private char[,] m_board;
        private int m_numberOfRows;
        private int m_numberColumns;
        private char m_CurrentPlayer = 'X';

        public fourInRowBackend(int i_numRows, int i_Columns)
        {
            if (isValidBoardSize(i_numRows, i_Columns))
            {
                this.m_numberOfRows = i_numRows;
                this.m_numberColumns = i_Columns;
                initializeBoard();

            }
            else { }
        }

        private bool isValidBoardSize(int i_numRows, int i_Columns)
        {
            return (i_numRows >= 4 && i_numRows <= 8) && (i_Columns >= 4 && i_Columns <= 8);
        }

        public int NumberOfRows
        {
            get
            {
                return m_numberOfRows;
            }
        }

        public int NumberOfColumns
        {
            get
            {
                return m_numberColumns;
            }
        }

        private void initializeBoard()
        {
            m_board = new char[NumberOfRows, NumberOfColumns];
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    m_board[i, j] = '-';
                }
            }
        }

        public string CreateStringFromBoard()
        {
            StringBuilder output = new StringBuilder();
            output.Append("  ");
            for (int i = 1; i < NumberOfColumns + 1; i++)
            {
                output.Append(i.ToString());
                output.Append("   ");
            }
            output.Append('\n');

            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    output.Append("| ");
                    if (m_board[i, j] != '-')
                    {
                        output.Append(m_board[i, j]).Append(' ');
                    }
                    else
                    {
                        output.Append("  ");
                    }
                }
                output.Append('|').Append('\n');
                output.Append(new string('=', NumberOfColumns * 4 + 1)).Append('\n');
            }

            return output.ToString();
        }

        public bool checkIfWin(int i_lastColumnPlayerInsertPin)
        {

            int lastRowPlayerInsertPin = 0;

            for (int i = 0; i < NumberOfRows - 1; i++)
            {
                if (m_board[lastRowPlayerInsertPin, i_lastColumnPlayerInsertPin] != m_CurrentPlayer)
                {
                    lastRowPlayerInsertPin++;
                }

            }

            return checkColumnWin(i_lastColumnPlayerInsertPin) ||
                   checkRowWin(lastRowPlayerInsertPin);
        }

        public bool checkColumnWin(int i_lastColumnPlayerInsertPin)
        {
            int countNumberOfPinInColumn = 0;
            bool found4InColumn = false;


            for (int i = 0; i < NumberOfRows; i++)
            {
                if (m_board[i, i_lastColumnPlayerInsertPin] == m_CurrentPlayer)
                {
                    countNumberOfPinInColumn++;

                    if (countNumberOfPinInColumn >= 4)
                    {
                        found4InColumn = true;
                        break;
                    }
                }
                else
                {
                    countNumberOfPinInColumn = 0;
                }
            }

            return found4InColumn;
        }

        public bool checkRowWin(int i_lastRowPlayerInsertPin)
        {
            int countNumberOfPinInRow = 0;
            bool found4InRow = false;


            for (int j = 0; j < NumberOfColumns; j++)
            {
                if (m_board[i_lastRowPlayerInsertPin, j] == m_CurrentPlayer)
                {
                    countNumberOfPinInRow++;

                    if (countNumberOfPinInRow >= 4)
                    {
                        found4InRow = true;
                        break;
                    }
                }
                else
                {
                    countNumberOfPinInRow = 0;
                }
            }

            return found4InRow;
        }

        internal void turnManager(int i_columnToInsert)
        {
            bool columnFull = true;

            for (int i = m_numberOfRows - 1; i >= 0; i--)
            {
                if (m_board[i, i_columnToInsert] == '-')
                {
                    m_board[i, i_columnToInsert] = m_CurrentPlayer;
                    columnFull = false;

                    break;
                }
            }

            if (columnFull)
            {
                Console.WriteLine("Column is full. Please try a different column."); // need to print it no idea where and why this is not printing
            }
        }

        internal bool isEvenGame() 
        {
            int countFullColumn = 0;

            for(int i = 0; i < m_numberOfRows; i++)
            {
                if(m_board[0,i] != '-')
                {
                    countFullColumn++;
                }
            }

            return countFullColumn == m_numberColumns;
        }

        internal void changePlayerTurn()
        {
            if (m_CurrentPlayer == 'X')
            {
                m_CurrentPlayer = 'O';
            }
            else
            {
                m_CurrentPlayer = 'X';
            }
        }
    }
}

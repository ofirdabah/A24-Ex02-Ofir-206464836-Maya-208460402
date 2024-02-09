using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_01.Class2
{
    class Class2
    {
       internal class fourInRowBackend
        {
            private char[,] m_board;
            private int m_numberOfRows;
            private int m_numberColumns;
            private char m_CurrentPlayer;



            public fourInRowBackend(int i_numRows, int i_Columns)
            {
                if (isValidBoardSize(i_numRows, i_Columns))
                {
                    this.m_numberOfRows = i_numRows;
                    this.m_numberColumns = i_Columns;
                    initializeBoard();
                    m_CurrentPlayer = 'X';
                    m_board[3, 4] = 'X';
                    m_board[2, 4] = 'X';
                    m_board[1, 4] = 'X';
                    m_board[0, 4] = 'X';
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

            public bool checkIfWin(char i_CurrentPlayer, int i_lastColumnPlayerInsertPin)
            {


                int lastRowPlayerInsertPin = 0;

                for (int i = 0; i < NumberOfRows - 1; i++)
                {
                    if (m_board[lastRowPlayerInsertPin, i_lastColumnPlayerInsertPin] != i_CurrentPlayer)
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
        }
    }
}

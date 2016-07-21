using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class SudokuBase
    {

        public int[,] matrix = new int[9, 9];
        int[,] row = new int[9, 9];
        int[,] col = new int[9, 9];
        int[,] sqr = new int[9, 9];

        public void Print()
        {
            for (int i = 0; i < 9; i++) 
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.Write("\n");
            }
        }

        // To solve a given Matrix
        public void Solve()
        {

        }

        public SudokuBase(SudokuBase s)
        {
            Array.Copy(s.matrix, matrix, 81);
            Array.Copy(s.row, row, 81);
            Array.Copy(s.col, col, 81);
            Array.Copy(s.sqr, sqr, 81);
        }

        public SudokuBase()
        {
           
        }


        // To Generate a New Sudoku Matrix
        public SudokuBase GenerateNew(int depth)
        {
            SudokuBase s = new SudokuBase();

           //********************************************

            //  Sudoku* s = NULL;
            int[] pos = new int[81];

            int p;
            int row;
            int col;
            int i;
            int j;
            int temp;
            int count;
            SudokuBase temps1;
            SudokuBase temps2;

            s = rand_full_sudoku();
            //if (s == NULL)
            //    return NULL;
            permute81(ref pos);


            for (i = 0; i < 81; i++)
            {
                p = pos[i];
                row = p / 9;
                col = p - 9 * row;

                temp = s.matrix[row,col];
                del(ref s, row, col);
                count = 0;
                for (j = 1; j <= 9; j++)
                    if (Convert.ToBoolean(add_feasible(ref s, row, col, j)))
                        count++;
                if (count == 1)
                    depth--;
                else
                    add(ref s, row, col, temp);
                if (depth == 0)
                    break;
            }

            if (depth == 0)
                return s;

            for (i = 0; i < 81; i++)
            {
                p = pos[i];
                row = p / 9;
                col = p - 9 * row;

                if (s.matrix[row,col] == 0)
                    continue;

                temp = s.matrix[row,col];
                del(ref s, row, col);
                temps1 = new SudokuBase(s);
                temps2 = new SudokuBase(s);

                backtrack(ref temps1, 0);
                backtrack_opst(ref temps2, 0);

                bool d = Convert.ToBoolean(is_equal(ref temps1, ref temps2));
                if (d)
                    depth--;
                else
                    add(ref s, row, col, temp);
                if (depth == 0)
                    break;
            }

            
            return s;
        }

        public SudokuBase GetTestMatrix()
        {
            SudokuBase s = new SudokuBase();

            int[,] _Matrix = new int[9, 9] {
            { 0,0,3,0,2,0,6,0,0 },
            { 9,0,0,3,0,5,0,0,1 },
            { 0,0,1,8,0,6,4,0,0 },
            { 0,0,8,1,0,2,9,0,0 },
            { 7,0,0,0,0,0,0,0,8 },
            { 0,0,6,7,0,8,2,0,0 },
            { 0,0,2,6,0,9,5,0,0 },
            { 8,0,0,2,0,3,0,0,9 },
            { 0,0,5,0,1,0,3,0,0 }};

            s.matrix = _Matrix;

            return s;
        }


        public SudokuBase rand_full_sudoku()
        {
            //Sudoku* s;
            SudokuBase s = new SudokuBase();

            int i;
            int row;
            int col;
            int[] rar = new int[9];

            s = EmptySudokuBase();
            //if (s == NULL)
            //    return NULL;

            permute9(ref rar);
            for (i = 0; i < 9; i++)
            {
                row = i / 3;
                col = i % 3;
                add(ref s, row, col, rar[i]);
            }
            permute9(ref rar);
            for (i = 0; i < 9; i++)
            {
                row = i / 3 + 3;
                col = i % 3 + 3;
                add(ref s, row, col, rar[i]);
            }
            permute9(ref rar);
            for (i = 0; i < 9; i++)
            {
                row = i / 3 + 6;
                col = i % 3 + 6;
                add(ref s, row, col, rar[i]);
            }
            if (random_backtrack(ref s, 0) != 1)
            {
                //free(s);
               // return NULL;
            }

            return s;
        }


        public SudokuBase EmptySudokuBase()
        {
            SudokuBase s = new SudokuBase();
            int[,] _Matrix = new int[9, 9] {
            { 0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0 }};

            s.matrix = _Matrix;
            return s;
        }
        public void permute9(ref int[] ar)
        {
            int[] select = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int r;
            int rs;
            int j;
            int i;

            for (i = 9; i >= 2; i--)
            {
                Random rnd = new Random();
                r = rnd.Next(i - 1);

                //r = rand() % i; // biased, but not noticeably
                rs = select[r];
                for (j = r + 1; j <= i - 1; j++)
                    select[j - 1] = select[j];
                ar[i - 1] = rs;
            }

            ar[0] = select[0];
        }
        public void add(ref SudokuBase s, int row, int col, int value)
        {
            s.row[row, value - 1] = 1;
            s.col[col, value - 1] = 1;
            s.sqr[((row / 3) * 3 + col / 3), value - 1] = 1;
            s.matrix[row, col] = value;

        }
        public int random_backtrack(ref SudokuBase s, int pos)
        {
            int row, col;
            int feasible;
            int i;
            int[] rar = new int[9];
            row = pos / 9;
            col = pos - 9 * row;


            if (pos == 81)
                return 1; //sudoku is solved

            if (s.matrix[row, col] != 0)
                return random_backtrack(ref s, pos + 1);

            permute9(ref rar);

            for (i = 0; i < 9; i++)
            {
                feasible = add_feasible(ref s, row, col, rar[i]); //checks if feasible
                if (Convert.ToBoolean(feasible))
                {
                    add(ref s, row, col, rar[i]); //adds number to sudoku	
                    if (random_backtrack(ref s, pos + 1) == 1) //moves to next empty
                        return 1;
                    else
                        del(ref s, row, col);
                }
            }
            return 0; //solution not found--backtrack	
        }
        public int add_feasible(ref SudokuBase s, int row, int col, int value)
        {
            int rf = s.row[row, value - 1];
            int cf = s.col[col, value - 1];
            int sf = s.sqr[((row / 3) * 3 + col / 3), value - 1];


            return Convert.ToInt32(!Convert.ToBoolean((rf | cf | sf)));
        }
        public void del(ref SudokuBase s, int row, int col)
        {
            int value = s.matrix[row , col];

            s.row[row,value - 1] = 0;
            s.col[col,value - 1] = 0;
            s.sqr[(row / 3) * 3 + col / 3 , value - 1] = 0;
            s.matrix[row , col] = 0;
        }
        public void permute81(ref int[] ar)
        {
            int[] select = new int[81];
            int r;
            int rs;
            int j;
            int i;

            for (i = 0; i < 81; i++)
                select[i] = i;

            for (i = 80; i >= 1; i--)
            {
                Random rnd = new Random();
                r = rnd.Next(i);

                //r = rand() % (i + 1); // biased, but not noticeably
                rs = select[r];
                for (j = r + 1; j <= i; j++)
                    select[j - 1] = select[j];
                ar[i] = rs;
            }
            ar[0] = select[0];
        }

        public int backtrack(ref SudokuBase s, int pos)
        {
            int row, col;
            int feasible;
            int i;
            row = pos / 9;
            col = pos - 9 * row;


            if (pos == 81)
                return 1; //sudoku is solved

            if (s.matrix[row,col] != 0)
                return backtrack(ref s, pos + 1);

            for (i = 1; i < 10; i++)
            {
                feasible = add_feasible(ref s, row, col, i); //checks if feasible
                if (Convert.ToBoolean(feasible))
                {
                    add(ref s, row, col, i); //adds number to sudoku	
                    if (backtrack(ref s, pos + 1) == 1) //moves to next empty
                        return 1;
                    else
                        del(ref s, row, col);
                }
            }
            return 0; //solution not found--backtrack	
        }
        int backtrack_opst(ref SudokuBase s, int pos)
        {
            int row, col;
            int feasible;
            int i;
            row = pos / 9;
            col = pos - 9 * row;


            if (pos == 81)
                return 1; //sudoku is solved

            if (s.matrix[row,col] != 0)
                return backtrack(ref s, pos + 1);

            for (i = 9; i > 0; i--)
            {
                feasible = add_feasible(ref s, row, col, i); //checks if feasible
                if (Convert.ToBoolean(feasible))
                {
                    add(ref s, row, col, i); //adds number to sudoku	
                    if (backtrack(ref s, pos + 1) == 1) //moves to next empty
                        return 1;
                    else
                        del(ref s, row, col);
                }
            }
            return 0; //solution not found--backtrack	
        }

        int isAvailable(ref int[,] puzzle, int row, int col, int num)
        {
            int rowStart = (row / 3) * 3;
            int colStart = (col / 3) * 3;
            int i, j;

            for (i = 0; i < 9; ++i)
            {
                if (puzzle[row,i] == num) return 0;
                if (puzzle[i,col] == num) return 0;
                if (puzzle[(rowStart + (i % 3)),colStart + (i / 3)] == num) return 0;
            }
            return 1;
        }

        public int laymansolve(ref SudokuBase s, int row, int col)
        {
            int i;
            if (row < 9 && col < 9)
            {
                if (s.matrix[row,col] != 0)
                {
                    if ((col + 1) < 9) return laymansolve(ref s, row, col + 1);
                    else if ((row + 1) < 9) return laymansolve(ref s, row + 1, 0);
                    else return 1;
                }
                else
                {
                    for (i = 0; i < 9; ++i)
                    {
                        if (Convert.ToBoolean(isAvailable(ref s.matrix, row, col, i + 1)))
                        {
                            s.matrix[row, col] = i + 1;
                            if ((col + 1) < 9)
                            {
                                if (Convert.ToBoolean(laymansolve(ref s, row, col + 1))) return 1;
                                else s.matrix[row, col] = 0;
                            }
                            else if ((row + 1) < 9)
                            {
                                if (Convert.ToBoolean(laymansolve(ref s, row + 1, 0))) return 1;
                                else s.matrix[row, col] = 0;
                            }
                            else return 1;
                        }
                    }
                }
                return 0;
            }
            else return 1;
        }
        int is_equal(ref SudokuBase s1, ref SudokuBase s2)
        {
            int r;
            int c;

            for (r = 0; r < 9; r++)
                for (c = 0; c < 9; c++)
                    if (s1.matrix[r,c] != s2.matrix[r,c])
                        return 0;
            return 1;
        }
    }
}

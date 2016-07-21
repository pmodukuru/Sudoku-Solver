#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "sudoku.h"




void add(Sudoku *s, int row, int col, int value)
{
	s->row[row][value - 1] = 1;
	s->col[col][value - 1] = 1;
	s->sqr[(row / 3) * 3 + col / 3][value - 1] = 1;
	s->matrix[row][col] = value;
}

void delete(Sudoku *s, int row, int col)
{
	int value = s->matrix[row][col];

	s->row[row][value - 1] = 0;
	s->col[col][value - 1] = 0;
	s->sqr[(row / 3) * 3 + col / 3][value - 1] = 0;
	s->matrix[row][col] = 0;
}

int add_feasible(Sudoku *s, int row, int col, int value)
{
	int rf = s->row[row][value - 1];
	int cf = s->col[col][value - 1];
	int sf = s->sqr[(row / 3) * 3 + col / 3][value - 1];

	return !(rf || cf || sf);
}

Sudoku *empty_sudoku(void)
{
	Sudoku *s;
	int r;
	int c;

	s = malloc(sizeof(*s));
	if (s == NULL)
		return NULL;
	for (r = 0; r < 9; r++)
		for (c = 0; c < 9; c++) {
			s->matrix[r][c] = 0;
			s->row[r][c] = 0;
			s->col[r][c] = 0;
			s->sqr[r][c] = 0;
		}
	return s;
}

int is_equal(Sudoku *s1, Sudoku *s2)
{
	int r;
	int c;
	
	for (r = 0; r < 9; r++)
		for (c = 0; c < 9; c++)
			if (s1->matrix[r][c] != s2->matrix[r][c])
				return 0;
	return 1;
}


int backtrack(Sudoku *s, int pos)
{
	int row, col;
	int feasible;
	int i;
	row = pos / 9;
	col = pos - 9*row;


	if (pos == 81)
		return 1; //sudoku is solved

	if (s->matrix[row][col] != 0)
		return backtrack(s, pos+1);
	
	for (i = 1; i <  10; i++) {	
		feasible = add_feasible(s, row, col, i); //checks if feasible
		if(feasible) {
			add(s, row, col, i); //adds number to sudoku	
			if (backtrack(s, pos + 1) == 1) //moves to next empty
				return 1;
			else 
				delete(s, row, col);
		}
	}
	return 0; //solution not found--backtrack	
}


//layman algorithm functions
int isAvailable(int puzzle[][9], int row, int col, int num)
{
    int rowStart = (row/3) * 3;
    int colStart = (col/3) * 3;
    int i, j;

    for(i=0; i<9; ++i)
    {
        if (puzzle[row][i] == num) return 0;
        if (puzzle[i][col] == num) return 0;
        if (puzzle[rowStart + (i%3)][colStart + (i/3)] == num) return 0;
    }
    return 1;
}

int laymansolve(int puzzle[][9], int row, int col)
{
    int i;
    if(row<9 && col<9)
    {
        if(puzzle[row][col] != 0)
        {
            if((col+1)<9) return laymansolve(puzzle, row, col+1);
            else if((row+1)<9) return laymansolve(puzzle, row+1, 0);
            else return 1;
        }
        else
        {
            for(i=0; i<9; ++i)
            {
                if(isAvailable(puzzle, row, col, i+1))
                {
                    puzzle[row][col] = i+1;
                    if((col+1)<9)
                    {
                        if(laymansolve(puzzle, row, col +1)) return 1;
                        else puzzle[row][col] = 0;
                    }
                    else if((row+1)<9)
                    {
                        if(laymansolve(puzzle, row+1, 0)) return 1;
                        else puzzle[row][col] = 0;
                    }
                    else return 1;
                }
            }
        }
        return 0;
    }
    else return 1;
}

//end of layman algorithms


Sudoku *read_sudoku_file(FILE *fpin)
{
	Sudoku *s;
	int r;
	int c;
	int ch;

	s = empty_sudoku();
	if (s == NULL) 
		return NULL;

	if (fseek(fpin, 0, SEEK_END) != 0)
		goto err;
	if (ftell(fpin) != 90)
		goto err;
	fseek(fpin, 0, SEEK_SET);
	
	for (r = 0; r < 9; r++) {
		for (c = 0; c < 9; c++) {
			ch = fgetc(fpin) - '0';
			if (ch < 0 || ch > 9)
				goto err;
			if (ch == 0) {
				s->matrix[r][c] = 0;
			}
			else {
				if (add_feasible(s, r, c, ch) == 0)
					goto err;
				add(s, r, c, ch);
			}
		}
		fseek(fpin, 1, SEEK_CUR);
	}

	return s;	
	err:
		free(s);
		return NULL;
}


int write_sudoku_file(Sudoku *s, FILE *fpout)
{
	int r;
	int c;

	for (r = 0; r < 9; r++) {
		for (c = 0; c < 9; c++) {
			if (fputc(s->matrix[r][c] + '0', fpout) == EOF)
				return -1;
		}
		if (fputc('\n', fpout) == EOF)
			return -1;
	}

	return 0;	
}
void print_sudoku(Sudoku *s)
{
	int r;
	int c;
	
	for (r = 0; r < 9; r++) {
		for (c = 0; c < 9; c++) 
			fputc(s->matrix[r][c] + '0', stdout);
		fputc('\n', stdout);
	}
	fputc('\n', stdout);
}


//functions to generate 
void permute9(int ar[])
{
	int select[9] = {1,2,3,4,5,6,7,8,9};
	int r;
	int rs;
	int j;
	int i;

	for (i = 9; i >= 2; i--) {
		r = rand() % i; // biased, but not noticeably
		rs = select[r];
		for (j = r+1; j <= i-1; j++)
			select[j-1] = select[j];
		ar[i-1] = rs;
	}
	ar[0] = select[0];
}

void permute81(int ar[])
{
	int select[81]; 
	int r;
	int rs;
	int j;
	int i;

	for (i = 0; i < 81; i++)
		select[i] = i;

	for (i = 80; i >= 1; i--) {
		r = rand() % (i+1); // biased, but not noticeably
		rs = select[r];
		for (j = r+1; j <= i; j++)
			select[j-1] = select[j];
		ar[i] = rs;
	}
	ar[0] = select[0];
}

int backtrack_opst(Sudoku *s, int pos)
{
	int row, col;
	int feasible;
	int i;
	row = pos / 9;
	col = pos - 9*row;


	if (pos == 81)
		return 1; //sudoku is solved

	if (s->matrix[row][col] != 0)
		return backtrack(s, pos+1);
	
	for (i = 9; i >  0; i--) {	
		feasible = add_feasible(s, row, col, i); //checks if feasible
		if(feasible) {
			add(s, row, col, i); //adds number to sudoku	
			if (backtrack(s, pos + 1) == 1) //moves to next empty
				return 1;
			else 
				delete(s, row, col);
		}
	}
	return 0; //solution not found--backtrack	
}

int random_backtrack(Sudoku *s, int pos)
{
	int row, col;
	int feasible;
	int i;
	int rar[9];
	row = pos / 9;
	col = pos - 9*row;


	if (pos == 81)
		return 1; //sudoku is solved

	if (s->matrix[row][col] != 0)
		return random_backtrack(s, pos+1);
	
	permute9(rar);

	for (i = 0; i <  9; i++) {	
		feasible = add_feasible(s, row, col, rar[i]); //checks if feasible
		if(feasible) {
			add(s, row, col, rar[i]); //adds number to sudoku	
			if (random_backtrack(s, pos + 1) == 1) //moves to next empty
				return 1;
			else 
				delete(s, row, col);
		}
	}
	return 0; //solution not found--backtrack	
}

Sudoku *rand_full_sudoku(void)
{
	Sudoku *s;
	int i;
	int row;
	int col;
	int rar[9];

	s = empty_sudoku();
	if (s == NULL)
		return NULL;

	permute9(rar);
	for (i = 0; i < 9; i++) {
		row = i/3;
		col = i%3;
		add(s, row, col, rar[i]);
	}
	permute9(rar);
	for (i = 0; i < 9; i++) {
		row = i/3 + 3;
		col = i%3 + 3;
		add(s, row, col, rar[i]);
	}
	permute9(rar);
	for (i = 0; i < 9; i++) {
		row = i/3 + 6;
		col = i%3 + 6;
		add(s, row, col, rar[i]);
	}
	if (random_backtrack(s, 0) != 1) {
		free(s);
		return NULL;
	}

	return s;
}

Sudoku *rand_sudoku(int depth) {
	Sudoku *s = NULL;
	int pos[81];
	int p;
	int row;
	int col;
	int i;
	int j;
	int temp;
	int count;
	Sudoku temps1;
	Sudoku temps2;

	s = rand_full_sudoku();
	if (s == NULL)
		return NULL;
	permute81(pos);


	for (i = 0; i < 81; i++) {
		p = pos[i];
		row = p / 9;
		col = p - 9*row;
	
		temp = s->matrix[row][col];
		delete(s, row, col);
		count = 0;
		for (j = 1; j <= 9; j++) 
			if (add_feasible(s, row, col, j))
				count++;
		if (count == 1)
			depth--;
		else
			add(s, row, col, temp);
		if (depth == 0)
			break;
	}

	if (depth == 0)
		return s;
	
	for (i = 0; i < 81; i++) {
		p = pos[i];
		row = p / 9;
		col = p - 9*row;
		
		if (s->matrix[row][col] == 0)
			continue;

		temp = s->matrix[row][col];
		delete(s, row, col);
		temps1 = *s;
		temps2 = *s;

		backtrack(&temps1, 0);
		backtrack_opst(&temps2, 0);
		
		if (is_equal(&temps1, &temps2))
			depth--;
		else
			add(s, row, col, temp);
		if (depth == 0)
			break;
	}
	
	return s;	
}
//end functions for generate

/*int main(int argc, char *argv[])
{
	clock_t begin, end;
	double time_spent;

	
	if (argc != 4 && argc != 3) //4 for solves, 2 for generate
		{
			printf("Error. Not correct inputs.\n");
			exit(1);
		}

	Sudoku *s;

	if (argc == 4)
	{

		FILE *fpin;
		FILE *fpout;
		
		fpin = fopen(argv[1], "r");
		if (fpin == NULL)
			exit(1);
		fpout = fopen(argv[2], "w");
		if (fpout == NULL) {
			fclose(fpin);
			exit(1);
		}

		int choice = atoi(argv[3]);

		s = read_sudoku_file(fpin);
		if (s == NULL) {
			fprintf(stderr, "Not a valid sudoku file\n");
			goto err;
		}


		begin = clock();
		if (choice == 1)
		{

			if (backtrack(s, 0) == 0)
				fprintf(stderr, "Sudoku failed\n");
		}
		else if (choice == 2)
		{
			if (laymansolve(s->matrix, 0, 0) == 0)
				fprintf(stderr, "Sudoku failed\n");
		}
		
		end = clock();
		time_spent = (double)(end-begin) / CLOCKS_PER_SEC;
		printf("%lf\n", time_spent);
		if (write_sudoku_file(s, fpout) != 0)
			goto err;

		free(s);
		fclose(fpin);
		fclose(fpout);
		exit(0);

		err:
		free(s);
		fclose(fpin);
		fclose(fpout);
		exit(1);

	}

	if (argc == 3)
	{
		FILE *fpout;
		fpout = fopen(argv[2], "w");
		if (fpout == NULL) {
			exit(1);
		}

		fprintf(stdout, "Generate:\n");
		int depth = atoi(argv[1]);
		int failed = 1;

		srand(time(NULL));
		while(failed == 1)
		{
			s = rand_sudoku(depth);
			if (s!=NULL)
			{
				failed = 0;
			}
		}
		write_sudoku_file(s, fpout);

		fclose(fpout);
		free(s);
		exit(0);

	}

	
	
}
*/
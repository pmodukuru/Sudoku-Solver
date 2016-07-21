#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#include "sudoku.h"

int main(int argc, char **argv)
{
	clock_t begin, end;
	long double time_spent;
	int failed = 1;
	int sudokucnt = 0;
	int check = 0;
	Sudoku *s;
	int depth = atoi(argv[1]); //change input to whatever difficulty level you want to analyze

	srand(time(NULL));

	FILE * fptr = fopen(argv[2], "w");

	while (sudokucnt != 1000)
	{
		//generate
		s = rand_sudoku(depth);
		sudokucnt += 1;

		//solve
		//print_sudoku(s);
		begin = clock();
		check = random_backtrack(s, 0); //change to whatever function to analyze
		end = clock();

		time_spent = (double)(end-begin) / CLOCKS_PER_SEC;
		fprintf(fptr, "%Lf\n" ,time_spent);
		free(s);

	}
	fclose(fptr);
}

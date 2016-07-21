typedef struct sudoku {
	int matrix[9][9];
	int row[9][9];
	int col[9][9];
	int sqr[9][9];
} Sudoku;

int add_feasible(Sudoku *s, int row, int col, int value);
void add(Sudoku *s, int row, int col, int value);
int EndofMatrix(int pos, Sudoku *s);
int backtrack(Sudoku *s, int pos);
Sudoku * read_sudoku_file(FILE *fpin);
//void generate(void);
int random_between(int min, int max);
int laymansolve(int puzzle[][9], int row, int col);
int isAvailable(int puzzle[][9], int row, int col, int num);
Sudoku *rand_sudoku(int depth);
Sudoku *rand_full_sudoku(void);
int random_backtrack(Sudoku *s, int pos);
int backtrack_opst(Sudoku *s, int pos);
void permute81(int ar[]);
void permute9(int ar[]);
void print_sudoku(Sudoku *s);
int write_sudoku_file(Sudoku *s, FILE *fpout);
Sudoku *empty_sudoku(void);
int is_equal(Sudoku *s1, Sudoku *s2);

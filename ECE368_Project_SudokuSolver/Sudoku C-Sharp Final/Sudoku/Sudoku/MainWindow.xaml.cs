using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Elysium.Controls.Window
    {
        public MainWindow()
        {



            //InitializeComponent();
        }

        SudokuBase MySudoku = new SudokuBase();

        private void buttonGenerate_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 0;


            // MySudoku = MySudoku.GetTestMatrix();
            // MySudoku.Print();


            if (LevelEasy.IsChecked == true)
            {
                MySudoku = MySudoku.GenerateNew(48);
            }
            else if (LevelMedium.IsChecked == true)
            {
                MySudoku = MySudoku.GenerateNew(51);
            }
            else if (LevelHard.IsChecked == true)
            {
                MySudoku = MySudoku.GenerateNew(54);
            }
            else
            {
                MySudoku = MySudoku.GenerateNew(57);
            }

            MySudoku.Print();

            ShowSudoku(MySudoku);


        }

       

        private void buttonSolve_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex= 0;


            if (SolveBackTrack.IsChecked == true)
            {
                MySudoku.backtrack(ref MySudoku, 0);
            }
            else
            {
                MySudoku.laymansolve(ref MySudoku,0,0);
            }

            ShowSudoku(MySudoku);
        }



        //Helper
        private void ShowSudoku(SudokuBase s)
        {
            A_0_0.Text = Convert.ToString(s.matrix[0, 0]);
            A_0_1.Text = Convert.ToString(s.matrix[0, 1]);
            A_0_2.Text = Convert.ToString(s.matrix[0, 2]);
            A_1_0.Text = Convert.ToString(s.matrix[1, 0]);
            A_1_1.Text = Convert.ToString(s.matrix[1, 1]);
            A_1_2.Text = Convert.ToString(s.matrix[1, 2]);
            A_2_0.Text = Convert.ToString(s.matrix[2, 0]);
            A_2_1.Text = Convert.ToString(s.matrix[2, 1]);
            A_2_2.Text = Convert.ToString(s.matrix[2, 2]);

            B_0_0.Text = Convert.ToString(s.matrix[0, 3]);
            B_0_1.Text = Convert.ToString(s.matrix[0, 4]);
            B_0_2.Text = Convert.ToString(s.matrix[0, 5]);
            B_1_0.Text = Convert.ToString(s.matrix[1, 3]);
            B_1_1.Text = Convert.ToString(s.matrix[1, 4]);
            B_1_2.Text = Convert.ToString(s.matrix[1, 5]);
            B_2_0.Text = Convert.ToString(s.matrix[2, 3]);
            B_2_1.Text = Convert.ToString(s.matrix[2, 4]);
            B_2_2.Text = Convert.ToString(s.matrix[2, 5]);

            C_0_0.Text = Convert.ToString(s.matrix[0, 6]);
            C_0_1.Text = Convert.ToString(s.matrix[0, 7]);
            C_0_2.Text = Convert.ToString(s.matrix[0, 8]);
            C_1_0.Text = Convert.ToString(s.matrix[1, 6]);
            C_1_1.Text = Convert.ToString(s.matrix[1, 7]);
            C_1_2.Text = Convert.ToString(s.matrix[1, 8]);
            C_2_0.Text = Convert.ToString(s.matrix[2, 6]);
            C_2_1.Text = Convert.ToString(s.matrix[2, 7]);
            C_2_2.Text = Convert.ToString(s.matrix[2, 8]);

            D_0_0.Text = Convert.ToString(s.matrix[3, 0]);
            D_0_1.Text = Convert.ToString(s.matrix[3, 1]);
            D_0_2.Text = Convert.ToString(s.matrix[3, 2]);
            D_1_0.Text = Convert.ToString(s.matrix[4, 0]);
            D_1_1.Text = Convert.ToString(s.matrix[4, 1]);
            D_1_2.Text = Convert.ToString(s.matrix[4, 2]);
            D_2_0.Text = Convert.ToString(s.matrix[5, 0]);
            D_2_1.Text = Convert.ToString(s.matrix[5, 1]);
            D_2_2.Text = Convert.ToString(s.matrix[5, 2]);

            E_0_0.Text = Convert.ToString(s.matrix[3, 3]);
            E_0_1.Text = Convert.ToString(s.matrix[3, 4]);
            E_0_2.Text = Convert.ToString(s.matrix[3, 5]);
            E_1_0.Text = Convert.ToString(s.matrix[4, 3]);
            E_1_1.Text = Convert.ToString(s.matrix[4, 4]);
            E_1_2.Text = Convert.ToString(s.matrix[4, 5]);
            E_2_0.Text = Convert.ToString(s.matrix[5, 3]);
            E_2_1.Text = Convert.ToString(s.matrix[5, 4]);
            E_2_2.Text = Convert.ToString(s.matrix[5, 5]);

            F_0_0.Text = Convert.ToString(s.matrix[3, 6]);
            F_0_1.Text = Convert.ToString(s.matrix[3, 7]);
            F_0_2.Text = Convert.ToString(s.matrix[3, 8]);
            F_1_0.Text = Convert.ToString(s.matrix[4, 6]);
            F_1_1.Text = Convert.ToString(s.matrix[4, 7]);
            F_1_2.Text = Convert.ToString(s.matrix[4, 8]);
            F_2_0.Text = Convert.ToString(s.matrix[5, 6]);
            F_2_1.Text = Convert.ToString(s.matrix[5, 7]);
            F_2_2.Text = Convert.ToString(s.matrix[5, 8]);

            G_0_0.Text = Convert.ToString(s.matrix[6, 0]);
            G_0_1.Text = Convert.ToString(s.matrix[6, 1]);
            G_0_2.Text = Convert.ToString(s.matrix[6, 2]);
            G_1_0.Text = Convert.ToString(s.matrix[7, 0]);
            G_1_1.Text = Convert.ToString(s.matrix[7, 1]);
            G_1_2.Text = Convert.ToString(s.matrix[7, 2]);
            G_2_0.Text = Convert.ToString(s.matrix[8, 0]);
            G_2_1.Text = Convert.ToString(s.matrix[8, 1]);
            G_2_2.Text = Convert.ToString(s.matrix[8, 2]);


            H_0_0.Text = Convert.ToString(s.matrix[6, 3]);
            H_0_1.Text = Convert.ToString(s.matrix[6, 4]);
            H_0_2.Text = Convert.ToString(s.matrix[6, 5]);
            H_1_0.Text = Convert.ToString(s.matrix[7, 3]);
            H_1_1.Text = Convert.ToString(s.matrix[7, 4]);
            H_1_2.Text = Convert.ToString(s.matrix[7, 5]);
            H_2_0.Text = Convert.ToString(s.matrix[8, 3]);
            H_2_1.Text = Convert.ToString(s.matrix[8, 4]);
            H_2_2.Text = Convert.ToString(s.matrix[8, 5]);

            I_0_0.Text = Convert.ToString(s.matrix[6, 6]);
            I_0_1.Text = Convert.ToString(s.matrix[6, 7]);
            I_0_2.Text = Convert.ToString(s.matrix[6, 8]);
            I_1_0.Text = Convert.ToString(s.matrix[7, 6]);
            I_1_1.Text = Convert.ToString(s.matrix[7, 7]);
            I_1_2.Text = Convert.ToString(s.matrix[7, 8]);
            I_2_0.Text = Convert.ToString(s.matrix[8, 6]);
            I_2_1.Text = Convert.ToString(s.matrix[8, 7]);
            I_2_2.Text = Convert.ToString(s.matrix[8, 8]);

        }
         
    }
}

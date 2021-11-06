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

namespace isu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Random random = new Random();

        Dictionary<int, double[]> Positions = new Dictionary<int, double[]>() {
            { 1, new double[]{ 50,  60 } },
            { 2, new double[]{ 160, 60 } },
            { 3, new double[]{ 270, 60 } },
            { 4, new double[]{ 50,  170 } },
            { 5, new double[]{ 160, 170 } },
            { 6, new double[]{ 270, 170 } },
            { 7, new double[]{ 50,  280 } },
            { 8, new double[]{ 160, 280 } },
            { 9, new double[]{ 270, 280 } },
        };

        double[] emptyPos = { 270, 280 };

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            double[] actualPosition = { (sender as Button).Margin.Left, (sender as Button).Margin.Top };
            bool movable = false;
            if ( (Math.Abs(actualPosition[0] - emptyPos[0]) == 110 &&
                 actualPosition[1] == emptyPos[1]) 
                 ||
                 (Math.Abs(actualPosition[1] - emptyPos[1]) == 110 &&
                 actualPosition[0] == emptyPos[0]))
            {
                double[] newPosition = MoveToFreePosition(actualPosition);
                (sender as Button).Margin = new Thickness(newPosition[0], newPosition[1], 0, 0);
                if (IsWin())
                    MessageBox.Show("You win boy!");
            }
        }

        public double[] MoveToFreePosition(double[] actualPosition)
        {
            double[] newPosition = emptyPos;
            emptyPos = actualPosition;
            return newPosition;
        }

        private void Shuffle(object sender, RoutedEventArgs e) {
            int[] generatedIndexs = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int genInd = random.Next(1, 10);
            Button[] buttons = { Btn1, Btn2, Btn3, Btn4, Btn5, Btn6, Btn7, Btn8 };
            int chaosParam = 0;
            // while chaosParam % 2 == 0
            for (int i=0; i<9; ++i)
            {
                if (i == 8)
                {
                    for (int j=1; j<10; ++j)
                    {
                        if (!generatedIndexs.Contains(j))
                            generatedIndexs[i] = j;
                    }
                }
                else
                {
                    if (generatedIndexs.Contains(genInd))
                    {
                        genInd = random.Next(1, 10);
                        i--;
                    }
                    else
                    {
                        generatedIndexs[i] = genInd;
                    }
                }            
            }
            // chaosParam calculation
            for (int i=0; i<8; ++i)
            {
                for (int j=i+1; j<8; ++j)
                {
                    if (generatedIndexs[j] < generatedIndexs[i])
                        chaosParam++;
                }
            }
            chaosParam += (generatedIndexs[8]-1)/3 + 1;

            for (int i=0; i<8; ++i)
            {
                double[] position = Positions[generatedIndexs[i]];
                buttons[i].Margin = new Thickness(position[0], position[1], 0, 0);
            }

            emptyPos = Positions[generatedIndexs[8]];
        }

        public bool IsWin()
        {
            if (Btn1.Margin.Left == Positions[1][0] && Btn1.Margin.Top == Positions[1][1] &&
                Btn2.Margin.Left == Positions[2][0] && Btn2.Margin.Top == Positions[2][1] &&
                Btn3.Margin.Left == Positions[3][0] && Btn3.Margin.Top == Positions[3][1] &&
                Btn4.Margin.Left == Positions[4][0] && Btn4.Margin.Top == Positions[4][1] &&

                Btn5.Margin.Left == Positions[5][0] && Btn5.Margin.Top == Positions[5][1] &&
                Btn6.Margin.Left == Positions[6][0] && Btn6.Margin.Top == Positions[6][1] &&
                Btn7.Margin.Left == Positions[7][0] && Btn7.Margin.Top == Positions[7][1] &&
                Btn8.Margin.Left == Positions[8][0] && Btn8.Margin.Top == Positions[8][1])
                return true;
            return false;
        }
    }
}

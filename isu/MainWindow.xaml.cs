using System;
using System.Collections;
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

        Node finalNode;

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

        Dictionary<int, int[]> CanBeMoved = new Dictionary<int, int[]>() {
            { 1, new int[]{ 2, 4 } },
            { 2, new int[]{ 1, 3, 5 } },
            { 3, new int[]{ 2, 6 } },
            { 4, new int[]{ 1, 5, 7 } },
            { 5, new int[]{ 2, 4, 6, 8 } },
            { 6, new int[]{ 3, 5, 9 } },
            { 7, new int[]{ 4, 8 } },
            { 8, new int[]{ 5, 7, 9 } },
            { 9, new int[]{ 6, 8 } },
        };

        /* храним в PosButton отображение позиций в номера кнопок, которые там находятся
           Позиция 1 => 2 Значит что в первой позиции (верхний левый угол) находиться кнопка с номером 2
           Пустая позиция имеет как кнопка номер 9
        */
        Dictionary<int, int> PosButtonState = new Dictionary<int, int>() {
            { 1, 1 },{ 2, 2 },{ 3, 3 },{ 4, 4 },{ 5, 5 },{ 6, 6 },{ 7, 7 },{ 8, 8 },{ 9, 9 }
        };

        Button GetButton(int index) {
            Dictionary<int, Button> buttons = new Dictionary<int, Button>() {
            { 1, Btn1 },
            { 2, Btn2 },
            { 3, Btn3 },
            { 4, Btn4 },
            { 5, Btn5 },
            { 6, Btn6 },
            { 7, Btn7 },
            { 8, Btn8 },
            };
            return buttons[index];
        }

        int GetButtonPosition(Button button)
        {
            double x = button.Margin.Left;
            double y = button.Margin.Top;
            for (int i=1; i<10; ++i)
            {
                if (Positions[i][0] == x && Positions[i][1] == y)
                    return i;
            }
            return 0;
        }

        int GetButtonPosition(double[] pos)
        {
            for (int i = 1; i < 10; ++i)
            {
                if (Positions[i][0] == pos[0] && Positions[i][1] == pos[1])
                    return i;
            }
            return 0;
        }
        

        double[] emptyPos = { 270, 280 };

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            double[] actualPosition = { (sender as Button).Margin.Left, (sender as Button).Margin.Top };
            if ( (Math.Abs(actualPosition[0] - emptyPos[0]) == 110 &&
                 actualPosition[1] == emptyPos[1])
                 ||
                 (Math.Abs(actualPosition[1] - emptyPos[1]) == 110 &&
                 actualPosition[0] == emptyPos[0]))
            {
                MoveToFreePosition(actualPosition, (sender as Button));
                PosButtonState[GetButtonPosition(sender as Button)] = int.Parse((sender as Button).Content.ToString());
                if (IsWin())
                    MessageBox.Show("You win boy!");
            }
        }

        // actualPosition = Positions[GetButtonPosition(GetButton(index))]
        public void MoveToFreePosition(double[] actualPosition, Button button)
        {
            double[] newPosition = emptyPos;
            emptyPos = actualPosition;
            PosButtonState[GetButtonPosition(emptyPos)] = 9;
            button.Margin = new Thickness(newPosition[0], newPosition[1], 0, 0);
        }

        private void Shuffle(object sender, RoutedEventArgs e) {
            int[] generatedIndexs = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int genInd = random.Next(1, 10);
            
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
                GetButton(i+1).Margin = new Thickness(position[0], position[1], 0, 0);
            }

            emptyPos = Positions[generatedIndexs[8]];

            for (int i = 1; i < 9; ++i)
            {
                PosButtonState[GetButtonPosition(GetButton(i))] = i;
            }
            PosButtonState[GetButtonPosition(emptyPos)] = 9;
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

        private void Debug(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(PosButtonState.ToString());
            Node rootNode = new Node(0, 0);
            ProcessState(PosButtonState, rootNode);
            MessageBox.Show(rootNode.ToString());
            MessageBox.Show(finalNode.ToString());
        }

        void ProcessState(Dictionary<int, int> state, Node rootNode, int depth = 0)
        {
            if (rootNode.depth > 30 || Intellectual.Heuristic(state) == 0)
            {
                finalNode = rootNode;
                return;
            }

            int emptyStatePos = GetEmptyFromState(state);
            int[] moves = CanBeMoved[emptyStatePos];
            List<Node> stateNodes = new List<Node>();

            foreach (var move in moves)
            {
                Dictionary<int, int> newState = ApplyMove(state, move, emptyStatePos);
                int cost = Intellectual.Heuristic(newState);
                Node stateNode = new Node(move, cost, rootNode, depth);
                if (cost == 0)
                {
                    finalNode = stateNode;
                    return;
                }

                stateNodes.Add(stateNode);
                //ProcessState(newState, stateNode, ++depth);
            }

            Node pickedNode = Node.MinCostNode(stateNodes);
            ProcessState(ApplyMove(state, pickedNode.value, emptyStatePos), pickedNode, ++depth);
        }

        int GetEmptyFromState(Dictionary<int, int> state)
        {
            for (int i=1; i<10; ++i)
            {
                if (state[i] == 9)
                    return i;
            }
            return 0;
        }

        Dictionary<int, int> ApplyMove(Dictionary<int, int> state, int move, int emptyStatePos)
        {
            Dictionary<int, int> newState = new Dictionary<int, int>(state);
            newState[emptyStatePos] = newState[move];
            newState[move] = 9;
            return newState;
        }
    }
}

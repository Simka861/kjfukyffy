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

namespace WpfApp1
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = "Игрок 1";
            CreateMap();
            //     Init(new object(), new RoutedEventArgs());

        }
        const int mapSize = 8;
        figure[,] simpleSteps = new figure[mapSize, mapSize];
        int hod = 1;
        int countWhiteFigure = 12;//12
        int countBlackFigure = 12;//12
        bool povtor = false;
        figure b = null;
        List<figure> Meal = null; // пешки которых можно съеть (лист)
        List<figure> lf = null; // возмоный ход (лист)

        List<figure> Meal_D1 = null; // 
        List<figure> Meal_D2 = null;//
        List<figure> Meal_D3 = null;//
        List<figure> Meal_D4 = null;//

        private void CreateMap()
        {
            int[,] map = new int[mapSize, mapSize]{
                { 0,1,0,1,0,1,0,1 },
                { 1,0,1,0,1,0,1,0 },
                { 0,1,0,1,0,1,0,1 },
                { 0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0 },
                { 2,0,2,0,2,0,2,0 },
                { 0,2,0,2,0,2,0,2 },
                { 2,0,2,0,2,0,2,0 }
            };
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    simpleSteps[i, j] = new figure(i, j, map[j, i]);
                    grids.Children.Add(simpleSteps[i, j].button);
                    Grid.SetColumn(simpleSteps[i, j].button, i);
                    Grid.SetRow(simpleSteps[i, j].button, j);
                    simpleSteps[i, j].button.Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (b is null)
            {
                foreach (var i in simpleSteps)
                    if ((Button)sender == i.button && i.Index == hod)
                    {
                        i.button.Background = Brushes.Red;
                        b = i;
                     Food_5(i.Row, i.Column, i.Dama);
                        break;
                    }
            }
            else if (b.button == (Button)sender)
            {
                otmena();
                if (povtor)
                  Perevod_hoda();
            }
            else if (((Button)sender).Background == Brushes.Green)
            {
                b.Index = 0;
                b.Image();
                foreach (var i in lf)
                    if ((Button)sender == i.button)
                    {
                        i.Background();
                        i.Index = hod;
                        bool isFood = false;
                        if (!(Meal is null))
                          isFood = Food_7(i);
                        i.Dama = b.Dama;
                        b.Dama = false;
                        if (!i.Dama && ((hod == 1 && i.Column == 7) || (hod == 2 && i.Column == 0)))
                            i.Dama = true;
                        i.Image();
                        if (isFood)
                        {
                          Food_8(i);
                            if (!(Meal is null))
                                return;
                        }
                        break;
                    }
                otmena();
               Perevod_hoda();
            }
        }


        private void Perevod_hoda()
        {
            if (hod == 1)
            {
                hod = 2;
                Title = "Игрой 2";
            }
            else
            {
                hod = 1;
                Title = "Игрой 1";
            }
            povtor = false;
            int i = -1;
            if (countBlackFigure == 0)
                i = 0;
            else if (countWhiteFigure == 0)
                i = 1;
            else
            {
                if (hod == 1 && countWhiteFigure == 1)
                    //  if (IsHod())
                    i = 1;
                if (hod == 2 && countBlackFigure == 1)
                    //if (IsHod())
                    i = 0;
                if (i == -1)
                {
                    bool Dama = true;
                    foreach (var f in simpleSteps)
                        if (f.Index != 0 && !(f.Dama))
                        {
                            Dama = false;
                            break;
                        }
                    if (Dama)
                    {
                        var w = new Window1();
                        if (w.ShowDialog() == true)
                            i = 2;
                    }
                }
            }
            //if (i != -1)
            //{
            //    var w = new Message(i);
            //    if (w.ShowDialog() == false)
            //        Close();
            //    else
            //        NewGames();
            //}
        }

        private void otmena()
        {
            if (!(b is null))
                b.Background();
            b = null;
            if (!(lf is null))
                foreach (var i in lf)
                    i.Background();
            lf = null;
            Meal = null;
         //   Meal_D1 = null;
         //   Meal_D2 = null;
         //   Meal_D3 = null;
         //   Meal_D4 = null;
        }


        private void PossibleMove(int i, int j)
        {
            if (simpleSteps[i, j].Index == 0)
            {
                simpleSteps[i, j].button.Background = Brushes.Green;
                if (lf is null)
                    lf = new List<figure>();
                lf.Add(simpleSteps[i, j]);
            }
        }


        private void Food_1(int i, int j)
        {
            if (simpleSteps[i, j].Index == ((hod == 1) ? 2 : 1))
                if (i + 1 < mapSize && j - 1 >= 0)
                {
                    PossibleMove(i + 1, j - 1);
                    if (simpleSteps[i + 1, j - 1].button.Background == Brushes.Green)
                    {
                        if (Meal is null)
                            Meal = new List<figure>();
                        Meal.Add(simpleSteps[i, j]);
                    }
                }
        }
        private void Food_2(int i, int j)
        {
            if (simpleSteps[i, j].Index == ((hod == 1) ? 2 : 1))
                if (i - 1 >= 0 && j - 1 >= 0)
                {
                    PossibleMove(i - 1, j - 1);
                    if (simpleSteps[i - 1, j - 1].button.Background == Brushes.Green)
                    {
                        if (Meal is null)
                            Meal = new List<figure>();
                        Meal.Add(simpleSteps[i, j]);
                    }
                }
        }
        private void Food_3(int i, int j)
        {
            if (simpleSteps[i, j].Index == ((hod == 1) ? 2 : 1))
                if (i - 1 >= 0 && j + 1 < mapSize)
                {
                    PossibleMove(i - 1, j + 1);
                    if (simpleSteps[i - 1, j + 1].button.Background == Brushes.Green)
                    {
                        if (Meal is null)
                            Meal = new List<figure>();
                        Meal.Add(simpleSteps[i, j]);
                    }
                }
        }
        private void Food_4(int i, int j)
        {
            if (simpleSteps[i, j].Index == ((hod == 1) ? 2 : 1))

                if (i + 1 < mapSize && j + 1 < mapSize)
                {
                    PossibleMove(i + 1, j + 1);
                    if (simpleSteps[i + 1, j + 1].button.Background == Brushes.Green)
                    {
                        if (Meal is null)
                            Meal = new List<figure>();
                        Meal.Add(simpleSteps[i, j]);
                    }
                }
        }
        private void Food_5(int i, int j, bool dama)
        {
            if (dama)
            {
                for (var a = 1; a < 8; a++)
                {
                    if (i + a < mapSize && j - a >= 0)
                    {
                        if (simpleSteps[i + a, j - a].Index == hod)
                            break;
                        PossibleMove(i + a, j - a);
                        Food_1(i + a, j - a);
                        if (i + a + 1 < mapSize && j - a - 1 >= 0)
                        {
                            if (simpleSteps[i + a, j - a].button.Background != Brushes.Green &&
                                simpleSteps[i + a + 1, j - a - 1].button.Background != Brushes.Green)
                                break;
                            if (simpleSteps[i + a + 1, j - a - 1].button.Background == Brushes.Green)
                            {
                               // if (Meal_D1 is null)
                               //     Meal_D1 = new List<figure>();
                             //   Meal_D1.Add(Meal[Meal.Count - 1]);
                            }
                        }
                    }
                    else
                        break;
                }
                for (var a = 1; a < 8; a++)
                {
                    if (i - a >= 0 && j - a >= 0)
                    {
                        if (simpleSteps[i - a, j - a].Index == hod)
                            break;
                        PossibleMove(i - a, j - a);
                        Food_2(i - a, j - a);
                        if (i - a - 1 >= 0 && j - a - 1 >= 0)
                        {
                            if (simpleSteps[i - a, j - a].button.Background != Brushes.Green &&
                                simpleSteps[i - a - 1, j - a - 1].button.Background != Brushes.Green)
                                break;
                            if (simpleSteps[i - a - 1, j - a - 1].button.Background == Brushes.Green)
                            {
                              ///  if (Meal_D2 is null)
                                 ///   Meal_D2 = new List<figure>();
                               // Meal_D2.Add(Meal[Meal.Count - 1]);
                            }
                        }
                    }
                    else
                        break;
                }
                for (var a = 1; a < 8; a++)
                {
                    if (i - a >= 0 && j + a < mapSize)
                    {
                        if (simpleSteps[i - a, j + a].Index == hod)
                            break;
                        PossibleMove(i - a, j + a);
                        Food_3(i - a, j + a);
                        if (i - a - 1 >= 0 && j + a + 1 < mapSize)
                        {
                            if (simpleSteps[i - a, j + a].button.Background != Brushes.Green &&
                                simpleSteps[i - a - 1, j + a + 1].button.Background != Brushes.Green)
                                break;
                            if (simpleSteps[i - a - 1, j + a + 1].button.Background == Brushes.Green)
                            {
                               // if (Meal_D3 is null)
                               //     Meal_D3 = new List<figure>();
                               // Meal_D3.Add(Meal[Meal.Count - 1]);
                            }
                        }
                    }
                    else
                        break;
                }
                for (var a = 1; a < 8; a++)
                {
                    if (i + a < mapSize && j + a < mapSize)
                    {
                        if (simpleSteps[i + a, j + a].Index == hod)
                            break;
                        PossibleMove(i + a, j + a);
                        Food_4(i + a, j + a);
                        if (i + a + 1 < mapSize && j + a + 1 < mapSize)
                        {
                            if (simpleSteps[i + a, j + a].button.Background != Brushes.Green &&
                                simpleSteps[i + a + 1, j + a + 1].button.Background != Brushes.Green)
                                break;
                            if (simpleSteps[i + a + 1, j + a + 1].button.Background == Brushes.Green)
                            {
                              //  if (Meal_D4 is null)
                              //      Meal_D4 = new List<figure>();
                              //  Meal_D4.Add(Meal[Meal.Count - 1]);
                            }
                        }
                    }
                    else
                        break;
                }
            }
            else
            {
                if (i + 1 < mapSize && j - 1 >= 0)
                {
                    if (hod == 2)
                        PossibleMove(i + 1, j - 1);
                    Food_1(i + 1, j - 1);
                }
                if (i - 1 >= 0 && j - 1 >= 0)
                {
                    if (hod == 2)
                        PossibleMove(i - 1, j - 1);
                    Food_2(i - 1, j - 1);
                }
                if (i - 1 >= 0 && j + 1 < mapSize)
                {
                    if (hod == 1)
                        PossibleMove(i - 1, j + 1);
                    Food_3(i - 1, j + 1);
                }
                if (i + 1 < mapSize && j + 1 < mapSize)
                {
                    if (hod == 1)
                        PossibleMove(i + 1, j + 1);
                    Food_4(i + 1, j + 1);
                }
            }
        }



        private bool Food_6(List<figure> figures, int start, int end)
        {
            bool isFood = false;
            foreach (var m in figures)
                for (int a = start; a < end; a++)
                    if (m.Row == a)
                    {
                        izmenenie(m);
                        isFood = true;
                        break;
                    }
            return isFood;
        }

        private bool Food_7(figure i)
        {
            bool isFood = false;
            //if (i.Dama)
            //{
            //    if (!(Meal_D1 is null))
            //    {
            //        if (b.Row < i.Row && b.Column > i.Column)
            //            isFood = Food_6(Meal_D1, b.Row, i.Row);
            //    }
            //    if (!(Meal_D2 is null))
            //    {
            //        if (b.Row > i.Row && b.Column > i.Column)
            //            isFood = Food_6(Meal_D2, i.Row, b.Row);
            //    }
            //    if (!(Meal_D3 is null))
            //    {
            //        if (b.Row > i.Row && b.Column < i.Column)
            //            isFood = Food_6(Meal_D3, i.Row, b.Row);
            //    }
            //    if (!(Meal_D4 is null))
            //    {
            //        if (b.Row < i.Row && b.Column < i.Column)
            //            isFood = Food_6(Meal_D4, b.Row, i.Row);
            //    }
            //    return isFood;
            //}
            //else
            {
                foreach (var m in Meal)
                    if ((i.Row + 1 == m.Row || i.Row - 1 == m.Row) &&
                        (i.Column + 1 == m.Column || i.Column - 1 == m.Column))
                    {
                        izmenenie(m);
                        return true;
                    }

            }
            return false;
        }




        private void Food_8(figure i)
        {
            otmena();
            Food_5(i.Row, i.Column, i.Dama);
            if (!(Meal is null))
            {
                povtor = true;
                b = i;
                b.button.Background = Brushes.Red;
                if (i.Dama)
                {
                    if (Meal_D1 is null)
                    {
                        for (var m = 0; m < lf.Count;)
                            if (b.Row < lf[m].Row && b.Column > lf[m].Column)
                            {
                                lf[m].Background();
                                lf.RemoveAt(m);
                            }
                            else m++;
                    }
                    else
                    {
                        for (var m = 0; m < lf.Count;)
                            if (b.Row < lf[m].Row && b.Column > lf[m].Column)
                                if (Meal_D1[0].Row > lf[m].Row && Meal_D1[0].Column < lf[m].Column)
                                {
                                    lf[m].Background();
                                    lf.RemoveAt(m);
                                }
                                else
                                    m++;
                            else
                                break;
                    }
                    if (Meal_D2 is null)
                    {
                        for (var m = 0; m < lf.Count;)
                            if (b.Row > lf[m].Row && b.Column > lf[m].Column)
                            {
                                lf[m].Background();
                                lf.RemoveAt(m);
                            }
                            else m++;
                    }
                    else
                    {
                        for (var m = 0; m < lf.Count;)
                            if (b.Row > lf[m].Row && b.Column > lf[m].Column)
                                if (Meal_D2[0].Row < lf[m].Row && Meal_D2[0].Column < lf[m].Column)
                                {
                                    lf[m].Background();
                                    lf.RemoveAt(m);
                                }
                                else
                                    m++;
                            else
                                break;
                    }
                    if (Meal_D3 is null)
                    {
                        for (var m = 0; m < lf.Count;)
                            if (b.Row > lf[m].Row && b.Column < lf[m].Column)
                            {
                                lf[m].Background();
                                lf.RemoveAt(m);
                            }
                            else m++;
                    }
                    else
                    {
                        for (var m = 0; m < lf.Count;)
                            if (b.Row > lf[m].Row && b.Column < lf[m].Column)
                                if (Meal_D3[0].Row < lf[m].Row && Meal_D3[0].Column > lf[m].Column)
                                {
                                    lf[m].Background();
                                    lf.RemoveAt(m);
                                }
                                else
                                    m++;
                            else
                                break;
                    }
                    if (Meal_D4 is null)
                    {
                        for (var m = 0; m < lf.Count;)
                            if (b.Row < lf[m].Row && b.Column < lf[m].Column)
                            {
                                lf[m].Background();
                                lf.RemoveAt(m);
                            }
                            else m++;
                    }
                    else
                    {
                        for (var m = 0; m < lf.Count;)
                            if (b.Row < lf[m].Row && b.Column < lf[m].Column)
                                if (Meal_D4[0].Row > lf[m].Row && Meal_D4[0].Column > lf[m].Column)
                                {
                                    lf[m].Background();
                                    lf.RemoveAt(m);
                                }
                                else
                                    m++;
                            else
                                break;
                    }
                }
                else
                {
                    foreach (var s in lf)
                        if ((i.Row + 1 == s.Row || i.Row - 1 == s.Row) &&
                        (i.Column + 1 == s.Column || i.Column - 1 == s.Column))
                            s.Background();
                }
            }
        }

        private void izmenenie(figure m)
        {
            if (m.Index == 1)
                countWhiteFigure--;
            else
                countBlackFigure--;
            m.Index = 0;
            m.Image();
            m.Dama = false;
        }

        //    public void Init(object sender, RoutedEventArgs e)
        //    {
        //        Step_1(sender, e/*, new GridView()*/);
        //    }
        //    public void Step_1(object sender, RoutedEventArgs e)
        //    {

        //        Button button = sender as Button;
        //        if (button == null)
        //        {
        //            return;
        //        }

        //        if (button.Content != null)
        //        {
        //            //foreach (FrameworkElement Button in grid.Children)
        //            //{
        //            //    if (Button is Button)
        //            //    {
        //            //        for (int i = 0; i <= gridView.Columns.Count; i++)
        //            //        { 
        //            ////            for(int j =0; j <= (int)gridView.GetValue(Grid.RowProperty); j++)
        //            ////            {


        //            //                if (( gridView.Columns[i] )
        //            //                {
        //            //                    return i;
        //            //                }
        //            //           // }
        //            //        }

        //            //    }

        //            if (button.Background != new SolidColorBrush(Colors.Red))
        //                button.Background = new SolidColorBrush(Colors.Red);
        //            if (button.Background == new SolidColorBrush(Colors.Red))
        //            {
        //                button.Background = new SolidColorBrush(Colors.WhiteSmoke);
        //            }
        //        }
        //        else { return; }
        //    }
    }



    class figure
    {
        private const int cellSize = 50;
        public Button button;
        public bool Dama;
        public int Index;
        public int Row;
        public int Column;

        public figure(int row, int column, int index, bool dama = false)
        {
            Index = index;
            Row = row;
            Column = column;
            Dama = dama;
            button = new Button();
            Background();
            Image();
        }

        public void Image()
        {
            if (Index == 1)
            {
                if (Dama)
                    button.Content = "Da⚪";
                else
                    button.Content = "⚪";
            }
            else if (Index == 2)
            {
                if (Dama)
                    button.Content = "Da⚫";
                else
                    button.Content = "⚫";
            }
            else
                button.Content = "";
        }

        public void Background()
        {
            if (Row % 2 == 0)
            {
                if (Column % 2 == 0)
                    button.Background = Brushes.WhiteSmoke;
                else
                    button.Background = Brushes.SaddleBrown;
            }
            else
            {
                if (Column % 2 == 1)
                    button.Background = Brushes.WhiteSmoke;
                else
                    button.Background = Brushes.SaddleBrown;
            }
        }

        public override string ToString()
        {
            return $"{Row} {Column} {Index} {Dama}";
        }
    }
}





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
using System.Windows.Threading;

namespace Game_Of_Life
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.3);
            timer.Tick += Timer_Tick;
        }

        const int celulasLongitudinal = 50;
        const int celulasLatitudinal = 40;
        Rectangle[,] campos = new Rectangle[celulasLatitudinal,celulasLongitudinal];
        DispatcherTimer timer = new DispatcherTimer();

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
           

            for (int i = 0; i < celulasLatitudinal; i++)
            {
                for (int j = 0; j < celulasLongitudinal; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = Campo.ActualWidth / celulasLongitudinal - 1.5;
                    r.Height = Campo.ActualHeight / celulasLatitudinal - 1.5;
                    r.Fill = Brushes.DarkGray;
                    Campo.Children.Add(r);
                    Canvas.SetLeft(r, j * Campo.ActualWidth / celulasLongitudinal);
                    Canvas.SetTop(r,i * Campo.ActualHeight / celulasLatitudinal);
                    r.MouseDown += R_MouseDown;

                    campos[i, j] = r;
                }
            }

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int[,] Qtdvizinhos = new int[celulasLatitudinal, celulasLongitudinal];
            for (int i = 0; i < celulasLatitudinal; i++)
            {
                for (int j = 0; j < celulasLongitudinal; j++)
                {

                    int iacima = i - 1;

                    if (iacima < 0)
                    {
                        iacima = celulasLatitudinal - 1;
                    }

                    int ientreCelulas = i + 1;

                    if (ientreCelulas >= celulasLatitudinal)
                    {
                        ientreCelulas = 0;
                    }

                    int jLinks = j - 1;

                    if (jLinks < 0)
                    {
                        jLinks = celulasLongitudinal - 1;
                    }

                    int jDireito = j + 1;

                    if (jDireito >= celulasLongitudinal)
                    {
                        jDireito = 0;
                    }

                    int vizinhos = 0;
                    //campos da matrix.

                    if (campos[iacima, jLinks].Fill == Brushes.OrangeRed)
                    {
                        vizinhos++;
                    }

                    if (campos[iacima, j].Fill == Brushes.OrangeRed)
                    {
                        vizinhos++;
                    }

                    if (campos[iacima, jDireito].Fill == Brushes.OrangeRed)
                    {
                        vizinhos++;
                    }

                    if (campos[i, jLinks].Fill == Brushes.OrangeRed)
                    {
                        vizinhos++;
                    }

                    if (campos[i, jDireito].Fill == Brushes.OrangeRed)
                    {
                        vizinhos++;
                    }

                    if (campos[ientreCelulas, jLinks].Fill == Brushes.OrangeRed)
                    {
                        vizinhos++;
                    }

                    if (campos[ientreCelulas, j].Fill == Brushes.OrangeRed)
                    {
                        vizinhos++;
                    }

                    if (campos[ientreCelulas, jDireito].Fill == Brushes.OrangeRed)
                    {
                        vizinhos++;
                    }


                    Qtdvizinhos[i, j] = vizinhos;
                }

            }

            for (int i = 0; i < celulasLatitudinal; i++)
            {
                for (int j = 0; j < celulasLongitudinal; j++)
                {
                    if (Qtdvizinhos[i, j] < 2 || Qtdvizinhos[i, j] > 3)
                    {
                        campos[i, j].Fill = Brushes.DarkGray;
                    }
                    else if (Qtdvizinhos[i, j] == 3)
                    {
                        campos[i, j].Fill = Brushes.OrangeRed;
                    }
                }
            }
        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Rectangle) sender).Fill =
                (((Rectangle) sender).Fill == Brushes.DarkGray) ? Brushes.OrangeRed : Brushes.DarkGray;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
    }
}

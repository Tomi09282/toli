using System.Windows;
using System.Windows.Controls;

namespace toli
{
    public partial class MainWindow : Window
    {
        public Button uresGomb;

        public MainWindow()
        {
            InitializeComponent();
            InicializalGrid();
            GombokGeneralasa();
        }

        private void InicializalGrid()
        {
            // Grid inicializálása 3x3 méretűre
            for (int i = 0; i < 3; i++)
            {
                ToliGrid.RowDefinitions.Add(new RowDefinition());
                ToliGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        int db = 1;
        private void GombokGeneralasa()
        {
            // Gombok generálása a rácsban
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button gomb = new Button();
                    if (i == 2 && j == 2)
                    {
                        uresGomb = gomb;
                        gomb.Content = "";
                    }
                    else
                    {
                        gomb.Content = $"{db++}";
                        gomb.Click += Gomb_Kattintas;
                    }
                    gomb.Margin = new Thickness(5);
                    ToliGrid.Children.Add(gomb);
                    Grid.SetRow(gomb, i);
                    Grid.SetColumn(gomb, j);
                }
            }
        }

        int lepesszam = 1;
        public void Gomb_Kattintas(object sender, RoutedEventArgs e)
        {
            // Kattintott gomb megkeresése
            Button kattintottGomb = sender as Button;
            int sor = (int)kattintottGomb.GetValue(Grid.RowProperty);
            int oszlop = (int)kattintottGomb.GetValue(Grid.ColumnProperty);

            // Ha a kattintott gomb mellett van az üres gomb
            if ((sor == (int)uresGomb.GetValue(Grid.RowProperty) && Math.Abs(oszlop - (int)uresGomb.GetValue(Grid.ColumnProperty)) == 1) ||
                (oszlop == (int)uresGomb.GetValue(Grid.ColumnProperty) && Math.Abs(sor - (int)uresGomb.GetValue(Grid.RowProperty)) == 1))
            {
                // Gomb és üres gomb pozíciójának cseréje
                int tempSor = (int)uresGomb.GetValue(Grid.RowProperty);
                int tempOszlop = (int)uresGomb.GetValue(Grid.ColumnProperty);
                Grid.SetRow(kattintottGomb, tempSor);
                Grid.SetColumn(kattintottGomb, tempOszlop);
                Grid.SetRow(uresGomb, sor);
                Grid.SetColumn(uresGomb, oszlop);
                lblDB.Content = $"Lépések száma: {lepesszam++}";

                // Győzelem ellenőrzése minden lépés után
                EllenorizGyozelmet();
            }
        }


        private bool EllenorizGyozelmet()
        {
            string elvartSorrend = "123485760";
            string aktualisSorrend = "";

            // Végigmegyünk a rácsban lévő gombokon a generálás sorrendjében
            for (int i = 0; i < ToliGrid.Children.Count; i++)
            {
                if (ToliGrid.Children[i] is Button gomb)
                {
                    if (gomb != uresGomb)
                    {
                        aktualisSorrend += gomb.Content.ToString();
                    }
                    else
                    {
                        aktualisSorrend += "0"; // Helykitöltő az üres gombnak
                    }
                }
            }

            // Ellenőrizzük, hogy az aktuális sorrend megegyezik-e az elvárt sorrenddel
            if (aktualisSorrend == elvartSorrend)
            {
                MessageBox.Show("nyertel");
                return true;
            }

            return false;
        }
    }
}

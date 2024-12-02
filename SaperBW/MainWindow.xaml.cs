using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SaperBW;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private int rozmiar = 10;
    private int iloscBomb = 10;
    private int iloscFlag;
    public MainWindow()
    {
        InitializeComponent();
        iloscFlag = iloscBomb;
        
        for (int i = 0; i < 10; i++)
        {
            plansza.RowDefinitions.Add(new RowDefinition());
            plansza.ColumnDefinitions.Add(new ColumnDefinition());
        }
        for(int i=0;i<rozmiar;i++)
        for (int j = 0; j < rozmiar; j++)
        {
            Przycisk przycisk = new Przycisk()
            {
                Wartosc = 0,
                FontSize = 30,
                Background = Brushes.Black,
                Foreground = Brushes.LightGray
            };
            przycisk.Click += PrzyciskOnClick;
            przycisk.MouseRightButtonDown += PrzyciskOnPrawyKlik;
            Grid.SetRow(przycisk, i);
            Grid.SetColumn(przycisk, j);
            plansza.Children.Add(przycisk);
        }
        
        RozstawBomby(iloscBomb);
        // PokazPlansze();
    }

    private void PrzyciskOnPrawyKlik(object sender, MouseButtonEventArgs e)
    {
        Przycisk przycisk = (Przycisk)sender;
        int x = Grid.GetRow(przycisk);
        int y = Grid.GetColumn(przycisk);
        Flaga(x, y);
    }

    private void PrzyciskOnClick(object sender, RoutedEventArgs e)
    {
        Przycisk przycisk = (Przycisk)sender;
        int x = Grid.GetRow(przycisk);
        int y = Grid.GetColumn(przycisk);
        OdkryjPrzycisk(x, y);
    }

    private void OdkryjPrzycisk(int x, int y)
    {
        Przycisk przycisk = WyszukajPrzycisk(x, y);
        if(przycisk.Content!=null)
            return;
        if (przycisk.Wartosc == 10)
        {
            MessageBox.Show("Przegrales");
            PokazPlansze();
        }
        else
        {
            przycisk.Content = przycisk.Wartosc;
            if (przycisk.Wartosc != 0)
                return;
            for (int i = x - 1; i <= x + 1; i++)
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i >= 0 && i < rozmiar && j >= 0 && j < rozmiar)
                {
                    Przycisk sasiad = WyszukajPrzycisk(i, j);
                    if (sasiad.Content == null)
                    {
                        sasiad.Content = sasiad.Wartosc;
                        if (sasiad.Wartosc == 0)
                            OdkryjPrzycisk(i,j); 
                    }
                }
            }
        }
    }

    private void Flaga(int x, int y)
    {
        Przycisk przycisk = WyszukajPrzycisk(x, y);
        if (przycisk.Content == null)
        {
            if (iloscFlag > 0)
            {
                przycisk.Content = "🏳️‍⚧️️";
                iloscFlag--;
            }
        }
        else
        {
            if (przycisk.Content.ToString() == "🏳️‍⚧️")
            {
                przycisk.Content = null;
                iloscFlag++;
            }
        }
    }
    
    private Przycisk WyszukajPrzycisk(int x, int y)
    {
        var przycisk = (Przycisk)plansza.Children
            .Cast<Przycisk>()
            .First(e => Grid.GetRow(e) == x && Grid.GetColumn(e) == y);
        return przycisk;
    }
    private void RozstawBomby(int ilosc)
    {
        Random random = new Random();
        while(ilosc>0)
        {
            int x = random.Next(rozmiar);
            int y = random.Next(rozmiar);
            Przycisk przycisk = WyszukajPrzycisk(x, y);
            if (przycisk.Wartosc == 0)
            {
                przycisk.Wartosc = 10;
                ilosc--;
                ZliczBomby(x,y);
            }
        }
    }

    private void ZliczBomby(int x, int y)
    {
        for(int i = x - 1;i<=x+1;i++) 
            for(int j=y-1;j<=y+1;j++)
            {
                if (i >= 0 && i < rozmiar && j >= 0 && j < rozmiar)
                {
                    Przycisk przycisk = WyszukajPrzycisk(i, j);
                    if (przycisk.Wartosc != 10)
                    {
                        przycisk.Wartosc++;
                    }
                }
            }
    }
    
    private void PokazPlansze()
    {
        plansza.Children.Cast<Przycisk>().ToList().ForEach(przycisk =>
        {
            if (przycisk.Wartosc == 10)
            {
                przycisk.Content = "🎅🏿";
            }
            else
            {
                przycisk.Content = przycisk.Wartosc;
            }
        });
    }
}
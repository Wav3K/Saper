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
    public MainWindow()
    {
        InitializeComponent();

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
            Grid.SetRow(przycisk, i);
            Grid.SetColumn(przycisk, j);
            plansza.Children.Add(przycisk);
        }
        
        RozstawBomby(iloscBomb);
        PokazPlansze();
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
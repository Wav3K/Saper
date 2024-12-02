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
                FontSize = 50,
                Background = Brushes.LightGray,
            };
            Grid.SetRow(przycisk, i);
            Grid.SetColumn(przycisk, j);
            plansza.Children.Add(przycisk);
        }
    }
}
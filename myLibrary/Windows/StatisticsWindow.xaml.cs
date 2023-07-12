using myLibrary.myLibraryClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace myLibrary.Windows
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        booksClass book = new booksClass();
        accountsClass user = new accountsClass();
        public bool IsAdministrator { get; set; }
        public StatisticsWindow()
        {
            InitializeComponent();
            var chartData = new List<KeyValuePair<string, int>>();
            var books = book.GetBooks();
            var chartDataReaders= new List<KeyValuePair<string, int>>();
            var accounts = user.GetAllUsers();
            //accounts = accounts.Where(a => a.IsAdministrator == false).ToList();

;            for (int i = 1; i<= 12; i++)
            {
                var booksPerMonthNumber = books.Where(b => b.CreationDate.Month == i).Count();
                var accountsPerMonthNumber= accounts.Where(b => b.CreationDate.Month == i).Count();
                chartData.Add(new KeyValuePair<string, int>(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i), booksPerMonthNumber));
                chartDataReaders.Add(new KeyValuePair<string, int>(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i), accountsPerMonthNumber));
            }
            ((ColumnSeries)mcChart.Series[0]).ItemsSource = chartData;
            ((ColumnSeries)mcChart2.Series[0]).ItemsSource = chartDataReaders;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(backButton))
            {
                if (IsAdministrator)
                {
                    AdministratorWindow wnd = new AdministratorWindow();
                    Visibility = Visibility.Hidden;
                    wnd.Show();
                }else
                {
                    ReaderWindow wnd = new ReaderWindow();
                    Visibility = Visibility.Hidden;
                    wnd.Show();
                }
            }
        }

    }
}

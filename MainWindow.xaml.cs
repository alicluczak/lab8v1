using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace lab8
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnWczytaj_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection poloczenie = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Sklep;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                poloczenie.Open();
                SqlCommand polecenie = new SqlCommand("SELECT * FROM Towary", poloczenie);
                SqlDataReader czytnik = polecenie.ExecuteReader();
                while (czytnik.Read())
                {
                    lbxLista.Items.Add($"{czytnik["TowarId"]},{czytnik["Nazwa"]},{czytnik["Cena"]:C},{czytnik["Ilosc"]}");
                }
                czytnik.Close();
                SqlCommand wys = new SqlCommand("SELECT AVG(cena) FROM Towary", poloczenie);
                lblWysw.Content = ((decimal)wys.ExecuteScalar()).ToString("C");
                poloczenie.Close();
            }
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            lbxLista.Items.Clear();
            using (SqlConnection poloczenie = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Sklep;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                poloczenie.Open();
                SqlCommand polecenie = new SqlCommand($"INSERT INTO Towary VALUES(@Nazwa,@Cena,@Ilosc)", poloczenie);
                // command.Parameters.Clear;
                polecenie.Parameters.Add("Nazwa", SqlDbType.VarChar);
                polecenie.Parameters["Nazwa"].Value = txtNazwa.Text;

                polecenie.Parameters.Add("Cena", SqlDbType.SmallMoney);
                polecenie.Parameters["Cena"].Value = txtCena.Text;

                polecenie.Parameters.Add("Ilosc", SqlDbType.Int);
                polecenie.Parameters["Ilosc"].Value = txtIlosc.Text;

                polecenie.ExecuteScalar(); // albo com.ExecuteNonQuery();

                btnWczytaj_Click(sender, e);
            }
        }
    }
}

using _2306calisma.YETKILENDIRME.BUSINESS;
using _2306calisma.YETKILENDIRME.CONNECTION;
using _2306calisma.YETKILENDIRME.ENTITIES;
using _2306calisma.YETKILENDIRME.LISTS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace _2306calisma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        YetkiAta1 kulad = new YetkiAta1();
        YetkiliGiris gir = new YetkiliGiris();

        public MainWindow()
        {
            InitializeComponent();

            Yetkilendirme yetkilendirme = new Yetkilendirme();

            YetkiAtaFill.Doldur(Yetkilendirme.YetkiAtas2);

            Dgv_Deneme.ItemsSource = null;
            Dgv_Deneme.ItemsSource = Yetkilendirme.YetkiAtas2;

            gir.kullanici_adi = Properties.Settings.Default.KULLANICI_ADI;
            gir.sifre = Properties.Settings.Default.KULLANICI_SIFRE;

        }

        public void tabloac_click(object sender, RoutedEventArgs e)
        {

            //CHECKBOX'A GÖRE YETKİ ATAMA
            foreach (var item in Yetkilendirme.YetkiAtas2)
            {
                if (item.TABLO_ADI == "KAPİ")
                {
                    MessageBox.Show(item.DUZENLEME.ToString());
                }
            }
        }
        private void yetki_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection SqlConnection = new SqlConnection(YetkilendirmeConnection.GetConnectionString(gir));
            SqlConnection.Open();
            foreach (var item in Yetkilendirme.YetkiAtas2)
            {
                if (item.TABLO_ADI == "KAPİ")
                {
                    string A = "kapi";
                    if (item.DUZENLEME && item.ERISIM)
                    {
                        try
                        {
                            string queryString1 = "GRANT SELECT ON " + A + " TO " + TB_ad.Text + " GRANT SELECT ON " + A + " TO " + TB_ad.Text;
                            //string queryString2 = ;
                            SqlCommand command1 = new SqlCommand(queryString1, SqlConnection);
                            //SqlCommand command2 = new SqlCommand(queryString2, SqlConnection);
                            //command2.ExecuteNonQuery();
                            command1.ExecuteNonQuery();
                            MessageBox.Show(TB_ad.Text + " KULLANICISINA " + A + " TABLOSUNA DÜZENLEME VE ERİŞİM YETKİSİ VERİLDİ");
                        }
                        catch
                        {
                            MessageBox.Show("BÖYLE BİR KULLANICI YOK");
                        }
                    }
                    else if (item.ERISIM)
                    {
                        try
                        {
                            string queryString1 = "GRANT SELECT ON " + A + " TO " + TB_ad.Text;
                            SqlCommand command1 = new SqlCommand(queryString1, SqlConnection);
                            command1.ExecuteNonQuery();
                            MessageBox.Show(TB_ad.Text + " KULLANICISINA " + A + " TABLOSUNA ERISIM YETKİSİ VERİLDİ");
                        }
                        catch
                        {
                            MessageBox.Show("BÖYLE BİR KULLANICI YOK");
                        }
                    }
                    else if (item.DUZENLEME)
                    {
                        try
                        {

                            string queryString = "GRANT UPDATE ON " + A + " TO " + TB_ad.Text;
                            SqlCommand command = new SqlCommand(queryString, SqlConnection);
                            command.ExecuteNonQuery();
                            MessageBox.Show(TB_ad.Text + " KULLANICISINA " + A + " TABLOSUNU DÜZENLEME YETKİSİ VERİLDİ");
                        }
                        catch
                        {
                            MessageBox.Show("BÖYLE BİR KULLANICI YOK");
                        }
                    }
                    SqlConnection.Close();
                }
            }
        }
        private void BT_Göster_Click(object sender, RoutedEventArgs e)
        {
            YetkiGösterFill.YetkiGösterDoldur(Yetkilendirme.YetkiGösters, TB_ad.Text);
          
            Dgv_Göster.ItemsSource = null;
            Dgv_Göster.ItemsSource = Yetkilendirme.YetkiGösters;

        }

        private void Dgv_Deneme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
     
    

    








using System;
using System.Windows;
using System.Data;


namespace Calculator
{
    /// <summary>
    /// Numeronäppäimissä käytetään perinteistä click - eventtiä, missä näppäintä painettaessa merkkijonon perään lisätään haluttu numero
    /// Operaattoreissa *,/,-,+ kutsutaan funktiota, missä tarkistetaan merkkijonon viimeinen merkki. Jos merkkijono päättyy operaattoriin, muutetaan se
    /// Muussa tapauksessa lisätään operaattori merkkijonon perään
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnTyhjaa_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.Clear();
        }

        private void BtnJako_Click(object sender, RoutedEventArgs e)
        {
            if (OnkoViimeinenMerkkiOperaattoriTaiPilkku(TxtNaytto.Text))
            {
                TxtNaytto.Text = MuunnaViimeinenMerkki(TxtNaytto.Text, "/");
            }
            else
            {
                TxtNaytto.AppendText("/");
            }
        }

        private void BtnKerto_Click(object sender, RoutedEventArgs e)
        {
            if (OnkoViimeinenMerkkiOperaattoriTaiPilkku(TxtNaytto.Text))
            {
                TxtNaytto.Text = MuunnaViimeinenMerkki(TxtNaytto.Text, "*");
            }
            else
            {
                TxtNaytto.AppendText("*");
            }
        }

        private void BtnPyyhi_Click(object sender, RoutedEventArgs e)
        {
            //Poistetaan stringin viimeisin merkki
            TxtNaytto.Text = TxtNaytto.Text.Remove(Convert.ToInt32(TxtNaytto.Text.Length) - 1);
        }

        private void Btn7_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("7");
        }

        private void Btn8_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("8");
        }

        private void Btn9_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("9");
        }

        private void BtnMiinus_Click(object sender, RoutedEventArgs e)
        {
            if (OnkoViimeinenMerkkiOperaattoriTaiPilkku(TxtNaytto.Text))
            {
                TxtNaytto.Text = MuunnaViimeinenMerkki(TxtNaytto.Text, "-");
            }
            else
            {
                TxtNaytto.AppendText("-");
            }
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("4");
        }

        private void Btn5_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("5");
        }

        private void Btn6_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("6");
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            if (OnkoViimeinenMerkkiOperaattoriTaiPilkku(TxtNaytto.Text))
            {
                TxtNaytto.Text = MuunnaViimeinenMerkki(TxtNaytto.Text, "+");
            }
            else
            {
                TxtNaytto.AppendText("+");
            }
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("1");
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("2");
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("3");
        }


        private void Btn0_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("0");
        }

        private void BtnPilkku_Click(object sender, RoutedEventArgs e)
        {
            //Jos merkkijonossa viimeisenä on operaattori tai pilkku, ei tehdä mitään, sillä tästä tulee virhe. 
            //Jos viimeinen merkki on numero, lisätään pilkku
            if (OnkoViimeinenMerkkiOperaattoriTaiPilkku(TxtNaytto.Text) == false)
            {
                TxtNaytto.AppendText(",");
            }
        }

        //Käytettäessä +,-,*,/ tai pilkkua tarkistetaan, onko viimeiseksi syötetty merkki jokin näistä operaattoreista. 
        private static bool OnkoViimeinenMerkkiOperaattoriTaiPilkku(string txt)
        {
            bool retval;

            //Tarkistetaan onko viimeinen merkki operaattori
            if (txt.EndsWith("/") || txt.EndsWith("*") || txt.EndsWith("-") || txt.EndsWith("+") || txt.EndsWith(","))
            {
                retval = true;
            }
            else
            {
                retval = false;
            }
            return retval;
        }

        //Muutetaan merkkijonon viimeinen merkki halutuksi operaattoriksi tarvittaessa
        private static string MuunnaViimeinenMerkki(string txt, string operaattori)
        {
            string retval;

            retval = txt.Substring(0, txt.Length - 1) + operaattori;

            return retval;
        }

        //Itse laskutoimituksen laskeminen
        //Lähde https://stackoverflow.com/questions/21950093/string-calculator
        private static string TeeLaskuToimitus(string laskutoimitus)
        {
            string retval = "";

            try
            {
                retval = new DataTable().Compute(laskutoimitus, null).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Virheellinen laskutoimitus");
            }
            return retval;
        }
        
        private void BtnYhtaKuin_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.Text = TeeLaskuToimitus(TxtNaytto.Text);
        }
    }
}

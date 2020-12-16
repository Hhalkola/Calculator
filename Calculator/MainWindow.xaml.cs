using System;
using System.Windows;
using System.Data;
using System.Text;
using System.Linq;

namespace Calculator
{
    /// <summary>
    /// 
    /// Tehtävän aloitus:
    /// Tein WPF:lla yksinkertaisen käyttöliittymän, missä käytin mallina Windowsin ja Android puhelimen omaa laskinta.
    /// 
    /// Varsinainen laskimen ohjelmointi:
    /// Ennen varsinaista ohjelmointia otin selvää, miten stringiä pystyy käyttämään laskukaavana, Googlesta hakusanat "C# string calculator etc."
    /// Tähän löytyi seuraava lähde: https://stackoverflow.com/questions/21950093/string-calculator
    /// Koska näin pystyi tekemään, ajatuksena oli, että käytetään jo näytöllä olevaa tekstiä, ja aina lisätään numero tai operaattori stringin perään.
    /// Tässä käytettiin Buttonin @Click - eventia. Eventin sisällä käytettiin .AppendText() - funktiota, mikä lisää syötetyn merkin stringin perään.
    /// 
    /// Jos viimeinen merkki on jokin operaattori (+,-,/,*) tai pilkku, ei anneta siihen lisätä perään toista operaattoria, vaan
    /// muutetaan tämä viimeinen merkki sen mukaisesti, mitä käyttäjä painoi. Tämä estää sen, että syöte olisi esimerkiksi 123+-*.
    /// 
    /// = -nappia painettaessa kutsutaan funktiota, joka laskee itse laskun ja näyttää käyttäjälle vastauksen, mikäli syöte on validi laskukaava. 
    /// Jos syöte ei ole validi, näytetään MessageBox, missä huomautetaan virheellisestä syötteestä.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Numeroiden 0-9 - click-eventit
        private void Btn9_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("9");
        }
        private void Btn8_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("8");
        }
        private void Btn7_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("7");
        }
        private void Btn6_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("6");
        }
        private void Btn5_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("5");
        }
        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("4");
        }
        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("3");
        }
        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("2");
        }
        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("1");
        }
        private void Btn0_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.AppendText("0");
        }
        #endregion

        #region Operaattoreiden click-eventit
        private void BtnMiinus_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.Text = LisaaTaiMuunnaOperaattori("-", TxtNaytto.Text);
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.Text = LisaaTaiMuunnaOperaattori("+", TxtNaytto.Text);
        }

        private void BtnKerto_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.Text = LisaaTaiMuunnaOperaattori("*", TxtNaytto.Text);
        }
        
        private void BtnJako_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.Text = LisaaTaiMuunnaOperaattori("/", TxtNaytto.Text);
        }
        #endregion

        #region Muut eventit
        private void BtnPyyhi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Estetään virhetilanne siinä vaiheessa, kun painetaan pyyhi - nappia merkkijonon ollessa jo tyhjä
                if(TxtNaytto.Text.Length != 0)
                {
                    //Poistetaan stringin viimeinen merkki
                    TxtNaytto.Text = TxtNaytto.Text.Remove(Convert.ToInt32(TxtNaytto.Text.Length) - 1);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            
        }
        private void BtnTyhjaa_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.Clear();
        }
        private void BtnPilkku_Click(object sender, RoutedEventArgs e)
        {
            //Jos merkkijonossa viimeisenä merkkinä on operaattori tai pilkku, ei tehdä mitään, sillä tästä tulee virhe laskua laskettaessa. 
            //Jos viimeinen merkki on numero, lisätään pilkku
            if (OnkoViimeinenMerkkiOperaattoriTaiPilkku(TxtNaytto.Text))
            {
                TxtNaytto.Text = MuunnaViimeinenMerkki(TxtNaytto.Text, ".");
            }
            else
            {
                TxtNaytto.AppendText(".");
            }
        }
        //Tänne tullaan = - nappia painettaessa
        private void BtnYhtaKuin_Click(object sender, RoutedEventArgs e)
        {
            TxtNaytto.Text = TeeLaskuToimitus(TxtNaytto.Text);
        }
        #endregion

        #region Funktiot

        //Käytettäessä +,-,*,/ tai pilkkua tarkistetaan, onko viimeiseksi syötetty merkki jokin näistä operaattoreista. 
        //Lisätään operaattorit Arrayhin. Näin ohjelmistoa on tarvittaessa helpompi laajentaa (ts. jos tulee lisää uusia operaattoreita)
        //https://stackoverflow.com/questions/2796891/how-to-use-string-endswith-to-test-for-multiple-endings
        private static bool OnkoViimeinenMerkkiOperaattoriTaiPilkku(string txt)
        {
            string[] operaattorit = { "+", "-", "*", "/", "." };
            bool retval = operaattorit.Any(x => txt.EndsWith(x));

            return retval;
        }

        //Muutetaan merkkijonon viimeinen merkki halutuksi operaattoriksi tarvittaessa
        private static string MuunnaViimeinenMerkki(string txt, string operaattori)
        {
            string retval = txt.Substring(0, txt.Length - 1) + operaattori;

            return retval;
        }

        //Laskutoimituksen laskeminen
        private static string TeeLaskuToimitus(string laskukaava)
        {
            //Määritetään paluuarvoksi syötetty laskutoimitus. Jos tulee virhe, näytölle jää syötetty laskutoimitus valmiiksi
            string retval = laskukaava;

            try
            {
                //Lähde https://stackoverflow.com/questions/21950093/string-calculator
                retval = new DataTable().Compute(laskukaava, null).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Virheellinen laskutoimitus");
            }
            return retval;
        }

        //Tarkistetaan päättyykö nykyinen syöte näytöllä operaattoriin tai pilkkuun
        //Jos päättyy, muutetaan viimeinen merkki käyttäjän painamaksi merkiksi
        //Jos ei, lisätään se
        private static string LisaaTaiMuunnaOperaattori(string operaattori, string laskukaava)
        {
            if (OnkoViimeinenMerkkiOperaattoriTaiPilkku(laskukaava))
            {
                laskukaava = MuunnaViimeinenMerkki(laskukaava, operaattori);
            }
            else
            {
                laskukaava = laskukaava + operaattori;
            }
            return laskukaava;
        }
        #endregion

    }
}

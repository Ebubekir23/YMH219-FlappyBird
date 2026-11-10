using FlappyBird.Models;
using FlappyBird.Services;

namespace FlappyBird
{
    public partial class AnaForm : Form
    {
        private readonly Oyun oyun = new Oyun();
        private int enYuksekSkor;
        private string skorUyarisi = "";

        public AnaForm()
        {
            InitializeComponent();
            oyunPaneli.OyunuBagla(oyun);
            enYuksekSkor = SkorDeposu.Oku();
            BilgiyiGuncelle();
            oyunZamanlayici.Start();
        }

        private void oyunZamanlayici_Tick(object? sender, EventArgs e)
        {
            OyunDurumu oncekiDurum = oyun.Durum;
            oyun.Guncelle();

            if (oncekiDurum == OyunDurumu.Oynaniyor && oyun.Durum == OyunDurumu.Bitti)
                OyunBitti();

            BilgiyiGuncelle();
            oyunPaneli.Invalidate();
        }

        private void OyunBitti()
        {
            if (oyun.Skor <= enYuksekSkor)
                return;

            enYuksekSkor = oyun.Skor;
            skorUyarisi = SkorDeposu.Kaydet(enYuksekSkor) ? "" : " (kaydedilemedi)";
        }

        private void AnaForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                oyun.Zipla();
                e.SuppressKeyPress = true; // boşluk tuşu odaklı butona basmasın
            }
            else if (e.KeyCode == Keys.Enter && oyun.Durum == OyunDurumu.Bitti)
            {
                YeniOyun();
                e.SuppressKeyPress = true;
            }
        }

        private void oyunPaneli_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                oyun.Zipla();
        }

        private void btnYeniOyun_Click(object? sender, EventArgs e)
        {
            YeniOyun();
        }

        private void YeniOyun()
        {
            oyun.Sifirla();
            BilgiyiGuncelle();
            oyunPaneli.Invalidate();
        }

        private void BilgiyiGuncelle()
        {
            lblBilgi.Text = $"Skor: {oyun.Skor}  En iyi: {enYuksekSkor}{skorUyarisi}  Seviye: {oyun.Seviye}  Hız: {oyun.Hiz:0.0}";
        }
    }
}

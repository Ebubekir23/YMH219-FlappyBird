using FlappyBird.Models;

namespace FlappyBird
{
    // Oyunu çizen panel. Titreme olmasın diye çift tamponlama açık.
    public class OyunPaneli : Panel
    {
        // Yazılar piksel cinsinden; ekran ölçeği büyüse de oyun alanıyla birlikte ölçeklenir
        private readonly Font skorYazisi = new Font("Segoe UI", 44F, FontStyle.Bold, GraphicsUnit.Pixel);
        private readonly Font baslikYazisi = new Font("Segoe UI", 40F, FontStyle.Bold, GraphicsUnit.Pixel);
        private readonly Font bilgiYazisi = new Font("Segoe UI", 17F, GraphicsUnit.Pixel);
        private readonly StringFormat ortala = new StringFormat { Alignment = StringAlignment.Center };

        private Oyun? oyun;

        public OyunPaneli()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void OyunuBagla(Oyun cizilecekOyun)
        {
            oyun = cizilecekOyun;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (oyun == null)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Oyun alanı 400x600 birim; ekran ölçeği farklıysa panele sığdır
            g.ScaleTransform(Width / (float)Oyun.AlanGenisligi, Height / (float)Oyun.AlanYuksekligi);

            g.Clear(Color.FromArgb(112, 197, 206));
            EngelleriCiz(g);
            ZeminiCiz(g);
            KusuCiz(g);

            g.DrawString(oyun.Skor.ToString(), skorYazisi, Brushes.White, Oyun.AlanGenisligi / 2f, 20, ortala);

            if (oyun.Durum == OyunDurumu.Hazir)
                MesajCiz(g, "Hazır", "Başlamak için Boşluk tuşuna\nveya fareye bas");
            else if (oyun.Durum == OyunDurumu.Bitti)
                MesajCiz(g, "GAME OVER", $"Skor: {oyun.Skor}\n\nYeniden başlamak için Enter\nveya Yeni Oyun butonu");
        }

        private void EngelleriCiz(Graphics g)
        {
            foreach (Engel engel in oyun!.Engeller)
            {
                BoruCiz(g, engel.UstParca);
                BoruCiz(g, engel.AltParca(oyun.ZeminY));
            }
        }

        private static void BoruCiz(Graphics g, RectangleF parca)
        {
            g.FillRectangle(Brushes.ForestGreen, parca);
            g.DrawRectangle(Pens.DarkGreen, parca.X, parca.Y, parca.Width, parca.Height);
        }

        private void ZeminiCiz(Graphics g)
        {
            float zeminY = oyun!.ZeminY;
            g.FillRectangle(Brushes.SandyBrown, 0, zeminY, Oyun.AlanGenisligi, Oyun.ZeminYuksekligi);
            g.FillRectangle(Brushes.YellowGreen, 0, zeminY, Oyun.AlanGenisligi, 10);
        }

        private void KusuCiz(Graphics g)
        {
            RectangleF kus = oyun!.Kus.Sinirlar;
            g.FillEllipse(Brushes.Gold, kus);
            g.DrawEllipse(Pens.DarkOrange, kus);
            g.FillEllipse(Brushes.White, kus.Right - 14, kus.Y + 4, 10, 10);
            g.FillEllipse(Brushes.Black, kus.Right - 9, kus.Y + 7, 4, 4);
            g.FillPolygon(Brushes.OrangeRed, new[]
            {
                new PointF(kus.Right - 2, kus.Y + 12),
                new PointF(kus.Right + 9, kus.Y + 16),
                new PointF(kus.Right - 2, kus.Y + 20)
            });
        }

        private void MesajCiz(Graphics g, string baslik, string bilgi)
        {
            using (Brush perde = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                g.FillRectangle(perde, 30, 300, Oyun.AlanGenisligi - 60, 180);

            // Kutu kuşun başlangıç yüksekliğinin altında, kuş görünür kalsın
            g.DrawString(baslik, baslikYazisi, Brushes.White, Oyun.AlanGenisligi / 2f, 312, ortala);
            g.DrawString(bilgi, bilgiYazisi, Brushes.White, Oyun.AlanGenisligi / 2f, 368, ortala);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                skorYazisi.Dispose();
                baslikYazisi.Dispose();
                bilgiYazisi.Dispose();
                ortala.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

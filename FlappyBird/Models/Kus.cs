namespace FlappyBird.Models
{
    public class Kus
    {
        public const int Genislik = 34;
        public const int Yukseklik = 26;

        private const float YerCekimi = 0.5f;
        private const float ZiplamaHizi = -8f;
        private const float MaksDusmeHizi = 10f;

        public float X { get; }
        public float Y { get; private set; }
        public float DikeyHiz { get; private set; }

        public Kus(float x, float y)
        {
            X = x;
            Y = y;
        }

        public RectangleF Sinirlar
        {
            get { return new RectangleF(X, Y, Genislik, Yukseklik); }
        }

        public void Zipla()
        {
            DikeyHiz = ZiplamaHizi;
        }

        // Her karede yer çekimi hızı artırır, hız da konumu değiştirir
        public void Guncelle()
        {
            DikeyHiz = Math.Min(DikeyHiz + YerCekimi, MaksDusmeHizi);
            Y += DikeyHiz;

            if (Y < 0)
            {
                Y = 0;
                DikeyHiz = 0;
            }
        }
    }
}

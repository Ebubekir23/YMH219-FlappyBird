namespace FlappyBird.Models
{
    // Ortasında kuşun geçeceği boşluk olan üst ve alt boru çifti
    public class Engel
    {
        public const int Genislik = 70;
        public const int BoslukYuksekligi = 160;

        public float X { get; private set; }
        public float BoslukY { get; private set; } // boşluğun üst kenarı
        public bool Gecildi { get; set; }

        public Engel(float x, float boslukY)
        {
            X = x;
            BoslukY = boslukY;
        }

        public bool EkranDisinda
        {
            get { return X + Genislik < 0; }
        }

        public RectangleF UstParca
        {
            get { return new RectangleF(X, 0, Genislik, BoslukY); }
        }

        public RectangleF AltParca(float zeminY)
        {
            float ust = BoslukY + BoslukYuksekligi;
            return new RectangleF(X, ust, Genislik, zeminY - ust);
        }

        public void Hareket(float hiz)
        {
            X -= hiz;
        }

        public void YenidenKonumla(float x, float boslukY)
        {
            X = x;
            BoslukY = boslukY;
            Gecildi = false;
        }
    }
}

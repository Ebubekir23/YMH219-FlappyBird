namespace FlappyBird.Models
{
    // Oyunun tüm kuralları burada; arayüz sadece çizer ve tuşları iletir
    public class Oyun
    {
        public const int AlanGenisligi = 400;
        public const int AlanYuksekligi = 600;
        public const int ZeminYuksekligi = 60;

        private const int EngelSayisi = 3;
        private const int EngelAraligi = 220;
        private const float BaslangicHizi = 3f;
        private const float HizArtisi = 0.5f;
        private const float MaksHiz = 7f;
        private const int SeviyeBasinaSkor = 5;
        private const int MaksBoslukFarki = 150;

        private readonly Random rastgele = new Random();
        private readonly List<Engel> engeller = new List<Engel>();

        public Kus Kus { get; private set; } = null!;
        public int Skor { get; private set; }
        public float Hiz { get; private set; }
        public OyunDurumu Durum { get; private set; }

        public IReadOnlyList<Engel> Engeller
        {
            get { return engeller; }
        }

        public int Seviye
        {
            get { return Skor / SeviyeBasinaSkor + 1; }
        }

        public float ZeminY
        {
            get { return AlanYuksekligi - ZeminYuksekligi; }
        }

        public Oyun()
        {
            Sifirla();
        }

        public void Sifirla()
        {
            Kus = new Kus(80, ZeminY / 2 - Kus.Yukseklik);
            engeller.Clear();
            float boslukY = ZeminY / 2 - Engel.BoslukYuksekligi / 2;
            for (int i = 0; i < EngelSayisi; i++)
            {
                boslukY = RastgeleBoslukY(boslukY);
                engeller.Add(new Engel(AlanGenisligi + 100 + i * EngelAraligi, boslukY));
            }

            Skor = 0;
            Hiz = BaslangicHizi;
            Durum = OyunDurumu.Hazir;
        }

        public void Zipla()
        {
            if (Durum == OyunDurumu.Bitti)
                return;

            if (Durum == OyunDurumu.Hazir)
                Durum = OyunDurumu.Oynaniyor;

            Kus.Zipla();
        }

        // Timer her tetiklendiğinde oyunu bir kare ilerletir
        public void Guncelle()
        {
            if (Durum != OyunDurumu.Oynaniyor)
                return;

            Kus.Guncelle();

            foreach (Engel engel in engeller)
            {
                engel.Hareket(Hiz);

                if (!engel.Gecildi && engel.X + Engel.Genislik < Kus.X)
                {
                    engel.Gecildi = true;
                    SkorArttir();
                }
            }

            foreach (Engel engel in engeller)
            {
                if (engel.EkranDisinda)
                {
                    Engel enSagdaki = EnSagdakiEngel();
                    engel.YenidenKonumla(enSagdaki.X + EngelAraligi, RastgeleBoslukY(enSagdaki.BoslukY));
                }
            }

            if (CarpismaVarMi())
                Durum = OyunDurumu.Bitti;
        }

        public bool CarpismaVarMi()
        {
            RectangleF kus = Kus.Sinirlar;
            kus.Inflate(-3, -3); // kuş oval çizildiği için köşeleri saymıyoruz

            if (kus.Bottom >= ZeminY)
                return true;

            foreach (Engel engel in engeller)
            {
                if (kus.IntersectsWith(engel.UstParca) || kus.IntersectsWith(engel.AltParca(ZeminY)))
                    return true;
            }
            return false;
        }

        private void SkorArttir()
        {
            Skor++;
            Hiz = Math.Min(BaslangicHizi + (Seviye - 1) * HizArtisi, MaksHiz);
        }

        private Engel EnSagdakiEngel()
        {
            Engel enSag = engeller[0];
            foreach (Engel engel in engeller)
            {
                if (engel.X > enSag.X)
                    enSag = engel;
            }
            return enSag;
        }

        // Yeni boşluk bir öncekinden çok uzak olmasın, yoksa yüksek hızda geçilemez
        private float RastgeleBoslukY(float oncekiBoslukY)
        {
            int enAz = Math.Max(60, (int)oncekiBoslukY - MaksBoslukFarki);
            int enCok = Math.Min((int)ZeminY - Engel.BoslukYuksekligi - 60, (int)oncekiBoslukY + MaksBoslukFarki);
            return rastgele.Next(enAz, enCok + 1);
        }
    }
}

namespace FlappyBird.Services
{
    // En yüksek skoru kullanıcının yerel uygulama klasöründe saklar
    public static class SkorDeposu
    {
        private static readonly string dosyaYolu = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "YMH219-FlappyBird",
            "enyuksekskor.txt");

        public static int Oku()
        {
            try
            {
                if (File.Exists(dosyaYolu) && int.TryParse(File.ReadAllText(dosyaYolu), out int skor))
                    return skor;
            }
            catch (Exception hata) when (hata is IOException || hata is UnauthorizedAccessException)
            {
                // Okunamazsa oyun 0 ile devam eder
            }
            return 0;
        }

        public static bool Kaydet(int skor)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dosyaYolu)!);
                File.WriteAllText(dosyaYolu, skor.ToString());
                return true;
            }
            catch (Exception hata) when (hata is IOException || hata is UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}

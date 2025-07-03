namespace IzmitRotaPlanlayici.Models
{
    public class Yolcu
    {
        public string Tip { get; set; } // "Genel", "Öğrenci", "Öğretmen", "65+"
        public OdemeBilgisi Odeme { get; set; }

        public double IndirimOraniGetir()
        {
            return Tip switch
            {
                "Öğrenci" => 0.50,
                "Öğretmen" => 0.75,
                "65+" => 0.00, // ücretsiz
                _ => 1.00 // Genel
            };
        }
    }

}
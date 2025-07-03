namespace IzmitRotaPlanlayici.Models
{
    public class OdemeBilgisi
    {
        public double Nakit { get; set; }
        public double KrediKarti { get; set; }
        public double KentKart { get; set; }

        public string UygunOdemeYontemi(double tutar)
        {
            if (KentKart >= tutar) return "KentKart";
            if (Nakit >= tutar) return "Nakit";
            if (KrediKarti >= tutar) return "KrediKarti";
            return "Yetersiz";
        }
    }

}
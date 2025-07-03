namespace IzmitRotaPlanlayici.Models
{
    public class Taksi
    {
        public double AcilisUcreti { get; set; }
        public double KmBasiUcret { get; set; }

        public Taksi() { }

        public Taksi(double acilisUcreti, double kmBasiUcret)
        {
            AcilisUcreti = acilisUcreti;
            KmBasiUcret = kmBasiUcret;
        }

        public double UcretHesapla(Konum baslangic, Konum hedef)
        {
            double mesafeKm = baslangic.MesafeHesapla(hedef);
            return AcilisUcreti + (mesafeKm * KmBasiUcret);
        }

        public bool ZorunluTaksiKullanimi(Konum konum, Durak durak)
        {
            return konum.MesafeHesapla(durak.Konum) > 3.0;
        }
    }
}

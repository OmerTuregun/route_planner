using System;
namespace IzmitRotaPlanlayici.Models
{
    public class Konum
    {
        public double Enlem { get; set; }
        public double Boylam { get; set; }

        public Konum(double enlem, double boylam)
        {
            Enlem = enlem;
            Boylam = boylam;
        }

        public double MesafeHesapla(Konum diger)
        {
            double R = 6371; // Dünya yarıçapı (km)
            double lat1 = this.Enlem * Math.PI / 180;
            double lon1 = this.Boylam * Math.PI / 180;
            double lat2 = diger.Enlem * Math.PI / 180;
            double lon2 = diger.Boylam * Math.PI / 180;

            double dlat = lat2 - lat1;
            double dlon = lon2 - lon1;

            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = R * c;

            return distance;
        }
    }
}
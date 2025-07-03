using System;
using System.Collections.Generic;
using System.Linq;
using IzmitRotaPlanlayici.Models;

namespace IzmitRotaPlanlayici.Services
{
    public class RotaHedefHesaplayici
    {
        private readonly List<Durak> duraklar;
        private readonly Taksi taksi;

        public RotaHedefHesaplayici(List<Durak> duraklar, Taksi taksi)
        {
            this.duraklar = duraklar;
            this.taksi = taksi;
        }

        public HedefRotaSonucu Hesapla(Konum hedefKonum)
        {
            var sonDuraklar = duraklar.Where(d => d.SonDurak).ToList();

            if (!sonDuraklar.Any())
                return null;

            var enYakinSonDurak = sonDuraklar
                .OrderBy(d => hedefKonum.MesafeHesapla(d.Konum))
                .First();

            double mesafe = hedefKonum.MesafeHesapla(enYakinSonDurak.Konum);
            bool taksiGerekli = mesafe > 3.0;
            double? taksiUcreti = taksiGerekli ? taksi.UcretHesapla(enYakinSonDurak.Konum, hedefKonum) : null;

            return new HedefRotaSonucu
            {
                EnYakinSonDurak = enYakinSonDurak,
                MesafeKm = mesafe,
                TaksiGerekli = taksiGerekli,
                TaksiUcreti = taksiUcreti
            };
        }
    }

    public class HedefRotaSonucu
    {
        public Durak EnYakinSonDurak { get; set; }
        public double MesafeKm { get; set; }
        public bool TaksiGerekli { get; set; }
        public double? TaksiUcreti { get; set; }
    }
}

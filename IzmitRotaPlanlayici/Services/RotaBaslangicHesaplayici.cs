using System;
using System.Collections.Generic;
using System.Linq;
using IzmitRotaPlanlayici.Models;

namespace IzmitRotaPlanlayici.Services
{
    public class RotaBaslangicHesaplayici
    {
        private List<Durak> duraklar;
        private Taksi taksi;

        public RotaBaslangicHesaplayici(List<Durak> duraklar, Taksi taksi)
        {
            this.duraklar = duraklar;
            this.taksi = taksi;
        }

        public BaslangicRotaSonucu Hesapla(Konum kullaniciKonumu)
        {
            var uygunDuraklar = duraklar
                .Where(d => !d.SonDurak && d.SonrakiDuraklar != null && d.SonrakiDuraklar.Count > 0)
                .Where(d => RotaUretilebilirMi(d))
                .OrderBy(d => kullaniciKonumu.MesafeHesapla(d.Konum))
                .ToList();

            var enYakinDurak = uygunDuraklar.FirstOrDefault();
            if (enYakinDurak == null)
                return null;

            double mesafe = kullaniciKonumu.MesafeHesapla(enYakinDurak.Konum);
            bool taksiGerekli = mesafe > 3.0;
            double? taksiUcreti = taksiGerekli ? taksi.UcretHesapla(kullaniciKonumu, enYakinDurak.Konum) : null;

            return new BaslangicRotaSonucu
            {
                EnYakinDurak = enYakinDurak,
                MesafeKm = mesafe,
                TaksiGerekli = taksiGerekli,
                TaksiUcreti = taksiUcreti
            };
        }

        private bool RotaUretilebilirMi(Durak baslangic)
        {
            var ziyaretEdilen = new HashSet<string> { baslangic.Id };
            var queue = new Queue<Durak>();
            queue.Enqueue(baslangic);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var baglanti in current.SonrakiDuraklar)
                {
                    if (!ziyaretEdilen.Contains(baglanti.StopId))
                    {
                        ziyaretEdilen.Add(baglanti.StopId);
                        var sonraki = duraklar.FirstOrDefault(d => d.Id == baglanti.StopId);
                        if (sonraki != null && sonraki.SonrakiDuraklar.Count > 0)
                            queue.Enqueue(sonraki);
                        else if (sonraki != null && sonraki.SonrakiDuraklar.Count == 0)
                            return true;
                    }
                }
            }

            return false;
        }
    }

    public class BaslangicRotaSonucu
    {
        public Durak EnYakinDurak { get; set; }
        public double MesafeKm { get; set; }
        public bool TaksiGerekli { get; set; }
        public double? TaksiUcreti { get; set; }
    }
}

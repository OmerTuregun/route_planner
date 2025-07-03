using System;
using System.Collections.Generic;
using System.Linq;
using IzmitRotaPlanlayici.Models;

namespace IzmitRotaPlanlayici.Services
{
    public class RotaSecenekHesaplayici
    {
        private List<Durak> duraklar;

        public RotaSecenekHesaplayici(List<Durak> duraklar)
        {
            this.duraklar = duraklar;
        }

        public List<RotaSonucu> RotaBul(string baslangicId, string hedefId, Yolcu yolcu)
        {
            var rotalar = new List<RotaSonucu>();
            var queue = new Queue<RotaDurumu>();
            var ziyaretEdilen = new HashSet<string>();

            queue.Enqueue(new RotaDurumu
            {
                GidilenDuraklar = new List<Durak> { GetDurakById(baslangicId) },
                ToplamUcret = 0,
                ToplamSure = 0,
                ToplamMesafe = 0
            });

            while (queue.Count > 0)
            {
                var mevcutRota = queue.Dequeue();
                var sonDurak = mevcutRota.GidilenDuraklar.Last();

                if (sonDurak.Id == hedefId)
                {
                    var indirimOrani = yolcu.IndirimOraniGetir();
                    rotalar.Add(new RotaSonucu
                    {
                        GidilenDuraklar = new List<Durak>(mevcutRota.GidilenDuraklar),
                        ToplamUcret = mevcutRota.ToplamUcret * indirimOrani,
                        ToplamSure = mevcutRota.ToplamSure,
                        ToplamMesafe = mevcutRota.ToplamMesafe
                    });
                    continue;
                }

                string rotaHash = string.Join("->", mevcutRota.GidilenDuraklar.Select(d => d.Id));
                if (ziyaretEdilen.Contains(rotaHash))
                    continue;
                ziyaretEdilen.Add(rotaHash);

                if (sonDurak.Id != hedefId && (sonDurak.SonrakiDuraklar == null || sonDurak.SonrakiDuraklar.Count == 0))
                    continue;

                foreach (var baglanti in sonDurak.SonrakiDuraklar)
                {
                    var sonrakiDurak = GetDurakById(baglanti.StopId);
                    if (sonrakiDurak == null || mevcutRota.GidilenDuraklar.Any(d => d.Id == sonrakiDurak.Id))
                        continue;

                    var yeniRota = new RotaDurumu
                    {
                        GidilenDuraklar = new List<Durak>(mevcutRota.GidilenDuraklar) { sonrakiDurak },
                        ToplamUcret = mevcutRota.ToplamUcret + baglanti.Ucret,
                        ToplamSure = mevcutRota.ToplamSure + baglanti.Sure,
                        ToplamMesafe = mevcutRota.ToplamMesafe + baglanti.Mesafe
                    };
                    queue.Enqueue(yeniRota);
                }

                if (sonDurak.Aktarma != null)
                {
                    var transferDurak = GetDurakById(sonDurak.Aktarma.TransferStopId);
                    if (transferDurak != null && !mevcutRota.GidilenDuraklar.Any(d => d.Id == transferDurak.Id))
                    {
                        double transferUcret = sonDurak.Aktarma.TransferUcret > 0 ? sonDurak.Aktarma.TransferUcret : 0.01;
                        int transferSure = sonDurak.Aktarma.TransferSure > 0 ? sonDurak.Aktarma.TransferSure : 1;

                        var yeniRota = new RotaDurumu
                        {
                            GidilenDuraklar = new List<Durak>(mevcutRota.GidilenDuraklar) { transferDurak },
                            ToplamUcret = mevcutRota.ToplamUcret + transferUcret,
                            ToplamSure = mevcutRota.ToplamSure + transferSure,
                            ToplamMesafe = mevcutRota.ToplamMesafe
                        };
                        queue.Enqueue(yeniRota);
                    }
                }
            }

            return rotalar
                .OrderBy(r => r.ToplamSure)
                .ToList();
        }

        private Durak GetDurakById(string id)
        {
            return duraklar.FirstOrDefault(d => d.Id == id);
        }
    }

    public class RotaSonucu
    {
        public List<Durak> GidilenDuraklar { get; set; }
        public double ToplamUcret { get; set; }
        public int ToplamSure { get; set; }
        public double ToplamMesafe { get; set; }
    }

    internal class RotaDurumu
    {
        public List<Durak> GidilenDuraklar { get; set; }
        public double ToplamUcret { get; set; }
        public int ToplamSure { get; set; }
        public double ToplamMesafe { get; set; }
    }
}

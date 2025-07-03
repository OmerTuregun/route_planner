using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using IzmitRotaPlanlayici.Models;


public class DurakVerisi
{
    public string City { get; set; }
    public Taksi Taxi { get; set; }
    public List<Durak> Duraklar { get; set; }

    public static DurakVerisi Yükle(string dosyaYolu)
    {
        System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("en-US");
        System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = new System.Globalization.CultureInfo("en-US");

        string json = File.ReadAllText(dosyaYolu);
        var settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Error = (sender, args) => { args.ErrorContext.Handled = true; }
        };

        var hamVeri = JsonConvert.DeserializeObject<HamVeri>(json, settings);
        var durakListesi = new List<Durak>();

        foreach (var item in hamVeri.duraklar)
        {
            var durak = new Durak
            {
                Id = item.id,
                Ad = item.name,
                Tip = item.type,
                SonDurak = item.sonDurak,
                Konum = new Konum(item.lat, item.lon),
                Aktarma = item.transfer != null ? new Transfer
                {
                    TransferStopId = item.transfer.transferStopId,
                    TransferSure = item.transfer.transferSure,
                    TransferUcret = item.transfer.transferUcret
                } : null,
                SonrakiDuraklar = new List<Baglanti>()
            };

            if (item.nextStops != null)
            {
                foreach (var ns in item.nextStops)
                {
                    durak.SonrakiDuraklar.Add(new Baglanti
                    {
                        StopId = ns.stopId,
                        Mesafe = ns.mesafe,
                        Sure = ns.sure,
                        Ucret = ns.ucret
                    });
                }
            }

            durakListesi.Add(durak);
        }

        return new DurakVerisi
        {
            City = hamVeri.city,
            Taxi = new Taksi
            {
                AcilisUcreti = hamVeri.taxi.openingFee,
                KmBasiUcret = hamVeri.taxi.costPerKm
            },
            Duraklar = durakListesi
        };
    }

    // JSON'dan geçici veri almak için kullanılan iç yapılar
    private class HamVeri
    {
        public string city { get; set; }
        public TaksiHam taxi { get; set; }
        public List<HamDurak> duraklar { get; set; }
    }

    private class TaksiHam
    {
        public double openingFee { get; set; }
        public double costPerKm { get; set; }
    }

    private class HamDurak
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public bool sonDurak { get; set; }
        public List<HamNextStop> nextStops { get; set; }
        public HamTransfer transfer { get; set; }
    }

    private class HamNextStop
    {
        public string stopId { get; set; }
        public double mesafe { get; set; }
        public int sure { get; set; }
        public double ucret { get; set; }
    }

    private class HamTransfer
    {
        public string transferStopId { get; set; }
        public int transferSure { get; set; }
        public double transferUcret { get; set; }
    }
}

using System.Collections.Generic;
namespace IzmitRotaPlanlayici.Models
{
    public class Durak
    {
        public string Id { get; set; }
        public string Ad { get; set; }
        public string Tip { get; set; } // "bus" ya da "tram"
        public Konum Konum { get; set; }
        public bool SonDurak { get; set; }
        public List<Baglanti> SonrakiDuraklar { get; set; }
        public Transfer Aktarma { get; set; }

        public Durak()
        {
            SonrakiDuraklar = new List<Baglanti>();
        }
    }
}
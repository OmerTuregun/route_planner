using System;
using IzmitRotaPlanlayici.Services;
using System.Collections.Generic;
using IzmitRotaPlanlayici.Models;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IzmitRotaPlanlayici
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcının girişlerini al
                double enlem = double.Parse(txtEnlem.Text, System.Globalization.CultureInfo.InvariantCulture);
                double boylam = double.Parse(txtBoylam.Text, System.Globalization.CultureInfo.InvariantCulture);
                double hedefEnlem = double.Parse(txtHedefEnlem.Text, System.Globalization.CultureInfo.InvariantCulture);
                double hedefBoylam = double.Parse(txtHedefBoylam.Text, System.Globalization.CultureInfo.InvariantCulture);

                var kullaniciKonumu = new Konum(enlem, boylam);
                var hedefKonumu = new Konum(hedefEnlem, hedefBoylam);

                // Durak verisini yükle
                var veri = DurakVerisi.Yükle("duraklar.json");

                // Yolcu tipini ve ödeme bilgisini al
                string yolcuTipi = cmbYolcuTipi.SelectedItem.ToString();
                var yolcu = new Yolcu
                {
                    Tip = yolcuTipi,
                    Odeme = new OdemeBilgisi
                    {
                        Nakit = double.TryParse(txtNakit.Text, out double nakit) ? nakit : 0,
                        KrediKarti = double.TryParse(txtKrediKarti.Text, out double krediKarti) ? krediKarti : 0,
                        KentKart = double.TryParse(txtKentKart.Text, out double kentKart) ? kentKart : 0
                    }
                };

                // Başlangıç noktasını hesapla
                var baslangicHesaplayici = new RotaBaslangicHesaplayici(veri.Duraklar, veri.Taxi);
                var baslangicSonuc = baslangicHesaplayici.Hesapla(kullaniciKonumu);

                // Hedef noktası için en yakın durak
                var hedefeEnYakinDurak = veri.Duraklar
                    .OrderBy(d => hedefKonumu.MesafeHesapla(d.Konum))
                    .FirstOrDefault();

                double hedefMesafe = hedefKonumu.MesafeHesapla(hedefeEnYakinDurak.Konum);
                bool hedefTaksiGerekli = hedefMesafe > 3.0;
                double? hedefTaksiUcreti = hedefTaksiGerekli
                    ? veri.Taxi.UcretHesapla(hedefKonumu, hedefeEnYakinDurak.Konum)
                    : null;

                if (baslangicSonuc == null || hedefeEnYakinDurak == null)
                {
                    lblSonuc.Text = "Başlangıç veya hedef konumu için uygun durak bulunamadı.";
                    return;
                }

                lblSonuc.Text =
                    $"📍 Başlangıç Konumu:\n" +
                    $"- En Yakın Durak: {baslangicSonuc.EnYakinDurak.Ad}\n" +
                    $"- Mesafe: {baslangicSonuc.MesafeKm:F2} km\n" +
                    (baslangicSonuc.TaksiGerekli
                        ? $"🚖 Taksi gerekli, ücret: {baslangicSonuc.TaksiUcreti:F2} TL\n"
                        : $"🚶 Yürüyerek ulaşabilirsiniz.\n") +

                    $"\n📍 Hedef Konumu:\n" +
                    $"- En Yakın Son Durak: {hedefeEnYakinDurak.Ad}\n" +
                    $"- Mesafe: {hedefMesafe:F2} km\n" +
                    (hedefTaksiGerekli
                        ? $"🚖 Taksi gerekli, ücret: {hedefTaksiUcreti:F2} TL"
                        : $"🚶 Yürüyerek ulaşabilirsiniz.");

                var rotaSecenekHesaplayici = new RotaSecenekHesaplayici(veri.Duraklar);
                var rotalar = rotaSecenekHesaplayici.RotaBul(baslangicSonuc.EnYakinDurak.Id, hedefeEnYakinDurak.Id, yolcu);

                if (rotalar.Count == 0)
                {
                    lblSonuc.Text += "\n\n🚫 Uygun rota bulunamadı.";
                }
                else
                {
                    var sb = new StringBuilder();

                    var enIyiRota = rotalar.First();
                    sb.AppendLine("\n\n✅ En Uygun Rota:");
                    foreach (var durak in enIyiRota.GidilenDuraklar)
                        sb.AppendLine($"   🔹 {durak.Ad} ({durak.Tip})");
                    sb.AppendLine($"   📏 Mesafe: {enIyiRota.ToplamMesafe:F2} km");
                    sb.AppendLine($"   ⏱️ Süre: {enIyiRota.ToplamSure} dk");
                    sb.AppendLine($"   💰 Ücret: {enIyiRota.ToplamUcret:F2} TL");

                    if (rotalar.Count > 1)
                    {
                        sb.AppendLine("\n🔁 Alternatif Rotalar:");
                        int sayac = 2;
                        foreach (var rota in rotalar.Skip(1))
                        {
                            sb.AppendLine($"Rota {sayac++}: {rota.ToplamMesafe:F2} km, {rota.ToplamSure} dk, {rota.ToplamUcret:F2} TL");
                        }
                    }

                    lblSonuc.Text += sb.ToString();
                }
            }
            catch (Exception ex)
            {
                lblSonuc.Text = "❌ Hata: " + ex.Message;
            }
        }

        private int AktarmaSayisiHesapla(RotaSonucu rota)
        {
            return rota.GidilenDuraklar.Count(d => d.Aktarma != null);
        }
    }
}

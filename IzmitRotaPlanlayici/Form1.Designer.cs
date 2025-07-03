using System.Windows.Forms;

namespace IzmitRotaPlanlayici
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        private System.Windows.Forms.Label lblEnlem;
        private System.Windows.Forms.TextBox txtEnlem;
        private System.Windows.Forms.Label lblBoylam;
        private System.Windows.Forms.TextBox txtBoylam;
        private System.Windows.Forms.Button btnHesapla;
        private System.Windows.Forms.Label lblSonuc;
        private Label lblHedefEnlem;
        private TextBox txtHedefEnlem;
        private Label lblHedefBoylam;
        private TextBox txtHedefBoylam;

        // Yolcu Tipi ve Ödeme Yöntemi için ComboBox ve TextBox'lar
        private ComboBox cmbYolcuTipi;
        private ComboBox cmbOdemeYontemi;
        private TextBox txtNakit;
        private TextBox txtKrediKarti;
        private TextBox txtKentKart;

        private ListView listViewRotalar;

        private void InitializeComponent()
        {
            this.lblEnlem = new System.Windows.Forms.Label();
            this.txtEnlem = new System.Windows.Forms.TextBox();
            this.lblBoylam = new System.Windows.Forms.Label();
            this.txtBoylam = new System.Windows.Forms.TextBox();
            this.lblHedefEnlem = new System.Windows.Forms.Label();
            this.txtHedefEnlem = new System.Windows.Forms.TextBox();
            this.lblHedefBoylam = new System.Windows.Forms.Label();
            this.txtHedefBoylam = new System.Windows.Forms.TextBox();
            this.btnHesapla = new System.Windows.Forms.Button();
            this.lblSonuc = new System.Windows.Forms.Label();

            // Enlem Label and TextBox
            this.lblEnlem.AutoSize = true;
            this.lblEnlem.Location = new System.Drawing.Point(40, 30);
            this.lblEnlem.Name = "lblEnlem";
            this.lblEnlem.Size = new System.Drawing.Size(40, 13);
            this.lblEnlem.Text = "Enlem:";
            this.Controls.Add(this.lblEnlem);

            this.txtEnlem.Location = new System.Drawing.Point(140, 27);
            this.txtEnlem.Name = "txtEnlem";
            this.txtEnlem.Size = new System.Drawing.Size(120, 20);
            this.Controls.Add(this.txtEnlem);

            // Boylam Label and TextBox
            this.lblBoylam.AutoSize = true;
            this.lblBoylam.Location = new System.Drawing.Point(40, 65);
            this.lblBoylam.Name = "lblBoylam";
            this.lblBoylam.Size = new System.Drawing.Size(46, 13);
            this.lblBoylam.Text = "Boylam:";
            this.Controls.Add(this.lblBoylam);

            this.txtBoylam.Location = new System.Drawing.Point(140, 62);
            this.txtBoylam.Name = "txtBoylam";
            this.txtBoylam.Size = new System.Drawing.Size(120, 20);
            this.Controls.Add(this.txtBoylam);

            // Hedef Enlem and Boylam
            this.lblHedefEnlem.AutoSize = true;
            this.lblHedefEnlem.Location = new System.Drawing.Point(40, 100);
            this.lblHedefEnlem.Text = "Hedef Enlem:";
            this.Controls.Add(this.lblHedefEnlem);

            this.txtHedefEnlem.Location = new System.Drawing.Point(140, 97);
            this.txtHedefEnlem.Size = new System.Drawing.Size(120, 20);
            this.Controls.Add(this.txtHedefEnlem);

            this.lblHedefBoylam.AutoSize = true;
            this.lblHedefBoylam.Location = new System.Drawing.Point(40, 135);
            this.lblHedefBoylam.Text = "Hedef Boylam:";
            this.Controls.Add(this.lblHedefBoylam);

            this.txtHedefBoylam.Location = new System.Drawing.Point(140, 132);
            this.txtHedefBoylam.Size = new System.Drawing.Size(120, 20);
            this.Controls.Add(this.txtHedefBoylam);

            // Yolcu Tipi Seçimi (ComboBox)
            this.cmbYolcuTipi = new ComboBox();
            this.cmbYolcuTipi.Items.Add("Genel");
            this.cmbYolcuTipi.Items.Add("Öğrenci");
            this.cmbYolcuTipi.Items.Add("Öğretmen");
            this.cmbYolcuTipi.Items.Add("65+");
            this.cmbYolcuTipi.Location = new System.Drawing.Point(140, 160);
            this.cmbYolcuTipi.Name = "cmbYolcuTipi";
            this.cmbYolcuTipi.Size = new System.Drawing.Size(120, 20);

            // Ödeme Yöntemi Seçimi (ComboBox)
            this.cmbOdemeYontemi = new ComboBox();
            this.cmbOdemeYontemi.Items.Add("Nakit");
            this.cmbOdemeYontemi.Items.Add("Kredi Kartı");
            this.cmbOdemeYontemi.Items.Add("KentKart");
            this.cmbOdemeYontemi.Location = new System.Drawing.Point(140, 190);
            this.cmbOdemeYontemi.Name = "cmbOdemeYontemi";
            this.cmbOdemeYontemi.Size = new System.Drawing.Size(120, 20);

            // Bakiyeler (TextBox) 
            this.txtNakit = new TextBox();
            this.txtNakit.Enabled = false; // Başlangıçta pasif
            this.txtNakit.Location = new System.Drawing.Point(140, 220);
            this.txtNakit.Size = new System.Drawing.Size(120, 20);

            this.txtKrediKarti = new TextBox();
            this.txtKrediKarti.Enabled = false; // Başlangıçta pasif
            this.txtKrediKarti.Location = new System.Drawing.Point(140, 250);
            this.txtKrediKarti.Size = new System.Drawing.Size(120, 20);

            this.txtKentKart = new TextBox();
            this.txtKentKart.Enabled = false; // Başlangıçta pasif
            this.txtKentKart.Location = new System.Drawing.Point(140, 280);
            this.txtKentKart.Size = new System.Drawing.Size(120, 20);

            // btnHesapla
            this.btnHesapla.Location = new System.Drawing.Point(140, 320);
            this.btnHesapla.Name = "btnHesapla";
            this.btnHesapla.Size = new System.Drawing.Size(120, 35);
            this.btnHesapla.Text = "Hesapla";
            this.btnHesapla.Click += new System.EventHandler(this.btnHesapla_Click);

            // lblSonuc
            this.lblSonuc.AutoSize = true;
            this.lblSonuc.Location = new System.Drawing.Point(40, 370);
            this.lblSonuc.Name = "lblSonuc";
            this.lblSonuc.Size = new System.Drawing.Size(0, 13);
            this.lblSonuc.MaximumSize = new System.Drawing.Size(500, 0); // Çok satırlı metin için genişlik sınırı


            // Ödeme Yöntemi Seçildiğinde, ilgili bakiyeyi aktif hale getiren event
            cmbOdemeYontemi.SelectedIndexChanged += (s, e) =>
            {
                // Önce tüm TextBox'ları pasif hale getir
                txtNakit.Enabled = false;
                txtKrediKarti.Enabled = false;
                txtKentKart.Enabled = false;

                // Seçilen ödeme türüne göre uygun TextBox'ı aktif hale getir
                switch (cmbOdemeYontemi.SelectedItem.ToString())
                {
                    case "Nakit":
                        txtNakit.Enabled = true;
                        break;
                    case "Kredi Kartı":
                        txtKrediKarti.Enabled = true;
                        break;
                    case "KentKart":
                        txtKentKart.Enabled = true;
                        break;
                }
            };

            this.listViewRotalar = new ListView();
            this.listViewRotalar.Location = new System.Drawing.Point(300, 27);
            this.listViewRotalar.Size = new System.Drawing.Size(280, 300);
            this.listViewRotalar.View = View.Details;
            this.listViewRotalar.Columns.Add("Rota Detayı", 270);
            


            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.lblEnlem);
            this.Controls.Add(this.txtEnlem);
            this.Controls.Add(this.lblBoylam);
            this.Controls.Add(this.txtBoylam);
            this.Controls.Add(this.lblHedefEnlem);
            this.Controls.Add(this.txtHedefEnlem);
            this.Controls.Add(this.lblHedefBoylam);
            this.Controls.Add(this.txtHedefBoylam);
            this.Controls.Add(this.cmbYolcuTipi);
            this.Controls.Add(this.cmbOdemeYontemi);
            this.Controls.Add(this.txtNakit);
            this.Controls.Add(this.txtKrediKarti);
            this.Controls.Add(this.txtKentKart);
            this.Controls.Add(this.listViewRotalar);
            this.Controls.Add(this.btnHesapla);
            this.Controls.Add(this.lblSonuc);
            this.Name = "Form1";
            this.Text = "Başlangıç Noktası Rota Hesaplama";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

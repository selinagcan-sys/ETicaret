namespace ETicaret
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string ad = textBox1.Text;
            double fiyat;
            int stok;

            // Girilen değerler sayıya çevrilebiliyor mu kontrol eder
            if (double.TryParse(textBox2.Text, out fiyat) && int.TryParse(textBox3.Text, out stok))
            {
                // Ürünü ListBox’a yaz (stok bilgisini yanında göster)
                listBox1.Items.Add(ad + " - " + fiyat + "₺ (Stok: " + stok + ")");
            }
            else
            {
                MessageBox.Show("Lütfen geçerli fiyat ve stok girin!");
            }

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir ürün seçin!");
                return;
            }

            // Seçilen ürünü al
            string secilen = listBox1.SelectedItem.ToString();

            // Stok bilgisini bulalım (örnek: "Telefon - 20000₺ (Stok: 3)")
            int basIndex = secilen.IndexOf("(Stok: ") + 7;
            int bitIndex = secilen.IndexOf(")", basIndex);
            int stok = int.Parse(secilen.Substring(basIndex, bitIndex - basIndex));

            // Stok varsa sepete ekle
            if (stok > 0)
            {
                listBox2.Items.Add(secilen.Split('(')[0].Trim()); // sadece ürün adı ve fiyat

                stok--; // stoktan 1 düşür

                // Yeni stokla ürün bilgisini güncelle
                string yeniUrun = secilen.Substring(0, basIndex - 7) + "(Stok: " + stok + ")";
                int index = listBox1.SelectedIndex;
                listBox1.Items[index] = yeniUrun;
            }
            else
            {
                MessageBox.Show("Stok kalmadı!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show("Sepet boş!");
                return;
            }

            double toplam = 0;

            // Sepetteki her ürünün fiyatını al
            foreach (string item in listBox2.Items)
            {
                // Örnek: "Telefon - 20000₺"
                int basIndex = item.IndexOf('-') + 1;
                int bitIndex = item.IndexOf('₺');
                string fiyatStr = item.Substring(basIndex, bitIndex - basIndex).Trim();
                double fiyat = double.Parse(fiyatStr);

                toplam += fiyat;
            }

            // KDV’yi hesapla (%20)
            double kdv = toplam * 0.20;
            double kdvDahil = toplam + kdv;

            // Ekrana yaz
            textBox4.Text = "Toplam: " + toplam.ToString("0.00") + "₺";
            textBox5.Text = "KDV Dahil: " + kdvDahil.ToString("0.00") + "₺";
        }
    }
}

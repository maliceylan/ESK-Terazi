using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TeraziDevEx
{
    public class FuncMusteri
    {
        SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);


        public bool Musterikaydet(string musterino, string adisoyadi, string tur, string tcno, string iletisim, string adres, string iban) // KAYIT
        {
            bool son = false;
            SqlCommand komut = new SqlCommand();
            int sonuc = KayitKontrolleri(musterino, adisoyadi, tur, tcno, iletisim, adres, iban);
            if (sonuc == 1)
            {
                try
                {
                    komut.Connection = conn;
                    komut.CommandText = "INSERT INTO [Terazi].[dbo].[Musteriler] ([MusteriNo],[Adi],[Tur],[TCNo],[GSM],[Adres],[IBAN]) VALUES (@musterino,@musteriadi,@musteritur,@musteritc,@musteritelefon,@musteriadres,@musteriiban)";
                    komut.Parameters.AddWithValue("@musterino", musterino);
                    komut.Parameters.AddWithValue("@musteriadi", adisoyadi);
                    komut.Parameters.AddWithValue("@musteritur", tur);
                    komut.Parameters.AddWithValue("@musteritc", tcno);
                    komut.Parameters.AddWithValue("@musteritelefon", iletisim);
                    komut.Parameters.AddWithValue("@musteriadres", adres);
                    komut.Parameters.AddWithValue("@musteriiban", iban);
                    conn.Open();
                    //MessageBox.Show(komut.CommandText);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kaydedildi.", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    son = true;

                }
                catch (Exception e)
                {
                    son = false;
                    conn.Close();
                    MessageBox.Show("Veritabanı kayıt yapılamadı.\n Hata: " + e, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return son;
        }

        private int KayitKontrolleri(string musterino, string adisoyadi, string tur, string tcno, string iletisim, string adres, string iban)
        {
            int musterinokayitlimi = MusteriNoKontrol(musterino);
            int tcnokayitlimi = MusteriTCKontrol(tcno);
            int sonuc = 0;
            if (musterino == "")
            {
                MessageBox.Show("Lütfen müşteri veya besici numarası giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (adisoyadi == "")
            {
                MessageBox.Show("Lütfen isim giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tur == "")
            {
                MessageBox.Show("Lütfen müşteri türü giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tcno == "")
            {
                MessageBox.Show("Lütfen Müşteri TC Kimlik numarası veya Vergi No giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //else if (iletisim == "")
            //{
            //    MessageBox.Show("Lütfen iletişim numarası giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (adres == "")
            //{
            //    MessageBox.Show("Lütfen adres giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (iban == "")
            //{
            //    MessageBox.Show("Lütfen IBAN numarası giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (iletisim.Length != 10) // İLETİŞİM NO UZUNLUK KONTROLÜ
            //{
            //    MessageBox.Show("Lütfen geçerli bir iletişim numarası giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (tcno.Length > 11) // TC NO UZUNLUK KONTROLÜ
            //{
            //    MessageBox.Show("Lütfen geçerli bir değer giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            else if (musterinokayitlimi == 1) // MÜŞTERİ NO TEKRARLI MI KONTROLÜ
            {
                MessageBox.Show("Aynı müşteri numarası ile ikinci kayıt yapılamaz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tcnokayitlimi == 1) // MÜŞTERİ NO TEKRARLI MI KONTROLÜ
            {
                MessageBox.Show("Aynı TC Kimlik Numarası ile ikinci kayıt yapılamaz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else sonuc = 1;
            return sonuc;

        }
        private int MusteriTCKontrol(string tcno)
        {
            SqlCommand komut = new SqlCommand();
            int sonuc = 0;
            int kayitsayisi = 0;
            try
            {
                komut.Connection = conn;
                komut.CommandText = "SELECT COUNT(*) FROM [Terazi].[dbo].[Musteriler] where TCNo=@tcno";
                komut.Parameters.AddWithValue("@tcno", tcno);
                conn.Open();
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    kayitsayisi = (int)dr[0];
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Tekrarlı kayıt kontrolü sırasında hata oluştu.\n Hata: " + e, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (kayitsayisi > 0)
            {
                sonuc = 1;
            }
            return sonuc;
        }
        private int MusteriNoKontrol(string musterino)
        {
            SqlCommand komut = new SqlCommand();
            int sonuc = 0;
            int kayitsayisi = 0;
            try
            {
                komut.Connection = conn;
                komut.CommandText = "SELECT COUNT(*) FROM [Terazi].[dbo].[Musteriler] where MusteriNo=@musterino";
                komut.Parameters.AddWithValue("@musterino", musterino);
                conn.Open();
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    kayitsayisi = (int)dr[0];
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Tekrarlı kayıt kontrolü sırasında hata oluştu.\n Hata: " + e, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (kayitsayisi > 0)
            {
                sonuc = 1;
            }

            return sonuc;
        }

        public DataTable tablodoldur(string arama, bool tumu)
        {
            SqlCommand komut = new SqlCommand();
            try
            {
                if (tumu == true)
                {
                    komut.CommandText = "SELECT MusteriNo[Müşteri No], Adi[Müşteri Adı],Tur[Müşteri Türü], TCNo[Müşteri TC No], GSM[İletişim], Adres, IBAN FROM [Terazi].[dbo].[Musteriler]";
                }
                else
                {
                    if (arama != "")
                    {
                        komut.CommandText = "SELECT MusteriNo[Müşteri No], Adi[Müşteri Adı],Tur[Müşteri Türü], TCNo[Müşteri TC No], GSM[İletişim], Adres, IBAN FROM [Terazi].[dbo].[Musteriler] where adi like '%" + arama + "%' or MusteriNo like '%" + arama + "%' or TCNo like '%" + arama + "%' or GSM like '%" + arama + "%' or Tur like '%" + arama + "%' ";
                    }
                }
                komut.Connection = conn;
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Tablo alınamadı.\n Hata: " + e, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public int satirsayisi()
        {
            SqlCommand komut = new SqlCommand();
            int satir = -1;
            try
            {
                komut.Connection = conn;
                komut.CommandText = "SELECT COUNT(*) FROM [Terazi].[dbo].[Musteriler]";
                conn.Open();
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    satir = (int)dr[0];
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Toplam kayıt sayısı okunamadı.\n Hata: " + e, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return satir;
        }

        public bool MusteriDuzenle(string musterino, string adisoyadi, string tur, string tcno, string iletisim, string adres, string iban)
        {
            bool son = false;
            SqlCommand komut = new SqlCommand();
            int sonuc = DuzenlemeKontrolleri(musterino, adisoyadi, tur, tcno, iletisim, adres, iban); //DÜZENLENECEK BİLGİLERİ KONTROL ET
            if (sonuc == 1) //GİRİLEN BİLGİLER İSTENİLEN ARALIKTAYSA DÜZENLEME İŞLEMİNİ KAYDET
            {
                try
                {
                    komut.Connection = conn;
                    komut.CommandText = "UPDATE [Terazi].[dbo].[Musteriler] SET [Adi]=@musteriadi, [Tur]=@musteritur ,[TCNo]=@musteritc,[GSM]=@musteritelefon,[Adres]=@musteriadres,[IBAN]=@musteriiban where MusteriNo=@musterino ";
                    komut.Parameters.AddWithValue("@musterino", musterino);
                    komut.Parameters.AddWithValue("@musteriadi", adisoyadi);
                    komut.Parameters.AddWithValue("@musteritur", tur);
                    komut.Parameters.AddWithValue("@musteritc", tcno);
                    komut.Parameters.AddWithValue("@musteritelefon", iletisim);
                    komut.Parameters.AddWithValue("@musteriadres", adres);
                    komut.Parameters.AddWithValue("@musteriiban", iban);
                    conn.Open();
                    komut.ExecuteNonQuery();

                    MessageBox.Show("Düzenlendi.", "Düzenleme İşlemi");
                    conn.Close();
                    son = true;
                }
                catch (Exception e)
                {
                    conn.Close();
                    MessageBox.Show("Kayıt düzenlenemedi.\n Hata: " + e, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    son = false;
                }

            }
            return son;
        }

        private int DuzenlemeKontrolleri(string musterino, string adisoyadi, string tur, string tcno, string iletisim, string adres, string iban)
        {
            int sonuc = 0;
            //int tcnokayitlimi = MusteriTCKontrol(tcno);
            if (musterino == "")//BOŞ KONTROLLERİ
            {
                MessageBox.Show("Lütfen müşteri veya besici numarası giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (adisoyadi == "")
            {
                MessageBox.Show("Lütfen isim giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tur == "")
            {
                MessageBox.Show("Lütfen müşteri türü giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tcno == "")
            {
                MessageBox.Show("Lütfen Müşteri TC Kimlik numarası veya Vergi No giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //else if (iletisim == "")
            //{
            //    MessageBox.Show("Lütfen iletişim numarası giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (adres == "")
            //{
            //    MessageBox.Show("Lütfen adres giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (iban == "")
            //{
            //    MessageBox.Show("Lütfen IBAN numarası giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (tcno.Length != 11)//TC UZUNLUK KONTROLÜ
            //{
            //    MessageBox.Show("Lütfen geçerli bir TC Kimlik Numarası giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (iletisim.Length != 10)//GSM UZUNLUK KONTROLÜ
            //{
            //    MessageBox.Show("Lütfen geçerli bir Telefon giriniz.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

            else sonuc = 1;
            return sonuc;
        }

        public bool MusteriSil(string musterino)
        {
            DialogResult diyalog = new DialogResult();
            diyalog = MessageBox.Show(musterino + " Müşteri numaralı kayıt silinsin mi?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand();
                try
                {
                    komut.Connection = conn;
                    komut.CommandText = "DELETE FROM [Terazi].[dbo].[Musteriler] WHERE MusteriNo=@musterino";
                    komut.Parameters.AddWithValue("@musterino", musterino);
                    conn.Open();
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Silindi.", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    return true;
                }
                catch (Exception e)
                {
                    conn.Close();
                    MessageBox.Show("Kayıt Silinemedi.\n Hata: " + e, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            return false;

        }




    }
}

using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace TeraziDevEx
{
    public partial class Terazi : DevExpress.XtraBars.TabForm
    {
        int secilenTarti = 0;
        int hayvanAlimSiraNo = 0;
        public static SimpleButton[] kisayolDizi = new SimpleButton[80];
        String[] kisayolSiraNo = new String[80];
        ComboBox[] cmbPortlar = new ComboBox[6];
        SerialPort sp;
        bool kesimDuzenle = false;
        string kurum = "kesim";

        public Terazi()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            kisayolDizi[0] = btnKisayol1; kisayolDizi[6] = btnKisayol7; kisayolDizi[12] = btnKisayol13; kisayolDizi[18] = btnKisayol19; kisayolDizi[24] = btnKisayol25;
            kisayolDizi[1] = btnKisayol2; kisayolDizi[7] = btnKisayol8; kisayolDizi[13] = btnKisayol14; kisayolDizi[19] = btnKisayol20; kisayolDizi[25] = btnKisayol26;
            kisayolDizi[2] = btnKisayol3; kisayolDizi[8] = btnKisayol9; kisayolDizi[14] = btnKisayol15; kisayolDizi[20] = btnKisayol21; kisayolDizi[26] = btnKisayol27;
            kisayolDizi[3] = btnKisayol4; kisayolDizi[9] = btnKisayol10; kisayolDizi[15] = btnKisayol16; kisayolDizi[21] = btnKisayol22; kisayolDizi[27] = btnKisayol28;
            kisayolDizi[4] = btnKisayol5; kisayolDizi[10] = btnKisayol11; kisayolDizi[16] = btnKisayol17; kisayolDizi[22] = btnKisayol23; kisayolDizi[28] = btnKisayol29;
            kisayolDizi[5] = btnKisayol6; kisayolDizi[11] = btnKisayol12; kisayolDizi[17] = btnKisayol18; kisayolDizi[23] = btnKisayol24; kisayolDizi[29] = btnKisayol30;
            kisayolDizi[30] = btnKisayol31; kisayolDizi[40] = btnKisayol41; kisayolDizi[49] = btnKisayol50; kisayolDizi[58] = btnKisayol59; kisayolDizi[67] = btnKisayol68;
            kisayolDizi[31] = btnKisayol32; kisayolDizi[41] = btnKisayol42; kisayolDizi[50] = btnKisayol51; kisayolDizi[59] = btnKisayol60; kisayolDizi[68] = btnKisayol69;
            kisayolDizi[32] = btnKisayol33; kisayolDizi[42] = btnKisayol43; kisayolDizi[51] = btnKisayol52; kisayolDizi[60] = btnKisayol61; kisayolDizi[69] = btnKisayol70;
            kisayolDizi[33] = btnKisayol34; kisayolDizi[43] = btnKisayol44; kisayolDizi[52] = btnKisayol53; kisayolDizi[61] = btnKisayol62; kisayolDizi[70] = btnKisayol71;
            kisayolDizi[34] = btnKisayol35; kisayolDizi[44] = btnKisayol45; kisayolDizi[53] = btnKisayol54; kisayolDizi[62] = btnKisayol63; kisayolDizi[71] = btnKisayol72;
            kisayolDizi[35] = btnKisayol36; kisayolDizi[45] = btnKisayol46; kisayolDizi[54] = btnKisayol55; kisayolDizi[63] = btnKisayol64; kisayolDizi[72] = btnKisayol73;
            kisayolDizi[36] = btnKisayol37; kisayolDizi[46] = btnKisayol47; kisayolDizi[55] = btnKisayol56; kisayolDizi[64] = btnKisayol65; kisayolDizi[73] = btnKisayol74;
            kisayolDizi[37] = btnKisayol38; kisayolDizi[47] = btnKisayol48; kisayolDizi[56] = btnKisayol57; kisayolDizi[65] = btnKisayol66; kisayolDizi[74] = btnKisayol75;
            kisayolDizi[38] = btnKisayol39; kisayolDizi[48] = btnKisayol49; kisayolDizi[57] = btnKisayol58; kisayolDizi[66] = btnKisayol67; kisayolDizi[39] = btnKisayol40;
            kisayolDizi[75] = btnKisayol76; kisayolDizi[76] = btnKisayol77; kisayolDizi[77] = btnKisayol78; kisayolDizi[78] = btnKisayol79; kisayolDizi[79] = btnKisayol80;

            cmbPortlar[0] = cmbCOMPort1; cmbPortlar[1] = cmbCOMPort2; cmbPortlar[2] = cmbCOMPort3; cmbPortlar[3] = cmbCOMPort4; cmbPortlar[4] = cmbCOMPort5; cmbPortlar[5] = cmbCOMPort6;

            //ProcessStartInfo p = new ProcessStartInfo(@"d:\enable.bat");
            //Process proc = new Process();
            //proc.StartInfo = p;
            //proc.Start();
            //proc.WaitForExit();
            if(kurum!="hayvanalimi")new Process() { StartInfo = new ProcessStartInfo("cmd") { Verb = "runas", Arguments = "/C devcon disable USB\\VID_1A*" } }.Start();
        }


        private void baslangicOzelAyarUygula()
        {
            portEkle();
            ayaroku();
            chTCP1.Enabled = false;
            chTCP2.Enabled = false;
            chTCP3.Enabled = false;
            chTCP4.Enabled = false;
            chTCP5.Enabled = false;
        }

        void portEkle()
        {
            cmbCOMPort1.Items.Clear();
            cmbCOMPort2.Items.Clear();
            cmbCOMPort3.Items.Clear();
            cmbCOMPort4.Items.Clear();
            cmbCOMPort5.Items.Clear();
            cmbCOMPort6.Items.Clear();

            for (int i = 0; i < 20; i++)
            {
                cmbCOMPort1.Items.Add("COM" + i);
                cmbCOMPort2.Items.Add("COM" + i);
                cmbCOMPort3.Items.Add("COM" + i);
                cmbCOMPort4.Items.Add("COM" + i);
                cmbCOMPort5.Items.Add("COM" + i);
                cmbCOMPort6.Items.Add("COM" + i);
            }
        }
        void OnOuterFormCreating(object sender, OuterFormCreatingEventArgs e)
        {
            Terazi form = new Terazi();
            form.TabFormControl.Pages.Clear();
            e.Form = form;
            OpenFormCount++;
        }
        static int OpenFormCount = 1;

        void tartisec(int sayi)
        {
            secilenTarti = sayi;
            if (portAc())
            {
                if (sayi < 3)
                {

                    tbKesimHane.Visible = true;
                    tbMenu.SelectedPage = tbKesimHane;
                    tabloBoyaKesimhane();
                }
                else
                {
                    tbParcalama.Visible = true;
                    tbMenu.SelectedPage = tbParcalama;
                }
            }
            else
            {
                secilenTarti = 0;
            }
        }
        private bool portAc()
        {
            bool ac = false;
            try
            {
                sp = new SerialPort(cmbPortlar[secilenTarti - 1].SelectedItem.ToString(), 9600, Parity.None, 8, StopBits.One);
                sp.Open();
                sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                ac = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Teraziye bağlanılamadı!\n" + ex.Message, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ac;
        }
        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                
                if ((secilenTarti == 5) && tbMenu.SelectedPage == tbParcalama && gcTartımBilgileriParcalama.Visible == false && cbTartimsizKayitParcalama.Checked == false)
                {
                    string gelenveri = sp.ReadExisting();
                    int ind = gelenveri.IndexOf("-");
                    int uzunluk = gelenveri.Length;
                    string urunAdi = gelenveri.Substring(0, ind);
                    string agirlik = gelenveri.Substring(ind + 1, uzunluk - ind - 1);
                    gcParcalamaEkran.Text = agirlik;
                    txtUrunAdiParcalama.Text = urunAdi;
                    txtBrutKgParcalama.Text = agirlik;
                    txtNetKgParcalama.Text = (Convert.ToDouble(agirlik) - Convert.ToDouble(txtToplamDaraParcalama.Text)).ToString();
                    if (cbOtomatikParcalama.Checked == true) parcalamaOnayla();

                }
                else if ((secilenTarti == 3 || secilenTarti == 4) && tbMenu.SelectedPage == tbParcalama && gcTartımBilgileriParcalama.Visible == false && cbTartimsizKayitParcalama.Checked == false)
                {
                    string gelenveri = sp.ReadExisting();
                    string agirlik = gelenveri.Substring(7, 7).Trim().Replace(".", ",");
                    gcParcalamaEkran.Text = agirlik;
                    txtBrutKgParcalama.Text = agirlik;
                    txtNetKgParcalama.Text = (Convert.ToDouble(agirlik) - Convert.ToDouble(txtToplamDaraParcalama.Text)).ToString();
                    if (cbOtomatikParcalama.Checked == true) parcalamaOnayla();
                }
                else if ((secilenTarti == 1 || secilenTarti == 2) && tbMenu.SelectedPage == tbKesimHane && btnKesimhaneBasla.Enabled == false)
                {
                    string gelenveri = sp.ReadExisting();
                    string agirlik = gelenveri.Substring(7, 7).Trim().Replace(".", ",");
                    gcKesimhaneEkran.Text = agirlik;
                    //txtKesimhaneBrut.Text = agirlik;
                    //txtKesimhaneNet.Text = (Convert.ToDouble(agirlik) - Convert.ToDouble(txtKesimhaneDaraKg.Text)).ToString();
                    if (cbKesimhaneOtomatik.Checked == true) kesimhaneOnayla();
                }
                else if (secilenTarti == 6 && tbMenu.SelectedPage == tbParcalama && gcTartımBilgileriParcalama.Visible == false && cbTartimsizKayitParcalama.Checked == false)
                {
                    string gelenveri = sp.ReadLine();
                    try
                    {
                        string agirlik = gelenveri.Substring(0, 8).Trim().Replace(".", ",");
                        gcParcalamaEkran.Text = agirlik;
                        txtBrutKgParcalama.Text = agirlik;
                        txtNetKgParcalama.Text = (Convert.ToDouble(agirlik) - Convert.ToDouble(txtToplamDaraParcalama.Text)).ToString();
                        //if (cbOtomatikParcalama.Checked == true) parcalamaOnayla();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void parcalamaUrunAdiDoldur()
        {
            txtUrunAdiParcalama.Items.Clear();
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                baglanti.Open();
                komut.CommandText = "select Adi from Urunler";
                SqlDataReader dr = komut.ExecuteReader();
                for (int i = 0; i < 80; i++)
                {
                    if (dr.Read())
                    {
                        if (dr[0].ToString() != "")
                        {
                            txtUrunAdiParcalama.Items.Add(dr[0].ToString());
                        }
                    }
                }
                dr.Close();
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void kisayolDoldur()
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                baglanti.Open();
                komut.CommandText = "select Adi,SiraNo from Urunler";
                SqlDataReader dr = komut.ExecuteReader();
                for (int i = 0; i < 80; i++)
                {
                    if (dr.Read())
                    {
                        if (dr[0].ToString() != "")
                        {
                            kisayolDizi[i].Text = dr[0].ToString();
                            kisayolDizi[i].ImageOptions.Image = TeraziDevEx.Properties.Resources.boorder_32x32;
                            kisayolDizi[i].ImageOptions.ImageToTextAlignment = ImageAlignToText.None;
                            kisayolDizi[i].Enabled = true;
                            kisayolSiraNo[i] = dr[1].ToString();
                        }
                        else
                        {
                            kisayolDizi[i].Text = "";
                            kisayolDizi[i].ImageOptions.Image = TeraziDevEx.Properties.Resources.delete_32x32;
                            kisayolDizi[i].ImageOptions.ImageToTextAlignment = ImageAlignToText.LeftCenter;
                            kisayolDizi[i].Enabled = false;
                            kisayolSiraNo[i] = "";
                        }
                    }
                }
                dr.Close();
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void partiIcerigiAc()
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "select SiraNo[Sıra No],KulakNo[Kulak Numarası],Agirlik[Canlı Ağırlık],Irk,Cinsiyet,Tarih[Tarih/Saat] from Hayvanlar where PartiNo=@partino order by SiraNo;";
                komut.Parameters.AddWithValue("@partino", tblPartiler.CurrentRow.Cells[0].Value.ToString());
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                tblPartiIcerigi.DataSource = dt;
                baglanti.Close();
                tblPartiIcerigi.AutoResizeColumns();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void partiArama()
        {
            string tarih = "";
            if (cbPartiAramaTarihAraligi.Checked == true) tarih = " and ( AlimTarihi between @dtp1 and @dtp2 )"; ;
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "select TOP 100 PartiNo[Parti No],MusteriAdi[Müşteri Adı],MusteriNo[Müşteri No],Adet,ToplamAgirlik[Toplam Ağırlık],GeldigiYer[Geldiği Yer],Plaka[Araç Plakası],Durum,AlimTarihi[Alım Tarihi] from Partiler where (PartiNo =@metin or MusteriAdi like '%'+@metin+'%' or MusteriNo=@metin)" + tarih + " order by AlimTarihi desc;";
                komut.Parameters.AddWithValue("@metin", txtPartiArama.Text);
                if (tarih != "")
                {
                    komut.Parameters.AddWithValue("@dtp1", dtp1Parti.Value);
                    komut.Parameters.AddWithValue("@dtp2", dtp2Parti.Value);
                }
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                tblPartiler.DataSource = dt;
                baglanti.Close();
                tblPartiler.AutoResizeColumns();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void musteriDoldurHayvanAlimi()
        {
            cmbMusteriHayvanAlimi.Items.Clear();
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "select Adi,MusteriNo from Musteriler where Tur='Satıcı';";
                baglanti.Open();
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbMusteriHayvanAlimi.Items.Add(dr.GetString(0) + " [" + dr.GetString(1) + "]");
                }
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void kullaniciArama()
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "select SicilNo[Sicil No],AdiSoyadi[Adı Soyadı],Username[Kullanıcı Adı],Password[Şifre],Departman from Kullanicilar where username!='admin' order by ID asc;";
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                tblKullanicilar.DataSource = dt;
                baglanti.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public bool noTekrarKontrol(string cmd, string prm, string username) //PRM SİCİL NO 
        {
            bool sonuc = false;
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = cmd;
                komut.Parameters.AddWithValue("@no", prm);
                komut.Parameters.AddWithValue("@user", username);
                baglanti.Open();
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read()) sonuc = true;
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sonuc;
        }

        private void btnAlimiBaslatHayvan_Click(object sender, EventArgs e)
        {
            if (cmbMusteriHayvanAlimi.SelectedIndex == -1) MessageBox.Show("Müşteri seçiniz!", "Girişlerinizi Kontrol Ediniz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtPartiNoHayvanAlimi.Text.Trim().Equals("")) MessageBox.Show("Parti No giriniz!", "Girişlerinizi Kontrol Ediniz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (Convert.ToDouble(txtToplamCanliAgirlik.Text) <= 0) MessageBox.Show("Toplam canlı ağırlık giriniz!", "Girişlerinizi Kontrol Ediniz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (Convert.ToInt32(txtAdetHayvanAlimi.Text) <= 0) MessageBox.Show("Adet giriniz!", "Girişlerinizi Kontrol Ediniz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    if (btnAlimiBaslatHayvan.Text == "Alımı Başlat")
                    {
                        if (noTekrarKontrol("select * from Partiler where PartiNo=@no", txtPartiNoHayvanAlimi.Text, "")) MessageBox.Show("Bu Parti Numarası zaten kayıtlı!", "Girişlerinizi Kontrol Ediniz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                            SqlCommand komut = new SqlCommand();
                            komut.Connection = baglanti;
                            komut.CommandText = "INSERT INTO [Terazi].[dbo].[Partiler] (PartiNo,MusteriAdi,MusteriNo,Adet,ToplamAgirlik,GeldigiYer,Plaka,Durum,AlimTarihi) values (@partino,@musteriadi,@musterino,@adet,@toplamagirlik,@yer,@plaka,'Kesim Bekleniyor...',@tarih);";
                            komut.Parameters.AddWithValue("@partino", txtPartiNoHayvanAlimi.Text);
                            komut.Parameters.AddWithValue("@musteriadi", cmbMusteriHayvanAlimi.SelectedItem.ToString().Substring(0, cmbMusteriHayvanAlimi.SelectedItem.ToString().IndexOf("[") - 1));
                            komut.Parameters.AddWithValue("@musterino", IDBul(cmbMusteriHayvanAlimi.SelectedItem.ToString(), "[", "]"));
                            komut.Parameters.AddWithValue("@adet", Convert.ToInt32(txtAdetHayvanAlimi.Text));
                            komut.Parameters.AddWithValue("@toplamagirlik", Convert.ToDouble(txtToplamCanliAgirlik.Text));
                            komut.Parameters.AddWithValue("@tarih", DateTime.Now);
                            komut.Parameters.AddWithValue("@yer", txtGeldigiYer.Text);
                            komut.Parameters.AddWithValue("@plaka", txtAracPlaka.Text);
                            baglanti.Open();
                            komut.ExecuteNonQuery();
                            komut.CommandText = "INSERT INTO [Terazi].[dbo].[Hayvanlar] (PartiNo,SiraNo,Agirlik) values (@partino,@sirano,@agirlik);";
                            double agirlik = Convert.ToDouble(txtToplamCanliAgirlik.Text) / Convert.ToDouble(txtAdetHayvanAlimi.Text);
                            for (int i = 0; i < Convert.ToInt32(txtAdetHayvanAlimi.Text); i++)
                            {
                                komut.Parameters.Clear();
                                komut.Parameters.AddWithValue("@partino", txtPartiNoHayvanAlimi.Text);
                                komut.Parameters.AddWithValue("@sirano", i + 1);
                                komut.Parameters.AddWithValue("@agirlik", Math.Round(agirlik, 1));
                                komut.ExecuteNonQuery();
                            }
                            logKayitYap("* * HAYVAN ALIM GİRİŞİ * *", "Parti No:" + txtPartiNoHayvanAlimi.Text + " Ağırlık:" + txtToplamCanliAgirlik.Text + " Adet:" + txtAdetHayvanAlimi.Text);
                            baglanti.Close();
                            partiArama();
                            txtAracPlaka.Enabled = false;
                            txtGeldigiYer.Enabled = false;
                            cmbMusteriHayvanAlimi.Enabled = false;
                            txtPartiNoHayvanAlimi.Enabled = false;
                            txtToplamCanliAgirlik.Enabled = false;
                            btnAlimiBaslatHayvan.Enabled = false;
                            txtAdetHayvanAlimi.Enabled = false;
                            gcPartiArama.Enabled = false;
                            txtKulakNoHayvanAlimi.Enabled = true;
                            cmbIrk.Enabled = true;
                            cmbCinsiyet.Enabled = true;
                            btnOnaylaHayvanAlimi.Enabled = true;
                            btnGeriAl.Enabled = true;
                            btnAlimiIptalEt.Enabled = true;
                            cmbIrk.SelectedIndex = 0;
                            cmbCinsiyet.SelectedIndex = 0;
                            partiIcerigiAc();
                            txtKulakNoHayvanAlimi.Focus();
                        }
                    }
                    else
                    {
                        if (txtPartiNoHayvanAlimi.Text != tblPartiler.SelectedRows[0].Cells[0].Value.ToString() && noTekrarKontrol("select * from Partiler where PartiNo=@no", txtPartiNoHayvanAlimi.Text, "")) MessageBox.Show("Bu Parti Numarası zaten kayıtlı!", "Girişlerinizi Kontrol Ediniz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                            SqlCommand komut = new SqlCommand();
                            komut.Connection = baglanti;
                            komut.CommandText = "update Partiler set PartiNo=@txtpartino,MusteriAdi=@adi,MusteriNo=@mno,Adet=@adet,ToplamAgirlik=@tagirlik,Plaka=@plaka,GeldigiYer=@yer where PartiNo=@partino";
                            komut.Parameters.AddWithValue("@txtpartino", txtPartiNoHayvanAlimi.Text);
                            komut.Parameters.AddWithValue("@adi", cmbMusteriHayvanAlimi.SelectedItem.ToString().Substring(0, cmbMusteriHayvanAlimi.SelectedItem.ToString().IndexOf("[") - 1));
                            komut.Parameters.AddWithValue("@mno", IDBul(cmbMusteriHayvanAlimi.SelectedItem.ToString(), "[", "]"));
                            komut.Parameters.AddWithValue("@adet", Convert.ToInt32(txtAdetHayvanAlimi.Text));
                            komut.Parameters.AddWithValue("@tagirlik", Convert.ToDouble(txtToplamCanliAgirlik.Text));
                            komut.Parameters.AddWithValue("@partino", tblPartiler.SelectedRows[0].Cells[0].Value.ToString());
                            komut.Parameters.AddWithValue("@plaka", txtAracPlaka.Text);
                            komut.Parameters.AddWithValue("@yer", txtGeldigiYer.Text);
                            baglanti.Open();
                            komut.ExecuteNonQuery();
                            baglanti.Close();

                            cmbMusteriHayvanAlimi.SelectedIndex = -1;
                            txtPartiNoHayvanAlimi.Clear();
                            txtAdetHayvanAlimi.Clear();
                            txtToplamCanliAgirlik.Clear();
                            txtGeldigiYer.Clear();
                            txtAracPlaka.Clear();
                            btnAlimiBaslatHayvan.Text = "Alımı Başlat";
                            btnAlimiBaslatHayvan.ImageOptions.Image = TeraziDevEx.Properties.Resources.play_32x32;
                            tblPartiler.Enabled = true;
                            partiArama();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnKaydetHayvanAlimi_Click(object sender, EventArgs e)
        {
            partiArama();
            txtKulakNoHayvanAlimi.Clear();
            cmbMusteriHayvanAlimi.SelectedIndex = -1;
            txtToplamCanliAgirlik.Clear();
            txtAdetHayvanAlimi.Clear();
            txtPartiNoHayvanAlimi.Clear();
            txtAracPlaka.Enabled = true;
            txtGeldigiYer.Enabled = true;
            cmbMusteriHayvanAlimi.Enabled = true;
            txtPartiNoHayvanAlimi.Enabled = true;
            txtToplamCanliAgirlik.Enabled = true;
            btnAlimiBaslatHayvan.Enabled = true;
            txtAdetHayvanAlimi.Enabled = true;
            gcPartiArama.Enabled = true;
            txtKulakNoHayvanAlimi.Enabled = false;
            cmbIrk.Enabled = false;
            cmbCinsiyet.Enabled = false;
            btnOnaylaHayvanAlimi.Enabled = false;
            btnGeriAl.Enabled = false;
            cmbIrk.SelectedIndex = -1;
            cmbCinsiyet.SelectedIndex = -1;
            btnKaydetHayvanAlimi.Enabled = false;
            btnAlimiIptalEt.Enabled = false;
            partiIcerigiAc();
            string[] secilenparti = new string[tblPartiler.SelectedRows.Count];
            for (int i = 0; i < tblPartiler.SelectedRows.Count; i++)
            {
                secilenparti[i] = tblPartiler.SelectedRows[i].Cells[0].Value.ToString();
            }
            rapor r = new rapor();
            r.raporla(secilenparti);
            r.Show();

        }


        private void btnOnaylaHayvanAlimi_Click(object sender, EventArgs e)
        {
            if (txtKulakNoHayvanAlimi.Text.Trim().Equals(string.Empty)) MessageBox.Show("Kulak numarası giriniz!", "Girişlerinizi Kontrol Ediniz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (btnOnaylaHayvanAlimi.Text != "Kaydet" && noTekrarKontrol("select * from Hayvanlar where KulakNo=@no", txtKulakNoHayvanAlimi.Text, "")) MessageBox.Show("Bu kulak numarası zaten kayıtlı!", "Girişlerinizi Kontrol Ediniz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                try
                {
                    hayvanAlimSiraNo++;
                    if (btnOnaylaHayvanAlimi.Text == "Kaydet")
                    {
                        hayvanAlimSiraNo = Convert.ToInt32(tblPartiIcerigi.CurrentRow.Cells[0].Value);
                    }
                    komut.CommandText = "update Hayvanlar set KulakNo=@kulakno,Irk=@irk,Cinsiyet=@cinsiyet,Tarih=@tarih where PartiNo=@partino and SiraNo=@sirano";
                    komut.Parameters.AddWithValue("@kulakno", txtKulakNoHayvanAlimi.Text);
                    komut.Parameters.AddWithValue("@irk", cmbIrk.SelectedItem.ToString());
                    komut.Parameters.AddWithValue("@cinsiyet", cmbCinsiyet.SelectedItem.ToString());
                    komut.Parameters.AddWithValue("@partino", tblPartiler.CurrentRow.Cells[0].Value.ToString());
                    komut.Parameters.AddWithValue("@sirano", hayvanAlimSiraNo);
                    komut.Parameters.AddWithValue("@tarih", DateTime.Now);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    logKayitYap("* * HAYVAN GİRİŞİ * *", "Parti No:" + txtPartiNoHayvanAlimi.Text + " Küpe No:" + txtKulakNoHayvanAlimi.Text);
                    partiIcerigiAc();
                    if (btnOnaylaHayvanAlimi.Text != "Kaydet")
                    {

                        if (Convert.ToInt32(txtAdetHayvanAlimi.Text) == hayvanAlimSiraNo)
                        {
                            btnOnaylaHayvanAlimi.Enabled = false;
                            btnGeriAl.Enabled = false;
                            hayvanAlimSiraNo = 0;
                            btnKaydetHayvanAlimi.Enabled = true;
                            btnKaydetHayvanAlimi.Focus();
                        }
                        else
                        {
                            txtKulakNoHayvanAlimi.Clear();
                            tblPartiIcerigi.ClearSelection();
                            tblPartiIcerigi.Rows[hayvanAlimSiraNo].Selected = true;
                            tblPartiIcerigi.CurrentCell = tblPartiIcerigi.Rows[hayvanAlimSiraNo].Cells[0];
                            txtKulakNoHayvanAlimi.Focus();
                        }
                    }
                    else
                    {
                        logKayitYap("* * HAYVAN DÜZENLEME * *", "Parti No:" + txtPartiNoHayvanAlimi.Text + " Küpe No:" + txtKulakNoHayvanAlimi.Text);
                        txtKulakNoHayvanAlimi.Clear();
                        cmbIrk.SelectedIndex = -1;
                        cmbCinsiyet.SelectedIndex = -1;
                        btnOnaylaHayvanAlimi.Enabled = false;
                        btnOnaylaHayvanAlimi.Text = "Onayla";
                        btnOnaylaHayvanAlimi.ImageOptions.Image = TeraziDevEx.Properties.Resources.play_32x32;
                        tblPartiIcerigi.Enabled = true;
                        gcPartiArama.Enabled = true;
                        cmbMusteriHayvanAlimi.Enabled = true;
                        txtPartiNoHayvanAlimi.Enabled = true;
                        txtAdetHayvanAlimi.Enabled = true;
                        txtToplamCanliAgirlik.Enabled = true;
                        btnAlimiBaslatHayvan.Enabled = true;
                        txtKulakNoHayvanAlimi.Enabled = false;
                        cmbIrk.Enabled = false;
                        cmbCinsiyet.Enabled = false;
                        btnKaydetHayvanAlimi.Focus();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGeriAl_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            try
            {
                komut.CommandText = "update Hayvanlar set KulakNo=NULL,Irk=NULL,Cinsiyet=NULL,Tarih=NULL where PartiNo=@partino and SiraNo=@sirano";
                komut.Parameters.AddWithValue("@partino", tblPartiler.CurrentRow.Cells[0].Value.ToString());
                komut.Parameters.AddWithValue("@sirano", hayvanAlimSiraNo);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                logKayitYap("* * ALIM GERİ ALMA * *", "Parti No:" + tblPartiler.CurrentRow.Cells[0].Value.ToString());
                hayvanAlimSiraNo--;
                if (hayvanAlimSiraNo < 0) hayvanAlimSiraNo = 0;
                partiIcerigiAc();
                tblPartiIcerigi.ClearSelection();
                tblPartiIcerigi.Rows[hayvanAlimSiraNo].Selected = true;
                tblPartiIcerigi.CurrentCell = tblPartiIcerigi.Rows[hayvanAlimSiraNo].Cells[0];

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void partiSil()
        {
            string partiNo = tblPartiler.CurrentRow.Cells[0].Value.ToString();
            DialogResult sonuc = MessageBox.Show(partiNo + " numaralı parti kaydını silmek istediğinize emin misiniz?\nPartiye ait hayvan alım kayıtları ve varsa kesim tartımları da silinecektir!\nBu işlem geri alınamaz!", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sonuc == DialogResult.Yes)
            {
                try
                {
                    SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                    SqlCommand komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = "delete from Partiler where PartiNo=@partino";
                    komut.Parameters.AddWithValue("@partino", partiNo);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    komut.CommandText = "delete from Hayvanlar where PartiNo=@partino";
                    komut.ExecuteNonQuery();
                    komut.CommandText = "delete from IslemKaydi where PartiNo=@partino";
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    partiArama();
                    logKayitYap("* * HAYVAN ALIM SİLME * *", "Parti No:" + partiNo);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            partiSil();
        }
        public string IDBul(string metin, string basla, string bitir)
        {
            string sonuc;
            try
            {
                int IcerikBaslangicIndex = metin.IndexOf(basla) + basla.Length;
                int IcerikBitisIndex = metin.Substring(IcerikBaslangicIndex).IndexOf(bitir);
                sonuc = metin.Substring(IcerikBaslangicIndex, IcerikBitisIndex);
            }
            catch (Exception)
            {
                sonuc = null;
            }
            return sonuc;
        }
        private void pcb1_MouseEnter(object sender, EventArgs e)
        {
            pcbKesimhaneBuyuk.Image = TeraziDevEx.Properties.Resources.basterred;
            lbl1.ForeColor = Color.IndianRed;
            lblt1.ForeColor = Color.IndianRed;
        }

        private void pcb2_MouseEnter(object sender, EventArgs e)
        {
            pcbKesimhaneKucuk.Image = TeraziDevEx.Properties.Resources.basterred;
            lbl2.ForeColor = Color.IndianRed;
            lblt2.ForeColor = Color.IndianRed;
        }

        private void pcb3_MouseEnter(object sender, EventArgs e)
        {
            pcb3.Image = TeraziDevEx.Properties.Resources.basterred;
            lbl3.ForeColor = Color.IndianRed;
            lblt3.ForeColor = Color.IndianRed;
        }

        private void pcb4_MouseEnter(object sender, EventArgs e)
        {
            pcb4.Image = TeraziDevEx.Properties.Resources.basterred;
            lbl4.ForeColor = Color.IndianRed;
            lblt4.ForeColor = Color.IndianRed;
        }

        private void pcb5_MouseEnter(object sender, EventArgs e)
        {
            pcb5.Image = TeraziDevEx.Properties.Resources.basterred;
            lbl5.ForeColor = Color.IndianRed;
            lblt5.ForeColor = Color.IndianRed;
        }

        private void pcb6_MouseEnter(object sender, EventArgs e)
        {
            pcb6.Image = TeraziDevEx.Properties.Resources.basterred;
            lbl6.ForeColor = Color.IndianRed;
            lblt6.ForeColor = Color.IndianRed;
        }

        private void pcb1_MouseLeave(object sender, EventArgs e)
        {
            pcbKesimhaneBuyuk.Image = TeraziDevEx.Properties.Resources.baster;
            lbl1.ForeColor = Color.DodgerBlue;
            lblt1.ForeColor = Color.DodgerBlue;
        }

        private void pcb2_MouseLeave(object sender, EventArgs e)
        {
            pcbKesimhaneKucuk.Image = TeraziDevEx.Properties.Resources.baster;
            lbl2.ForeColor = Color.DodgerBlue;
            lblt2.ForeColor = Color.DodgerBlue;
        }

        private void pcb3_MouseLeave(object sender, EventArgs e)
        {
            pcb3.Image = TeraziDevEx.Properties.Resources.baster;
            lbl3.ForeColor = Color.DodgerBlue;
            lblt3.ForeColor = Color.DodgerBlue;
        }

        private void pcb4_MouseLeave(object sender, EventArgs e)
        {
            pcb4.Image = TeraziDevEx.Properties.Resources.baster;
            lbl4.ForeColor = Color.DodgerBlue;
            lblt4.ForeColor = Color.DodgerBlue;
        }

        private void pcb5_MouseLeave(object sender, EventArgs e)
        {
            pcb5.Image = TeraziDevEx.Properties.Resources.baster;
            lbl5.ForeColor = Color.DodgerBlue;
            lblt5.ForeColor = Color.DodgerBlue;
        }

        private void pcb6_MouseLeave(object sender, EventArgs e)
        {
            pcb6.Image = TeraziDevEx.Properties.Resources.baster;
            lbl6.ForeColor = Color.DodgerBlue;
            lblt6.ForeColor = Color.DodgerBlue;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            FuncUser login = new FuncUser(); //USER LOGİN
            string girissonuc = login.userlogin(txtKAdi.Text, txtSifre.Text);
            if (girissonuc == "OK")
            {
                tbMenu.Pages[0].Visible = false;
                lblKullanici.Text = txtKAdi.Text;
                kullaniciYetkiDenetle();
                departmanKontrol();
                lblKullanici.Visible = true;
                logKayitYap("* * KULLANICI GİRİŞİ * * ", lblKullanici.Text + " Adlı Kullanıcı Giriş Yaptı.");
                if(kurum!="hayvanalimi")new Process() { StartInfo = new ProcessStartInfo("cmd") { Verb = "runas", Arguments = "/C devcon enable USB\\VID_1A*" } }.Start();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı/Şifre hatalı.");
            }
        }

        private void departmanKontrol() //DEPARTMAN
        {
            string okunan = "";
            SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
            SqlCommand komut = new SqlCommand();
            try
            {
                komut.Connection = conn;
                komut.CommandText = "SELECT Departman FROM Kullanicilar WHERE username=@username;";
                komut.Parameters.AddWithValue("@username", lblKullanici.Text);
                conn.Open();
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    okunan = dr[0].ToString().Trim();
                }
                conn.Close();
                //MessageBox.Show("\""+okunan+"\"");
                if (okunan == "Teknik")
                {
                    tbMenu.Pages[1].Visible = true; //TERAZİ
                    tbMenu.Pages[2].Visible = true; //MÜŞTERİ
                    tbMenu.Pages[3].Visible = true; //KESİMHANE
                    tbMenu.Pages[4].Visible = true; //HAYVAN ALIM
                    tbMenu.Pages[5].Visible = true; //PARÇALAMA
                    tbMenu.Pages[6].Visible = true; //TARTIM 
                    tbMenu.Pages[8].Visible = true; //YÖNETİM
                    tbMenu.SelectedPage = tbYonetim;
                }
                if (okunan == "Yönetim")
                {
                    tbMenu.Pages[1].Visible = true; //TERAZİ
                    tbMenu.Pages[2].Visible = true; //MÜŞTERİ
                    tbMenu.Pages[3].Visible = true; //KESİMHANE
                    tbMenu.Pages[4].Visible = true; //HAYVAN ALIM
                    tbMenu.Pages[5].Visible = true; //PARÇALAMA
                    tbMenu.Pages[6].Visible = true; //TARTIM 
                    tbMenu.Pages[8].Visible = true; //YÖNETİM
                    tbMenu.SelectedPage = tbYonetim;
                }
                if (okunan == "Kesimhane")
                {
                    tbMenu.Pages[1].Visible = true; //TERAZİ
                    tbMenu.Pages[6].Visible = true; //TARTIM
                    tbMenu.SelectedPage = tbTarti;

                }
                if (okunan == "Parçalama")
                {
                    tbMenu.Pages[1].Visible = true; //TERAZİ
                    tbMenu.Pages[6].Visible = true; //TARTIM 
                    tbMenu.SelectedPage = tbTarti;

                }
                if (okunan == "Hayvan Alım")
                {
                    tbMenu.Pages[2].Visible = true; //MÜŞTERİ
                    tbMenu.Pages[4].Visible = true; //HAYVAN ALIM
                    tbTartimlar.Visible = true;
                    tbMenu.SelectedPage = tbHayvanAlimi;
                }

            }
            catch (SqlException ex)
            {
                conn.Close();
                MessageBox.Show("Departman alınamadı.\n Hata: " + ex, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void kullaniciYetkiDenetle()
        {
            string okunan = "";
            SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
            SqlCommand komut = new SqlCommand();
            try
            {
                komut.Connection = conn;
                komut.CommandText = "SELECT Yetkiler FROM [Terazi].[dbo].[Kullanicilar] WHERE username=@username;";
                komut.Parameters.AddWithValue("@username", txtKAdi.Text);
                conn.Open();
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    okunan = dr[0].ToString();
                }
                conn.Close();

            }
            catch (SqlException ex)
            {
                conn.Close();
                MessageBox.Show("Yetki alınamadı.\n Hata: " + ex, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            grpKesimGirispaneli.Enabled = false;
            grpParcalamaMenu.Enabled = false;
            gcKisayollar.Enabled = false;
            msiDuzenle.Enabled = false;
            silToolStripMenuItem.Enabled = false;
            gcurunler.Enabled = false;
            gcKullanicilar.Enabled = false;
            tbAyarlar.Visible = false;

            char[] okunanchar = okunan.ToCharArray();
            for (int i = 0; i < okunanchar.Length; i++)
            {
                if (okunanchar[i].ToString() == "1")
                {
                    grpKesimGirispaneli.Enabled = true;
                    grpParcalamaMenu.Enabled = true;
                    gcKisayollar.Enabled = true;
                }
                if (okunanchar[i].ToString() == "2")
                {
                    tbMenu.Pages[7].Visible = true;
                    //yetki += "Tüm İşlemleri Görüntüleme\n";
                }
                if (okunanchar[i].ToString() == "3")
                {
                    msiDuzenle.Enabled = true;
                    silToolStripMenuItem.Enabled = true;
                    btnDuzenleParcalama.Enabled = true;
                    btnSilParcalama.Enabled = true;
                    //yetki += "İşlem Düzenleme/Silme\n";
                }
                if (okunanchar[i].ToString() == "4")
                {
                    gcurunler.Enabled = true;
                    //yetki += "Ürünler Yönetimi\n";
                }
                if (okunanchar[i].ToString() == "5")
                {
                    gcKullanicilar.Enabled = true;
                    //yetki += "Kullanıcılar Yönetimi\n";
                }
                if (okunanchar[i].ToString() == "6")
                {
                    tbMenu.Pages[9].Visible = true;
                    // yetki += "Ayarlara Erişim\n"; 
                }
                if (okunanchar[i].ToString() == "0")
                {
                    //yetki += "Kullanıcı Yetkisi Yok.\n";
                }
            }
        }

        private void pcb1_Click(object sender, EventArgs e)
        {
            tartisec(1);
        }

        private void pcb2_Click(object sender, EventArgs e)
        {
            tartisec(2);
        }

        private void pcb3_Click(object sender, EventArgs e)
        {
            tartisec(3);
        }

        private void pcb4_Click(object sender, EventArgs e)
        {
            tartisec(4);
        }

        private void pcb5_Click(object sender, EventArgs e)
        {
            tartisec(5);
        }

        private void pcb6_Click(object sender, EventArgs e)
        {
            tartisec(6);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbParcalamaAdet.SelectedIndex = 0;
            tbMenu.SelectedPage = tbKullanici;
            txtKAdi.Select();
            tbMenu.Pages[0].Visible = true;
            tbMenu.Pages[1].Visible = false;
            tbMenu.Pages[2].Visible = false;
            tbMenu.Pages[3].Visible = false;
            tbMenu.Pages[4].Visible = false;
            tbMenu.Pages[5].Visible = false;
            tbMenu.Pages[6].Visible = false;
            tbMenu.Pages[7].Visible = false;
            tbMenu.Pages[8].Visible = false;
            tbMenu.Pages[9].Visible = false;
            lblKullanici.Text = "Kullanıcı";
            //DTP
            baslangicOzelAyarUygula();
            dtpLog1.Value = DateTime.Now.AddMonths(-1);
            string[] portlar = SerialPort.GetPortNames();
            foreach (string prt in portlar)
            {
                cmbCOMPort1.Items.Add(prt);
            }
            tmrOtoYenile.Start();
        }
        private void tbMenu_SelectedPageChanged(object sender, TabFormSelectedPageChangedEventArgs e)
        {

            if (tbMenu.SelectedPage == tbHayvanAlimi)
            {
                partiArama();
                musteriDoldurHayvanAlimi();
            }
            else if (tbMenu.SelectedPage == tbTarti)
            {
                lbl1.Text = txtTeraziAdi1.Text;
                lbl2.Text = txtTeraziAdi2.Text;
                lbl3.Text = txtTeraziAdi3.Text;
                lbl4.Text = txtTeraziAdi4.Text;
                lbl5.Text = txtTeraziAdi5.Text;
                lbl6.Text = txtTeraziAdi6.Text;
                teraziEnableKontrol();
            }
            else if (tbMenu.SelectedPage == tbMusteriler) // MUSTERİLER 
            {
                FuncMusteri tabloal = new FuncMusteri();
                tblMusteriler.DataSource = tabloal.tablodoldur("", true);
                txtMusteriToplam.Text = tabloal.satirsayisi().ToString();
                kutusifirla();
            }
            else if (tbMenu.SelectedPage == tbYonetim) //YONETİM AÇILDIĞINDA 
            {
                kullaniciButonSifirla();
                kullaniciArama();
                urunArama();
            }
            else if (tbMenu.SelectedPage == tbKesimHane)
            {
                kesimhaneButonPasifle();
                kesimhanePartiAl();
                tabloBoyaKesimhane();
                if (sp != null)
                {
                    if (sp.IsOpen)
                    {
                        gcKesimhaneEkran.Text = "0,0";
                        lblKesimhaneDurum.Text = "Terazi verisi bekleniyor...";
                        lblKesimhaneDurum.ForeColor = Color.DarkGreen;
                    }
                    else
                    {
                        gcKesimhaneEkran.Text = "-----";
                        lblKesimhaneDurum.Text = "Terazi bağlantısı yok!";
                        lblKesimhaneDurum.ForeColor = Color.DarkRed;
                    }
                }
                else gcKesimhaneEkran.Text = "-----";
            }
            else if (tbMenu.SelectedPage == tbAyarlar)
            {
                string[] portlar = SerialPort.GetPortNames();
                foreach (string prt in portlar)
                {
                    cmbCOMPort1.Items.Add(prt);
                }

            }
            else if (tbMenu.SelectedPage == tbParcalama)
            {
                if (sp != null)
                {
                    if (sp.IsOpen) gcParcalamaEkran.Text = "0,0";
                    else gcParcalamaEkran.Text = "-----";
                }
                else gcParcalamaEkran.Text = "-----";
                kisayolDoldur();
                cmbTartimTurParcalama.SelectedIndex = 0;
                tartimNoDoldur();
                parcalamaTartimCek();
                parcalamaUrunAdiDoldur();
            }
            else if (tbMenu.SelectedPage == tbTumIslemler)
            {
                comboTumIslemlerKullaniciDoldur();
                comboIslemDoldurTumIslemler();
                logKayitTabloDoldur();
                chTumIslemlerFiltre.Checked = true;
                chTumIslemlerFiltre.Checked = false;
            }
            else if (tbMenu.SelectedPage == tbTartimlar)
            {
                dtpTartimlar1.Value = DateTime.Now.AddMonths(-1);
                dtpTartimlar2.Value = DateTime.Now;
                chTartimlarFiltrele.Checked = true;
                chTartimlarFiltrele.Checked = false;
                cmbTartimlarNoktalar.SelectedIndex = 0;//PARÇALAMA
                btnTartimlarIncele.Enabled = false;
                btnTartimlarRaporla.Enabled = false;
                btnTartimlarKapat.Enabled = false;
            }
        }


        public void comboTumIslemlerKullaniciDoldur()
        {
            cmbTumIslemlerKullanici.Items.Clear();
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT DISTINCT KULLANICI FROM TUMISLEMLER;";
                baglanti.Open();
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbTumIslemlerKullanici.Items.Add(dr[0].ToString());
                }
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void comboIslemDoldurTumIslemler() //TÜM İŞLEMLER 
        {
            cmbTumIslemler.Items.Clear();
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT DISTINCT ISLEMADI FROM TUMISLEMLER;";
                baglanti.Open();
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbTumIslemler.Items.Add(dr[0].ToString());
                }
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void tartimNoDoldur() //DATA BASE DEN TARTIM NUMARALARI ÇEKİLİR.
        {
            cmbTartimNoParcalama.Items.Clear();
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT TartimNo FROM [Terazi].[dbo].[ParcalamaTartimlar] where Durum=0 order by Tarih desc;";
                baglanti.Open();
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbTartimNoParcalama.Items.Add(dr[0].ToString());
                }
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void parcalamaTartimCek()//DETAYSIZ TARTIM LİSTESİ PARCALAMA TABLOSUNA CEKİLİR.
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "select top 100 TartimNo[Tartım No],Tur[Tür],HareketYeri[Hareket Yeri],Aciklama[Ek Açıklama],Tarih[Kayıt Tarihi],DuzenlemeTarihi[Düzenleme Tarihi] from ParcalamaTartimlar where Durum=0 order by Tarih desc;";
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                tblParcalamaKayit.DataSource = dt;
                baglanti.Close();
                tartimNoDoldur();
                gcKisayollar.Enabled = false;
                if (tblParcalamaKayit.Rows.Count > 0)
                {
                    if (gcTartımBilgileriParcalama.Visible == true) cmbTartimNoParcalama.Text = tblParcalamaKayit.CurrentRow.Cells[0].Value.ToString();
                    else
                    {
                        if (cbTartimsizKayitParcalama.Checked == false && tblParcalamaKayit.SelectedRows[0].Cells[4].Value.ToString() == "0")
                        {
                            txtPartiNoParcalama.Text = tblParcalamaKayit.SelectedRows[0].Cells[1].Value.ToString();
                            txtUrunAdiParcalama.Text = tblParcalamaKayit.SelectedRows[0].Cells[2].Value.ToString();
                            btnDuzenleParcalama.Enabled = false;
                        }
                        else
                        {
                            txtPartiNoParcalama.Text = "";
                            txtUrunAdiParcalama.Text = "";
                            btnDuzenleParcalama.Enabled = true;
                        }
                    }
                }
                else
                {
                    cmbTartimNoParcalama.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void tartimNoAra() // COMBOBOX'A GİRİLEN DEĞER TARTIM LİSTESİNDE ARANIR.
        {
            if (!cmbTartimNoParcalama.Text.Trim().Equals(""))
            {
                try
                {
                    SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                    SqlCommand komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = "select TartimNo,Tur,HareketYeri,Aciklama,Durum from ParcalamaTartimlar where TartimNo=@parametre1;";
                    komut.Parameters.AddWithValue("@parametre1", cmbTartimNoParcalama.Text);
                    baglanti.Open();
                    SqlDataReader dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr[4].ToString() == "0")
                        {
                            cmbTartimTurParcalama.SelectedItem = dr[1].ToString();
                            txtHareketYeriParcalama.Text = dr[2].ToString();
                            txtAciklamaParcalama.Text = dr[3].ToString();
                            btnYeniTartımParcalama.Enabled = false;
                            btnTartimaDevamEtParcalama.Enabled = true;
                            btnTartimiSilParcalama.Enabled = true;
                            btnRaporlaParcalama2.Enabled = true;
                        }
                        else
                        {
                            cmbTartimTurParcalama.SelectedIndex = -1;
                            txtAciklamaParcalama.Text = "";
                            txtHareketYeriParcalama.Text = "";
                            btnYeniTartımParcalama.Enabled = false;
                            btnTartimaDevamEtParcalama.Enabled = false;
                            btnTartimiSilParcalama.Enabled = false;
                            btnRaporlaParcalama2.Enabled = false;
                        }
                    }
                    else
                    {
                        cmbTartimTurParcalama.SelectedIndex = -1;
                        txtAciklamaParcalama.Text = "";
                        txtHareketYeriParcalama.Text = "";
                        btnYeniTartımParcalama.Enabled = true;
                        btnTartimaDevamEtParcalama.Enabled = false;
                        btnTartimiSilParcalama.Enabled = false;
                        btnRaporlaParcalama2.Enabled = false;
                    }
                    baglanti.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                cmbTartimTurParcalama.SelectedIndex = -1;
                txtAciklamaParcalama.Text = "";
                txtHareketYeriParcalama.Text = "";
                btnYeniTartımParcalama.Enabled = false;
                btnTartimaDevamEtParcalama.Enabled = false;
                btnTartimiSilParcalama.Enabled = false;
            }
        }

        public void parcalamaTartimIcerikCek() // TARTIM SEÇİLİNCE TABLO DOLACAK
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "select Sira[Sıra No],PartiNo[Parti No],UrunAdi[Ürün Adı],Adet, BrutKg[Brüt Ağırlık],Dara[Dara Ağırlık],NetKg[Net Ağırlık],Tarih from ParcalamaIcerik where TartimNo=@tartimno order by Sira";
                komut.Parameters.AddWithValue("@tartimno", cmbTartimNoParcalama.Text);
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                tblParcalamaKayit.DataSource = dt;
                baglanti.Close();
                if (tblParcalamaKayit.RowCount > 0) tblParcalamaKayit.CurrentCell = tblParcalamaKayit.Rows[tblParcalamaKayit.RowCount - 1].Cells[0];
                gcKisayollar.Enabled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void urunArama() //YONETİM ÜRÜN TABLO DOLDURMA 
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "select SiraNo[Sıra No],Adi[Ürün Adı] from Urunler order by SiraNo;";
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                tblUrunler.DataSource = dt;
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tblUrunler.ClearSelection();
        }
        private void btnYeniMusteri_Click(object sender, EventArgs e) //MÜŞTERİ BUTONLARI AÇILIR
        {
            btnYeniMusteri.Enabled = false;
            btnMusteriSil.Enabled = false;
            btnMusteriDuzenle.Enabled = false;
            btnMusteriKaydet.Enabled = true;
            btnMusteriIptal.Enabled = true;
            txtMusteriNo.Enabled = true;
            txtMusteriAdi.Enabled = true;
            cmbMusteriTur.Enabled = true;
            txtMusteriTC.Enabled = true;
            txtMusteriIletisim.Enabled = true;
            txtMusteriAdres.Enabled = true;
            txtMustseriIban.Enabled = true;
        }

        private void kullaniciButonSifirla() //KULLANICI BUTONLARI
        {
            txtKullanicilarAdi.Enabled = false;
            txtKullanicilarSicilNo.Enabled = false;
            txtKullanicilarUsername.Enabled = false;
            txtKullanicilarSifre.Enabled = false;
            cmbKullanicilarDepartman.Enabled = false;
            cmbKullanicilarDepartman.SelectedIndex = -1;
            btnKullanicilarYeni.Enabled = true;
            btnKullanicilarDuzenle.Enabled = true;
            btnKullanicilarSil.Enabled = true;
            btnKullanicilarKaydet.Enabled = false;
            btnKullanicilarIptal.Enabled = false;
            txtKullanicilarSicilNo.Text = "";
            txtKullanicilarAdi.Text = "";
            txtKullanicilarUsername.Text = "";
            txtKullanicilarSifre.Text = "";
            chIslemDuzenleme.Enabled = false;
            chIslemKaydi.Enabled = false;
            chUrunlerYetki.Enabled = false;
            chTumIslemleriGoruntule.Enabled = false;
            chKullanicilarYetki.Enabled = false;
            chAyarlarYetki.Enabled = false;
            chIslemKaydi.Checked = false;
            chIslemDuzenleme.Checked = false;
            chTumIslemleriGoruntule.Checked = false;
            chUrunlerYetki.Checked = false;
            chKullanicilarYetki.Checked = false;
            chAyarlarYetki.Checked = false;
        }
        private void btnMusteriKaydet_Click(object sender, EventArgs e)  //KAYDET VE DÜZENLE
        {
            FuncMusteri kaydet = new FuncMusteri();
            if (txtMusteriNo.Enabled == true)
            {
                bool sonuc = kaydet.Musterikaydet(txtMusteriNo.Text, txtMusteriAdi.Text, cmbMusteriTur.Text, txtMusteriTC.Text, txtMusteriIletisim.Text, txtMusteriAdres.Text, txtMustseriIban.Text);
                if (sonuc == true)
                {
                    logKayitYap(" * * MÜŞTERİ KAYDI * * ", txtMusteriNo.Text + " ile Yeni Müşteri Kaydı Yapıldı.");
                    kutusifirla();
                }
            }
            else if (txtMusteriNo.Enabled == false)
            {
                bool sonuc = kaydet.MusteriDuzenle(txtMusteriNo.Text, txtMusteriAdi.Text, cmbMusteriTur.Text, txtMusteriTC.Text, txtMusteriIletisim.Text, txtMusteriAdres.Text, txtMustseriIban.Text);
                if (sonuc == true)
                {
                    logKayitYap(" * * MÜŞTERİ DÜZENLEME * * ", txtMusteriNo.Text + " nolu Müşteri Bilgileri Düzenlendi.");
                    kutusifirla();
                }
            }
            tblMusteriler.DataSource = kaydet.tablodoldur("", true);
            txtMusteriToplam.Text = kaydet.satirsayisi().ToString();
        }

        public void kutusifirla()
        {
            txtMusteriAdi.Text = "";
            txtMusteriAdres.Text = "";
            txtMusteriNo.Text = "";
            txtMustseriIban.Text = "";
            txtMusteriIletisim.Text = "";
            txtMusteriTC.Text = "";
            cmbMusteriTur.SelectedIndex = -1;
            txtMusteriNo.Enabled = false;
            txtMusteriAdi.Enabled = false;
            cmbMusteriTur.Enabled = false;
            txtMusteriTC.Enabled = false;
            txtMusteriIletisim.Enabled = false;
            txtMusteriAdres.Enabled = false;
            txtMustseriIban.Enabled = false;
            btnMusteriSil.Enabled = true;
            btnMusteriDuzenle.Enabled = true;
            btnYeniMusteri.Enabled = true;
            btnMusteriKaydet.Enabled = false;
            btnMusteriIptal.Enabled = false;
        }
        private void btnMusteriTumu_Click(object sender, EventArgs e)
        {

        }
        private void txtMusteriArama_TextChanged(object sender, EventArgs e)
        {
            if (txtMusteriArama.Text != "")
            {
                cmbMusteriAramaTur.SelectedIndex = -1;
                FuncMusteri tabloal = new FuncMusteri();
                tblMusteriler.DataSource = tabloal.tablodoldur(txtMusteriArama.Text, false);
                txtMusteriToplam.Text = tabloal.satirsayisi().ToString();
            }
            else btnMusteriTumu.PerformClick();
        }
        private void cmbMusteriAramaTur_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMusteriAramaTur.SelectedIndex != -1)
            {
                FuncMusteri tabloal = new FuncMusteri();
                tblMusteriler.DataSource = tabloal.tablodoldur(cmbMusteriAramaTur.SelectedItem.ToString(), false);
                txtMusteriToplam.Text = tabloal.satirsayisi().ToString();
            }
        }
        private void btnMusteriIptal_Click(object sender, EventArgs e)
        {
            btnYeniMusteri.Enabled = true;
            btnMusteriDuzenle.Enabled = true;
            btnMusteriSil.Enabled = true;
            btnMusteriKaydet.Enabled = false;
            btnMusteriIptal.Enabled = false;
            kutusifirla();
        }
        private void btnMusteriDuzenle_Click(object sender, EventArgs e)
        {
            if (tblMusteriler.RowCount > 0)
            {
                btnMusteriKaydet.Enabled = true;
                btnMusteriIptal.Enabled = true;
                btnMusteriDuzenle.Enabled = false;
                btnYeniMusteri.Enabled = false;
                btnMusteriSil.Enabled = false;
                txtMusteriNo.Enabled = false;
                txtMusteriAdi.Enabled = true;
                cmbMusteriTur.Enabled = true;
                txtMusteriTC.Enabled = true;
                txtMusteriIletisim.Enabled = true;
                txtMusteriAdres.Enabled = true;
                txtMustseriIban.Enabled = true;
                if (tblMusteriler.SelectedRows.Count > 0)
                {
                    txtMusteriNo.Text = tblMusteriler.CurrentRow.Cells[0].Value.ToString();
                    txtMusteriAdi.Text = tblMusteriler.CurrentRow.Cells[1].Value.ToString();
                    cmbMusteriTur.SelectedItem = tblMusteriler.CurrentRow.Cells[2].Value.ToString();
                    txtMusteriTC.Text = tblMusteriler.CurrentRow.Cells[3].Value.ToString();
                    txtMusteriIletisim.Text = tblMusteriler.CurrentRow.Cells[4].Value.ToString();
                    txtMusteriAdres.Text = tblMusteriler.CurrentRow.Cells[5].Value.ToString();
                    txtMustseriIban.Text = tblMusteriler.CurrentRow.Cells[6].Value.ToString();
                }
            }
        }
        private void btnMusteriSil_Click(object sender, EventArgs e)
        {

            FuncMusteri sil = new FuncMusteri();
            bool sonuc = sil.MusteriSil(tblMusteriler.CurrentRow.Cells[0].Value.ToString());
            if (sonuc == true)
            {
                logKayitYap(" * * MÜŞTERİ SİLME * * ", tblMusteriler.CurrentRow.Cells[0].Value.ToString() + " Kaydı Silindi.");
            }
            tblMusteriler.DataSource = sil.tablodoldur("", true);
            txtMusteriToplam.Text = sil.satirsayisi().ToString();
        }

        private void txtPartiArama_TextChanged(object sender, EventArgs e)
        {
            partiArama();
        }

        private void cbPartiAramaTarihAraligi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPartiAramaTarihAraligi.Checked == true)
            {
                dtp1Parti.Enabled = true;
                dtp2Parti.Enabled = true;
            }
            else
            {
                dtp1Parti.Enabled = false;
                dtp2Parti.Enabled = false;
            }
            partiArama();
        }

        private void dtp1Parti_ValueChanged(object sender, EventArgs e)
        {
            partiArama();
        }

        private void dtp2Parti_ValueChanged(object sender, EventArgs e)
        {
            partiArama();
        }

        private void btnPartiTumu_Click(object sender, EventArgs e)
        {
            txtPartiArama.Clear();
            cbPartiAramaTarihAraligi.Checked = false;
            partiArama();
        }

        private void btnPartiNoDüzenle_Click(object sender, EventArgs e)
        {
            txtPartiNoHayvanAlimi.Enabled = true;
        }

        private void msiDuzenle_Click(object sender, EventArgs e)
        {
            cmbMusteriHayvanAlimi.SelectedItem = tblPartiler.SelectedRows[0].Cells[1].Value.ToString() + " [" + tblPartiler.SelectedRows[0].Cells[2].Value.ToString() + "]";
            txtPartiNoHayvanAlimi.Text = tblPartiler.SelectedRows[0].Cells[0].Value.ToString();
            txtToplamCanliAgirlik.Text = tblPartiler.SelectedRows[0].Cells[4].Value.ToString();
            txtAdetHayvanAlimi.Text = tblPartiler.SelectedRows[0].Cells[3].Value.ToString();
            txtAracPlaka.Text = tblPartiler.SelectedRows[0].Cells[6].Value.ToString();
            txtGeldigiYer.Text = tblPartiler.SelectedRows[0].Cells[5].Value.ToString();
            btnAlimiBaslatHayvan.Text = "Değişiklikleri Kaydet!";
            btnAlimiBaslatHayvan.ImageOptions.Image = TeraziDevEx.Properties.Resources.saveto_32x32;
            tblPartiler.Enabled = false;
        }

        private void btnKullanicilarYeni_Click(object sender, EventArgs e)
        {
            txtKullanicilarSicilNo.Enabled = true;
            txtKullanicilarAdi.Enabled = true;
            cmbKullanicilarDepartman.Enabled = true;
            txtKullanicilarUsername.Enabled = true;
            txtKullanicilarSifre.Enabled = true;
            btnKullanicilarKaydet.Enabled = true;
            btnKullanicilarIptal.Enabled = true;
            btnKullanicilarDuzenle.Enabled = false;
            btnKullanicilarYeni.Enabled = false;
            btnKullanicilarSil.Enabled = false;
            chIslemDuzenleme.Enabled = true;
            chIslemKaydi.Enabled = true;
            chUrunlerYetki.Enabled = true;
            chTumIslemleriGoruntule.Enabled = true;
            chKullanicilarYetki.Enabled = true;
            chAyarlarYetki.Enabled = true;
            txtKullanicilarSicilNo.Text = "";
            txtKullanicilarAdi.Text = "";
            txtKullanicilarSifre.Text = "";
            txtKullanicilarUsername.Text = "";
            cmbKullanicilarDepartman.SelectedIndex = -1;

        }
        private void btnKullanicilarSil_Click(object sender, EventArgs e)
        {
            if (tblKullanicilar.SelectedRows.Count > 0)
            {
                string silinecek = tblKullanicilar.SelectedRows[0].Cells[2].Value.ToString();
                DialogResult diyalog = new DialogResult();
                diyalog = MessageBox.Show(silinecek + " kullanıcısını silmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (diyalog == DialogResult.Yes)
                {
                    SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                    SqlCommand komut = new SqlCommand();
                    try
                    {
                        komut.Connection = conn;
                        komut.CommandText = "DELETE FROM [Terazi].[dbo].[Kullanicilar] WHERE Username=@username";
                        komut.Parameters.AddWithValue("@username", silinecek);
                        conn.Open();
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Silindi.");
                        conn.Close();
                        kullaniciArama();
                    }
                    catch (Exception exc)
                    {
                        conn.Close();
                        MessageBox.Show("Kayıt Silinemedi.\n Hata: " + exc, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnKullanicilarIptal_Click(object sender, EventArgs e)
        {
            kullaniciButonSifirla();
        }

        private void btnKullanicilarDuzenle_Click(object sender, EventArgs e)
        {
            if (tblKullanicilar.RowCount > 0)
            {
                if (tblKullanicilar.SelectedRows.Count > 0)
                {
                    txtKullanicilarAdi.Text = tblKullanicilar.CurrentRow.Cells[1].Value.ToString();
                    txtKullanicilarSicilNo.Text = tblKullanicilar.CurrentRow.Cells[0].Value.ToString();
                    txtKullanicilarUsername.Text = tblKullanicilar.CurrentRow.Cells[2].Value.ToString();
                    txtKullanicilarSifre.Text = tblKullanicilar.CurrentRow.Cells[3].Value.ToString();
                    cmbKullanicilarDepartman.SelectedItem = tblKullanicilar.CurrentRow.Cells[4].Value.ToString();
                    txtKullanicilarSicilNo.Enabled = true;
                    txtKullanicilarAdi.Enabled = true;
                    cmbKullanicilarDepartman.Enabled = true;
                    txtKullanicilarUsername.Enabled = false;
                    txtKullanicilarSifre.Enabled = true;
                    btnKullanicilarKaydet.Enabled = true;
                    btnKullanicilarIptal.Enabled = true;
                    btnKullanicilarDuzenle.Enabled = false;
                    btnKullanicilarYeni.Enabled = false;
                    btnKullanicilarSil.Enabled = false;
                    chIslemDuzenleme.Enabled = true;
                    chIslemKaydi.Enabled = true;
                    chUrunlerYetki.Enabled = true;
                    chTumIslemleriGoruntule.Enabled = true;
                    chKullanicilarYetki.Enabled = true;
                    chAyarlarYetki.Enabled = true;
                    yetkioku();

                }
            }
        }

        public void yetkioku()
        {
            string okunan = "";
            SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
            SqlCommand komut = new SqlCommand();
            try
            {
                komut.Connection = conn;
                komut.CommandText = "SELECT Yetkiler FROM [Terazi].[dbo].[Kullanicilar] WHERE username=@username ";
                komut.Parameters.AddWithValue("@username", tblKullanicilar.SelectedRows[0].Cells[2].Value.ToString());
                conn.Open();
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    okunan = dr[0].ToString();
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Kayıt düzenlenemedi.\n Hata: " + ex, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            char[] okunanchar = okunan.ToCharArray();
            for (int i = 0; i < okunanchar.Length; i++)
            {
                if (okunanchar[i].ToString() == "1")
                {
                    chIslemKaydi.Checked = true;

                }
                if (okunanchar[i].ToString() == "2")
                {
                    chTumIslemleriGoruntule.Checked = true;

                }
                if (okunanchar[i].ToString() == "3")
                {
                    chIslemDuzenleme.Checked = true;

                }
                if (okunanchar[i].ToString() == "4")
                {
                    chUrunlerYetki.Checked = true;

                }
                if (okunanchar[i].ToString() == "5")
                {
                    chKullanicilarYetki.Checked = true;

                }
                if (okunanchar[i].ToString() == "6")
                {
                    chAyarlarYetki.Checked = true;

                }
            }
        }
        private void btnKullanicilarKaydet_Click(object sender, EventArgs e)
        {
            if (txtKullanicilarUsername.Enabled == false)
            {
                string secilenyetki = yetkiHesapla();
                kullaniciDuzenle(secilenyetki);
            }
            else if (txtKullanicilarUsername.Enabled == true)// YENİ KAYIT !!!!
            {
                string secilenyetki = yetkiHesapla();
                //MessageBox.Show(secilenyetki);
                yeniKullanici(secilenyetki);
            }

        }
        private string yetkiHesapla()
        {
            string yetkiler = "0";
            if (chIslemKaydi.Checked == true)
            {
                yetkiler += "1";
                yetkiler = yetkiler.Replace("0", "");
            }
            if (chTumIslemleriGoruntule.Checked == true)
            {
                yetkiler += "2";
                yetkiler = yetkiler.Replace("0", "");
            }
            if (chIslemDuzenleme.Checked == true)
            {
                yetkiler += "3";
                yetkiler = yetkiler.Replace("0", "");
            }
            if (chUrunlerYetki.Checked == true)
            {
                yetkiler += "4";
                yetkiler = yetkiler.Replace("0", "");
            }
            if (chKullanicilarYetki.Checked == true)
            {
                yetkiler += "5";
                yetkiler = yetkiler.Replace("0", "");
            }
            if (chAyarlarYetki.Checked == true)
            {
                yetkiler += "6";
                yetkiler = yetkiler.Replace("0", "");
            }
            return yetkiler;
        }
        private void raporGetirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] secilenparti = new string[tblPartiler.SelectedRows.Count];
            for (int i = 0; i < tblPartiler.SelectedRows.Count; i++)
            {
                secilenparti[i] = tblPartiler.SelectedRows[i].Cells[0].Value.ToString();
            }
            rapor r = new rapor();
            r.raporla(secilenparti);
            r.Show();
        }

        private void btnMusteriTumu_Click_1(object sender, EventArgs e)
        {
            txtMusteriArama.Text = "";
            cmbMusteriAramaTur.SelectedIndex = -1;
            FuncMusteri tabloal = new FuncMusteri();
            tblMusteriler.DataSource = tabloal.tablodoldur("", true);
            txtMusteriToplam.Text = tabloal.satirsayisi().ToString();
        }
        private void kullanıcıYetkileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string okunan = "";
            SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
            SqlCommand komut = new SqlCommand();
            try
            {
                komut.Connection = conn;
                komut.CommandText = "SELECT Yetkiler FROM [Terazi].[dbo].[Kullanicilar] WHERE username=@username ";
                komut.Parameters.AddWithValue("@username", tblKullanicilar.SelectedRows[0].Cells[2].Value.ToString());
                conn.Open();
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    okunan = dr[0].ToString();
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Kayıt düzenlenemedi.\n Hata: " + ex, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            char[] okunanchar = okunan.ToCharArray();
            string yetki = "";
            for (int i = 0; i < okunanchar.Length; i++)
            {
                if (okunanchar[i].ToString() == "1")
                {
                    yetki += "İşlem Kaydı Girme\n";

                }
                if (okunanchar[i].ToString() == "2")
                {
                    yetki += "Tüm İşlemleri Görüntüleme\n";

                }
                if (okunanchar[i].ToString() == "3")
                {
                    yetki += "İşlem Düzenleme/Silme\n";

                }
                if (okunanchar[i].ToString() == "4")
                {
                    yetki += "Ürünler Yönetimi\n";

                }
                if (okunanchar[i].ToString() == "5")
                {
                    yetki += "Kullanıcılar Yönetimi\n";

                }
                if (okunanchar[i].ToString() == "6")
                {
                    yetki += "Ayarlara Erişim\n";

                }
                if (okunanchar[i].ToString() == "0")
                {
                    yetki += "Kullanıcı Yetkisi Yok.\n";

                }
            }
            MessageBox.Show(yetki, "Kullanıcı Yetkileri", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        private void msiDuzenlePartiIcerigi_Click(object sender, EventArgs e)
        {
            txtKulakNoHayvanAlimi.Text = tblPartiIcerigi.CurrentRow.Cells[1].Value.ToString();
            cmbIrk.SelectedItem = tblPartiIcerigi.CurrentRow.Cells[3].Value.ToString();
            cmbCinsiyet.SelectedItem = tblPartiIcerigi.CurrentRow.Cells[4].Value.ToString();
            btnOnaylaHayvanAlimi.Enabled = true;
            btnOnaylaHayvanAlimi.Text = "Kaydet";
            btnOnaylaHayvanAlimi.ImageOptions.Image = TeraziDevEx.Properties.Resources.saveto_32x32;
            tblPartiIcerigi.Enabled = false;
            gcPartiArama.Enabled = false;
            cmbMusteriHayvanAlimi.Enabled = false;
            txtPartiNoHayvanAlimi.Enabled = false;
            txtAdetHayvanAlimi.Enabled = false;
            txtToplamCanliAgirlik.Enabled = false;
            btnAlimiBaslatHayvan.Enabled = false;
            txtKulakNoHayvanAlimi.Enabled = true;
            cmbIrk.Enabled = true;
            cmbCinsiyet.Enabled = true;
        }
        private void btnAlimiIptalEt_Click(object sender, EventArgs e)
        {
            partiSil();
            logKayitYap("* * ALIM SİLME * *", "Parti No:" + txtPartiNoHayvanAlimi.Text);
            txtKulakNoHayvanAlimi.Clear();
            cmbMusteriHayvanAlimi.SelectedIndex = -1;
            txtToplamCanliAgirlik.Clear();
            txtAdetHayvanAlimi.Clear();
            txtPartiNoHayvanAlimi.Clear();
            txtAracPlaka.Clear();
            txtGeldigiYer.Clear();
            txtAracPlaka.Enabled = true;
            txtGeldigiYer.Enabled = true;
            cmbMusteriHayvanAlimi.Enabled = true;
            txtPartiNoHayvanAlimi.Enabled = true;
            txtToplamCanliAgirlik.Enabled = true;
            btnAlimiBaslatHayvan.Enabled = true;
            txtAdetHayvanAlimi.Enabled = true;
            gcPartiArama.Enabled = true;
            txtKulakNoHayvanAlimi.Enabled = false;
            cmbIrk.Enabled = false;
            cmbCinsiyet.Enabled = false;
            btnOnaylaHayvanAlimi.Enabled = false;
            btnGeriAl.Enabled = false;
            cmbIrk.SelectedIndex = -1;
            cmbCinsiyet.SelectedIndex = -1;
            btnAlimiIptalEt.Enabled = false;
            btnKaydetHayvanAlimi.Enabled = false;
            partiIcerigiAc();
        }
        private void msPartiIcerigi_Opening(object sender, CancelEventArgs e)
        {
            if (btnOnaylaHayvanAlimi.Enabled == true)
            {
                msiDuzenlePartiIcerigi.Enabled = false;
            }
            else
            {
                msiDuzenlePartiIcerigi.Enabled = true;
            }
        }
        private void cmbKesimhaneParti_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbKesimhaneParti.SelectedIndex == -1) kesimhaneButonPasifle();

        }

        private void kesimhaneKulakNoAl()
        {
            if (tblKulakNolar.RowCount > 0) tblKulakNolar.Rows.Clear();
            if (cmbKesimhaneParti.Text != "")
            {
                SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                try
                {
                    komut.Connection = conn;
                    komut.CommandText = "SELECT KulakNo FROM [Terazi].[dbo].[Hayvanlar] where PartiNo=@parti order by SiraNo; ";
                    komut.Parameters.AddWithValue("@parti", cmbKesimhaneParti.SelectedItem.ToString());
                    conn.Open();
                    SqlDataReader dr = komut.ExecuteReader();
                    int sirano = 1;
                    while (dr.Read())
                    {
                        tblKulakNolar.Rows.Add(sirano, dr[0].ToString());
                        sirano += 1;
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Kulak No alımı sırasında hata oluştu..\n Hata: " + ex, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                tblKulakNolar.CurrentCell = tblKulakNolar.Rows[0].Cells[0];
            }
        }

        private void kesimhanePartiAl()
        {
            cmbKesimhaneParti.Items.Clear();
            SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
            SqlCommand komut = new SqlCommand();
            try
            {
                komut.Connection = conn;
                komut.CommandText = "SELECT PartiNo FROM [Terazi].[dbo].[Partiler] where Durum!='KESİLDİ' order by AlimTarihi ";
                conn.Open();
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbKesimhaneParti.Items.Add(dr[0]);
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Kayıt düzenlenemedi.\n Hata: " + ex, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //KESİMHANE PARTİ LİSTESİ AL
            try
            {
                komut.Connection = conn;
                komut.CommandText = "SELECT PartiNo,Durum,AlimTarihi,KesimTarihi FROM [Terazi].[dbo].[Partiler] order by AlimTarihi desc";
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                tblKesimhane.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Kayıt düzenlenemedi.\n Hata: " + ex, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tabloBoyaKesimhane();
        }

        private void tabloBoyaKesimhane()
        {
            if (tblKesimhane.Columns[0].HeaderText == "PartiNo" && tblKesimhane.RowCount > 0)
            {
                for (int i = 0; i < tblKesimhane.RowCount; i++)
                {
                    if (tblKesimhane.Rows[i].Cells[1].Value.ToString() == "KESİLDİ")
                    {
                        tblKesimhane.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else if (tblKesimhane.Rows[i].Cells[1].Value.ToString() == "Kesim Bekleniyor...")
                    {
                        tblKesimhane.Rows[i].DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                    else
                    {
                        tblKesimhane.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                }
            }
        }
        private void yeniKullanici(string yetki)
        {
            bool usernametekrar = noTekrarKontrol("SELECT * FROM [Terazi].[dbo].[Kullanicilar] where Username=@no", txtKullanicilarUsername.Text, "");
            bool siciltekrar = noTekrarKontrol("SELECT * FROM [Terazi].[dbo].[Kullanicilar] where SicilNo=@no", txtKullanicilarSicilNo.Text, txtKullanicilarUsername.Text);
            if (usernametekrar == true)
            {
                MessageBox.Show("Kullanıcı adı sistemde kayıtlıdır. Lütfen başka bir kullanıcı adı seçiniz.", "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (siciltekrar == true)
            {
                MessageBox.Show("Sicil numarası sistemde kayıtlıdır. Lütfen kontrol ediniz.", "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (txtKullanicilarSifre.TextLength < 4)
            {
                MessageBox.Show("Girilen şifre uzunluğu minimum 4 hane olmalıdır.", "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (txtKullanicilarUsername.Text != "" && txtKullanicilarSifre.Text != "" && txtKullanicilarSifre.TextLength > 3 && usernametekrar == false && siciltekrar == false)
            {
                SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                try
                {

                    komut.Connection = conn;
                    komut.CommandText = "INSERT INTO [Terazi].[dbo].[Kullanicilar] ([Username],[Password],[SicilNo],[AdiSoyadi],[Departman],[Yetkiler]) VALUES (@username,@password,@sicil,@adsoyad,@departman,@yetki)";
                    komut.Parameters.AddWithValue("@sicil", txtKullanicilarSicilNo.Text);
                    komut.Parameters.AddWithValue("@username", txtKullanicilarUsername.Text);
                    komut.Parameters.AddWithValue("@password", txtKullanicilarSifre.Text);
                    komut.Parameters.AddWithValue("@adsoyad", txtKullanicilarAdi.Text);
                    komut.Parameters.AddWithValue("@departman", cmbKullanicilarDepartman.Text);
                    komut.Parameters.AddWithValue("@yetki", yetki);
                    conn.Open();
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kaydedildi.");
                    conn.Close();
                    kullaniciArama();
                    kullaniciButonSifirla();

                }
                catch (Exception e)
                {
                    conn.Close();
                    MessageBox.Show("Veritabanı kayıt yapılamadı.\n Hata: " + e, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void kullaniciDuzenle(string yetki)
        {
            SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
            SqlCommand komut = new SqlCommand();
            bool siciltekrar = noTekrarKontrol("SELECT * FROM [Terazi].[dbo].[Kullanicilar] where SicilNo=@no and Username!=@user", txtKullanicilarSicilNo.Text, txtKullanicilarUsername.Text);
            if (siciltekrar == true)
            {
                MessageBox.Show("Sicil numarası sistemde kayıtlıdır. Lütfen kontrol ediniz.", "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (siciltekrar == false)
            {
                try
                {
                    komut.Connection = conn;
                    komut.CommandText = "UPDATE [Terazi].[dbo].[Kullanicilar] SET [Password]=@password, [SicilNo]=@sicil ,[AdiSoyadi]=@adi,[Departman]=@departman,[Yetkiler]=@yetki where username=@username ";
                    komut.Parameters.AddWithValue("@username", txtKullanicilarUsername.Text);
                    komut.Parameters.AddWithValue("@password", txtKullanicilarSifre.Text);
                    komut.Parameters.AddWithValue("@sicil", txtKullanicilarSicilNo.Text);
                    komut.Parameters.AddWithValue("@adi", txtKullanicilarAdi.Text);
                    komut.Parameters.AddWithValue("@departman", cmbKullanicilarDepartman.Text);
                    komut.Parameters.AddWithValue("@yetki", yetki);
                    conn.Open();
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Düzenlendi.");
                    conn.Close();
                    kullaniciArama();
                    kullaniciButonSifirla();
                }
                catch (Exception e)
                {
                    conn.Close();
                    MessageBox.Show("Kayıt düzenlenemedi.\n Hata: " + e, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void BtnKesimhaneIptal_Click(object sender, EventArgs e)
        {
            //if (tblKesimhane.RowCount > 0) KesimhanePartiIptal(cmbKesimhaneParti.Text);
            kesimhaneButonPasifle();
            kesimhanePartiAl();
            txtKesimhaneBrut.ReadOnly = true;
            txtKesimhaneDaraKg.ReadOnly = true;
            txtKesimhaneNet.ReadOnly = true;
        }

        void kesimhaneButonPasifle()
        {
            btnKesimhaneOnayla.Enabled = false;
            cmbKesimhaneParti.Enabled = true;
            tblKulakNolar.Enabled = false;
            txtKesimhaneUrunAdi.Enabled = false;
            txtKesimhaneDaraKg.Enabled = false;
            txtKesimhaneBrut.Enabled = false;
            txtKesimhaneNet.Enabled = false;
            btnKesimhaneIptal.Enabled = false;
            btnKesimhaneBitir.Enabled = false;
            btnKesimhaneNext.Enabled = false;
            txtKesimhaneUrunAdi.Text = "";
            cmbKesimhaneParti.SelectedIndex = -1;
            tblKulakNolar.Rows.Clear();
            txtKesimhaneBrut.Text = "0,0";
            txtKesimhaneNet.Text = "0,0";
            txtKesimhaneDaraKg.Text = "0,0";
            gcKesimhaneEkran.Text = "0";
            groupControl11.Text = "Tartım için sağ menüden parti seçiniz...";
            btnKesimhaneBasla.Enabled = true;
            btnKesimhaneGeriAL.Enabled = false;
            tblKulakNolar.Rows.Clear();
            tblKesimhane.DataBindings.Clear();
            tblKesimhane.Refresh();
            tblKulakNolar.Enabled = false;
            btnKesimhaneRaporAL.Enabled = false;
        }

        void KesimhanePartiIptal(string partiNo)
        {
            DialogResult diyalog = new DialogResult();
            diyalog = MessageBox.Show(partiNo + " parti tartımını iptal etmek istediğinize emin misiniz?\nGirdiğiniz kayıtlar silinsin mi?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (diyalog == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                try
                {
                    komut.Connection = conn;
                    komut.CommandText = "DELETE FROM [Terazi].[dbo].[IslemKaydi] WHERE PartiNo=@partiNo";
                    komut.Parameters.AddWithValue("@PartiNo", partiNo);
                    conn.Open();
                    komut.ExecuteNonQuery();
                    conn.Close();
                    logKayitYap("* * KESİMHANE - PARTİ İPTAL * *", "Parti No:" + partiNo);
                    kesimhanetabloal(partiNo);
                }
                catch (Exception exc)
                {
                    conn.Close();
                    MessageBox.Show("Kayıt Silinemedi.\n Hata: " + exc, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnKesimhaneOnayla_Click(object sender, EventArgs e)
        {
            kesimhaneOnayla();
        }

        private void kesimhaneOnayla()
        {
            if (txtKesimhaneUrunAdi.Text.Trim().Equals("")) MessageBox.Show("Ürün adı giriniz!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (Convert.ToDouble(txtKesimhaneNet.Text) <= 0) MessageBox.Show("Net ağırlık sıfırdan büyük olmalıdır!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    bool ozel = false;
                    if (cbOzelDurum.Checked == true && txtAciklamaKesimhane.Text.Trim().Equals(""))
                    {
                        ozel = true;
                        MessageBox.Show("Eğer özel bir durum varsa açıklama girmelisiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (txtKesimhaneNet.Text != "0,0" && ozel == false)
                        {
                            SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                            SqlCommand komut = new SqlCommand();
                            try
                            {
                                komut.Connection = conn;
                                komut.CommandText = "INSERT INTO [Terazi].[dbo].[IslemKaydi] ([PartiNo],[KulakNo],[UrunAdi],[Dara],[BrutKg],[NetKg],[Tarih],[OzelDurum],[Kullanici]) VALUES (@partino,@kulakno,@urunadi,@darakg,@brut,@net,@tarih,@ozeldurum,@kullanici) ;";
                                komut.Parameters.AddWithValue("@partino", cmbKesimhaneParti.Text);
                                if (chKesimhaneKulakNosuz.Checked == false)
                                {
                                    komut.Parameters.AddWithValue("@kulakno", tblKulakNolar.SelectedRows[0].Cells[1].Value.ToString());
                                }
                                else
                                {
                                    komut.Parameters.AddWithValue("@kulakno", "NUMARASIZ TARTIM");
                                }
                                komut.Parameters.AddWithValue("@urunadi", txtKesimhaneUrunAdi.Text);
                                komut.Parameters.AddWithValue("@darakg", Convert.ToDouble(txtKesimhaneDaraKg.Text));
                                komut.Parameters.AddWithValue("@brut", Convert.ToDouble(txtKesimhaneBrut.Text));
                                komut.Parameters.AddWithValue("@net", Convert.ToDouble(txtKesimhaneNet.Text));
                                komut.Parameters.AddWithValue("@tarih", DateTime.Now);
                                komut.Parameters.AddWithValue("@ozeldurum", txtAciklamaKesimhane.Text);
                                komut.Parameters.AddWithValue("@kullanici", lblKullanici.Text);
                                conn.Open();
                                komut.ExecuteNonQuery();
                                conn.Close();
                                logKayitYap("* * KESİMHANE - TARTIM * *", "Kulak No: " + tblKulakNolar.SelectedRows[0].Cells[1].Value.ToString() + " Tartılan: " + txtKesimhaneNet.Text);
                                tblKesimhane.Visible = false;
                                kesimhanetabloal(cmbKesimhaneParti.Text);
                                tblKesimhane.Visible = true;
                                tblKesimhane.CurrentCell = tblKesimhane.Rows[tblKesimhane.RowCount - 1].Cells[0];
                                gcKesimhaneEkran.Text = "0,0";
                                btnKesimhaneNext.PerformClick();
                            }
                            catch (Exception e)
                            {
                                conn.Close();
                                MessageBox.Show("Kayıt girilemedi.\n Hata: " + e, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void kesimhanetabloal(string parti)
        {
            SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
            SqlCommand komut = new SqlCommand();

            try
            {
                komut.Connection = conn;
                komut.CommandText = "SELECT ROW_NUMBER() OVER(ORDER BY ID), ID[Sıra No], KulakNo[Kulak No], UrunAdi[Hayvan Cinsi],  BrutKg[Brüt],Dara, NetKg[Net], Tarih[Tarih], OzelDurum[Özel Durum] FROM[Terazi].[dbo].[IslemKaydi] WHERE PartiNo = @partino;";
                komut.Parameters.AddWithValue("@partino", cmbKesimhaneParti.Text);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                tblKesimhane.DataSource = dt;
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Tablo İşlemleri Hatası.\n Hata: " + e, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKesimhaneNext_Click(object sender, EventArgs e)
        {
            if (tblKulakNolar.Rows[tblKulakNolar.Rows.Count - 1].Selected == false)
            {
                tblKulakNolar.CurrentCell = tblKulakNolar.Rows[tblKulakNolar.CurrentRow.Index + 1].Cells[0];
            }
        }

        public void kesimBaslat()
        {
            SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
            SqlCommand komut = new SqlCommand();

            try
            {
                komut.Connection = conn;
                komut.CommandText = "update Partiler set Durum='Kesim Devam Ediyor...',KesimTarihi=@tarih where PartiNo=@partino";
                komut.Parameters.AddWithValue("@partino", cmbKesimhaneParti.SelectedItem.ToString());
                komut.Parameters.AddWithValue("@tarih", DateTime.Now);
                conn.Open();
                komut.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Tablo İşlemleri Hatası.\n Hata: " + e, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtKesimhaneUrunAdi.Text = "Tosun";
        }

        private void BtnKesimhaneBasla_Click(object sender, EventArgs e)
        {
            if (secilenTarti == 0) MessageBox.Show("Terazi seçmediniz!\nTerazi seçilmeden tartım başlatılamaz!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (cmbKesimhaneParti.Text != "")
                {
                    cmbKesimhaneParti.Enabled = false;
                    btnKesimhaneBasla.Enabled = false;
                    btnKesimhaneGeriAL.Enabled = true;
                    cbOzelDurum.Enabled = true;
                    kesimhaneKulakNoAl();
                    chKesimhaneKulakNosuz.Enabled = true;
                    txtKesimhaneUrunAdi.Enabled = true;
                    txtKesimhaneDaraKg.Enabled = true;
                    txtKesimhaneBrut.Enabled = true;
                    txtKesimhaneNet.Enabled = true;
                    btnKesimhaneOnayla.Enabled = true;
                    btnKesimhaneIptal.Enabled = true;
                    btnKesimhaneBitir.Enabled = true;
                    btnKesimhaneRaporAL.Enabled = true;
                    groupControl11.Text = cmbKesimhaneParti.Text + " Numaralı Parti Tartım Listesi";
                    logKayitYap("* * KESİMHANE - TARTIM BAŞLATILDI * *", "Parti No: " + cmbKesimhaneParti.Text);
                    kesimhanetabloal(cmbKesimhaneParti.Text);
                    chKesimhaneKulakNosuz.Checked = false;
                    kesimBaslat();
                }
                else
                {
                    MessageBox.Show("Lütfen tartım yapılacak parti numarasını seçiniz...", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnKesimhaneGeriAL_Click(object sender, EventArgs e) // AKTARIM
        {
            if (tblKesimhane.RowCount > 0)
            {
                KesimhaneSonSatirSil(tblKesimhane.CurrentRow.Cells[0].Value.ToString());
            }
        }
        void KesimhaneSonSatirSil(string ID)
        {
            DialogResult diyalog = new DialogResult();
            diyalog = MessageBox.Show(ID + " numaralı tartımın geri alma işlemini onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                try
                {
                    komut.Connection = conn;
                    komut.CommandText = "DELETE FROM [Terazi].[dbo].[IslemKaydi] WHERE ID=@id";
                    komut.Parameters.AddWithValue("@id", ID);
                    conn.Open();
                    komut.ExecuteNonQuery();
                    conn.Close();
                    logKayitYap("* * KESİMHANE - GERİ ALIM * *", "Parti No:" + cmbKesimhaneParti.Text);
                    kesimhanetabloal(cmbKesimhaneParti.Text);
                    if(tblKesimhane.Rows.Count>0)tblKesimhane.CurrentCell = tblKesimhane.Rows[tblKesimhane.RowCount - 1].Cells[0];
                }
                catch (Exception exc)
                {
                    conn.Close();
                    MessageBox.Show("Kayıt Silinemedi.\n Hata: " + exc, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void urunAl(string siraNo)
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                baglanti.Open();
                komut.CommandText = "select Adi from Urunler where SiraNo= " + siraNo;
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    txtUrunAdiParcalama.Text = dr[0].ToString();
                }
                else
                {
                    MessageBox.Show("Ürün Bulunamadı!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKesimhaneBitir_Click(object sender, EventArgs e)
        {
            if (tblKesimhane.RowCount > 0 && tblKulakNolar.RowCount > 0)
            {
                if (tblKesimhane.RowCount < tblKulakNolar.RowCount)
                {
                    if (MessageBox.Show("Partideki hayvan sayısından az tartım yapıldı. \n Yine de tartımı bitirmek istiyor musunuz?", "Tartım Bitir!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        partikesimbitir();
                        logKayitYap("* * KESİMHANE - KAYIT SONU * *", "Parti No:" + cmbKesimhaneParti.Text);
                        kesimhaneButonPasifle();
                        kesimhanePartiAl();
                    }
                }
                else
                {
                    partikesimbitir();
                    logKayitYap("* * KESİMHANE - KAYIT SONU * *", "Parti No:" + cmbKesimhaneParti.Text);
                    kesimhaneButonPasifle();
                    kesimhanePartiAl();
                }
            }
            else
            {
                MessageBox.Show("Tablo boş, kayıt girmelisiniz!\nÇıkmak için 'İptal' butonuna basınız.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            txtKesimhaneBrut.ReadOnly = true;
            txtKesimhaneDaraKg.ReadOnly = true;
            txtKesimhaneNet.ReadOnly = true;
        }

        private void partikesimbitir()
        {
            SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
            SqlCommand komut = new SqlCommand();
            try
            {
                komut.Connection = conn;
                komut.CommandText = "UPDATE Partiler set Durum='KESİLDİ',KesimTarihi=@tarih where PartiNo=@parti;";
                komut.Parameters.AddWithValue("@parti", cmbKesimhaneParti.Text);
                komut.Parameters.AddWithValue("@tarih", DateTime.Now);
                conn.Open();
                komut.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKisayol1_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[0]);
        }

        private void BtnKisayol2_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[1]);
        }

        private void BtnKisayol3_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[2]);
        }

        private void BtnKisayol4_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[3]);
        }

        private void BtnKisayol5_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[4]);
        }

        private void BtnKisayol6_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[5]);
        }

        private void BtnKisayol7_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[6]);
        }

        private void BtnKisayol8_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[7]);
        }

        private void BtnKisayol9_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[8]);
        }

        private void BtnKisayol10_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[9]);
        }

        private void BtnKisayol11_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[10]);
        }

        private void BtnKisayol12_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[11]);
        }

        private void BtnKisayol13_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[12]);
        }

        private void BtnKisayol14_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[13]);
        }

        private void BtnKisayol15_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[14]);
        }

        private void BtnKisayol16_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[15]);
        }

        private void BtnKisayol17_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[16]);
        }

        private void BtnKisayol18_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[17]);
        }

        private void BtnKisayol19_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[18]);
        }

        private void BtnKisayol20_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[19]);
        }

        private void BtnKisayol21_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[20]);
        }

        private void BtnKisayol22_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[21]);
        }

        private void BtnKisayol23_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[22]);
        }

        private void BtnKisayol24_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[23]);
        }

        private void BtnKisayol25_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[24]);
        }

        private void BtnKisayol26_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[25]);
        }

        private void BtnKisayol27_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[26]);
        }

        private void BtnKisayol28_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[27]);
        }

        private void BtnKisayol29_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[28]);
        }

        private void BtnKisayol30_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[29]);
        }

        private void CbOzelDurum_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOzelDurum.Checked == true)
            {
                txtAciklamaKesimhane.Enabled = true;
            }
            else
            {
                txtAciklamaKesimhane.Enabled = false;
                txtAciklamaKesimhane.Clear();
            }
        }

        private void TxtPaletAgirligiParcalama_TextChanged(object sender, EventArgs e)
        {
            daraHesapParcalama();
        }

        private void TxtKoliAdetParcalama_TextChanged(object sender, EventArgs e)
        {
            daraHesapParcalama();
        }

        private void TxtKoliAgirligiParcalama_TextChanged(object sender, EventArgs e)
        {
            daraHesapParcalama();
        }

        private void TxtToplamDaraParcalama_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
            txtPaletAgirligiParcalama.Text = "0,0";
            txtKoliAdetParcalama.Text = "0";
            txtKoliAgirligiParcalama.Text = "0,0";
        }

        public void daraHesapParcalama()
        {
            double okunanDeger;
            double toplamDara;
            double palet;
            double koliAgirlik;
            int koliAdet;
            try
            {
                okunanDeger = Convert.ToDouble(gcParcalamaEkran.Text);
            }
            catch
            {
                okunanDeger = 0;
            }
            try
            {
                palet = Convert.ToDouble(txtPaletAgirligiParcalama.Text);
            }
            catch
            {
                palet = 0;
            }
            try
            {
                koliAgirlik = Convert.ToDouble(txtKoliAgirligiParcalama.Text);
            }
            catch
            {
                koliAgirlik = 0;
            }
            try
            {
                koliAdet = Convert.ToInt32(txtKoliAdetParcalama.Text);
            }
            catch
            {
                koliAdet = 0;
            }
            toplamDara = palet + (koliAdet * koliAgirlik);
            txtToplamDaraParcalama.Text = toplamDara.ToString();
            txtBrutKgParcalama.Text = okunanDeger.ToString();
            txtNetKgParcalama.Text = (okunanDeger - toplamDara).ToString();
        }

        private void BtnYeniTartımParcalama_Click(object sender, EventArgs e)
        {
            bool gir = false;
            if (cmbTartimTurParcalama.SelectedIndex == -1) MessageBox.Show("Lütfen tartım türü seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (secilenTarti == 0)
                {
                    if (MessageBox.Show("Terazi seçimi yapmadınız! Terazi seçmeden sadece sipariş formu girebilirsiniz ya da düzenleme/silme yapabilirsiniz.\nYine de devam etmek istiyor musunuz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        gir = true;
                    }
                }
                else gir = true;

                if (gir)
                {
                    try
                    {
                        SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                        SqlCommand komut = new SqlCommand();
                        komut.Connection = baglanti;
                        komut.CommandText = "insert into ParcalamaTartimlar (TartimNo,Tur,HareketYeri,Aciklama,Tarih,Kullanici,Durum,DuzenlemeTarihi) values (@tartimno,@tur,@hareket,@aciklama,@tarih,@kullanici,0,@tarih)";
                        komut.Parameters.AddWithValue("@tartimno", cmbTartimNoParcalama.Text);
                        komut.Parameters.AddWithValue("@tur", cmbTartimTurParcalama.SelectedItem.ToString());
                        komut.Parameters.AddWithValue("@hareket", txtHareketYeriParcalama.Text);
                        komut.Parameters.AddWithValue("@aciklama", txtAciklamaParcalama.Text);
                        komut.Parameters.AddWithValue("@tarih", DateTime.Now);
                        komut.Parameters.AddWithValue("@kullanici", lblKullanici.Text);
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        logKayitYap("* * PARÇALAMA - YENİ TARTIM * *", cmbTartimNoParcalama.Text + " Numaralı Tartım Başlatıldı.");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    gcTartımBilgileriParcalama.Visible = false;
                    cmbTartimNoParcalama.Enabled = false;
                    btnTartimNoVer.Enabled = false;
                    gcKisayollar.Enabled = true;
                    parcalamaTartimIcerikCek();
                }
            }
        }

        private void CmbTartimNoParcalama_TextChanged(object sender, EventArgs e)
        {
            tartimNoAra();
        }

        private void BtnTartimaDevamEtParcalama_Click(object sender, EventArgs e)
        {
            bool gir = false;
            if (secilenTarti == 0)
            {
                if (MessageBox.Show("Terazi seçimi yapmadınız! Terazi seçmeden sadece sipariş formu girebilirsiniz ya da düzenleme/silme yapabilirsiniz.\nYine de devam etmek istiyor musunuz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    gir = true;
                }
            }
            else gir = true;
            if (gir)
            {
                try
                {
                    SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                    SqlCommand komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = "update ParcalamaTartimlar set DuzenlemeTarihi=@tarih, Aciklama=@aciklama,HareketYeri=@yer,Tur=@tur,Kullanici=@kullanici where TartimNo=@tartimno";
                    komut.Parameters.AddWithValue("@tarih", DateTime.Now);
                    komut.Parameters.AddWithValue("@aciklama", txtAciklamaParcalama.Text);
                    komut.Parameters.AddWithValue("@yer", txtHareketYeriParcalama.Text);
                    komut.Parameters.AddWithValue("@tur", cmbTartimTurParcalama.SelectedItem.ToString());
                    komut.Parameters.AddWithValue("@tartimno", cmbTartimNoParcalama.Text);
                    komut.Parameters.AddWithValue("@kullanici", lblKullanici.Text);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    logKayitYap("* * PARÇALAMA - TARTIM DEVAM * *", cmbTartimNoParcalama.Text + " Numaralı Tartım Devam Edildi.");
                }
                catch (SqlException ex) //BURDA HATAYA DÜŞÜYO BAK DİKKAT ET 
                {
                    MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                gcTartımBilgileriParcalama.Visible = false;
                cmbTartimNoParcalama.Enabled = false;
                btnTartimNoVer.Enabled = false;
                gcKisayollar.Enabled = true;
                parcalamaTartimIcerikCek();
            }
        }

        private void BtnTartimiSilParcalama_Click(object sender, EventArgs e) //TARTIM SİLME 
        {
            if (MessageBox.Show(cmbTartimNoParcalama.Text + " numaralı tartımı silmek istediğinize emin misiniz?\nTartım içeriğide silinecektir. Bu işlem geri alınamaz!", "Tartımı Sil!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                    SqlCommand komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = "UPDATE ParcalamaTartimlar set Durum=1 where TartimNo=@tartimno";
                    komut.Parameters.AddWithValue("@tartimno", cmbTartimNoParcalama.Text);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    logKayitYap("* * PARÇALAMA - TARTIM SİLME * *", cmbTartimNoParcalama.Text + " Numaralı Tartım Silindi.");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                parcalamaTartimCek();
            }
        }

        private void BtnTartimiBitirParcalama_Click(object sender, EventArgs e) //TARTIM BİTİR
        {
            cbTartimsizKayitParcalama.Checked = false;
            gcTartımBilgileriParcalama.Visible = true;
            cmbTartimNoParcalama.Enabled = true;
            btnTartimNoVer.Enabled = true;
            gcKisayollar.Enabled = false;
            txtPartiNoParcalama.Clear();
            txtUrunAdiParcalama.Text = "";
            txtPaletAgirligiParcalama.Text = "0,0";
            txtKoliAdetParcalama.Text = "0";
            txtKoliAgirligiParcalama.Text = "0,0";
            txtToplamDaraParcalama.Text = "0,0";
            gcParcalamaEkran.Text = "0,0";
            txtNetKgParcalama.Text = "0,0";
            txtBrutKgParcalama.Text = "0,0";
            parcalamaTartimCek();
        }

        private void TblParcalamaKayit_Click(object sender, EventArgs e)//TABLO KAYIT SEÇİMİ
        {
            if (tblParcalamaKayit.Rows.Count > 0)
            {
                if (gcTartımBilgileriParcalama.Visible == true) cmbTartimNoParcalama.Text = tblParcalamaKayit.CurrentRow.Cells[0].Value.ToString();
                else
                {
                    if (cbTartimsizKayitParcalama.Checked == false && tblParcalamaKayit.SelectedRows[0].Cells[4].Value.ToString() == "0")
                    {
                        txtPartiNoParcalama.Text = tblParcalamaKayit.SelectedRows[0].Cells[1].Value.ToString();
                        txtUrunAdiParcalama.Text = tblParcalamaKayit.SelectedRows[0].Cells[2].Value.ToString();
                    }
                    else
                    {
                        txtPartiNoParcalama.Text = "";
                        txtUrunAdiParcalama.Text = "";
                    }
                }
            }
        }

        private void BtnYonetimUrunlerYeni_Click(object sender, EventArgs e) // YÖNETİM ÜRÜNLER YENİ 
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "UPDATE Urunler SET Adi=@adi WHERE SiraNo=@sirano;";
                komut.Parameters.AddWithValue("@sirano", txtYonetimUrunNo.Text);
                komut.Parameters.AddWithValue("@adi", txtYonetimUrunAdi.Text);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                urunArama();
                logKayitYap("* * ÜRÜN GÜNCELLEME * *", txtYonetimUrunAdi.Text + " Ürünü listesi güncellendi.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnYonetimUrunlerSil_Click(object sender, EventArgs e) //YÖNETİM ÜRÜNLER SİLME 
        {
            if (tblUrunler.RowCount > 0)
            {
                string urunadi = tblUrunler.CurrentRow.Cells[1].Value.ToString();
                if (MessageBox.Show(urunadi + " adlı ürünü silmek istediğinize emin misiniz?", "Ürünü Sil!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                        SqlCommand komut = new SqlCommand();
                        komut.Connection = baglanti;
                        komut.CommandText = "UPDATE Urunler SET Adi=NULL WHERE SiraNo=@sirano;";
                        komut.Parameters.AddWithValue("@sirano", txtYonetimUrunNo.Text);
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        urunArama();
                        logKayitYap("* * ÜRÜN SİLME * *", urunadi + " Ürünü Listeden Silindi.");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void BtnOnaylaParcalama_Click(object sender, EventArgs e) //onayla
        {
            parcalamaOnayla();
        }

        public void parcalamaOnayla()
        {
            if (secilenTarti == 0 && cbTartimsizKayitParcalama.Checked == false && btnOnaylaParcalama.Text != "Kaydet") MessageBox.Show("Terazi bağlantınızda sorun var!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                double net;
                double brut;
                double dara;
                try
                {
                    net = Convert.ToDouble(txtNetKgParcalama.Text);
                }
                catch
                {
                    net = 0;
                    txtNetKgParcalama.Text = "0,0";
                }
                try
                {
                    dara = Convert.ToDouble(txtToplamDaraParcalama.Text);
                }
                catch
                {
                    dara = 0;
                    txtToplamDaraParcalama.Text = "0,0";
                }
                try
                {
                    brut = Convert.ToDouble(txtBrutKgParcalama.Text);
                }
                catch
                {
                    brut = 0;
                    txtBrutKgParcalama.Text = "0,0";
                }
                if (txtPartiNoParcalama.Text.Trim().Equals("") || txtUrunAdiParcalama.Text.Trim().Equals("")) MessageBox.Show("Girişler boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (net <= 0 && cbTartimsizKayitParcalama.Checked == false) MessageBox.Show("Net ağırlık sıfırdan büyük olmalıdır!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (net > brut && cbTartimsizKayitParcalama.Checked == false) MessageBox.Show("Net ağırlık brüt ağırlıktan büyük olamaz!!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    try
                    {
                        bool doldur = false;
                        SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                        SqlCommand komut = new SqlCommand();
                        komut.Connection = baglanti;
                        if (tblParcalamaKayit.RowCount > 0 && cbTartimsizKayitParcalama.Checked == false)
                        {
                            if (tblParcalamaKayit.SelectedRows[0].Cells[4].Value.ToString() == "0") doldur = true;
                        }
                        if (btnOnaylaParcalama.Text == "Kaydet" || doldur)
                        {
                            komut.CommandText = "update ParcalamaIcerik set PartiNo=@partino, UrunAdi=@adi,Adet=@adet,BrutKg=@brut,Dara=@dara,NetKg=@net,Tarih=@tarih,Kullanici=@kullanici where TartimNo=@tartimno and Sira=@sirano";
                            komut.Parameters.AddWithValue("@sirano", tblParcalamaKayit.CurrentRow.Cells[0].Value);
                            komut.Parameters.AddWithValue("@tartimno", cmbTartimNoParcalama.Text);
                            komut.Parameters.AddWithValue("@partino", txtPartiNoParcalama.Text);
                            komut.Parameters.AddWithValue("@adi", txtUrunAdiParcalama.Text);
                            komut.Parameters.AddWithValue("@adet", Convert.ToInt32(cmbParcalamaAdet.Text));
                            komut.Parameters.AddWithValue("@brut", brut);
                            komut.Parameters.AddWithValue("@dara", dara);
                            komut.Parameters.AddWithValue("@net", net);
                            komut.Parameters.AddWithValue("@tarih", DateTime.Now);
                            komut.Parameters.AddWithValue("@kullanici", lblKullanici.Text);


                            txtPaletAgirligiParcalama.Enabled = true;
                            txtKoliAdetParcalama.Enabled = true;
                            txtKoliAgirligiParcalama.Enabled = true;
                            txtToplamDaraParcalama.Enabled = true;
                            btnOnaylaParcalama.Text = "Onayla";
                            btnOnaylaParcalama.ImageOptions.Image = TeraziDevEx.Properties.Resources.apply_32x32;
                            btnIptalParcalama.Enabled = true;
                            btnTartimiBitirParcalama.Enabled = true;
                            tblParcalamaKayit.Enabled = true;
                            txtNetKgParcalama.ReadOnly = true;
                            txtBrutKgParcalama.ReadOnly = true;
                            btnDuzenleParcalama.Enabled = true;
                            btnSilParcalama.Enabled = true;
                            btnRaporlaParcalama.Enabled = true;
                            cbTartimsizKayitParcalama.Enabled = true;
                            if (cbTartimsizKayitParcalama.Checked == false)
                            {
                                btnIptalParcalama.PerformClick();
                            }
                        }
                        else
                        {
                            komut.CommandText = "INSERT INTO ParcalamaIcerik (TartimNo,PartiNo,UrunAdi,Adet,BrutKg,Dara,NetKg,Tarih,Kullanici) VALUES (@tartimno,@partino,@adi,@adet,@brut,@dara,@net,@tarih,@kullanici)";
                            komut.Parameters.AddWithValue("@tartimno", cmbTartimNoParcalama.Text);
                            komut.Parameters.AddWithValue("@partino", txtPartiNoParcalama.Text);
                            komut.Parameters.AddWithValue("@adi", txtUrunAdiParcalama.Text);
                            komut.Parameters.AddWithValue("@adet", Convert.ToInt32(cmbParcalamaAdet.Text));
                            komut.Parameters.AddWithValue("@brut", brut);
                            komut.Parameters.AddWithValue("@dara", dara);
                            komut.Parameters.AddWithValue("@net", net);
                            komut.Parameters.AddWithValue("@tarih", DateTime.Now);
                            komut.Parameters.AddWithValue("@kullanici", lblKullanici.Text);
                        }
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        tblParcalamaKayit.Visible = false;
                        parcalamaTartimIcerikCek();
                        tblParcalamaKayit.Visible = true;
                        logKayitYap("* * PARÇALAMA - KAYIT * *", "No:" + cmbTartimNoParcalama.Text + " PartiNo:" + txtPartiNoParcalama.Text + " Adı:" + txtUrunAdiParcalama.Text + " Kg:" + txtNetKgParcalama.Text);
                        if (cbTartimsizKayitParcalama.Checked == false)
                        {
                            if (secilenTarti == 0) gcParcalamaEkran.Text = "-----";
                            else gcParcalamaEkran.Text = "0,0";
                            txtBrutKgParcalama.Text = "0,0";
                            txtNetKgParcalama.Text = (0 - Convert.ToDouble(txtToplamDaraParcalama.Text)).ToString();
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnSilParcalama_Click(object sender, EventArgs e) //PARÇALAMA SİLME
        {
            if (tblParcalamaKayit.RowCount > 0)
            {
                if (MessageBox.Show(tblParcalamaKayit.CurrentRow.Cells[0].Value.ToString() + " numaralı tartımı silmek istediğinize emin misiniz?", "Ürünü Sil!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                        SqlCommand komut = new SqlCommand();
                        komut.Connection = baglanti;
                        komut.CommandText = "DELETE FROM ParcalamaIcerik where Sira=@sirano;";
                        komut.Parameters.AddWithValue("@sirano", tblParcalamaKayit.CurrentRow.Cells[0].Value);
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        logKayitYap("* * KAYIT SİLME * *", tblParcalamaKayit.CurrentRow.Cells[0].Value.ToString() + " Kaydı Listeden Silindi.");
                        parcalamaTartimIcerikCek();
                        //tblParcalamaKayit.CurrentCell = tblParcalamaKayit.Rows[tblParcalamaKayit.RowCount - 1].Cells[0];
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnIptalParcalama_Click(object sender, EventArgs e)
        {
            if (btnOnaylaParcalama.Text == "Kaydet")
            {
                cbTartimsizKayitParcalama.Enabled = true;
                txtPaletAgirligiParcalama.Enabled = true;
                txtKoliAdetParcalama.Enabled = true;
                txtKoliAgirligiParcalama.Enabled = true;
                txtToplamDaraParcalama.Enabled = true;
                btnOnaylaParcalama.Text = "Onayla";
                btnOnaylaParcalama.ImageOptions.Image = TeraziDevEx.Properties.Resources.apply_32x32;
                btnIptalParcalama.Enabled = true;
                btnTartimiBitirParcalama.Enabled = true;
                tblParcalamaKayit.Enabled = true;
                txtNetKgParcalama.ReadOnly = true;
                txtBrutKgParcalama.ReadOnly = true;
                btnDuzenleParcalama.Enabled = true;
                btnSilParcalama.Enabled = true;
                btnRaporlaParcalama.Enabled = true;
                btnIptalParcalama.PerformClick();
            }
            else
            {
                cmbParcalamaAdet.SelectedIndex = 0;
                if (secilenTarti != 0) gcParcalamaEkran.Text = "0,0";
                else gcParcalamaEkran.Text = "-----";
                txtBrutKgParcalama.Text = "0,0";
                txtToplamDaraParcalama.Text = "0,0";
                txtPartiNoParcalama.Clear();
                txtUrunAdiParcalama.Text = "";
                txtPaletAgirligiParcalama.Text = "0,0";
                txtKoliAdetParcalama.Text = "0";
                txtKoliAgirligiParcalama.Text = "0,0";
                txtNetKgParcalama.Text = "0,0";
            }

        }

        private void BtnDuzenleParcalama_Click(object sender, EventArgs e)
        {
            if (tblParcalamaKayit.Rows.Count > 0)
            {
                cbTartimsizKayitParcalama.Enabled = false;
                txtPaletAgirligiParcalama.Text = "0,0";
                txtKoliAdetParcalama.Text = "0";
                txtKoliAgirligiParcalama.Text = "0,0";
                txtPaletAgirligiParcalama.Enabled = false;
                txtKoliAdetParcalama.Enabled = false;
                txtKoliAgirligiParcalama.Enabled = false;
                txtToplamDaraParcalama.Enabled = true;
                btnOnaylaParcalama.Text = "Kaydet";
                btnOnaylaParcalama.ImageOptions.Image = TeraziDevEx.Properties.Resources.saveto_32x32;
                btnTartimiBitirParcalama.Enabled = false;
                tblParcalamaKayit.Enabled = false;
                txtNetKgParcalama.ReadOnly = false;
                txtBrutKgParcalama.ReadOnly = false;
                btnDuzenleParcalama.Enabled = false;
                btnSilParcalama.Enabled = false;
                btnRaporlaParcalama.Enabled = false;
                cmbParcalamaAdet.Text = tblParcalamaKayit.CurrentRow.Cells[3].Value.ToString();
                txtUrunAdiParcalama.Text = tblParcalamaKayit.CurrentRow.Cells[2].Value.ToString();
                txtPartiNoParcalama.Text = tblParcalamaKayit.CurrentRow.Cells[1].Value.ToString();
                txtNetKgParcalama.Text = tblParcalamaKayit.CurrentRow.Cells[6].Value.ToString();
                txtToplamDaraParcalama.Text = tblParcalamaKayit.CurrentRow.Cells[5].Value.ToString();
                txtBrutKgParcalama.Text = tblParcalamaKayit.CurrentRow.Cells[4].Value.ToString();
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chKesimhaneKulakNosuz.Checked == true)
            {
                tblKulakNolar.Enabled = false;
                btnKesimhaneNext.Enabled = false;
            }
            else
            {
                btnKesimhaneNext.Enabled = true;
                tblKulakNolar.Enabled = true;
            }
        }

        private void TbMenu_SelectedPageChanging(object sender, TabFormSelectedPageChangingEventArgs e)
        {
            if (tbMenu.SelectedPage == tbKesimHane)
            {
                if (tblKesimhane.Rows.Count > 0 && tblKesimhane.Columns[0].HeaderText != "PartiNo")
                {
                    KesimhanePartiIptal(cmbKesimhaneParti.Text);
                }
                if (secilenTarti != 0)
                {
                    sp.Close();
                    secilenTarti = 0;
                }
            }
            else if (tbMenu.SelectedPage == tbAyarlar)
            {
                btnAyarlarIptalEt.PerformClick();
            }
            else if (tbMenu.SelectedPage == tbParcalama)
            {
                if (gcTartımBilgileriParcalama.Visible == false)
                {
                    btnTartimiBitirParcalama.PerformClick();
                }
                if (secilenTarti != 0)
                {
                    sp.Close();
                    secilenTarti = 0;
                }
            }
            else if (tbMenu.SelectedPage==tbTartimlar)
            {
                btnTartimlarKapat.PerformClick();
            }

        }

        public void logKayitYap(string adi, string aciklama)
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "INSERT INTO TUMISLEMLER (ISLEMADI,ISLEMACIKLAMA,KULLANICI,TARIH) values (@adi,@aciklama,@user,@tarih);";
                komut.Parameters.AddWithValue("@adi", adi);
                komut.Parameters.AddWithValue("@aciklama", aciklama);
                komut.Parameters.AddWithValue("@user", lblKullanici.Text);
                komut.Parameters.AddWithValue("@tarih", DateTime.Now);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void logKayitTabloDoldur()
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT TOP 200 ID[Log No],ISLEMADI[İşlem Adı],ISLEMACIKLAMA[Açıklama],KULLANICI[Kullanıcı],TARIH[Tarih] FROM TUMISLEMLER order by Tarih desc;";
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                tblLoglar.DataSource = dt;
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChTCP1_CheckedChanged(object sender, EventArgs e)
        {
            if (chTCP1.Checked == true) txtTCP1.Enabled = true;
            else txtTCP1.Enabled = false;
        }

        private void ChTCP2_CheckedChanged(object sender, EventArgs e)
        {
            if (chTCP2.Checked == true) txtTCP2.Enabled = true;
            else txtTCP2.Enabled = false;
        }

        private void ChTCP3_CheckedChanged(object sender, EventArgs e)
        {
            if (chTCP3.Checked == true) txtTCP3.Enabled = true;
            else txtTCP3.Enabled = false;
        }

        private void ChTCP4_CheckedChanged(object sender, EventArgs e)
        {
            if (chTCP4.Checked == true) txtTCP4.Enabled = true;
            else txtTCP4.Enabled = false;
        }

        private void ChTCP5_CheckedChanged(object sender, EventArgs e)
        {
            if (chTCP5.Checked == true) txtTCP5.Enabled = true;
            else txtTCP5.Enabled = false;
        }

        private void BtnTumIslemlerAra_Click(object sender, EventArgs e)
        {
            try
            {
                string islem = "";
                string kullanici = "";
                string kelime = "";
                if (cmbTumIslemler.SelectedIndex != -1) islem = " and ISLEMADI='" + cmbTumIslemler.Text + "'";
                if (cmbTumIslemlerKullanici.SelectedIndex != -1) kullanici = " and KULLANICI='" + cmbTumIslemlerKullanici.Text + "'";
                if (txtTumIslemlerArama.Text != "") kelime = " and ISLEMACIKLAMA like '%" + txtTumIslemlerArama.Text + "%' ";
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT TOP 200 ID[Log No],ISLEMADI[İşlem Adı],ISLEMACIKLAMA[Açıklama],KULLANICI[Kullanıcı],TARIH[Tarih] FROM TUMISLEMLER WHERE TARIH BETWEEN @dtp1 AND @dtp2" + islem + kullanici + kelime + " order by Tarih desc;";
                komut.Parameters.AddWithValue("@dtp1", dtpLog1.Value);
                komut.Parameters.AddWithValue("@dtp2", dtpLog2.Value);
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                tblLoglar.DataSource = dt;
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ChTumIslemlerFiltre_CheckedChanged(object sender, EventArgs e)
        {
            if (chTumIslemlerFiltre.Checked == false)
            {
                cmbTumIslemler.SelectedIndex = -1;
                cmbTumIslemlerKullanici.SelectedIndex = -1;
                txtTumIslemlerArama.Text = "";
                dtpLog1.Value = DateTime.Now.AddMonths(-1);
                dtpLog2.Value = DateTime.Now;
                cmbTumIslemler.Enabled = false;
                dtpLog1.Enabled = false;
                dtpLog2.Enabled = false;
                txtTumIslemlerArama.Enabled = false;
                cmbTumIslemlerKullanici.Enabled = false;
                btnTumIslemlerAra.Enabled = false;
            }
            else
            {
                cmbTumIslemler.Enabled = true;
                dtpLog1.Enabled = true;
                dtpLog2.Enabled = true;
                txtTumIslemlerArama.Enabled = true;
                cmbTumIslemlerKullanici.Enabled = true;
                btnTumIslemlerAra.Enabled = true;
            }
        }

        private void BtnTumIslemlerTumu_Click(object sender, EventArgs e)
        {
            cmbTumIslemler.SelectedIndex = -1;
            cmbTumIslemlerKullanici.SelectedIndex = -1;
            txtTumIslemlerArama.Text = "";
            dtpLog1.Value = DateTime.Now.AddMonths(-1);
            dtpLog2.Value = DateTime.Now;
            logKayitTabloDoldur();
        }

        private void TblKullanicilar_MouseClick(object sender, MouseEventArgs e)
        {
            kullaniciBilgiAl();
        }

        private void kullaniciBilgiAl()
        {
            chIslemKaydi.Checked = false;
            chTumIslemleriGoruntule.Checked = false;
            chIslemDuzenleme.Checked = false;
            chUrunlerYetki.Checked = false;
            chAyarlarYetki.Checked = false;
            chKullanicilarYetki.Checked = false;
            if (tblKullanicilar.RowCount > 0)
            {
                txtKullanicilarSicilNo.Text = tblKullanicilar.CurrentRow.Cells[0].Value.ToString();
                txtKullanicilarAdi.Text = tblKullanicilar.CurrentRow.Cells[1].Value.ToString();
                txtKullanicilarUsername.Text = tblKullanicilar.CurrentRow.Cells[2].Value.ToString();
                txtKullanicilarSifre.Text = tblKullanicilar.CurrentRow.Cells[3].Value.ToString();
                cmbKullanicilarDepartman.SelectedItem = tblKullanicilar.CurrentRow.Cells[4].Value.ToString();
                string okunan = "";
                SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                try
                {
                    komut.Connection = conn;
                    komut.CommandText = "SELECT Yetkiler FROM [Terazi].[dbo].[Kullanicilar] WHERE username=@username ";
                    komut.Parameters.AddWithValue("@username", tblKullanicilar.CurrentRow.Cells[2].Value.ToString());
                    conn.Open();
                    SqlDataReader dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        okunan = dr[0].ToString();
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Kayıt düzenlenemedi.\n Hata: " + ex, "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                char[] okunanchar = okunan.ToCharArray();
                for (int i = 0; i < okunanchar.Length; i++)
                {
                    if (okunanchar[i].ToString() == "1")
                    {
                        chIslemKaydi.Checked = true;
                    }
                    if (okunanchar[i].ToString() == "2")
                    {
                        chTumIslemleriGoruntule.Checked = true;
                    }
                    if (okunanchar[i].ToString() == "3")
                    {
                        chIslemDuzenleme.Checked = true;
                    }
                    if (okunanchar[i].ToString() == "4")
                    {
                        chUrunlerYetki.Checked = true;
                    }
                    if (okunanchar[i].ToString() == "5")
                    {
                        chKullanicilarYetki.Checked = true;
                    }
                    if (okunanchar[i].ToString() == "6")
                    {
                        chAyarlarYetki.Checked = true;
                    }
                    if (okunanchar[i].ToString() == "0")
                    {
                        chIslemKaydi.Checked = false;
                        chTumIslemleriGoruntule.Checked = false;
                        chIslemDuzenleme.Checked = false;
                        chUrunlerYetki.Checked = false;
                        chAyarlarYetki.Checked = false;
                        chKullanicilarYetki.Checked = false;
                    }
                }
            }

        }

        private void TblKullanicilar_SelectionChanged(object sender, EventArgs e)
        {
            kullaniciBilgiAl();
        }

        private void TumTartimlarPartiAl()
        {
            if (true)
            {

            }
        }

        private void ChTartimlarFiltrele_CheckedChanged(object sender, EventArgs e)
        {
            if (chTartimlarFiltrele.Checked == false) //KAPALI
            {
                //dtpTartimlar1.Enabled = false;
                //dtpTartimlar2.Enabled = false;
                txtTartimlarKelime.Enabled = false;
                btnTartimlarAra.Enabled = false;
                txtTartimlarKelime.Text = "";
                dtpTartimlar1.Value = DateTime.Now.AddMonths(-1);
                dtpTartimlar2.Value = DateTime.Now;
                chTartimlarSilinen.Enabled = false;
            }
            else //AÇIK
            {
                if (cmbTartimlarNoktalar.SelectedIndex == 1) chTartimlarSilinen.Enabled = true;
                else chTartimlarSilinen.Enabled = false;
                dtpTartimlar1.Enabled = true;
                dtpTartimlar2.Enabled = true;
                txtTartimlarKelime.Enabled = true;
                btnTartimlarAra.Enabled = true;
            }
        }

        private void CmbTartimlarNoktalar_SelectedIndexChanged(object sender, EventArgs e)
        {
            chTartimlarFiltrele.Checked = false;
            if (cmbTartimlarNoktalar.SelectedIndex == 1)//PARÇALAMA
            {
                TartimlarParcalamaKayitlariAl();
            }
            else if (cmbTartimlarNoktalar.SelectedIndex == 0)//KESİMHANE
            {
                TartimlarKesimhaneKayitlariAl();
            }
        }

        private void TartimlarParcalamaKayitlariAl()
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "Select top 500 TartimNo[Tartım No],Tur[Hareket Yönü],HareketYeri[Hareket Yeri],Aciklama[Açıklama],Tarih from ParcalamaTartimlar Where Durum=0 order by Tarih desc; ";
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                tblTartimlar.DataSource = dt;
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TartimlarKesimhaneKayitlariAl()
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT top 500 PartiNo[Parti No],MusteriNo[Müşteri No],MusteriAdi[Müşteri Adı],Adet,ToplamAgirlik[Toplam Ağırlık],Durum,AlimTarihi[Alım Tarihi],KesimTarihi[Kesim Tarihi] FROM [Terazi].[dbo].[Partiler]  order by KesimTarihi desc";//;
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                tblTartimlar.DataSource = dt;
                baglanti.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TblTartimlar_SelectionChanged(object sender, EventArgs e)
        {
            if (tblTartimlar.RowCount > 0 && btnTartimlarKapat.Enabled == false)
            {
                btnTartimlarIncele.Enabled = true;
            }
        }
        public static string tartimnosu = "";
        private void BtnTartimlarIncele_Click(object sender, EventArgs e)
        {
            if (tblTartimlar.RowCount > 0)
            {
                if (cmbTartimlarNoktalar.SelectedIndex == 1) //PARÇALAMA 0
                {
                    string secilentartim = tblTartimlar.CurrentRow.Cells[0].Value.ToString();
                    tartimnosu = secilentartim;
                    if (secilentartim != string.Empty)
                    {
                        try
                        {
                            SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                            SqlCommand komut = new SqlCommand();
                            komut.Connection = baglanti;
                            komut.CommandText = "SELECT TartimNo[Tartım No],PartiNo[Parti No],UrunAdi[Ürün Adı],Adet[Adet],BrutKg[Brüt Kg],Dara[Dara Kg],NetKg[Net Kg],Kullanici[İşlemi Yapan],Tarih[İşlem Tarihi] FROM ParcalamaIcerik Where TartimNo=@p1";
                            komut.Parameters.AddWithValue("@p1", secilentartim);
                            baglanti.Open();
                            SqlDataAdapter da = new SqlDataAdapter(komut);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            tblTartimlar.DataSource = dt;
                            baglanti.Close();
                            grpTartimlarArama.Enabled = false;
                            btnTartimlarIncele.Enabled = false;
                            grpTartimlarNoktalar.Enabled = false;
                            btnTartimlarKapat.Enabled = true;
                            btnTartimlarRaporla.Enabled = true;
                        }
                        catch (SqlException ex)
                        {

                            MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (cmbTartimlarNoktalar.SelectedIndex == 0) //KESİMHANE
                {
                    string secilentartim = tblTartimlar.CurrentRow.Cells[0].Value.ToString();
                    if (secilentartim != string.Empty)
                    {
                        try
                        {
                            SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                            SqlCommand komut = new SqlCommand();
                            komut.Connection = baglanti;
                            komut.CommandText = "SELECT PartiNo[Parti No],KulakNo[Kulak No],UrunAdi[Hayvan Cinsi],NetKg[Net],OzelDurum[Açıklama],Kullanici[İşlemi Yapan],Tarih FROM [Terazi].[dbo].[IslemKaydi] Where PartiNo=@p1";
                            komut.Parameters.AddWithValue("@p1", secilentartim);
                            baglanti.Open();
                            SqlDataAdapter da = new SqlDataAdapter(komut);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            tblTartimlar.DataSource = dt;
                            baglanti.Close();
                            grpTartimlarArama.Enabled = false;
                            btnTartimlarIncele.Enabled = false;
                            grpTartimlarNoktalar.Enabled = false;
                            btnTartimlarKapat.Enabled = true;
                            btnTartimlarRaporla.Enabled = true;
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void BtnTartimlarKapat_Click(object sender, EventArgs e)
        {
            btnTartimlarIncele.Enabled = true;
            grpTartimlarArama.Enabled = true;
            grpTartimlarNoktalar.Enabled = true;
            cmbTartimlarNoktalar.SelectedIndex = 0;
            btnTartimlarKapat.Enabled = false;
            btnTartimlarRaporla.Enabled = false;
            if (cmbTartimlarNoktalar.SelectedIndex == 1) TartimlarParcalamaKayitlariAl();
            else TartimlarKesimhaneKayitlariAl();

        }

        private void BtnTartimlarRaporla_Click(object sender, EventArgs e)
        {
            if (tblTartimlar.Columns[0].HeaderText == "Parti No") //KESİMHANE
            {
                try
                {
                    string parti = tblTartimlar.CurrentRow.Cells[0].Value.ToString();
                    logKayitYap("* * RAPOR ALINDI - KESİM * *", "Parti No:" + parti);
                    rapor r = new rapor();
                    r.raporlakesimhane(parti);
                    r.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Rapor Alımı Sırasında hata Oluştu." + ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (cmbTartimlarNoktalar.Text == "PARÇALAMA") //PARÇALAMA
            {
                try
                {
                    string tartimno = tblTartimlar.Rows[0].Cells[0].Value.ToString();
                    logKayitYap("* * RAPOR ALINDI - PARÇALAMA * *", "Tartım No:" + tartimnosu);
                    //MessageBox.Show(tartimnosu);
                    rapor r = new rapor();
                    r.raporlaparcalama(tartimnosu,null);
                    r.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Rapor Alımı Sırasında hata Oluştu." + ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnTartimlarAra_Click(object sender, EventArgs e)
        {
            if (cmbTartimlarNoktalar.SelectedIndex == 1) //PARÇALAMA ARAMA
            {
                try
                {
                    string silindimi = " Durum=0 ";
                    string kelime = " ";
                    if (txtTartimlarKelime.Text != "") kelime = " and (Aciklama like '%" + txtTartimlarKelime.Text + "%' or HareketYeri like '%" + txtTartimlarKelime.Text + "%' or Tur='" + txtTartimlarKelime.Text + "' )";
                    if (chTartimlarSilinen.Checked) silindimi = " Durum=1 ";
                    SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                    SqlCommand komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = "Select TartimNo[Tartım No],Tur[Hareket Yönü],HareketYeri[Hareket Yeri],Aciklama[Açıklama],Tarih from ParcalamaTartimlar Where " + silindimi + kelime + " and (Tarih between @dtp1 and @dtp2) order by Tarih desc; ";
                    komut.Parameters.AddWithValue("@dtp1", dtpTartimlar1.Value);
                    komut.Parameters.AddWithValue("@dtp2", dtpTartimlar2.Value);
                    baglanti.Open();
                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tblTartimlar.DataSource = dt;
                    baglanti.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (cmbTartimlarNoktalar.SelectedIndex == 0) //KESİMHANE ARAMA
            {
                try
                {
                    string kelime = "";
                    if (txtTartimlarKelime.Text != "") kelime = " and ((MusteriAdi like '%" + txtTartimlarKelime.Text + "%') or (MusteriNo like '%" + txtTartimlarKelime.Text + "%') or (PartiNo like '%" + txtTartimlarKelime.Text + "%')) ";
                    SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                    SqlCommand komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = "SELECT PartiNo[Parti No],MusteriNo[Müşteri No],MusteriAdi[Müşteri Adı],Adet,ToplamAgirlik[Toplam Ağırlık],AlimTarihi[Alım Tarihi],KesimTarihi[Kesim Tarihi] FROM [Terazi].[dbo].[Partiler] where Durum='KESİLDİ'" + kelime + " and (KesimTarihi between @dtp1 and @dtp2);";
                    komut.Parameters.AddWithValue("@dtp1", dtpTartimlar1.Value);
                    komut.Parameters.AddWithValue("@dtp2", dtpTartimlar2.Value);
                    baglanti.Open();
                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    tblTartimlar.DataSource = dt;
                    baglanti.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CmbKullanicilarDepartman_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKullanicilarDepartman.SelectedIndex == 0) //YÖNETİM
            {
                chAyarlarYetki.Checked = true;
                chKullanicilarYetki.Checked = true;
                chIslemKaydi.Checked = false;
                chIslemDuzenleme.Checked = true;
                chUrunlerYetki.Checked = true;
                chTumIslemleriGoruntule.Checked = true;
            }
            else if (cmbKullanicilarDepartman.SelectedIndex == 1) //KESİM
            {
                chAyarlarYetki.Checked = true;
                chKullanicilarYetki.Checked = false;
                chIslemKaydi.Checked = true;
                chIslemDuzenleme.Checked = true;
                chUrunlerYetki.Checked = false;
                chTumIslemleriGoruntule.Checked = false;
            }
            else if (cmbKullanicilarDepartman.SelectedIndex == 2) //HAYVAN ALIM
            {
                chAyarlarYetki.Checked = true;
                chKullanicilarYetki.Checked = false;
                chIslemKaydi.Checked = true;
                chIslemDuzenleme.Checked = true;
                chUrunlerYetki.Checked = false;
                chTumIslemleriGoruntule.Checked = false;
            }
            else if (cmbKullanicilarDepartman.SelectedIndex == 3) //TEKNİK
            {
                chAyarlarYetki.Checked = true;
                chKullanicilarYetki.Checked = false;
                chIslemKaydi.Checked = true;
                chIslemDuzenleme.Checked = true;
                chUrunlerYetki.Checked = true;
                chTumIslemleriGoruntule.Checked = true;
            }
            else if (cmbKullanicilarDepartman.SelectedIndex == 4) //PARÇALAMA
            {
                chAyarlarYetki.Checked = true;
                chKullanicilarYetki.Checked = false;
                chIslemKaydi.Checked = true;
                chIslemDuzenleme.Checked = true;
                chUrunlerYetki.Checked = true;
                chTumIslemleriGoruntule.Checked = false;
            }
            else if (cmbKullanicilarDepartman.SelectedIndex == -1)
            {
                chAyarlarYetki.Checked = false;
                chKullanicilarYetki.Checked = false;
                chIslemKaydi.Checked = false;
                chIslemDuzenleme.Checked = false;
                chUrunlerYetki.Checked = false;
                chTumIslemleriGoruntule.Checked = false;
            }
            else
            {
                chAyarlarYetki.Checked = false;
                chKullanicilarYetki.Checked = false;
                chIslemKaydi.Checked = false;
                chIslemDuzenleme.Checked = false;
                chUrunlerYetki.Checked = false;
                chTumIslemleriGoruntule.Checked = false;
            }
        }

        private void ayaroku()
        {
            if (TeraziDevEx.Properties.Settings.Default.Terazi1Aktivite.Equals(false)) rbTeraziPasif1.Checked = true;
            else rbTeraziAktif1.Checked = true;
            if (TeraziDevEx.Properties.Settings.Default.Terazi2Aktivite.Equals(false)) rbTeraziPasif2.Checked = true;
            else rbTeraziAktif2.Checked = true;
            if (TeraziDevEx.Properties.Settings.Default.Terazi3Aktivite.Equals(false)) rbTeraziPasif3.Checked = true;
            else rbTeraziAktif3.Checked = true;
            if (TeraziDevEx.Properties.Settings.Default.Terazi4Aktivite.Equals(false)) rbTeraziPasif4.Checked = true;
            else rbTeraziAktif4.Checked = true;
            if (TeraziDevEx.Properties.Settings.Default.Terazi5Aktivite.Equals(false)) rbTeraziPasif5.Checked = true;
            else rbTeraziAktif5.Checked = true;
            if (TeraziDevEx.Properties.Settings.Default.Terazi6Aktivite.Equals(false)) rbTeraziPasif6.Checked = true;
            else rbTeraziAktif6.Checked = true;

            txtTeraziAdi1.Text = Properties.Settings.Default.TeraziIsim1;
            txtTeraziAdi2.Text = Properties.Settings.Default.TeraziIsim2;
            txtTeraziAdi3.Text = Properties.Settings.Default.TeraziIsim3;
            txtTeraziAdi4.Text = Properties.Settings.Default.TeraziIsim4;
            txtTeraziAdi5.Text = Properties.Settings.Default.TeraziIsim5;
            txtTeraziAdi6.Text = Properties.Settings.Default.TeraziIsim6;

            cmbCOMPort1.Text = Properties.Settings.Default.TeraziCom1;
            cmbCOMPort2.Text = Properties.Settings.Default.TeraziCom2;
            cmbCOMPort3.Text = Properties.Settings.Default.TeraziCom3;
            cmbCOMPort4.Text = Properties.Settings.Default.TeraziCom4;
            cmbCOMPort5.Text = Properties.Settings.Default.TeraziCom5;
            cmbCOMPort6.Text = Properties.Settings.Default.TeraziCom6;
            teraziEnableKontrol();
        }

        private void ayaruygula()
        {
            if (MessageBox.Show("Ayarlar kaydedilecektir.Devam etmek istediğinize emin misiniz?", "Ayarları Uygula!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Properties.Settings.Default.TeraziCom1 = cmbCOMPort1.Text;
                Properties.Settings.Default.TeraziCom2 = cmbCOMPort2.Text;
                Properties.Settings.Default.TeraziCom3 = cmbCOMPort3.Text;
                Properties.Settings.Default.TeraziCom4 = cmbCOMPort4.Text;
                Properties.Settings.Default.TeraziCom5 = cmbCOMPort5.Text;
                Properties.Settings.Default.TeraziCom6 = cmbCOMPort6.Text;
                Properties.Settings.Default.TeraziIsim1 = txtTeraziAdi1.Text;
                Properties.Settings.Default.TeraziIsim2 = txtTeraziAdi2.Text;
                Properties.Settings.Default.TeraziIsim3 = txtTeraziAdi3.Text;
                Properties.Settings.Default.TeraziIsim4 = txtTeraziAdi4.Text;
                Properties.Settings.Default.TeraziIsim5 = txtTeraziAdi5.Text;
                Properties.Settings.Default.TeraziIsim6 = txtTeraziAdi6.Text;

                if (rbTeraziAktif1.Checked == true) Properties.Settings.Default.Terazi1Aktivite = true;
                else Properties.Settings.Default.Terazi1Aktivite = false;
                if (rbTeraziAktif2.Checked == true) Properties.Settings.Default.Terazi2Aktivite = true;
                else Properties.Settings.Default.Terazi2Aktivite = false;
                if (rbTeraziAktif3.Checked == true) Properties.Settings.Default.Terazi3Aktivite = true;
                else Properties.Settings.Default.Terazi3Aktivite = false;
                if (rbTeraziAktif4.Checked == true) Properties.Settings.Default.Terazi4Aktivite = true;
                else Properties.Settings.Default.Terazi4Aktivite = false;
                if (rbTeraziAktif5.Checked == true) Properties.Settings.Default.Terazi5Aktivite = true;
                else Properties.Settings.Default.Terazi5Aktivite = false;
                if (rbTeraziAktif6.Checked == true) Properties.Settings.Default.Terazi6Aktivite = true;
                else Properties.Settings.Default.Terazi6Aktivite = false;
                Properties.Settings.Default.Save();
            }
        }

        private void teraziEnableKontrol()
        {
            if (rbTeraziAktif1.Checked == true)
            {
                lbl1.Enabled = true;
                lblt1.Enabled = true;
                pcbKesimhaneBuyuk.Enabled = true;
            }
            else
            {
                lbl1.Enabled = false;
                lblt1.Enabled = false;
                pcbKesimhaneBuyuk.Enabled = false;
            }
            if (rbTeraziAktif2.Checked == true)
            {
                lbl2.Enabled = true;
                lblt2.Enabled = true;
                pcbKesimhaneKucuk.Enabled = true;
            }
            else
            {
                lbl2.Enabled = false;
                lblt2.Enabled = false;
                pcbKesimhaneKucuk.Enabled = false;
            }
            if (rbTeraziAktif3.Checked == true)
            {
                lbl3.Enabled = true;
                lblt3.Enabled = true;
                pcb3.Enabled = true;
            }
            else
            {
                lbl3.Enabled = false;
                lblt3.Enabled = false;
                pcb3.Enabled = false;
            }
            if (rbTeraziAktif4.Checked == true)
            {
                lbl4.Enabled = true;
                lblt4.Enabled = true;
                pcb4.Enabled = true;
            }
            else
            {
                lbl4.Enabled = false;
                lblt4.Enabled = false;
                pcb4.Enabled = false;
            }
            if (rbTeraziAktif5.Checked == true)
            {
                lbl5.Enabled = true;
                lblt5.Enabled = true;
                pcb5.Enabled = true;
            }
            else
            {
                lbl5.Enabled = false;
                lblt5.Enabled = false;
                pcb5.Enabled = false;
            }
            if (rbTeraziAktif6.Checked == true)
            {
                lbl6.Enabled = true;
                lblt6.Enabled = true;
                pcb6.Enabled = true;
            }
            else
            {
                lbl6.Enabled = false;
                lblt6.Enabled = false;
                pcb6.Enabled = false;
            }
        }

        private void BtnAyarlarIptalEt_Click(object sender, EventArgs e)
        {
            ayaroku();
        }

        private void BtnAyarlarKaydet_Click(object sender, EventArgs e)
        {
            ayaruygula();
        }

        private void TbMenu_Click(object sender, EventArgs e)
        {
            if (tbMenu.SelectedPage == tbKesimHane)
            {
                tabloBoyaKesimhane();
            }
        }

        private void TxtPaletAgirligiParcalama_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void TxtToplamDaraParcalama_TextChanged(object sender, EventArgs e)
        {
            if (cbTartimsizKayitParcalama.Checked == false && btnOnaylaParcalama.Text != "Kaydet")
            {
                double gcDeger;
                double toplamDara;
                try
                {
                    gcDeger = Convert.ToDouble(gcParcalamaEkran.Text);
                }
                catch
                {
                    gcDeger = 0;
                }
                try
                {
                    toplamDara = Convert.ToDouble(txtToplamDaraParcalama.Text);
                }
                catch
                {
                    toplamDara = 0;
                }
                txtNetKgParcalama.Text = (gcDeger - toplamDara).ToString();
            }
        }

        private void TblKesimhane_MouseClick(object sender, MouseEventArgs e)
        {
            if (tblKesimhane.RowCount > 0)
            {
                if (tblKesimhane.CurrentRow.Cells[1].Value.ToString() != "KESİLDİ")
                {
                    cmbKesimhaneParti.SelectedItem = tblKesimhane.CurrentRow.Cells[0].Value.ToString();
                    btnKesimhaneRaporAL.Enabled = false;
                }
                else
                {
                    cmbKesimhaneParti.SelectedIndex = -1;
                    btnKesimhaneRaporAL.Enabled = true;
                }
            }
        }

        private void MsiKesimhaneDuzenle_Click(object sender, EventArgs e)
        {
            
            txtKesimhaneBrut.ReadOnly = false;
            txtKesimhaneDaraKg.ReadOnly = false;
            txtKesimhaneNet.ReadOnly = false;

            cmbKesimhaneParti.Items.Add(tblKesimhane.CurrentRow.Cells[0].Value.ToString());
            cmbKesimhaneParti.SelectedItem = tblKesimhane.CurrentRow.Cells[0].Value.ToString();
            if (cmbKesimhaneParti.Text != "")
            {
                cmbKesimhaneParti.Enabled = false;
                btnKesimhaneBasla.Enabled = false;
                btnKesimhaneGeriAL.Enabled = true;
                cbOzelDurum.Enabled = true;
                kesimhaneKulakNoAl();
                tblKulakNolar.Enabled = true;
                txtKesimhaneUrunAdi.Enabled = true;
                txtKesimhaneDaraKg.Enabled = true;
                txtKesimhaneBrut.Enabled = true;
                txtKesimhaneNet.Enabled = true;
                btnKesimhaneOnayla.Enabled = true;
                btnKesimhaneIptal.Enabled = true;
                btnKesimhaneBitir.Enabled = true;
                if(!chKesimhaneKulakNosuz.Checked)btnKesimhaneNext.Enabled = true;
                btnKesimhaneRaporAL.Enabled = true;

                groupControl11.Text = cmbKesimhaneParti.Text + " Numaralı Parti Tartım Listesi";
                logKayitYap("* * KESİMHANE - TARTIM DUZENLEME * *", "Parti No: " + cmbKesimhaneParti.Text);
                kesimhanetabloal(cmbKesimhaneParti.Text);
                txtKesimhaneUrunAdi.Text = "Tosun";
            }
            else
            {
                MessageBox.Show("Lütfen tartım yapılacak parti numarasını seçiniz...", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void MsKesimhane_Opened(object sender, EventArgs e)
        {
            if (tblKesimhane.RowCount > 0)
            {
                if (tblKesimhane.Columns[0].HeaderText != "PartiNo" || tblKesimhane.CurrentRow.Cells[1].Value.ToString() != "KESİLDİ")
                {
                    msiKesimhaneRaporAL.Enabled = false;
                }
                else msiKesimhaneRaporAL.Enabled = true;

                if (tblKesimhane.Columns[0].HeaderText == "PartiNo" && tblKesimhane.CurrentRow.Cells[1].Value.ToString() == "KESİLDİ")
                {
                    msiKesimhaneDuzenle.Enabled = true;
                    msiSilKesimhane.Enabled = true;
                }
                else
                {
                    msiKesimhaneDuzenle.Enabled = false;
                    msiSilKesimhane.Enabled = false;
                }
            }
            else
            {
                msiKesimhaneDuzenle.Enabled = false;
                msiSilKesimhane.Enabled = false;
            }
        }

        private void RaporÇıktısıAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rapor r = new rapor();
                r.raporlakesimhane(tblKesimhane.CurrentRow.Cells[0].Value.ToString());
                r.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rapor Alımı Sırasında hata Oluştu." + ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void BtnRaporlaParcalama_Click(object sender, EventArgs e)
        {
            if (tblParcalamaKayit.RowCount > 0)
            {
                DialogResult sonuc = MessageBox.Show("Rapor ürün bazlı olarak (ayrı sayfalarda) alınsın mı?", "Rapor Türü", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (sonuc == DialogResult.Yes)
                {
                    try
                    {
                        SqlConnection baglanti = new SqlConnection(Properties.Settings.Default.conString);
                        SqlCommand komut = new SqlCommand();
                        komut.Connection = baglanti;
                        komut.CommandText = "SELECT UrunAdi FROM ParcalamaIcerik WHERE TartimNo=@tartimno GROUP BY UrunAdi;";
                        komut.Parameters.AddWithValue("@tartimno", cmbTartimNoParcalama.Text);
                        baglanti.Open();
                        SqlDataReader dr = komut.ExecuteReader();
                        while (dr.Read())
                        {
                            rapor r = new rapor();
                            r.raporlaparcalama(cmbTartimNoParcalama.Text, dr[0].ToString());
                            r.Show();
                        }
                        dr.Close();
                        baglanti.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Rapor Alımı Sırasında hata Oluştu." + ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    logKayitYap("* * RAPOR ALINDI - PARÇALAMA * *", "Tartim No:" + cmbTartimNoParcalama.Text);
                }
                else if (sonuc == DialogResult.No)
                {
                    try
                    {
                        logKayitYap("* * RAPOR ALINDI - PARÇALAMA * *", "Tartim No:" + cmbTartimNoParcalama.Text);
                        rapor r = new rapor();
                        r.raporlaparcalama(cmbTartimNoParcalama.Text,null);
                        r.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Rapor Alımı Sırasında hata Oluştu." + ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else MessageBox.Show("Tablo boş!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CbTartimsizKayitParcalama_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTartimsizKayitParcalama.Checked == true)
            {
                gcParcalamaEkran.Text = "-----";
                txtNetKgParcalama.Text = "----";
                txtBrutKgParcalama.Text = "----";
                txtToplamDaraParcalama.Text = "----";
                cmbParcalamaAdet.Text = "0";
                cmbParcalamaAdet.Enabled = false;
                txtPaletAgirligiParcalama.Enabled = false;
                txtKoliAdetParcalama.Enabled = false;
                txtKoliAgirligiParcalama.Enabled = false;
                txtToplamDaraParcalama.Enabled = false;
                btnDuzenleParcalama.Enabled = false;
                btnSilParcalama.Enabled = false;
            }
            else
            {
                if (secilenTarti == 0) gcParcalamaEkran.Text = "-----";
                else gcParcalamaEkran.Text = "0,0";
                txtNetKgParcalama.Text = "0,0";
                txtBrutKgParcalama.Text = "0,0";
                txtToplamDaraParcalama.Text = "0,0";
                cmbParcalamaAdet.Text = "1";
                cmbParcalamaAdet.Enabled = true;
                txtPaletAgirligiParcalama.Enabled = true;
                txtKoliAdetParcalama.Enabled = true;
                txtKoliAgirligiParcalama.Enabled = true;
                txtToplamDaraParcalama.Enabled = true;
                btnDuzenleParcalama.Enabled = true;
                btnSilParcalama.Enabled = true;
            }
        }

        private void BtnTartimNoVer_Click(object sender, EventArgs e)
        {
            DateTime sontarih = DateTime.Today;
            string tarih = DateTime.Now.ToString("dd") + "." + DateTime.Now.ToString("MM") + "." + DateTime.Now.ToString("yy") + "-";
            string maxkayit = "0";
            try
            {
                SqlConnection baglanti = new SqlConnection(Properties.Settings.Default.conString);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "select MAX(TartimNo) from ParcalamaTartimlar where TartimNo like '" + tarih + "%';";
                baglanti.Open();
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    if (dr[0].ToString() == "")
                    {
                        maxkayit = "1";
                    }
                    else
                    {
                        maxkayit = dr[0].ToString();
                    }
                }
                dr.Close();
                if (maxkayit.ToString().Length > 6)
                {
                    int max;
                    try
                    {
                        max = Convert.ToInt32(maxkayit.ToString().Replace(tarih, ""));
                    }
                    catch
                    {
                        max = 0;
                    }
                    max += 1;
                    cmbTartimNoParcalama.Text = tarih + max.ToString();
                }
                else cmbTartimNoParcalama.Text = tarih + maxkayit.ToString();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Numara Oluşturma Sırasında hata Oluştu." + ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TxtKesimhaneDaraKg_TextChanged(object sender, EventArgs e)
        {
            double gcDeger;
            double toplamDara;
            try
            {
                gcDeger = Convert.ToDouble(gcKesimhaneEkran.Text);
            }
            catch
            {
                gcDeger = 0;
            }
            try
            {
                toplamDara = Convert.ToDouble(txtKesimhaneDaraKg.Text);
            }
            catch
            {
                toplamDara = 0;
            }
            txtKesimhaneNet.Text = (gcDeger - toplamDara).ToString();
        }

        private void BtnKesimhaneRaporAL_Click(object sender, EventArgs e)
        {
            if (cmbKesimhaneParti.Text != "" && tblKesimhane.RowCount > 0)
            {
                string partiNo = cmbKesimhaneParti.Text;
                btnKesimhaneBitir.PerformClick();
                rapor r = new rapor();
                r.raporlakesimhane(partiNo);
                r.Show();
            }
            else if (tblKesimhane.SelectedRows.Count>0)
            {
                if (tblKesimhane.SelectedRows[0].Cells[1].Value.ToString() == "KESİLDİ")
                {
                    string partiNo = tblKesimhane.SelectedRows[0].Cells[0].Value.ToString();
                    rapor r = new rapor();
                    r.raporlakesimhane(partiNo);
                    r.Show();
                }
            }
        }

        private void BtnKisayol61_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[60]);
        }

        private void BtnKisayol62_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[61]);
        }

        private void BtnKisayol63_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[62]);
        }

        private void BtnKisayol64_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[63]);
        }

        private void BtnKisayol65_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[64]);
        }

        private void BtnKisayol66_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[65]);
        }

        private void BtnKisayol67_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[66]);
        }

        private void BtnKisayol68_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[67]);
        }

        private void BtnKisayol69_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[68]);
        }

        private void BtnKisayol70_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[69]);
        }

        private void BtnKisayol71_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[70]);
        }

        private void BtnKisayol72_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[71]);
        }

        private void BtnKisayol73_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[72]);
        }

        private void BtnKisayol74_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[73]);
        }

        private void BtnKisayol75_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[74]);
        }

        private void BtnKisayol76_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[75]);
        }

        private void BtnKisayol77_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[76]);
        }

        private void BtnKisayol78_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[77]);
        }

        private void BtnKisayol79_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[78]);
        }

        private void BtnKisayol80_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[79]);
        }

        private void BtnKisayol31_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[30]);
        }

        private void BtnKisayol32_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[31]);
        }

        private void BtnKisayol33_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[32]);
        }

        private void BtnKisayol41_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[40]);
        }

        private void BtnKisayol34_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[33]);
        }

        private void BtnKisayol35_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[34]);
        }

        private void BtnKisayol36_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[35]);
        }

        private void BtnKisayol37_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[36]);
        }

        private void BtnKisayol38_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[37]);
        }

        private void BtnKisayol39_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[38]);
        }

        private void BtnKisayol40_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[39]);
        }

        private void BtnKisayol42_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[41]);
        }

        private void BtnKisayol43_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[42]);
        }

        private void BtnKisayol44_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[43]);
        }

        private void BtnKisayol45_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[44]);
        }

        private void BtnKisayol46_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[45]);
        }

        private void BtnKisayol47_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[46]);
        }

        private void BtnKisayol48_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[47]);
        }

        private void BtnKisayol49_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[48]);
        }

        private void BtnKisayol50_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[49]);
        }

        private void BtnKisayol51_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[50]);
        }

        private void BtnKisayol52_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[51]);
        }

        private void BtnKisayol53_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[52]);
        }

        private void BtnKisayol54_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[53]);
        }

        private void BtnKisayol55_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[54]);
        }

        private void BtnKisayol56_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[55]);
        }

        private void BtnKisayol57_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[56]);
        }

        private void BtnKisayol58_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[57]);
        }

        private void BtnKisayol59_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[58]);
        }

        private void BtnKisayol60_Click(object sender, EventArgs e)
        {
            urunAl(kisayolSiraNo[59]);
        }

        private void TblUrunler_Click(object sender, EventArgs e)
        {
            txtYonetimUrunNo.Text = tblUrunler.CurrentRow.Cells[0].Value.ToString();
            txtYonetimUrunAdi.Text = tblUrunler.CurrentRow.Cells[1].Value.ToString();
        }

        private void TblKesimhane_Sorted(object sender, EventArgs e)
        {
            tabloBoyaKesimhane();
        }

        private void TbMenu_SizeChanged(object sender, EventArgs e)
        {

        }

        private void TxtKAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnGiris.PerformClick();
            }
        }

        private void TxtSifre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnGiris.PerformClick();
            }
        }

        private void gcKesimhaneEkran_Changed(object sender, EventArgs e)
        {
            try
            {
                txtKesimhaneBrut.Text = gcKesimhaneEkran.Text;
                txtKesimhaneNet.Text = (Convert.ToDouble(txtKesimhaneBrut.Text) - Convert.ToDouble(txtKesimhaneDaraKg.Text)).ToString();
            }
            catch
            {
                txtKesimhaneBrut.Text = "0,0";
                txtKesimhaneNet.Text = "0,0";
                txtKesimhaneDaraKg.Text = "0,0";
            }
        }

        private void msiSilKesimhane_Click(object sender, EventArgs e)
        {
            string partino = tblKesimhane.CurrentRow.Cells[0].Value.ToString();
            if (MessageBox.Show(partino + " parti numaralı kesim kaydını silmek istediğinize emin misiniz?\nYalnızca tartım içeriği silinecekir ve işlem durumu 'Kesim Bekleniyor...' olarak düzenlenecektir.\nAlım partisini sadece Hayvan Alımı Birimi silebilir.", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                    SqlCommand komut = new SqlCommand();
                    komut.Connection = baglanti;
                    komut.CommandText = "update Partiler set Durum='Kesim Bekleniyor...',KesimTarihi=NULL where PartiNo=@partino;";
                    komut.Parameters.AddWithValue("@partino", partino);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    komut.CommandText = "delete from IslemKaydi where PartiNo=@partino";
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    kesimhanePartiAl();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Veritabanı Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tblPartiler_Click(object sender, EventArgs e)
        {
            partiIcerigiAc();
        }

        private void txtKulakNoHayvanAlimi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnOnaylaHayvanAlimi.PerformClick();
            }
        }

        private void btnYenileKesimhane_Click(object sender, EventArgs e)
        {
            if (btnKesimhaneOnayla.Enabled == true) kesimhanetabloal(cmbKesimhaneParti.Text);
            else kesimhanePartiAl();

        }

        private void btnYenileTartimlar_Click(object sender, EventArgs e)
        {
            if (btnTartimlarIncele.Enabled == true)
            {
                chTartimlarFiltrele.Checked = false;
                if (cmbTartimlarNoktalar.SelectedIndex == 1)//PARÇALAMA
                {
                    TartimlarParcalamaKayitlariAl();
                }
                else if (cmbTartimlarNoktalar.SelectedIndex == 0)//KESİMHANE
                {
                    TartimlarKesimhaneKayitlariAl();
                }
            }
            else
            {
                if (tblTartimlar.RowCount > 0)
                {
                    if (cmbTartimlarNoktalar.SelectedIndex == 1) //PARÇALAMA 0
                    {
                        string secilentartim = tblTartimlar.CurrentRow.Cells[0].Value.ToString();
                        tartimnosu = secilentartim;
                        if (secilentartim != string.Empty)
                        {
                            try
                            {
                                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                                SqlCommand komut = new SqlCommand();
                                komut.Connection = baglanti;
                                komut.CommandText = "SELECT TartimNo[Tartım No],PartiNo[Parti No],UrunAdi[Ürün Adı],Adet[Adet],BrutKg[Brüt Kg],Dara[Dara Kg],NetKg[Net Kg],Kullanici[İşlemi Yapan],Tarih[İşlem Tarihi] FROM ParcalamaIcerik Where TartimNo=@p1";
                                komut.Parameters.AddWithValue("@p1", secilentartim);
                                baglanti.Open();
                                SqlDataAdapter da = new SqlDataAdapter(komut);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                tblTartimlar.DataSource = dt;
                                baglanti.Close();
                            }
                            catch (SqlException ex)
                            {

                                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if (cmbTartimlarNoktalar.SelectedIndex == 0) //KESİMHANE
                    {
                        string secilentartim = tblTartimlar.CurrentRow.Cells[0].Value.ToString();
                        if (secilentartim != string.Empty)
                        {
                            try
                            {
                                SqlConnection baglanti = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
                                SqlCommand komut = new SqlCommand();
                                komut.Connection = baglanti;
                                komut.CommandText = "SELECT PartiNo[Parti No],KulakNo[Kulak No],UrunAdi[Hayvan Cinsi],NetKg[Net],OzelDurum[Açıklama],Kullanici[İşlemi Yapan],Tarih FROM [Terazi].[dbo].[IslemKaydi] Where PartiNo=@p1";
                                komut.Parameters.AddWithValue("@p1", secilentartim);
                                baglanti.Open();
                                SqlDataAdapter da = new SqlDataAdapter(komut);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                tblTartimlar.DataSource = dt;
                                baglanti.Close();
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show(ex.Message, "Veritabanı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void btnYenileParcalama_Click(object sender, EventArgs e)
        {
            if (gcTartımBilgileriParcalama.Visible == true) parcalamaTartimCek();
            else
            {
                parcalamaTartimIcerikCek();
            }
        }

        private void txtHareketYeriParcalama_TextChanged(object sender, EventArgs e)
        {

        }

        private void tmrOtoYenile_Tick(object sender, EventArgs e)
        {
            if (tbMenu.SelectedPage == tbKesimHane && cbOtoYenileKesimhane.Checked)
            {
                btnYenileKesimhane.PerformClick();
            }
            else if (tbMenu.SelectedPage == tbParcalama && cbOtoYenileParcalama.Checked)
            {
                btnYenileParcalama.PerformClick();
            }
            else if (tbMenu.SelectedPage == tbTartimlar && cbOtoYenileTartimlar.Checked)
            {
                btnYenileTartimlar.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void grpKesimGirispaneli_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtKesimhaneBrut_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }
    }
}

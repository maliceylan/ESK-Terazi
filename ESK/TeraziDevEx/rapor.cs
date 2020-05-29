using System;
using System.Windows.Forms;

namespace TeraziDevEx
{
    public partial class rapor : Form
    {
        public rapor()
        {
            InitializeComponent();
        }

        private void rapor_Load(object sender, EventArgs e)
        {

        }

        public void raporla(string[] parametre)
        {
            alimrapor rep = new alimrapor();
            rep.Parameters["Parti"].Value = parametre;
            rep.CreateDocument();
            documentViewer1.DocumentSource = rep;
            documentViewer1.Refresh();
        }

        public void raporlakesimhane(string prmParti)
        {
            kesim rep = new kesim();
            rep.Parameters["parti"].Value = prmParti;
            rep.CreateDocument();
            documentViewer1.DocumentSource = rep;
            documentViewer1.Refresh();
        }

        public void raporlaparcalama(string prm1,string urun)
        {
            parcalamaRaporu rep = new parcalamaRaporu();
            rep.Parameters["tartimNo"].Value = prm1;
            if (urun == null) rep.Parameters["urunAdi"].Visible = false;
            else rep.Parameters["urunAdi"].Value = urun;
            rep.CreateDocument();
            documentViewer1.DocumentSource = rep;
            documentViewer1.Refresh();
        }

        private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}

using System.Windows.Forms;

namespace TeraziDevEx
{
    public partial class parcalamaRaporu : DevExpress.XtraReports.UI.XtraReport
    {
        public parcalamaRaporu()
        {
            InitializeComponent();
            
        }

        private void parcalamaRaporu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.Parameters["urunAdi"].Visible==false)
            {
                this.dr1.FilterString = "";
                this.dr2.FilterString = "";
            }
        }

        private void parcalamaRaporu_ParametersRequestSubmit(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            
        }
    }
}
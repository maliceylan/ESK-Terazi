using System.Data.SqlClient;
using System.Windows.Forms;

namespace TeraziDevEx
{
    public class FuncUser
    {
        SqlConnection conn = new SqlConnection(TeraziDevEx.Properties.Settings.Default.conString);
        //GLOBAL VARİABLES
        public string userlogin(string username, string password)
        {
            string durum = "";

            try
            {
                SqlCommand komut = new SqlCommand();
                komut.Connection = conn;
                komut.CommandText = "SELECT * FROM [Terazi].[dbo].[Kullanicilar] where username = '" + username + "' and password = '" + password + "'";
                conn.Open();
                SqlDataReader reader = komut.ExecuteReader();
                if (reader.Read())
                {
                    durum = "OK";
                }
                else
                {
                    durum = "NO";
                }
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return durum;
        }





    }
}

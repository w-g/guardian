using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.Model
{
    public class Memo : IDisposable
    {
        public Memo()
        { }

        public Memo(int id)
        {
            string sql = string.Format("Select * From Memo Where Id={0}", id);

            using (SqlCommand cmd = new SqlCommand(sql, DBContext))
            {
                DBContext.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            this.Id = reader.GetFieldValue<int>(0);
                            this.Product = reader.GetFieldValue<string>(1);
                            this.UserName = reader.GetFieldValue<string>(2);
                            this.Password = reader.GetFieldValue<string>(3);
                            this.Email = reader.GetFieldValue<string>(4);
                            this.Company = reader.GetFieldValue<string>(5);
                        }
                    }
                }

                this.Decode();
            }
        }

        private SqlConnection _dbContext;
        private SqlConnection DBContext 
        {
            get 
            {
                if (_dbContext == null)
                {
                    _dbContext = new SqlConnection(@"Data Source=(LocalDb)\v11.0;Initial Catalog=Memo;Integrated Security=SSPI;AttachDBFilename=E:\Guardian\Guardian\AppData\Memo.mdf");
                }
                
                return _dbContext;
            } 
        }

        public int Id { get; set; }

        public string Product { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public void Save()
        {
            this.Encode();

            string sql = string.Format("Insert Into Memo(Product, UserName, Password, Email, Company) Values('{0}', '{1}', '{2}', '{3}', '{4}')",
                this.Product, this.UserName, this.Password, this.Email, this.Company);

            using (SqlCommand cmd = new SqlCommand(sql, DBContext))
            {
                DBContext.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            this.DBContext.Close();
            this.DBContext.Dispose();
        }

        #region 加密

        private short _salt = 43;

        private void Encode()
        {
            string output = string.Empty;
            foreach (var i in this.Password)
            {
                output += (char)((int)i * _salt);
            }

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            
            this.Password = output;
        }

        private void Decode()
        {
            string output = string.Empty;
            foreach (var i in this.Password)
            {
                output += (char)((int)i / _salt);
            }

            this.Password = output;
        }
 
        #endregion
    }
}

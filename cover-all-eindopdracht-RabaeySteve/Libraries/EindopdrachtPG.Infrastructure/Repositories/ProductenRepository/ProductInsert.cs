using Ado.Data.SqlServer;
using EindopdrachtPG.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.Repositories.ProductenRepository {
    public partial class ProductInsert {
        private readonly SqlServerTable _table;

        public ProductInsert(SqlServerTable table) {
            this._table = table;
        }

        public virtual void NewRecord(Product product) {

            using (SqlCommand sqlCommand = new SqlCommand()) {
                
                sqlCommand.CommandText = "INSERT INTO Producten (Id, NederlandseNaam, WetenschappelijkeNaam, Prijs, Beschrijving) VALUES (@Id, @NederlandseNaam, @WetenschappelijkeNaam, @Prijs, @Beschrijving);";
                sqlCommand.Parameters.AddWithValue("@Id", product.Id);
                sqlCommand.Parameters.AddWithValue("@NederlandseNaam", product.NederlandseNaam);
                sqlCommand.Parameters.AddWithValue("@WetenschappelijkeNaam", product.WetenschappelijkeNaam);
                sqlCommand.Parameters.AddWithValue("@Prijs", product.Prijs);
                sqlCommand.Parameters.AddWithValue("@Beschrijving", product.Beschrijving);
                
                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }
        }
        public virtual void NewRecordIdentity(Product product) {
            using (SqlCommand sqlCommand = new SqlCommand()) {

                sqlCommand.CommandText = "SET IDENTITY_INSERT Producten ON";
                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }

            using (SqlCommand sqlCommand = new SqlCommand()) {


                sqlCommand.CommandText = "INSERT INTO Producten (Id, NederlandseNaam, WetenschappelijkeNaam, Prijs, Beschrijving) VALUES (@Id, @NederlandseNaam, @WetenschappelijkeNaam, @Prijs, @Beschrijving);";
                sqlCommand.Parameters.AddWithValue("@Id", product.Id);
                sqlCommand.Parameters.AddWithValue("@NederlandseNaam", product.NederlandseNaam);
                sqlCommand.Parameters.AddWithValue("@WetenschappelijkeNaam", product.WetenschappelijkeNaam);
                sqlCommand.Parameters.AddWithValue("@Prijs", product.Prijs);
                sqlCommand.Parameters.AddWithValue("@Beschrijving", product.Beschrijving);

                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }
            using (SqlCommand sqlCommand = new SqlCommand()) {

                sqlCommand.CommandText = "SET IDENTITY_INSERT Producten OFF"; ;
                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }
        }
    }
}

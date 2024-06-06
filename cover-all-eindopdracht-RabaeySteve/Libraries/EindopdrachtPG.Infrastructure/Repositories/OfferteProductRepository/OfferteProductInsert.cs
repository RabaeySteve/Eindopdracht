using Ado.Data.SqlServer;
using EindopdrachtPG.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.Repositories.OfferteProductRepository {
    public partial class OfferteProductInsert {
        private readonly SqlServerTable _table;

        public OfferteProductInsert(SqlServerTable table) {
            this._table = table;
        }
        
        public void NewRecord(OfferteProduct offerteProduct) {

            using (SqlCommand sqlCommand = new SqlCommand()) {
                
                sqlCommand.CommandText = "INSERT INTO OfferteProducten (OfferteId, ProductId, Aantal) VALUES (@OfferteId, @ProductId, @Aantal);";
                sqlCommand.Parameters.AddWithValue("@OfferteId", offerteProduct.OfferteId);
                sqlCommand.Parameters.AddWithValue("@ProductId", offerteProduct.ProductId);
                sqlCommand.Parameters.AddWithValue("@Aantal", offerteProduct.Aantal);
               
                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }
        }
    }
}

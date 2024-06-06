using Ado.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.Repositories.OfferteProductRepository {
    public partial class OfferteProductDelete {
        private readonly SqlServerTable _table;

        public OfferteProductDelete(SqlServerTable table) {
            this._table = table;
        }
        public void DeleteAllesByOfferteId(int offerteId) {
            
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "DELETE FROM [OfferteProducten] WHERE OfferteId=@OfferteId;";
                sqlCommand.Parameters.AddWithValue("@OfferteId", offerteId);
                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }

            
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "DELETE FROM [Offertes] WHERE Id=@OfferteId;";
                sqlCommand.Parameters.AddWithValue("@OfferteId", offerteId);
                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }
        }
        public virtual void DeleteByOfferteId(int offerteId) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "DELETE FROM [OfferteProducten] WHERE [OfferteId] = @OfferteId;";
                sqlCommand.Parameters.AddWithValue("@OfferteId", offerteId);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

    }
}

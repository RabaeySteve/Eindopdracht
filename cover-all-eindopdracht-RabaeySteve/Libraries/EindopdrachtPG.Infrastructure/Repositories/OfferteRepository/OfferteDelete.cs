using Ado.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.Repositories.OfferteRepository {
    public partial class OfferteDelete {
        private readonly SqlServerTable _table;

        public OfferteDelete(SqlServerTable table) {
            this._table = table;
        }
        public void DeleteById(int offerteId) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "DELETE FROM [Offertes] WHERE Id=@OfferteId;";
                sqlCommand.Parameters.AddWithValue("@OfferteId", offerteId);
                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }
        }
    }
}

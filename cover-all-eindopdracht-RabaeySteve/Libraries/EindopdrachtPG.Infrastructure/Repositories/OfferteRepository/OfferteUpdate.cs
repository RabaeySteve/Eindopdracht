using Ado.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.Repositories.OfferteRepository {
    public partial class OfferteUpdate {
        private readonly SqlServerTable _table;

        public OfferteUpdate(SqlServerTable table) {
            this._table = table;
        }

        private void SetSqlCommandParameter(SqlCommand sqlCommand, Offerte offerte) {
            sqlCommand.Parameters.AddWithValue("@Id", offerte.Id);
            sqlCommand.Parameters.AddWithValue("@Datum", offerte.Datum);
            sqlCommand.Parameters.AddWithValue("@KlantId", offerte.KlantId);
            sqlCommand.Parameters.AddWithValue("@Afhaal", offerte.Afhaal);
            sqlCommand.Parameters.AddWithValue("@Aanleg", offerte.Aanleg);
            sqlCommand.Parameters.AddWithValue("@TotaalPrijs", offerte.TotaalPrijs);
        }
        public void Update(Offerte offerte) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "UPDATE [Offertes] SET Datum=@Datum, KlantId=@KlantId, Afhaal=@Afhaal, Aanleg=@Aanleg, TotaalPrijs=@TotaalPrijs WHERE Id=@Id;";
                SetSqlCommandParameter(sqlCommand, offerte);

                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }
            
        }
    }
}

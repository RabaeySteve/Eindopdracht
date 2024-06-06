using Ado.Data.SqlServer;
using EindopdrachtPG.Domain;
using System;
using System.Data.SqlClient;

namespace EindopdrachtPG.Infrastructure.Repositories.OfferteRepository {
    public partial class OfferteInsert {
        private readonly SqlServerTable _table;

        public OfferteInsert(SqlServerTable table) {
            this._table = table;
        }

        public virtual void NewRecordIdentity(Offerte offerte) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SET IDENTITY_INSERT Offertes ON";
                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }

            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "INSERT INTO Offertes (Id, Datum, KlantId, Afhaal, Aanleg, TotaalPrijs) VALUES (@Id, @Datum, @KlantId, @Afhaal, @Aanleg, @TotaalPrijs)";
                sqlCommand.Parameters.AddWithValue("@Id", offerte.Id);
                sqlCommand.Parameters.AddWithValue("@Datum", offerte.Datum);
                sqlCommand.Parameters.AddWithValue("@KlantId", offerte.KlantId); 
                sqlCommand.Parameters.AddWithValue("@Afhaal", offerte.Afhaal);
                sqlCommand.Parameters.AddWithValue("@Aanleg", offerte.Aanleg);
                sqlCommand.Parameters.AddWithValue("@TotaalPrijs", offerte.TotaalPrijs);

                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }

            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SET IDENTITY_INSERT Offertes OFF";
                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }
        }

        public virtual void NewRecord(Offerte offerte) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "INSERT INTO Offertes (Datum, KlantId, Afhaal, Aanleg, TotaalPrijs) VALUES (@Datum, @KlantId, @Afhaal, @Aanleg, @TotaalPrijs)";
                sqlCommand.Parameters.AddWithValue("@Datum", offerte.Datum);
                sqlCommand.Parameters.AddWithValue("@KlantId", offerte.KlantId); 
                sqlCommand.Parameters.AddWithValue("@Afhaal", offerte.Afhaal);
                sqlCommand.Parameters.AddWithValue("@Aanleg", offerte.Aanleg);
                sqlCommand.Parameters.AddWithValue("@TotaalPrijs", offerte.TotaalPrijs);

                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }
        }
    }
}

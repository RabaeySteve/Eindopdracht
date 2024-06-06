using Ado.Data.SqlServer;
using EindopdrachtPG.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EindopdrachtPG.Infrastructure.Repositories.OfferteRepository {
    public partial class OfferteQuery {
        private readonly SqlServerTable _table;

        public OfferteQuery(SqlServerTable table) {
            this._table = table;
        }
        private List<Offerte> ToList(SqlCommand sqlCommand) {
            var dt = _table.DbAccess.GetDataTable(sqlCommand);
            var list = new List<Offerte>();
            foreach (DataRow dataRow in dt.Rows) {
                var offerte = new Offerte {
                    Id = (int)dataRow["Id"],
                    Datum = (DateTime)dataRow["Datum"],
                    KlantId = (int)dataRow["KlantId"],
                    Afhaal = (bool)dataRow["Afhaal"],
                    Aanleg = (bool)dataRow["Aanleg"],
                    TotaalPrijs = (decimal)dataRow["TotaalPrijs"]
                };
                list.Add(offerte);
            }
            return list;
        }

        public List<Offerte> GetById(int id) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SELECT [Id], [Datum], [KlantId], [Afhaal], [Aanleg], [TotaalPrijs] FROM [Offertes] WHERE [Id] = @Id;";
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return ToList(sqlCommand);
            }
        }
        public List<Offerte> GetLastOfferte() {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = @"
                SELECT TOP 1 [Id], [Datum], [KlantId], [Afhaal], [Aanleg], [TotaalPrijs] 
                FROM [Offertes] 
                ORDER BY [Id] DESC;";

                return ToList(sqlCommand);
            }
        }


        public List<Offerte> GetByDatum(DateTime datum) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SELECT [Id], [Datum], [KlantId], [Afhaal], [Aanleg], [TotaalPrijs] FROM [Offertes] WHERE [Datum] = @Datum;";
                sqlCommand.Parameters.AddWithValue("@Datum", datum);
                return ToList(sqlCommand);
            }
        }


        public List<Offerte> GetAll() {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SELECT [Id], [Datum], [KlantId], [Afhaal], [Aanleg], [TotaalPrijs] FROM [Offertes];";
                return ToList(sqlCommand);
            }
        }
        public List<Offerte> GetAllByKlantId(int klantId) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SELECT [Id], [Datum], [KlantId], [Afhaal], [Aanleg], [TotaalPrijs] FROM [Offertes] WHERE [KlantId] = @KlantId;";
                sqlCommand.Parameters.AddWithValue("@KlantId", klantId);
                return ToList(sqlCommand);
            }
        }


    }
}

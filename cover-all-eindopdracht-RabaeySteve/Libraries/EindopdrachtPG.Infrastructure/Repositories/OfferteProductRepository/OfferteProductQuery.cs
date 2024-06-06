using Ado.Data.SqlServer;
using EindopdrachtPG.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.Repositories.OfferteProductRepository {
    public partial class OfferteProductQuery {
        private readonly SqlServerTable _table;

        public OfferteProductQuery(SqlServerTable table) {
            this._table = table;
        }


        public List<OfferteProduct> GetByOfferteId(int offerteId) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SELECT [OfferteId], [ProductId], [Aantal] FROM [OfferteProducten] WHERE [OfferteId] = @OfferteId";
                sqlCommand.Parameters.AddWithValue("@OfferteId", offerteId);
                return ToList(sqlCommand);
            }
        }

        private List<OfferteProduct> ToList(SqlCommand sqlCommand) {
            var dt = _table.DbAccess.GetDataTable(sqlCommand);
            var list = new List<OfferteProduct>();
            foreach (DataRow dataRow in dt.Rows) {
                var offerteProduct = new OfferteProduct {
                    OfferteId = (int)dataRow["OfferteId"],
                    ProductId = (int)dataRow["ProductId"],
                    Aantal = (int)dataRow["Aantal"]
                };
                list.Add(offerteProduct);
            }
            return list;
        }
    }
}

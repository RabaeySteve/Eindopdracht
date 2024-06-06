using Ado.Data.SqlServer;
using EindopdrachtPG.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.Repositories.ProductenRepository {
    public partial class ProductQuery {
        private readonly SqlServerTable _table;

        public ProductQuery(SqlServerTable table) {
            this._table = table;
        }
        private List<Product> ToList(SqlCommand sqlCommand) {
            var dt = _table.DbAccess.GetDataTable(sqlCommand);
            List<Product> list = new List<Product>();
            foreach (DataRow dataRow in dt.Rows) {
                Product product = new Product {
                    Id = (int)dataRow["Id"],
                    NederlandseNaam = (string)dataRow["NederlandseNaam"],
                    WetenschappelijkeNaam = (string)dataRow["WetenschappelijkeNaam"],
                    Beschrijving = (string)dataRow["Beschrijving"],
                    Prijs = (decimal)dataRow["Prijs"]
                };
                list.Add(product);
            }
            return list;
        }
        public virtual List<Product> All() {
            using (SqlCommand sqlCommand = new()) {
                sqlCommand.CommandText = "SELECT [Id], [NederlandseNaam], [WetenschappelijkeNaam], [Beschrijving], [Prijs] FROM [Producten];";
                return ToList(sqlCommand);
            }
        }

        public List<Product> GetProductById(int productId) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SELECT [Id], [NederlandseNaam], [WetenschappelijkeNaam], [Beschrijving], [Prijs] FROM [Producten] WHERE [Id] = @productId;";
                sqlCommand.Parameters.AddWithValue("@productId", productId);
                return ToList(sqlCommand);
            }
        }

        public virtual List<Product> ZoekProducten(string zoekterm) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SELECT * FROM Producten WHERE LOWER(WetenschappelijkeNaam) LIKE @zoekterm";
                sqlCommand.Parameters.AddWithValue("@zoekterm", "%" + zoekterm + "%");
                return ToList(sqlCommand);
            }
        }
        public virtual List<Product> OpWetenschappelijkeNaam(string zoekterm) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SELECT Id, NederlandseNaam, WetenschappelijkeNaam, Beschrijving, Prijs FROM Producten WHERE LOWER(WetenschappelijkeNaam) LIKE @zoekterm";
                sqlCommand.Parameters.AddWithValue("@zoekterm", "%" + zoekterm + "%");
                return ToList(sqlCommand);
            }
        }
        public decimal GetProductPrijs(int productId) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SELECT Prijs FROM Producten WHERE Id = @Id";
                sqlCommand.Parameters.AddWithValue("@Id", productId);

                var dt = _table.DbAccess.GetDataTable(sqlCommand);
                if (dt.Rows.Count == 1) {
                    return (decimal)dt.Rows[0]["Prijs"];
                }
            }
            return 0;
        }


    }
}

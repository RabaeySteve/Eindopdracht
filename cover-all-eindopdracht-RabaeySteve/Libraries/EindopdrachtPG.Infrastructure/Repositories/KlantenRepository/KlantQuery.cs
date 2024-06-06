using Ado.Data.SqlServer;
using EindopdrachtPG.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.Repositories.KlantenRepository {
    public partial class KlantQuery {
        private readonly SqlServerTable _table;

        public KlantQuery(SqlServerTable table) {
            this._table = table;
        }

        private Dictionary<int, Klant> ToDictionary(SqlCommand sqlCommand) {
            var dt = _table.DbAccess.GetDataTable(sqlCommand);
            Dictionary<int, Klant> dictionary = new Dictionary<int, Klant>();
            foreach (DataRow dataRow in dt.Rows) {
                Klant klant = new() {
                    Id = (int)dataRow["Id"],
                    Naam = (string)(dataRow["Naam"]),
                    Adres = (string)(dataRow["Adres"]),
                };
                dictionary[klant.Id] = klant;
            }
            return dictionary;
        }

        public virtual Dictionary<int, Klant> All() {
            using (SqlCommand sqlCommand = new()) {
                sqlCommand.CommandText = "SELECT [Id], [Naam], [Adres] FROM [Klanten];";
                return ToDictionary(sqlCommand);
            }
        }
        public virtual List<Klant> AllList() {
            using (SqlCommand sqlCommand = new()) {
                sqlCommand.CommandText = "SELECT [Id], [Naam], [Adres] FROM [Klanten];";
                return ToList(sqlCommand);
            }
        }

        public List<Klant> ZoekKlanten(string zoekterm) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SELECT * FROM Klanten WHERE LOWER(Naam) LIKE @zoekterm";
                sqlCommand.Parameters.AddWithValue("@zoekterm", "%" + zoekterm.ToLower() + "%");
                return ToList(sqlCommand);
            }
        }

        public virtual Dictionary<int, Klant> ByName(string naam) {
            using (SqlCommand sqlCommand = new()) {
                sqlCommand.CommandText = "SELECT * FROM Klanten WHERE Naam = @Naam";
                sqlCommand.Parameters.AddWithValue("@Naam", naam);
                return ToDictionary(sqlCommand);
            }
        }

        public Klant GetById(int id) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = "SELECT [Id], [Naam], [Adres] FROM [Klanten] WHERE [Id] = @Id;";
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var klanten = ToList(sqlCommand);
                return klanten.FirstOrDefault(); // Return the first or default Klant from the list
            }
        }
        private List<Klant> ToList(SqlCommand sqlCommand) {
            var dt = _table.DbAccess.GetDataTable(sqlCommand);
            List<Klant> list = new List<Klant>();
            foreach (DataRow dataRow in dt.Rows) {
                Klant klant = new() {
                    Id = (int)dataRow["Id"],
                    Naam = (string)(dataRow["Naam"]),
                    Adres = (string)(dataRow["Adres"]),
                };
                list.Add(klant);
            }
            return list;
        }
    }
}

using Ado.Data.SqlServer;
using EindopdrachtPG.Domain;

using System.Data.SqlClient;


namespace EindopdrachtPG.Infrastructure.Repositories.KlantenRepository {
    public partial class KlantInsert {
        
        private readonly SqlServerTable _table;

        public KlantInsert(SqlServerTable table) {
            this._table = table;
        }
        public virtual void NewRecordIdentity(Klant klant) {
            using (SqlCommand sqlCommand = new SqlCommand()) {

                sqlCommand.CommandText = "SET IDENTITY_INSERT Klanten ON";
                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }

            using (SqlCommand sqlCommand = new SqlCommand()) {
                
                sqlCommand.CommandText = "INSERT INTO Klanten (Id, Naam, Adres) VALUES (@Id, @Naam, @Adres);";
                sqlCommand.Parameters.AddWithValue("@Id", klant.Id);
                sqlCommand.Parameters.AddWithValue("@Naam", klant.Naam);
                sqlCommand.Parameters.AddWithValue("@Adres", klant.Adres);
                
                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }
            using (SqlCommand sqlCommand = new SqlCommand()) {

                sqlCommand.CommandText = "SET IDENTITY_INSERT Klanten OFF";;
                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }
        }
        public virtual void NewRecord(Klant klant) {

            using (SqlCommand sqlCommand = new SqlCommand()) {

                sqlCommand.CommandText = "INSERT INTO Klanten (Id, Naam, Adres) VALUES (@Id, @Naam, @Adres);";
                sqlCommand.Parameters.AddWithValue("@Id", klant.Id);
                sqlCommand.Parameters.AddWithValue("@Naam", klant.Naam);
                sqlCommand.Parameters.AddWithValue("@Adres", klant.Adres);

                _table.DbAccess.Commands.Add(sqlCommand);
                _table.DbAccess.SaveChanges();
            }
        }





    }
}

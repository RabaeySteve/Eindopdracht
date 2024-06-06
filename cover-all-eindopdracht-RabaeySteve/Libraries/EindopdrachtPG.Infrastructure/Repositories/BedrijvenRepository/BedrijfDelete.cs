using Ado.Data.SqlServer;
using System.Data.SqlClient;

/* OPGELET: wil je soft delete met IsDeleted, dan moeten onderstaande queries aangepast worden */

namespace EindopdrachtPG.Infrastructure.BedrijvenRepository
{
    public partial class BedrijfDelete
    {
        private readonly SqlServerTable _table;

        public BedrijfDelete(SqlServerTable table)
        {
            this._table = table;
        }

        public virtual void ByPrimaryKey(int bedrijfid)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "DELETE [Bedrijven] WHERE BedrijfID=@BedrijfID;";
                sqlCommand.Parameters.AddWithValue("@BedrijfID", bedrijfid);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByBedrijfID(int bedrijfid)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "DELETE [Bedrijven] WHERE BedrijfID=@BedrijfID;";
                sqlCommand.Parameters.AddWithValue("@BedrijfID", bedrijfid);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByNaam(string naam)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "DELETE [Bedrijven] WHERE Naam=@Naam;";
                sqlCommand.Parameters.AddWithValue("@Naam", naam ?? (object)DBNull.Value);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByBTWNummer(string btwnummer)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "DELETE [Bedrijven] WHERE BTWNummer=@BTWNummer;";
                sqlCommand.Parameters.AddWithValue("@BTWNummer", btwnummer ?? (object)DBNull.Value);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByAdres(string adres)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "DELETE [Bedrijven] WHERE Adres=@Adres;";
                sqlCommand.Parameters.AddWithValue("@Adres", adres ?? (object)DBNull.Value);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }
        public virtual void ByTelefoon(string telefoon)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "DELETE [Bedrijven] WHERE Telefoon=@Telefoon;";
                sqlCommand.Parameters.AddWithValue("@Telefoon", telefoon ?? (object)DBNull.Value);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByEmail(string email)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "DELETE [Bedrijven] WHERE Email=@Email;";
                sqlCommand.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }
        public virtual void ByIsDeleted(Nullable<bool> isdeleted)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "DELETE [Bedrijven] WHERE IsDeleted=@IsDeleted;";
                sqlCommand.Parameters.AddWithValue("@IsDeleted", isdeleted ?? (object)DBNull.Value);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }
    }}
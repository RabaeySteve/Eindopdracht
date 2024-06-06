using Ado.Data.SqlServer;
using EindopdrachtPG.Domain;
using System.Data.SqlClient;

namespace EindopdrachtPG.Infrastructure.BedrijvenRepository
{
    public partial class BedrijfUpdate
    {
        private readonly SqlServerTable _table;

        public BedrijfUpdate(SqlServerTable table)
        {
            this._table = table;
        }

        private void SetSqlCommandParameter(SqlCommand sqlCommand, Bedrijf bedrijven)
        {
            sqlCommand.Parameters.AddWithValue("@BedrijfID", bedrijven.BedrijfID);
            sqlCommand.Parameters.AddWithValue("@Naam", bedrijven.Naam ?? (object)DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@BTWNummer", bedrijven.BTWNummer ?? (object)DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@Adres", bedrijven.Adres ?? (object)DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@Telefoon", bedrijven.Telefoon ?? (object)DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@Email", bedrijven.Email ?? (object)DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@IsDeleted", bedrijven.IsDeleted ?? (object)DBNull.Value);
        }

        public virtual void ByPrimaryKey(Bedrijf bedrijven)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET Naam=@Naam, BTWNummer=@BTWNummer, Adres=@Adres, Telefoon=@Telefoon, Email=@Email, IsDeleted=@IsDeleted WHERE BedrijfID=@BedrijfID;";
                SetSqlCommandParameter(sqlCommand, bedrijven);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByBedrijfID(Bedrijf bedrijven)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET Naam=@Naam, BTWNummer=@BTWNummer, Adres=@Adres, Telefoon=@Telefoon, Email=@Email, IsDeleted=@IsDeleted WHERE BedrijfID=@BedrijfID;";
                SetSqlCommandParameter(sqlCommand, bedrijven);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByNaam(Bedrijf bedrijven)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET BTWNummer=@BTWNummer, Adres=@Adres, Telefoon=@Telefoon, Email=@Email, IsDeleted=@IsDeleted WHERE Naam=@Naam;";
                SetSqlCommandParameter(sqlCommand, bedrijven);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByBTWNummer(Bedrijf bedrijven)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET Naam=@Naam, Adres=@Adres, Telefoon=@Telefoon, Email=@Email, IsDeleted=@IsDeleted WHERE BTWNummer=@BTWNummer;";
                SetSqlCommandParameter(sqlCommand, bedrijven);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByAdres(Bedrijf bedrijven)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET Naam=@Naam, BTWNummer=@BTWNummer, Telefoon=@Telefoon, Email=@Email, IsDeleted=@IsDeleted WHERE Adres=@Adres;";
                SetSqlCommandParameter(sqlCommand, bedrijven);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByTelefoon(Bedrijf bedrijven)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET Naam=@Naam, BTWNummer=@BTWNummer, Adres=@Adres, Email=@Email, IsDeleted=@IsDeleted WHERE Telefoon=@Telefoon;";
                SetSqlCommandParameter(sqlCommand, bedrijven);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByEmail(Bedrijf bedrijven)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET Naam=@Naam, BTWNummer=@BTWNummer, Adres=@Adres, Telefoon=@Telefoon, IsDeleted=@IsDeleted WHERE Email=@Email;";
                SetSqlCommandParameter(sqlCommand, bedrijven);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void ByIsDeleted(Bedrijf bedrijven)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET Naam=@Naam, BTWNummer=@BTWNummer, Adres=@Adres, Telefoon=@Telefoon, Email=@Email WHERE IsDeleted=@IsDeleted;";
                SetSqlCommandParameter(sqlCommand, bedrijven);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void NaamByPrimaryKey(string naam, int bedrijfid)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET Naam=@Naam WHERE BedrijfID=@BedrijfID;";
                sqlCommand.Parameters.AddWithValue("@Naam", naam ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@BedrijfID", bedrijfid);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void BTWNummerByPrimaryKey(string btwnummer, int bedrijfid)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET BTWNummer=@BTWNummer WHERE BedrijfID=@BedrijfID;";
                sqlCommand.Parameters.AddWithValue("@BTWNummer", btwnummer ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@BedrijfID", bedrijfid);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void AdresByPrimaryKey(string adres, int bedrijfid)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET Adres=@Adres WHERE BedrijfID=@BedrijfID;";
                sqlCommand.Parameters.AddWithValue("@Adres", adres ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@BedrijfID", bedrijfid);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void TelefoonByPrimaryKey(string telefoon, int bedrijfid)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET Telefoon=@Telefoon WHERE BedrijfID=@BedrijfID;";
                sqlCommand.Parameters.AddWithValue("@Telefoon", telefoon ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@BedrijfID", bedrijfid);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void EmailByPrimaryKey(string email, int bedrijfid)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET Email=@Email WHERE BedrijfID=@BedrijfID;";
                sqlCommand.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@BedrijfID", bedrijfid);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }

        public virtual void IsDeletedByPrimaryKey(Nullable<bool> isdeleted, int bedrijfid)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "UPDATE [Bedrijven] SET IsDeleted=@IsDeleted WHERE BedrijfID=@BedrijfID;";
                sqlCommand.Parameters.AddWithValue("@IsDeleted", isdeleted ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@BedrijfID", bedrijfid);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }
    }
}

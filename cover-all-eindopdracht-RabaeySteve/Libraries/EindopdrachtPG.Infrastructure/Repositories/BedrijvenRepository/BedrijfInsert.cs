using Ado.Data.SqlServer;
using EindopdrachtPG.Domain;
using System;
using System.Data.SqlClient;

namespace EindopdrachtPG.Infrastructure.BedrijvenRepository
{
    public partial class BedrijfInsert
    {
        private readonly SqlServerTable _table;

        public BedrijfInsert(SqlServerTable table)
        {
            this._table = table;
        }

        public virtual void NewRecord(Bedrijf bedrijven)
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandText = "INSERT INTO [Bedrijven] (Naam, BTWNummer, Adres, Telefoon, Email, IsDeleted) VALUES (@Naam, @BTWNummer, @Adres, @Telefoon, @Email, @IsDeleted); SELECT SCOPE_IDENTITY() AS INT;";
                sqlCommand.Parameters.AddWithValue("@Naam", bedrijven.Naam ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@BTWNummer", bedrijven.BTWNummer ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Adres", bedrijven.Adres ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Telefoon", bedrijven.Telefoon ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Email", bedrijven.Email ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@IsDeleted", bedrijven.IsDeleted ?? (object)DBNull.Value);
                _table.DbAccess.Commands.Add(sqlCommand);
            }
        }
    }
}

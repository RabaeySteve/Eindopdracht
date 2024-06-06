using Ado.Data.SqlServer;
using EindopdrachtPG.Domain;
using System.Data;
using System.Data.SqlClient;

/* OPGELET: "soft delete" IsDeleted is nog niet in rekening gebracht */

namespace EindopdrachtPG.Infrastructure.BedrijvenRepository
{
    public partial class BedrijfQuery
    {
        private readonly SqlServerTable _table;

        public BedrijfQuery(SqlServerTable table)
        {
            this._table = table;
        }
        private List<Bedrijf> ToList(SqlCommand sqlCommand)
        {
            var dt = _table.DbAccess.GetDataTable(sqlCommand);
            List<Bedrijf> list = [];
            foreach (DataRow dataRow in dt.Rows)
            {
                Bedrijf bedrijven = new()
                {
                    BedrijfID = (int)dataRow["BedrijfID"],
                    Naam = (string?)(dataRow["Naam"] == DBNull.Value ? null : dataRow["Naam"]),
                    BTWNummer = (string?)(dataRow["BTWNummer"] == DBNull.Value ? null : dataRow["BTWNummer"]),
                    Adres = (string?)(dataRow["Adres"] == DBNull.Value ? null : dataRow["Adres"]),
                    Telefoon = (string?)(dataRow["Telefoon"] == DBNull.Value ? null : dataRow["Telefoon"]),
                    Email = (string?)(dataRow["Email"] == DBNull.Value ? null : dataRow["Email"]),
                    IsDeleted = (Nullable<bool>)(dataRow["IsDeleted"] == DBNull.Value ? null : dataRow["IsDeleted"])
                };
                list.Add(bedrijven);
            }
            return list;
        }

        public List<Bedrijf> ToList(string sql)
        {
            return ToList(new SqlCommand(sql));
        }

        public virtual int Count()
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT COUNT(*) FROM [Bedrijven];";
                return Convert.ToInt32(_table.DbAccess.ExecuteScalar(sqlCommand));
            }
        }

        public virtual int CountByKeyword(string keyword)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT COUNT(BedrijfID) FROM [Bedrijven] WHERE (Naam LIKE '%' + @Keyword + '%' OR BTWNummer LIKE '%' + @Keyword + '%' OR Adres LIKE '%' + @Keyword + '%' OR Telefoon LIKE '%' + @Keyword + '%' OR Email LIKE '%' + @Keyword + '%');";
                sqlCommand.Parameters.AddWithValue("@Keyword", keyword);
                return Convert.ToInt32(_table.DbAccess.ExecuteScalar(sqlCommand));
            }
        }

        public virtual List<Bedrijf> All()
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [BedrijfID], [Naam], [BTWNummer], [Adres], [Telefoon], [Email], [IsDeleted] FROM [Bedrijven];";
                return ToList(sqlCommand);
            }
        }

        public virtual List<Bedrijf> ByKeyword(string keyword, int start, int end, string orderByColumnName, string orderDirection = "ASC")
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = $"SELECT [BedrijfID], [Naam], [BTWNummer], [Adres], [Telefoon], [Email], [IsDeleted] FROM (SELECT ROW_NUMBER() OVER (ORDER BY {orderByColumnName} {orderDirection}) AS RowSequence, [BedrijfID], [Naam], [BTWNummer], [Adres], [Telefoon], [Email], [IsDeleted] FROM [Bedrijven] WHERE (Naam LIKE '%' + @Keyword + '%' OR BTWNummer LIKE '%' + @Keyword + '%' OR Adres LIKE '%' + @Keyword + '%' OR Telefoon LIKE '%' + @Keyword + '%' OR Email LIKE '%' + @Keyword + '%')) AS [Bedrijven] WHERE RowSequence BETWEEN @Start AND @End;"; 
                sqlCommand.Parameters.AddWithValue("@Keyword", keyword);
                sqlCommand.Parameters.AddWithValue("@Start", start);
                sqlCommand.Parameters.AddWithValue("@End", end);
                return ToList(sqlCommand);
            }
        }

        public virtual Bedrijf? ByPrimaryKey(int bedrijfid)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT TOP 1 [BedrijfID], [Naam], [BTWNummer], [Adres], [Telefoon], [Email], [IsDeleted] FROM [Bedrijven] WHERE BedrijfID=@BedrijfID;";
                sqlCommand.Parameters.AddWithValue("@BedrijfID", bedrijfid);
                var list = ToList(sqlCommand);
                if (list.Count > 0)
                {
                    return list[0];
                }
                return null;
            }
        }
        public virtual List<Bedrijf> ByBedrijfID(int bedrijfid)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [BedrijfID], [Naam], [BTWNummer], [Adres], [Telefoon], [Email], [IsDeleted] FROM [Bedrijven]; WHERE BedrijfID = @BedrijfID;";
                sqlCommand.Parameters.AddWithValue("@BedrijfID", bedrijfid);
                return ToList(sqlCommand);
            }
        }

        public virtual List<Bedrijf> ByNaam(string naam)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [BedrijfID], [Naam], [BTWNummer], [Adres], [Telefoon], [Email], [IsDeleted] FROM [Bedrijven]; WHERE Naam = @Naam;";
                sqlCommand.Parameters.AddWithValue("@Naam", naam);
                return ToList(sqlCommand);
            }
        }

        public virtual List<Bedrijf> ByBTWNummer(string btwnummer)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [BedrijfID], [Naam], [BTWNummer], [Adres], [Telefoon], [Email], [IsDeleted] FROM [Bedrijven]; WHERE BTWNummer = @BTWNummer;";
                sqlCommand.Parameters.AddWithValue("@BTWNummer", btwnummer);
                return ToList(sqlCommand);
            }
        }

        public virtual List<Bedrijf> ByAdres(string adres)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [BedrijfID], [Naam], [BTWNummer], [Adres], [Telefoon], [Email], [IsDeleted] FROM [Bedrijven]; WHERE Adres = @Adres;";
                sqlCommand.Parameters.AddWithValue("@Adres", adres);
                return ToList(sqlCommand);
            }
        }
        public virtual List<Bedrijf> ByTelefoon(string telefoon)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [BedrijfID], [Naam], [BTWNummer], [Adres], [Telefoon], [Email], [IsDeleted] FROM [Bedrijven]; WHERE Telefoon = @Telefoon;";
                sqlCommand.Parameters.AddWithValue("@Telefoon", telefoon);
                return ToList(sqlCommand);
            }
        }

        public virtual List<Bedrijf> ByEmail(string email)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [BedrijfID], [Naam], [BTWNummer], [Adres], [Telefoon], [Email], [IsDeleted] FROM [Bedrijven]; WHERE Email = @Email;";
                sqlCommand.Parameters.AddWithValue("@Email", email);
                return ToList(sqlCommand);
            }
        }

        public virtual List<Bedrijf> ByIsDeleted(Nullable<bool> isdeleted)
        {
            using (SqlCommand sqlCommand = new())
            {
                sqlCommand.CommandText = "SELECT [BedrijfID], [Naam], [BTWNummer], [Adres], [Telefoon], [Email], [IsDeleted] FROM [Bedrijven]; WHERE IsDeleted = @IsDeleted;";
                sqlCommand.Parameters.AddWithValue("@IsDeleted", isdeleted);
                return ToList(sqlCommand);
            }
        }
    }
}

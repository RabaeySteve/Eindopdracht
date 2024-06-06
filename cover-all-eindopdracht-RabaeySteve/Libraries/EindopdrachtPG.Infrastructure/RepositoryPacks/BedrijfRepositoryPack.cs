using Ado.Data.SqlServer;
using EindopdrachtPG.Infrastructure.BedrijvenRepository;

namespace EindopdrachtPG.Infrastructure.RepositoryPacks
{
    public partial class BedrijfRepositoryPack : SqlServerTable
    {
        public virtual BedrijfQuery Query { get; set; }
        public virtual BedrijfInsert Insert { get; set; }
        public virtual BedrijfUpdate Update { get; set; }
        public virtual BedrijfDelete Delete { get; set; }

        public BedrijfRepositoryPack(SqlServerDbAccess dbAccess) : base(dbAccess)
        {
            Query = new BedrijfQuery(this);
            Insert = new BedrijfInsert(this);
            Update = new BedrijfUpdate(this);
            Delete = new BedrijfDelete(this);
        }
    }
}

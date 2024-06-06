using Ado.Data.SqlServer;
using EindopdrachtPG.Infrastructure.BedrijvenRepository;
using EindopdrachtPG.Infrastructure.Repositories.KlantenRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.RepositoryPacks {
    public partial class KlantRepositoryPack : SqlServerTable {
        public virtual KlantQuery Query { get; set; }
        public virtual KlantInsert Insert { get; set; }
        public virtual KlantUpdate Update { get; set; }
        public virtual KlantDelete Delete { get; set; }

        public KlantRepositoryPack(SqlServerDbAccess dbAccess) : base(dbAccess) {
            Query = new KlantQuery(this);
            Insert = new KlantInsert(this);
            Update = new KlantUpdate(this);
            Delete = new KlantDelete(this);
        }
    }
}

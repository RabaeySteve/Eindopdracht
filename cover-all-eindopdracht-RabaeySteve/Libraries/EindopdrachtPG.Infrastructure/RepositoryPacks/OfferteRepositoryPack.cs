using Ado.Data.SqlServer;
using EindopdrachtPG.Infrastructure.BedrijvenRepository;
using EindopdrachtPG.Infrastructure.Repositories.OfferteRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.RepositoryPacks {
    public partial class OfferteRepositoryPack : SqlServerTable {
        public virtual OfferteQuery Query { get; set; }
        public virtual OfferteInsert Insert { get; set; }
        public virtual OfferteUpdate Update { get; set; }
        public virtual OfferteDelete Delete { get; set; }

        public OfferteRepositoryPack(SqlServerDbAccess dbAccess) : base(dbAccess) {
            Query = new OfferteQuery(this);
            Insert = new OfferteInsert(this);
            Update = new OfferteUpdate(this);
            Delete = new OfferteDelete(this);
        }
    }
}

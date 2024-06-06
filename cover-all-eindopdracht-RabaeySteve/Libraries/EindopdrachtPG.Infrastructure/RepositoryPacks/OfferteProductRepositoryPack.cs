using Ado.Data.SqlServer;
using EindopdrachtPG.Infrastructure.Repositories.OfferteProductRepository;
using EindopdrachtPG.Infrastructure.Repositories.ProductenRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.RepositoryPacks {
    public partial class OfferteProductRepositoryPack : SqlServerTable {
        public virtual OfferteProductQuery Query { get; set; }
        public virtual OfferteProductInsert Insert { get; set; }
        public virtual OfferteProductUpdate Update { get; set; }
        public virtual OfferteProductDelete Delete { get; set; }

        public OfferteProductRepositoryPack(SqlServerDbAccess dbAccess) : base(dbAccess) {
            Query = new OfferteProductQuery(this);
            Insert = new OfferteProductInsert(this);
            Update = new OfferteProductUpdate(this);
            Delete = new OfferteProductDelete(this);
        }
    }
}

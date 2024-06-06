using Ado.Data.SqlServer;
using EindopdrachtPG.Infrastructure.BedrijvenRepository;
using EindopdrachtPG.Infrastructure.Repositories.ProductenRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.RepositoryPacks {
    public partial class ProductRepositoryPack : SqlServerTable {
        public virtual ProductQuery Query { get; set; }
        public virtual ProductInsert Insert { get; set; }
        public virtual ProductUpdate Update { get; set; }
        public virtual ProductDelete Delete { get; set; }

        public ProductRepositoryPack(SqlServerDbAccess dbAccess) : base(dbAccess) {
            Query = new ProductQuery(this);
            Insert = new ProductInsert(this);
            Update = new ProductUpdate(this);
            Delete = new ProductDelete(this);
        }
    }
}

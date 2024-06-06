using Ado.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.Repositories.ProductenRepository {
    public partial class ProductDelete {
        private readonly SqlServerTable _table;

        public ProductDelete(SqlServerTable table) {
            this._table = table;
        }
    }
}

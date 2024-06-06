using Ado.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.Repositories.OfferteProductRepository {
    public partial class OfferteProductUpdate {
        private readonly SqlServerTable _table;

        public OfferteProductUpdate(SqlServerTable table) {
            this._table = table;
        }
    }
}

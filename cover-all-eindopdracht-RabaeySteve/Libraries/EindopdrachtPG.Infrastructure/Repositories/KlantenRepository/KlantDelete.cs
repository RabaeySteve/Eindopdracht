using Ado.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Infrastructure.Repositories.KlantenRepository {
    public partial class KlantDelete {
        private readonly SqlServerTable _table;

        public KlantDelete(SqlServerTable table) {
            this._table = table;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindopdrachtPG.Domain {
    #region Properties
    public partial class OfferteProduct {
        public int OfferteId { get; set; }
        public int ProductId { get; set; }
        public int Aantal { get; set; }
    }
    #endregion
}

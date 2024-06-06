using EindopdrachtPG.Domain;
using System.Collections.Generic;
using System.Linq;

public class Offerte {
    #region Properties
    public int Id { get; set; }
    public DateTime Datum { get; set; }
    public int KlantId { get; set; }
    public bool Afhaal { get; set; }
    public bool Aanleg { get; set; }
    public decimal TotaalPrijs { get; set; }
    public List<OfferteProduct> OfferteProducten { get; set; }

    #endregion

    #region Ctor
    public Offerte() {
        OfferteProducten = new List<OfferteProduct>();
    }
    #endregion

    public void BerekenTotalePrijs(Func<int, decimal> getProductPrijs) {
        decimal productKost = OfferteProducten.Sum(op => getProductPrijs(op.ProductId) * op.Aantal);

        
        if (productKost > 5000) {
            productKost *= 0.90m; 
        } else if (productKost > 2000) {
            productKost *= 0.95m; 
        }

        TotaalPrijs = productKost;

       
        if (!Afhaal) {
            if (productKost < 500) {
                TotaalPrijs += 100; 
            } else if (productKost < 1000) {
                TotaalPrijs += 50; 
            }
        }

        
        if (Aanleg) {
            if (productKost > 5000) {
                TotaalPrijs += productKost * 0.05m; 
            } else if (productKost > 2000) {
                TotaalPrijs += productKost * 0.10m; 
            } else {
                TotaalPrijs += productKost * 0.15m; 
            }
        }
    }
}

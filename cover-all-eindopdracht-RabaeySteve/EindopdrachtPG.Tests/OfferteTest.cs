using System;
using System.Collections.Generic;
using Xunit;
using EindopdrachtPG.Domain;
namespace EindopdrachtPG.Tests {
    public class OfferteTests {
        private decimal GetProductPrijs(int productId) {
            return productId switch {
                1 => 1000,
                2 => 1500,
                3 => 2000,
                4 => 300,
                5 => 600,
               
            };
        }

        [Fact]
        public void BerekenTotalePrijs_NoDiscount_NoDelivery() {
            var offerte = new Offerte {
                OfferteProducten = new List<OfferteProduct>
                {
                new OfferteProduct { ProductId = 4, Aantal = 1 }
            },
                Afhaal = true
            };

            offerte.BerekenTotalePrijs(GetProductPrijs);

            Assert.Equal(300, offerte.TotaalPrijs);
        }

        [Fact]
        public void BerekenTotalePrijs_NoDiscount_DeliveryUnder500() {
            var offerte = new Offerte {
                OfferteProducten = new List<OfferteProduct>
                {
                new OfferteProduct { ProductId = 4, Aantal = 1 }
            },
                Afhaal = false
            };

            offerte.BerekenTotalePrijs(GetProductPrijs);

            Assert.Equal(400, offerte.TotaalPrijs);
        }

        [Fact]
        public void BerekenTotalePrijs_NoDiscount_DeliveryBetween500And1000() {
            var offerte = new Offerte {
                OfferteProducten = new List<OfferteProduct>
                {
                new OfferteProduct { ProductId = 5, Aantal = 1 }
            },
                Afhaal = false
            };

            offerte.BerekenTotalePrijs(GetProductPrijs);

            Assert.Equal(650, offerte.TotaalPrijs);
        }

        [Fact]
        public void BerekenTotalePrijs_5PercentDiscount_NoDelivery() {
            var offerte = new Offerte {
                OfferteProducten = new List<OfferteProduct>
                {
                new OfferteProduct { ProductId = 1, Aantal = 2 }
            },
                Afhaal = true
            };

            offerte.BerekenTotalePrijs(GetProductPrijs);

            Assert.Equal(2000, offerte.TotaalPrijs);
        }

        [Fact]
        public void BerekenTotalePrijs_5PercentDiscount_WithDelivery() {
            var offerte = new Offerte {
                OfferteProducten = new List<OfferteProduct>
                {
                new OfferteProduct { ProductId = 1, Aantal = 3 }
            },
                Afhaal = false
            };

            offerte.BerekenTotalePrijs(GetProductPrijs);

            Assert.Equal(2850, offerte.TotaalPrijs);
        }

        [Fact]
        public void BerekenTotalePrijs_10PercentDiscount_NoDelivery() {
            var offerte = new Offerte {
                OfferteProducten = new List<OfferteProduct>
                {
                new OfferteProduct { ProductId = 3, Aantal = 3 }
            },
                Afhaal = true
            };

            offerte.BerekenTotalePrijs(GetProductPrijs);

            Assert.Equal(5400, offerte.TotaalPrijs);
        }

        [Fact]
        public void BerekenTotalePrijs_10PercentDiscount_WithDelivery() {
            var offerte = new Offerte {
                OfferteProducten = new List<OfferteProduct>
                {
                new OfferteProduct { ProductId = 3, Aantal = 3 }
            },
                Afhaal = false
            };

            offerte.BerekenTotalePrijs(GetProductPrijs);

            Assert.Equal(5400, offerte.TotaalPrijs);
        }

        [Fact]
        public void BerekenTotalePrijs_AddsAnlegCost_LessThan2000() {
            var offerte = new Offerte {
                OfferteProducten = new List<OfferteProduct>
                {
                new OfferteProduct { ProductId = 5, Aantal = 1 }
            },
                Afhaal = false,
                Aanleg = true
            };

            offerte.BerekenTotalePrijs(GetProductPrijs);

            Assert.Equal(740, offerte.TotaalPrijs);
        }

        [Fact]
        public void BerekenTotalePrijs_AddsAnlegCost_MoreThan2000() {
            var offerte = new Offerte {
                OfferteProducten = new List<OfferteProduct>
                {
                new OfferteProduct { ProductId = 1, Aantal = 3 }
            },
                Afhaal = false,
                Aanleg = true
            };

            offerte.BerekenTotalePrijs(GetProductPrijs);

            Assert.Equal(3135, offerte.TotaalPrijs);
        }

        [Fact]
        public void BerekenTotalePrijs_AddsAnlegCost_MoreThan5000() {
            var offerte = new Offerte {
                OfferteProducten = new List<OfferteProduct>
                {
                new OfferteProduct { ProductId = 3, Aantal = 3 }
            },
                Afhaal = false,
                Aanleg = true
            };

            offerte.BerekenTotalePrijs(GetProductPrijs);

            Assert.Equal(5670, offerte.TotaalPrijs);
        }
    }
}

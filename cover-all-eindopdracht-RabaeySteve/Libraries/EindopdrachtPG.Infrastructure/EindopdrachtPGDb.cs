using Ado.Data.SqlServer;
using EindopdrachtPG.Infrastructure.RepositoryPacks;

namespace EindopdrachtPG.Infrastructure
{
    public partial class EindopdrachtPGDb : SqlServerDbAccess
    {
        public virtual BedrijfRepositoryPack Bedrijven { get; set; }
        public virtual KlantRepositoryPack Klanten { get; set; }
        public virtual OfferteRepositoryPack Offerte { get; set; }
        public virtual ProductRepositoryPack Producten { get; set; }

        public virtual OfferteProductRepositoryPack OfferteProduct { get; set; }

        public EindopdrachtPGDb(string? connectionString) : base(new ConnectionStringBuilder { ConnectionString = connectionString })
        {
            Bedrijven = new BedrijfRepositoryPack(this);
            Klanten = new KlantRepositoryPack(this);
            Offerte = new OfferteRepositoryPack(this);
            Producten = new ProductRepositoryPack(this);
            OfferteProduct = new OfferteProductRepositoryPack(this);
        }
    }
}

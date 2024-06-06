namespace EindopdrachtPG.Domain
{

    public class Bedrijf
    {
        #region Properties
        public int BedrijfID { get; set; }
        public string? Naam { get; set; }
        public string? BTWNummer { get; set; }
        public string? Adres { get; set; }
        public string? Telefoon { get; set; }
        public string? Email { get; set; }
        public bool? IsDeleted { get; set; }
        #endregion

        #region Ctor
        public Bedrijf()
        {
        }
        #endregion
    }
}

namespace pruebaTecnicaSTG.Models
{
    public class Purchase
    {
        public string name { get; set; }
        public string breed  { get; set; }
        public decimal  price { get; set; }

        public int purchaseamount { get; set; }
        public decimal discount { get; set; }
        public decimal freight { get; set; }
        public decimal totalvalue { get; set; }

    }
}

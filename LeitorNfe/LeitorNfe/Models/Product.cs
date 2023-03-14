namespace LeitorNfe.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int NotaFiscalId { get; set; }
        public int NumberItem { get; set; }
        public int CodeItem { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float UnityValue { get; set; }
        public float TotalValue { get; set; }

        public nFe NotaFiscal { get; set; }
    }
}

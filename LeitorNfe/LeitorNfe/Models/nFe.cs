namespace LeitorNfe.Models
{
    public class nFe
    {
        public int Id { get; set; }
        public string NumberNfe { get; set; }
        public string AccessKey { get; set; }
        public DateTime DateEmission { get; set; }
        public float TotalValue { get; set; }
        public int OrderNumber { get; set; }
        public string Description { get; set; }
        public int EmitId { get; set; }
        public int DestId { get; set; }

        public Person Emit { get; set; }
        public Person Dest { get; set; }
    }
}

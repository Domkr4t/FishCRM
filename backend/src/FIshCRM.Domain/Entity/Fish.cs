namespace FIshCRM.Domain.Entity
{
    public class Fish
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public float PricePerKilo { get; set; }
    }
}

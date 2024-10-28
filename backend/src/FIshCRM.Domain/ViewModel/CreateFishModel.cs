namespace FIshCRM.Domain.ViewModel
{
    public class CreateFishModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public float PricePerKilo { get; set; }

        public int FishBaseId { get; set; }
    }
}

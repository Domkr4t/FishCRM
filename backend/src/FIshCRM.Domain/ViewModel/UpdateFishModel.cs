namespace FIshCRM.Domain.ViewModel
{
    public class UpdateFishModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; } 

        public float PricePerKilo { get; set; }

        public int FishBaseId { get; set; }
    }
}

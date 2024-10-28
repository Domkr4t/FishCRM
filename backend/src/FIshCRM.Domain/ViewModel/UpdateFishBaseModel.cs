namespace FIshCRM.Domain.ViewModel
{
    public class UpdateFishBaseModel
    {
        public int Id { get; set; }

        public string? FishBaseName { get; set; } 

        public int FishBasePricePerHour { get; set; }

        public int FishBaseEntryPrice { get; set; }
    }
}

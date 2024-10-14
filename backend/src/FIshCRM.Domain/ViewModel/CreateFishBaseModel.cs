namespace FIshCRM.Domain.ViewModel
{
    public class CreateFishBaseModel
    {
        public int Id { get; set; }

        public string CompanyName { get; set; } = default!;

        public float FishBaseLatitude { get; set; }

        public float FishBaseLongitude { get; set; }

        public string FishBaseAddress { get; set; } = default!;

        public string FishBaseName { get; set; } = default!;

        public int FishBasePricePerHour { get; set; }

        public int FishBaseEntryPrice { get; set; }
    }
}

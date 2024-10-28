namespace FIshCRM.Domain.Entity
{
    public class FisherSession
    {
        public int Id { get; set; }

        public int FisherId { get; set; }

        public int FishBaseId { get; set; }

        public DateTime StartSession { get; set; }

        public DateTime? EndSession { get; set; }

        public TimeSpan? DurationSession { get; set; }
    }
}

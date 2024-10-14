using FIshCRM.Domain.Enum;

namespace FIshCRM.Domain.Response
{
    public class FishResponse<T> : IFishResponse<T>
    {
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }

    public interface IFishResponse<T>
    {
        string Description { get; }
        StatusCode StatusCode { get; }
        T Data { get; }
    }
}

using FIshCRM.Domain.Response;
using FIshCRM.Domain.ViewModel;

namespace FishCRM.Application.Interfaces
{
    public interface IFishCRMService
    {
        Task<IFishResponse<CreateFishBaseModel>> CreateFishBase(CreateFishBaseModel model);
        Task<IFishResponse<IEnumerable<FishBaseModel>>> GetAllFishBases();
    }
}

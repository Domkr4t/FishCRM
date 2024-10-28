using FIshCRM.Domain.Response;
using FIshCRM.Domain.ViewModel;
using System.Threading.Tasks;

namespace FishCRM.Application.Interfaces
{
    public interface IFishCRMService
    {
        Task<IFishResponse<CreateFishBaseModel>> CreateFishBase(CreateFishBaseModel model);
        Task<IFishResponse<IEnumerable<FishBaseModel>>> GetAllFishBases();
        Task<IFishResponse<FishBaseModel>> DeleteFishBaseById(int id);
        Task<IFishResponse<UpdateFishBaseModel>> PatchFishBase(UpdateFishBaseModel model);


        Task<IFishResponse<CreateFishModel>> CreateFish(CreateFishModel model);
        Task<IFishResponse<IEnumerable<FishModel>>> GetAllFishInBase(int FishBaseId);
        Task<IFishResponse<FishModel>> DeleteFishById(int fishBaseId, int fishId);
        Task<IFishResponse<UpdateFishModel>> PatchFishInBase(UpdateFishModel model);


        Task<IFishResponse<CreateFisherModel>> CreateFisher(CreateFisherModel model);
        Task<IFishResponse<IEnumerable<FisherModel>>> GetAllFishers();


        Task<IFishResponse<CreateFisherSessionModel>> StartSession(CreateFisherSessionModel model);
        Task<IFishResponse<IEnumerable<FisherSessionModel>>> GetAllSession();
        Task<IFishResponse<UpdateFisherSessionModel>> StopSession(UpdateFisherSessionModel model);
    }
}

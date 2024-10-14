using FishCRM.Application.Interfaces;
using FIshCRM.Domain.Entity;
using FIshCRM.Domain.Response;
using FIshCRM.Domain.ViewModel;
using FishCRM.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using FIshCRM.Domain.Enum;

namespace FishCRM.Application.Implementations
{
    public class FishCRMService : IFishCRMService
    {
        private readonly IBaseRepository<FishBase> _fishBaseRepository;

        public FishCRMService(IBaseRepository<FishBase> fishBaseRepository)
        {
            _fishBaseRepository = fishBaseRepository;
        }

        public async Task<IFishResponse<CreateFishBaseModel>> CreateFishBase(CreateFishBaseModel model)
        {
            try
            {
                var fishBase = await _fishBaseRepository.GetAll().FirstOrDefaultAsync(f =>  f.Id == model.Id);

                if (fishBase != null)
                {
                    return new FishResponse<CreateFishBaseModel>
                    {
                        Description = $"Рыблолвная база c ID {model.Id} уже существует",
                        StatusCode = StatusCode.FishBaseAlreadyExists
                    };
                }

                fishBase = new FishBase
                {
                    CompanyName = model.CompanyName,
                    FishBaseLatitude = model.FishBaseLatitude,
                    FishBaseLongitude = model.FishBaseLongitude,
                    FishBaseAddress = model.FishBaseAddress,
                    FishBaseName = model.FishBaseName,
                    FishBasePricePerHour = model.FishBasePricePerHour,
                    FishBaseEntryPrice = model.FishBaseEntryPrice,
                };

                await _fishBaseRepository.Create(fishBase);

                return new FishResponse<CreateFishBaseModel>
                {
                    Description = $"Рыблолвная база c ID {model.Id} создана",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex) 
            {
                return new FishResponse<CreateFishBaseModel>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IFishResponse<IEnumerable<FishBaseModel>>> GetAllFishBases()
        {
            try
            {
                var fishBases = await _fishBaseRepository.GetAll().Select(f => new FishBaseModel
                {
                    Id = f.Id,
                    CompanyName = f.CompanyName,
                    FishBaseLatitude = f.FishBaseLatitude,
                    FishBaseLongitude = f.FishBaseLongitude,
                    FishBaseAddress= f.FishBaseAddress,
                    FishBaseName = f.FishBaseName,
                    FishBasePricePerHour = f.FishBasePricePerHour,
                    FishBaseEntryPrice = f.FishBaseEntryPrice,
                    FishInBase = f.FishInBase
                }).ToListAsync();

                return new FishResponse<IEnumerable<FishBaseModel>>
                {
                    Data = fishBases,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new FishResponse<IEnumerable<FishBaseModel>>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}

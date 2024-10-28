using FishCRM.Application.Interfaces;
using FishCRM.Infrastructure.Interfaces;
using FIshCRM.Domain.Entity;
using FIshCRM.Domain.Enum;
using FIshCRM.Domain.Response;
using FIshCRM.Domain.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace FishCRM.Application.Implementations
{
    public class FishCRMService : IFishCRMService
    {
        private readonly IBaseRepository<FishBase> _fishBaseRepository;
        private readonly IBaseRepository<Fish> _fishRepository;
        private readonly IBaseRepository<Fisher> _fisherRepository;
        private readonly IBaseRepository<FisherSession> _fisherSessionRepository;

        public FishCRMService(IBaseRepository<FishBase> fishBaseRepository,
                              IBaseRepository<Fish> fishRepository,
                              IBaseRepository<Fisher> fisherRepository,
                              IBaseRepository<FisherSession> fisherSessionRepository)
        {
            _fishBaseRepository = fishBaseRepository;
            _fishRepository = fishRepository;
            _fisherRepository = fisherRepository;
            _fisherSessionRepository = fisherSessionRepository;
        }

        public async Task<IFishResponse<CreateFishBaseModel>> CreateFishBase(CreateFishBaseModel model)
        {
            try
            {
                var fishBase = await _fishBaseRepository.GetAll().FirstOrDefaultAsync(f => f.FishBaseName == model.FishBaseName);

                if (fishBase != null)
                {
                    return new FishResponse<CreateFishBaseModel>
                    {
                        Description = $"Рыблолвная база {model.FishBaseName} уже существует",
                        StatusCode = StatusCode.FishBaseAlreadyExists
                    };
                }

                fishBase = new FishBase
                {
                    Id = model.Id,
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
                    Description = $"Рыблолвная база {model.FishBaseName} создана",
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
                var fishBases = await _fishBaseRepository.GetAll()
                   .Select(f => new FishBaseModel
                   {
                       Id = f.Id,
                       CompanyName = f.CompanyName,
                       FishBaseLatitude = f.FishBaseLatitude,
                       FishBaseLongitude = f.FishBaseLongitude,
                       FishBaseAddress = f.FishBaseAddress,
                       FishBaseName = f.FishBaseName,
                       FishBasePricePerHour = f.FishBasePricePerHour,
                       FishBaseEntryPrice = f.FishBaseEntryPrice,
                       FishInBase = f.FishInBase.Where(fish => fish.FishBaseId == f.Id).ToList()
                   })
                   .ToListAsync();

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

        public async Task<IFishResponse<FishBaseModel>> DeleteFishBaseById(int id)
        {
            try
            {
                var fishBase = await _fishBaseRepository.GetAll().FirstOrDefaultAsync(f => f.Id == id);

                var fishInBase = await _fishRepository.GetAll().Where(x => x.FishBaseId == id).ToListAsync();

                if (fishBase == null)
                {
                    return new FishResponse<FishBaseModel>
                    {
                        Description = $"Базы с ID {id} не существует",
                        StatusCode = StatusCode.FishBaseNotFound
                    };
                }

                await _fishBaseRepository.Delete(fishBase);

                foreach (var fish in fishInBase)
                    await _fishRepository.Delete(fish);

                return new FishResponse<FishBaseModel>
                {
                    Description = $"База с ID {id} удалена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new FishResponse<FishBaseModel>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IFishResponse<UpdateFishBaseModel>> PatchFishBase(UpdateFishBaseModel model)
        {
            try
            {
                var fishBase = await _fishBaseRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);

                if (fishBase == null)
                {
                    return new FishResponse<UpdateFishBaseModel>
                    {
                        Description = $"Базы с ID {model.Id} не существует",
                        StatusCode = StatusCode.FishBaseNotFound
                    };
                }

                fishBase.FishBaseName = model.FishBaseName == null ? fishBase.FishBaseName : model.FishBaseName;
                fishBase.FishBasePricePerHour = model.FishBasePricePerHour;
                fishBase.FishBaseEntryPrice = model.FishBaseEntryPrice;

                await _fishBaseRepository.Update(fishBase);

                return new FishResponse<UpdateFishBaseModel>
                {
                    Description = $"База с ID {model.Id} изменена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch(Exception ex)
            {
                return new FishResponse<UpdateFishBaseModel>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IFishResponse<CreateFishModel>> CreateFish(CreateFishModel model)
        {
            try
            {
                var fish = await _fishRepository.GetAll()
                    .FirstOrDefaultAsync(f => f.Name == model.Name && f.FishBaseId == model.FishBaseId);

                if (fish != null)
                {
                    return new FishResponse<CreateFishModel>
                    {
                        Description = $"Рыба {model.Name} на базе с ID {model.FishBaseId} уже существует",
                        StatusCode = StatusCode.FishAlreadyExists
                    };
                }

                fish = new Fish
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description == null ? string.Empty : model.Description,
                    PricePerKilo = model.PricePerKilo,
                    FishBaseId = model.FishBaseId
                };

                await _fishRepository.Create(fish);

                return new FishResponse<CreateFishModel>
                {
                    Description = $"Рыба {model.Name} на базе с ID {model.FishBaseId} создана",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new FishResponse<CreateFishModel>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IFishResponse<IEnumerable<FishModel>>> GetAllFishInBase(int fishBaseId)
        {
            try
            {
                var fishes = await _fishRepository.GetAll().Select(x => new FishModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PricePerKilo = x.PricePerKilo,
                    FishBaseId = x.FishBaseId
                }).Where(x => x.FishBaseId == fishBaseId).ToListAsync();

                return new FishResponse<IEnumerable<FishModel>>
                {
                    Data = fishes,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new FishResponse<IEnumerable<FishModel>>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IFishResponse<FishModel>> DeleteFishById(int fishBaseId, int fishId)
        {
            try
            {
                var fish = await _fishRepository.GetAll()
                       .FirstOrDefaultAsync(x => x.Id == fishId && x.FishBaseId == fishBaseId);

                if (fish == null)
                {
                    return new FishResponse<FishModel>()
                    {
                        Description = "Такой рыбы нет",
                        StatusCode = StatusCode.FishNotFound
                    };
                }

                await _fishRepository.Delete(fish);

                return new FishResponse<FishModel>()
                {
                    Description = "Рыба удалена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                return new FishResponse<FishModel>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IFishResponse<UpdateFishModel>> PatchFishInBase(UpdateFishModel model)
        {
            try
            {
                var fish = await _fishRepository.GetAll()
                       .FirstOrDefaultAsync(x => x.Name == model.Name && x.FishBaseId == model.FishBaseId);

                if (fish == null)
                {
                    return new FishResponse<UpdateFishModel>()
                    {
                        Description = "Такой рыбы нет",
                        StatusCode = StatusCode.FishNotFound
                    };
                }

                fish.Description = model.Description == null ? fish.Description : model.Description;
                fish.PricePerKilo = model.PricePerKilo;

                await _fishRepository.Update(fish);

                return new FishResponse<UpdateFishModel>()
                {
                    Description = $"Рыба изменена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch(Exception ex)
            {
                return new FishResponse<UpdateFishModel>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IFishResponse<CreateFisherModel>> CreateFisher(CreateFisherModel model)
        {
            try
            {
                var fisher = await _fisherRepository.GetAll().FirstOrDefaultAsync(f => f.Id == model.Id);

                if (fisher != null)
                {
                    return new FishResponse<CreateFisherModel>
                    {
                        Description = $"Рыбак {model.Id} уже существует",
                        StatusCode = StatusCode.FishBaseAlreadyExists
                    };
                }

                fisher = new Fisher
                {
                    Id = model.Id,
                    Name = model.Name,
                    Telephone = model.Telephone
                };

                await _fisherRepository.Create(fisher);

                return new FishResponse<CreateFisherModel>
                {
                    Description = $"Рыбак {model.Id} создан",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new FishResponse<CreateFisherModel>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IFishResponse<IEnumerable<FisherModel>>> GetAllFishers()
        {
            try
            {
                var fishers = await _fisherRepository.GetAll()
                   .Select(f => new FisherModel
                   {
                       Id = f.Id,
                       Name = f.Name,
                       Telephone = f.Telephone
                   }).ToListAsync();

                return new FishResponse<IEnumerable<FisherModel>>
                {
                    Data = fishers,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new FishResponse<IEnumerable<FisherModel>>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IFishResponse<CreateFisherSessionModel>> StartSession(CreateFisherSessionModel model)
        {
            try
            {
                var fisherSession = await _fisherSessionRepository.GetAll().FirstOrDefaultAsync(f => f.Id == model.Id);

                var fisher = await _fisherRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.FisherId);
                var fishBase = await _fishBaseRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.FishBaseId);

                if (fisherSession != null)
                {
                    return new FishResponse<CreateFisherSessionModel>
                    {
                        Description = $"Сессия уже существует",
                        StatusCode = StatusCode.FishBaseAlreadyExists
                    };
                }
                else if (fisher == null)
                {
                    return new FishResponse<CreateFisherSessionModel>
                    {
                        Description = $"Рыбака с ID {model.FisherId} не существует",
                        StatusCode = StatusCode.FishBaseAlreadyExists
                    };
                }
                else if (fishBase == null)
                {
                    return new FishResponse<CreateFisherSessionModel>
                    {
                        Description = $"Базы с ID {model.FishBaseId} уже существует",
                        StatusCode = StatusCode.FishBaseAlreadyExists
                    };
                }



                fisherSession = new FisherSession
                {
                    Id = model.Id,
                    FisherId = model.Id,
                    FishBaseId = model.Id,
                    StartSession = DateTime.Now,
                    DurationSession = null,
                    EndSession = null
                };

                await _fisherSessionRepository.Create(fisherSession);

                return new FishResponse<CreateFisherSessionModel>
                {
                    Description = $"Сессия {model.Id} создана",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new FishResponse<CreateFisherSessionModel>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IFishResponse<IEnumerable<FisherSessionModel>>> GetAllSession()
        {
            try
            {
                var fisherSessions = await _fisherSessionRepository.GetAll()
                   .Select(f => new FisherSessionModel
                   {
                       Id = f.Id,
                       FisherId = f.FisherId,
                       FishBaseId= f.FishBaseId,
                       StartSession = f.StartSession,
                       EndSession = f.EndSession,
                       DurationSession = f.EndSession == null ? DateTime.Now - f.StartSession : f.EndSession - f.StartSession
                   }).ToListAsync();

                return new FishResponse<IEnumerable<FisherSessionModel>>
                {
                    Data = fisherSessions,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new FishResponse<IEnumerable<FisherSessionModel>>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IFishResponse<UpdateFisherSessionModel>> StopSession(UpdateFisherSessionModel model)
        {
            try
            {
                var session = await _fisherSessionRepository.GetAll()
                       .Where(x => x.EndSession == null)
                       .FirstOrDefaultAsync(x => x.FisherId == model.FisherId && x.FishBaseId == model.FishBaseId);

                if (session == null)
                {
                    return new FishResponse<UpdateFisherSessionModel>()
                    {
                        Description = "Такой сессии нет",
                        StatusCode = StatusCode.FishNotFound
                    };
                }

                session.DurationSession = null;
                session.EndSession = DateTime.Now;

                await _fisherSessionRepository.Update(session);

                return new FishResponse<UpdateFisherSessionModel>()
                {
                    Description = $"Сессия закончена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new FishResponse<UpdateFisherSessionModel>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}

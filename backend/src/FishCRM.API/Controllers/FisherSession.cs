using FishCRM.Application.Interfaces;
using FIshCRM.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FishCRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FisherSession : Controller
    {
        private readonly IFishCRMService _fishCRMService;

        public FisherSession(IFishCRMService fishCRMService)
        {
            _fishCRMService = fishCRMService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _fishCRMService.GetAllSession();
            return Json(new { data = response.Data });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFisherSessionModel model)
        {
            var response = await _fishCRMService.StartSession(model);

            if (response.StatusCode == FIshCRM.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(UpdateFisherSessionModel model)
        {
            var response = await _fishCRMService.StopSession(model);

            if (response.StatusCode == FIshCRM.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }
    }
}

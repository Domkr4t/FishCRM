using FishCRM.Application.Interfaces;
using FIshCRM.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FishCRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FishController : Controller
    {
        private readonly IFishCRMService _fishCRMService;

        public FishController(IFishCRMService fishCRMService)
        {
            _fishCRMService = fishCRMService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int fishBaseId)
        {
            var response = await _fishCRMService.GetAllFishInBase(fishBaseId);
            return Json(new { data = response.Data });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFishModel model)
        {
            var response = await _fishCRMService.CreateFish(model);

            if (response.StatusCode == FIshCRM.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int fishBaseId ,int fishId)
        {
            var response = await _fishCRMService.DeleteFishById(fishBaseId, fishId);

            if (response.StatusCode == FIshCRM.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(UpdateFishModel model)
        {
            var response = await _fishCRMService.PatchFishInBase(model);

            if (response.StatusCode == FIshCRM.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }
    }
}

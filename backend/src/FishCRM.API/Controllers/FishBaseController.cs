using FishCRM.Application.Interfaces;
using FIshCRM.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FishCRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FishBaseController : Controller
    {
        private readonly IFishCRMService _fishCRMService;

        public FishBaseController(IFishCRMService fishCRMService)
        {
            _fishCRMService = fishCRMService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFishBase([FromBody] CreateFishBaseModel model)
        {
            var response = await _fishCRMService.CreateFishBase(model);

            if (response.StatusCode == FIshCRM.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFishBases()
        {
            var response = await _fishCRMService.GetAllFishBases();
            return Json(new { data = response.Data });
        }
    }
}

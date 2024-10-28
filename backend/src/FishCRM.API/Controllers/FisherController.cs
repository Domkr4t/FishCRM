using FishCRM.Application.Interfaces;
using FIshCRM.Domain.Entity;
using FIshCRM.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FishCRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FisherController : Controller
    {
        private readonly IFishCRMService _fishCRMService;

        public FisherController(IFishCRMService fishCRMService)
        {
            _fishCRMService = fishCRMService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _fishCRMService.GetAllFishers();
            return Json(new { data = response.Data });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFisherModel model)
        {
            var response = await _fishCRMService.CreateFisher(model);

            if (response.StatusCode == FIshCRM.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }
    }
}

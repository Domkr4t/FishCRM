﻿using FishCRM.Application.Interfaces;
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
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _fishCRMService.GetAllFishBases();
            return Json(new { data = response.Data });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFishBaseModel model)
        {
            var response = await _fishCRMService.CreateFishBase(model);

            if (response.StatusCode == FIshCRM.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _fishCRMService.DeleteFishBaseById(id);

            if (response.StatusCode == FIshCRM.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(UpdateFishBaseModel model)
        {
            var response = await _fishCRMService.PatchFishBase(model);

            if (response.StatusCode == FIshCRM.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }
    }
}

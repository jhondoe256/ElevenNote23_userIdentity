using ElevenNote.Models.CategoryModels;
using ElevenNote.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Categorycontroller : ControllerBase
    {
        private readonly ICategoryService _cService;

        public Categorycontroller(ICategoryService cService)
        {
            _cService = cService;
        }

        [Authorize(Roles ="User, Administrator")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _cService.GetCategories());
        }

        [Authorize(Roles ="User, Administrator")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _cService.GetCategory(id));
        }
        
        [Authorize(Roles ="Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateVM model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await _cService.CreateCategory(model))
            {
                return Ok("Success!");
            }
            else
            return StatusCode(500,"Internal Server Error.");
        }

        [Authorize(Roles ="Administrator")]
        [HttpPut]
        public async Task<IActionResult> Put(CategoryEditVM model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await _cService.UpdateCategory(model))
            {
                return Ok("Success!");
            }
            else
                return StatusCode(500,"Internal Server Error.");
        }

        [Authorize(Roles ="Administrator")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id<=0)
            {
                return BadRequest("Invalid Id.");
            }

            if(await _cService.DeleteCategory(id))
            {
                return Ok("Success!");
            }
            else
                return StatusCode(500,"Internal Server Error.");
        }
    }
}
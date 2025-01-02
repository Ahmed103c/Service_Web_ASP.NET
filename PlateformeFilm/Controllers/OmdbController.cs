using Microsoft.AspNetCore.Mvc;
using PlateformeFilm.Models;
using PlateformeFilm.data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using PlateformeFilm.Services;

namespace PlateformeFilm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OmdbController : ControllerBase
    {
        private readonly Omdbservice _omdbservice;
        public OmdbController(Omdbservice omdbservice)
        {
            _omdbservice=omdbservice;
        }
        [HttpGet("search/{title}")]
        public async Task<IActionResult> Search(string title)
        {
            var films = await _omdbservice.SearchByTitle(title);
            return Ok(films);
        }
    }
      
}
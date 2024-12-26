using Microsoft.AspNetCore.Mvc;
using PlateformeFilm.Models;
using PlateformeFilm.data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace PlateformeFilm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly FilmContext _context;
        public FilmController(FilmContext ctx)
        {
            _context = ctx;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            // on récupère l'utilisateur correspondant a l'id
            var film = await _context.Films.FindAsync(id);

            if (film == null)
            {
                // return NotFound("Id n'est pas trouvable ! ");
                return  Unauthorized("Identifiant Incorrect");
            }
            // on retourne l'utilisateur
            return Ok(film);
        }
        public class FilmCreation
        {
            public int id{get;set;}
            public string Title{get;set;}
        }
             [HttpPost("register")]
        public async Task<ActionResult<Film>> PostFilm(FilmCreation filmCreation)
        {
            // on créer un nouveau film avec les informations reçu
            Film film = new Film {
                Id=filmCreation.id,
                Title=filmCreation.Title,
                Poster="poster",
                IMDB="imdb",
                dateDeSortie=2002
                
            };
            // on l'ajoute a notre contexte (BDD)
            _context.Films.Add(film);
            // on enregistre les modifications dans la BDD ce qui remplira le champ Id de notre objet
            await _context.SaveChangesAsync();
            // on retourne un code 201 pour indiquer que la création a bien eu lieu
            return  Ok(new {message ="Film Bien Ajouté !",FilmName = film.Title});
        }

    }
}
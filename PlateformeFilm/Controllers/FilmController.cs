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
        [HttpGet("All")]
        public async Task<ActionResult<Film>> GetFilmAll()
        {
            // on récupère l'utilisateur correspondant a l'id
            //var film = await _context.Films.FindAsync();
            List<Film> films=await _context.Films.ToListAsync();

            if (films == null)
            {
                // return NotFound("Id n'est pas trouvable ! ");
                return  Unauthorized("Identifiant Incorrect");
            }
            // on retourne l'utilisateur
            return Ok(films);
        }
        [HttpGet("Search")]
        public async Task<ActionResult<Film>> GetFilmSearch(string title)
        {
            // on récupère l'utilisateur correspondant a l'id
            //var user = await _context.Users.FirstOrDefaultAsync(u =>u.Pseudo == userInfo.Pseudo);
            var film = await _context.Films.FirstOrDefaultAsync(f=>f.Title==title);

            if (film == null)
            {
                // return NotFound("Id n'est pas trouvable ! ");
                return  Unauthorized("title Incorrect");
            }
            // on retourne l'utilisateur
            return Ok(film);
        }
        [HttpGet("Info")]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilmInfo([FromQuery] int[] ids)
        {
            var films = new List<Film>();
            foreach (var id in ids) // mieux d'utiliser a chaque fois longueur
            {
                var film = await _context.Films.FindAsync(id);
                if (film!=null)
                {
                    films.Add(film);
                }
            }
            if (!films.Any())
            {
                return NotFound("Aucun film n'est trouvé");
            }
            return Ok(films);
        }










        public class FilmCreation
        {
            public int id{get;set;}
            public string Title{get;set;}

            public string poster{get;set;}

            public string IMBD{get;set;}

            public int dateDeSortie{get;set;}
        }
        [HttpPost("register")]
        public async Task<ActionResult<Film>> PostFilm(FilmCreation filmCreation)
        {
            // on créer un nouveau film avec les informations reçu
            Film film = new Film {
                Id=filmCreation.id,
                Title=filmCreation.Title,
                Poster=filmCreation.poster,
                IMDB=filmCreation.IMBD,
                dateDeSortie=filmCreation.dateDeSortie
                
            };
            // on l'ajoute a notre contexte (BDD)
            _context.Films.Add(film);
            // on enregistre les modifications dans la BDD ce qui remplira le champ Id de notre objet
            await _context.SaveChangesAsync();
            // on retourne un code 201 pour indiquer que la création a bien eu lieu
            return  Ok(new {message ="Film Bien Ajouté !",FilmName = film.Title});
        }
                //Supprimer les Données 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            // on récupère la film que l'on souhaite supprimer
            Film film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            // on indique a notre contexte que l'objet a été supprimé
            _context.Films.Remove(film);
            // on enregistre les modifications
            await _context.SaveChangesAsync();
            // on retourne un code 204 pour indiquer que la suppression a bien eu lieu
            return NoContent();
        }

    }
}
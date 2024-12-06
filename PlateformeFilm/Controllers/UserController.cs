using Microsoft.AspNetCore.Mvc;
using PlateformeFilm.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlateformeFilm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Simule une base de données en mémoire
        private static List<Utilisateur> utilisateurs = new List<Utilisateur>
        {
            new Utilisateur { Pseudo = "Admin", MotDePasse = "admin123", Role = Role.Admin },
            new Utilisateur { Pseudo = "User1", MotDePasse = "password", Role = Role.User }
        };

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<Utilisateur> Get(int id)
        {
            if (id < 0 || id >= utilisateurs.Count)
                return NotFound("Utilisateur non trouvé.");

            return utilisateurs[id];
        }

        // POST: api/user/register
        [HttpPost("register")]
        public ActionResult<Utilisateur> Register([FromBody] Utilisateur nouvelUtilisateur)
        {
            if (utilisateurs.Any(u => u.Pseudo == nouvelUtilisateur.Pseudo))
                return BadRequest("Un utilisateur avec ce pseudo existe déjà.");

            utilisateurs.Add(nouvelUtilisateur);
            return CreatedAtAction(nameof(Get), new { id = utilisateurs.Count - 1 }, nouvelUtilisateur);
        }

        // POST: api/user/login
        [HttpPost("login")]
        public ActionResult<Utilisateur> Login([FromBody] Utilisateur credentials)
        {
            var utilisateur = utilisateurs.FirstOrDefault(u => u.Pseudo == credentials.Pseudo && u.MotDePasse == credentials.MotDePasse);

            if (utilisateur == null)
                return Unauthorized("Pseudo ou mot de passe incorrect.");

            return Ok(utilisateur);
        }
    }
}

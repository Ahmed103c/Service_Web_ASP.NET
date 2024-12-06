using Microsoft.AspNetCore.Mvc;
using PlateformeFilm.Models;
using PlateformeFilm.data;
using System.Collections.Generic;
using System.Linq;

namespace PlateformeFilm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

    private readonly UserContext _context;
    public UserController(UserContext ctx)
    {
        _context = ctx;
    }
    //Récupérer des données 
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        // on récupère la confiture correspondant a l'id
        var user = await _context.Users.FindAsync(id);

        if (User == null)
        {
            return NotFound();
        }
        // on retourne la confiture
        return Ok(user);
    }
    //Ajouter des données 
    public class UserCreation
    {
        public int id { get; set; }
        public string Pseudo { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(UserCreation userCreation)
    {
        // on créer un nouveau user avec les informations reçu
        User user = new User {
            Id=userCreation.id,
            Pseudo=userCreation.Pseudo,
            Password=userCreation.Password,
            Role=userCreation.Role
        };
        // on l'ajoute a notre contexte (BDD)
        _context.Users.Add(user);
        // on enregistre les modifications dans la BDD ce qui remplira le champ Id de notre objet
        await _context.SaveChangesAsync();
        // on retourne un code 201 pour indiquer que la création a bien eu lieu
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
    }
}

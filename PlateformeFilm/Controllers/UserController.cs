using Microsoft.AspNetCore.Mvc;
using PlateformeFilm.Models;
using PlateformeFilm.data;
using Microsoft.EntityFrameworkCore;
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


    //Modifier un utilisateur déja existant 
    [HttpPut("{id}")]
    
    public async Task<ActionResult<User>> PutUser(User userUpdate) // c pas <IActionResult<User>>
    {
        // on récupère la confiture que l'on souhaite modifier
        User user = await _context.Users.FindAsync(userUpdate.Id);
        if (user == null)
        {
            return NotFound();
        }

        // on met a jour les informations de la confiture

        user.Id=userUpdate.Id;
        user.Pseudo=userUpdate.Pseudo;
        user.Password=userUpdate.Password;



        // on indique a notre contexte que l'objet a été modifié
        _context.Entry(user).State = EntityState.Modified;

        try
        {
            // on enregistre les modifications
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // si une erreur de concurrence survient on retourne un code 500
            return StatusCode(500, "Erreur de concurrence");
        }
        // on retourne un code 200 pour indiquer que la modification a bien eu lieu
        return Ok(user);
    }
    //Supprimer les Données 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConfiture(int id)
    {
        // on récupère la user que l'on souhaite supprimer
        User user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        // on indique a notre contexte que l'objet a été supprimé
        _context.Users.Remove(user);
        // on enregistre les modifications
        await _context.SaveChangesAsync();
        // on retourne un code 204 pour indiquer que la suppression a bien eu lieu
        return NoContent();
    }

    }
}

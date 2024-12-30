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
    public class FavoriteController : ControllerBase
    {
        private readonly FavoriteContext _context;
        private readonly FilmContext _filmContext;
        public FavoriteController(FavoriteContext ctx,FilmContext filmContext)
        {
            _context = ctx;
            _filmContext=filmContext;
        }
        public class FavoriteCreation
        {        
            public int Id{get;set;}
            public int UserId{get;set;}

            public int FilmId{get;set;}
        }
     [HttpPost("Add")]

        public async Task<ActionResult<Favorite>> PostFavorite(FavoriteCreation favoriteCreation)
        {
            var filmExist = await _filmContext.Films.AnyAsync(f => f.Id ==favoriteCreation.FilmId);
            if (!filmExist)
            {
                return NotFound("Film non trouvé dans BDD de film");
            }
            Favorite favorite = new Favorite {
              Id=favoriteCreation.Id,
              UserId=favoriteCreation.UserId,
              FilmId=favoriteCreation.FilmId  
            };
     
            _context.Favorites.Add(favorite);
            
            await _context.SaveChangesAsync();
           
            return  Ok(new {message ="Film Bien Ajouté dans la liste des films !"});
        }
                
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
    
            Favorite favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }
        
            _context.Favorites.Remove(favorite);
        
            await _context.SaveChangesAsync();
          
            return NoContent();
        }
    }

}
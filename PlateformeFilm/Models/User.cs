using System;

namespace PlateformeFilm.Models
{
    // Définition de l'énumération pour les rôles
    public enum Role
    {
        User,
        Admin
    }

    public class User
    {
        public int Id{get;set;}
        public string Pseudo { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public User(){}
       
    }

}

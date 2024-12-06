using System;

namespace PlateformeFilm.Models
{
    // Définition de l'énumération pour les rôles
    public enum Role
    {
        User,
        Admin
    }

    // Définition de la classe Utilisateur
    public class User
    {
        // Propriété pour le pseudo de l'User
        public int Id{get;set;}
        public string Pseudo { get; set; }

        // Propriété pour le mot de passe
        public string Password { get; set; }

        // Propriété pour le rôle de l'User
        public Role Role { get; set; }
        public User(){}
       
    }

}

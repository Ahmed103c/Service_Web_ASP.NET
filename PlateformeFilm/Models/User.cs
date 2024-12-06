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
    public class Utilisateur
    {
        // Propriété pour le pseudo de l'utilisateur
        public string Pseudo { get; set; }

        // Propriété pour le mot de passe
        public string MotDePasse { get; set; }

        // Propriété pour le rôle de l'utilisateur
        public Role Role { get; set; }
    }
}

using System;
namespace OrangeSD25 
{
    public class Personne
    {
        protected int idpersonne;
        protected string nom, prenom, email, mdp;

        // constructeur 
        public Personne(int idpersonne, string nom, string prenom, string email, string mdp)
        {
            this.idpersonne = idpersonne;
            this.nom = nom;
            this.prenom = prenom;
            this.email = email;
            this.mdp = mdp;
        }
        public Personne(string nom, string prenom, string email, string mdp)
        {
            this.idpersonne = 0;
            this.nom = nom;
            this.prenom = prenom;
            this.email = email;
            this.mdp = mdp;
        }
        //get et set 
        public int IdPersonne { get => idpersonne; set => idpersonne = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set =>  prenom = value; }
        public string Email { get => email; set => email = value; }
        public string Mdp { get => mdp; set => mdp = value; }
    }
}


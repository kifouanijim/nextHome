using System;
namespace Nexthome
{
    public class Agent
    {
        protected int idagent;
        protected string nom, prenom, email, mdp;

        // constructeur 
        public Agent(int idagent, string nom, string prenom, string email, string mdp)
        {
            this.idagent = idagent;
            this.nom = nom;
            this.prenom = prenom;
            this.email = email;
            this.mdp = mdp;
        }
        public Agent(string nom, string prenom, string email, string mdp)
        {
            this.idagent = 0;
            this.nom = nom;
            this.prenom = prenom;
            this.email = email;
            this.mdp = mdp;
        }
        //get et set 
        public int IdAgent { get => idagent; set => idagent = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set =>  prenom = value; }
        public string Email { get => email; set => email = value; }
        public string Mdp { get => mdp; set => mdp = value; }
    }
}


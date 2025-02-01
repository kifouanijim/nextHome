using System;
namespace Nexthome
{
    public class Agentloc : Agent
    {
        private string qualification;
        private float salaire;
        // constructeur
        public Agentloc(int idagent, string nom, string prenom, string email, string mdp, string qualification, float salaire) : base(idagent, nom, prenom, email, mdp)
        {
            this.qualification = qualification;
            this.salaire = salaire;
        }
        public Agentloc(string nom, string prenom, string email, string mdp, string qualification, float salaire) : base(nom, prenom, email, mdp)
        {
            this.qualification = qualification;
            this.salaire = salaire;
        }
        public string Qualification { get => qualification; set => qualification = value; }
        public float Salaire { get => salaire; set => salaire = value; }
    }
}


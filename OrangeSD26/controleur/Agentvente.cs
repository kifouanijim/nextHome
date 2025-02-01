using System;
namespace Nexthome
{
    public  class Agentvente : Agent
    {
        private string departement;
        private float commission;

        // constructeur 
        public Agentvente(int idagent, string nom, string prenom,
         string email, string mdp, string departement,
         float commission) : base(idagent, nom, prenom, email, mdp)
        {
            this.departement = departement;
            this.commission = commission;
        }
        public Agentvente(string nom, string prenom, string email,
         string mdp, string departement,
         float commission) : base(nom, prenom, email, mdp)
        {
            this.departement = departement;
            this.commission = commission;
        }
        public string Departement { get => departement; set => departement = value; }
        public float Commission { get => commission; set => commission = value; }
    }

}


using System;
namespace OrangeSD25
{
    public  class Commercial : Personne
    {
        private string secteurAct;
        private float commission;

        // constructeur 
        public Commercial(int idpersonne, string nom, string prenom,
         string email, string mdp, string secteurAct,
         float commission) : base(idpersonne, nom, prenom, email, mdp)
        {
            this.secteurAct = secteurAct;
            this.commission = commission;
        }
        public Commercial(string nom, string prenom, string email,
         string mdp, string secteurAct,
         float commission) : base(nom, prenom, email, mdp)
        {
            this.secteurAct = secteurAct;
            this.commission = commission;
        }
        public string SecteurAct { get => secteurAct; set => secteurAct = value; }
        public float Commission { get => commission; set => commission = value; }
    }

}


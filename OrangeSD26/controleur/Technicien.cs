using System;
namespace OrangeSD25 
{
    public class Technicien : Personne
    {
        private string qualification;
        private float salaire;
        // constructeur
        public Technicien(int idpersonne, string nom, string prenom, string email, string mdp, string qualification, float salaire) : base(idpersonne, nom, prenom, email, mdp)
        {
            this.qualification = qualification;
            this.salaire = salaire;
        }
        public Technicien(string nom, string prenom, string email, string mdp, string qualification, float salaire) : base(nom, prenom, email, mdp)
        {
            this.qualification = qualification;
            this.salaire = salaire;
        }
        public string Qualification { get => qualification; set => qualification = value; }
        public float Salaire { get => salaire; set => salaire = value; }
    }
}


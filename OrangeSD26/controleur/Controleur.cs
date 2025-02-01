using System;
namespace Nexthome
{
	public class Controleur
	{	//Mysql : 3306
		//Mariadb : 3307
		//MAc Mamp : 8889

		private static Modele unModele = new Modele("localhost", "nexthome2", "root","");
		public static bool ControlerDonnees (List<string> donnees)
        {
            bool ok = true;
            foreach (string champ in donnees)
            {
                if (string.IsNullOrEmpty(champ))
                {
                    ok = false;
                }
            }
            return ok;
        }
		public static void InsertAgentvente(Agentvente unAgentvente)
		{
			//on controle les données du commercial avant insertion 
			unModele.insertagentvente(unAgentvente); 
		}
        public static void UpdateAgentvente(Agentvente unAgentvente)
        {
            //on controle les données du commercial avant mise à jour  
            unModele.updateAgentvente(unAgentvente);
        }
        public static void DeleteAgentvente(int idAgentvente)
        { 
            unModele.deleteAgentvente(idAgentvente);
        }
        public static Agentvente SelectWhereAgentvente(int idAgent)
        {
            return unModele.selectWhereAgentvente(idAgent);
        }
        public static Agentvente SelectWhereAgentvente(string email, string mdp)
        {
            return unModele.selectWhereAgentvente(email, mdp);
        }
        public static List<Agentvente> SelectAllAgentvente()
        {
            return unModele.selectAllAgentvente(); 
        }
        public static List<Agentvente> SelectLikeAgentvente(String filtre)
        {
            return unModele.selectLikeAgentvente(filtre);
        }
    }
}


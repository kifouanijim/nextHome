using System;
namespace OrangeSD25
{
	public class Controleur
	{	//Mysql : 3306
		//Mariadb : 3307
		//MAc Mamp : 8889

		private static Modele unModele = new Modele("localhost", "OrangeSD25", "root","");
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
		public static void InsertCommercial (Commercial unCommercial)
		{
			//on controle les données du commercial avant insertion 
			unModele.InsertCommercial(unCommercial); 
		}
        public static void UpdateCommercial(Commercial unCommercial)
        {
            //on controle les données du commercial avant mise à jour  
            unModele.UpdateCommercial(unCommercial);
        }
        public static void DeleteCommercial(int idCommercial)
        { 
            unModele.DeleteCommercial(idCommercial);
        }
        public static Commercial SelectWhereCommercial(int idPersonne)
        {
            return unModele.SelectWhereCommercial(idPersonne);
        }
        public static Commercial SelectWhereCommercial(string email, string mdp)
        {
            return unModele.SelectWhereCommercial(email, mdp);
        }
        public static List<Commercial> SelectAllCommerciaux()
        {
            return unModele.SelectAllCommerciaux(); 
        }
        public static List<Commercial> SelectLikeCommerciaux(String filtre)
        {
            return unModele.SelectLikeCommerciaux(filtre);
        }
    }
}


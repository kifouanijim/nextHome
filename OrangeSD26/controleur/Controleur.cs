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
        // Functions for Agentloc (added functions)
        public static void InsertAgentloc(Agentloc unAgentloc)
        {
            //on controle les données de l'agent locataire avant insertion
            unModele.insertAgentloc(unAgentloc);
        }

        public static void UpdateAgentloc(Agentloc unAgentloc)
        {
            //on controle les données de l'agent locataire avant mise à jour
            unModele.updateAgentloc(unAgentloc);
        }

        public static void DeleteAgentloc(int idAgentloc)
        {
            unModele.deleteAgentloc(idAgentloc);
        }

        public static Agentloc SelectWhereAgentloc(int idAgent)
        {
            return unModele.selectWhereAgentloc(idAgent);
        }

        public static Agentloc SelectWhereAgentloc(string email, string mdp)
        {
            return unModele.selectWhereAgentloc(email, mdp);
        }

        public static List<Agentloc> SelectAllAgentloc()
        {
            return unModele.selectAllAgentloc();
        }

        public static List<Agentloc> SelectLikeAgentloc(string filtre)
        {
            return unModele.selectLikeAgentloc(filtre);
        }
    }
}
  


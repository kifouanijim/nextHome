using System;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Diagnostics;
namespace Nexthome
{
	public class Modele
	{
		private string serveur, bdd, user, mdp;
		private MySqlConnection maConnexion;

		public Modele(string serveur, string bdd, string user, string mdp)
		{
			this.serveur = serveur;
			this.bdd = bdd;
			this.user = user;
			this.mdp = mdp;
			string url = "SslMode=None";
			url += "; Server=" + this.serveur;
			url += "; Port=3307"; //3306 mysql ou bien 3307 Mariadb
			url += "; Database=" + this.bdd;
			url += "; User Id=" + this.user;
			url += "; Password=" + this.mdp;
			try
			{
				//instancier la connexion à la BDD
				this.maConnexion = new MySqlConnection(url);
			}
			catch (Exception exp)
			{
				Debug.WriteLine("Erreur de connexion à l'url : " + url);
			}

		}
		/******************** Gestion des agent de ventes  ************/
		public void insertagentvente(Agentvente unAgentvente)
        {
			string requete = "call insertagentvente(@nom, @prenom, @email, @mdp, @departement, @commission); ";
			MySqlCommand uneCmde = null;
			try
			{
				this.maConnexion.Open();
				uneCmde = this.maConnexion.CreateCommand();
				uneCmde.CommandText = requete;
				
				uneCmde.Parameters.AddWithValue("@nom", unAgentvente.Nom);
                uneCmde.Parameters.AddWithValue("@prenom", unAgentvente.Prenom);
                uneCmde.Parameters.AddWithValue("@email", unAgentvente.Email);
                uneCmde.Parameters.AddWithValue("@mdp", unAgentvente.Mdp);
                uneCmde.Parameters.AddWithValue("@departement", unAgentvente.Departement);
                uneCmde.Parameters.AddWithValue("@commission", unAgentvente.Commission);
				//Execution de la requete
				uneCmde.ExecuteNonQuery();
                Debug.WriteLine(" execution requete: " + requete);
                this.maConnexion.Close(); 
			}
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + requete);
            }
        }
        public void deleteAgentvente(int idAgentvente)
        {
            string requete = "call deleteAgentvente(@idAgentvente); ";
            MySqlCommand uneCmde = null;
            try
            {
                this.maConnexion.Open();
                uneCmde = this.maConnexion.CreateCommand();
                uneCmde.CommandText = requete;
                //faire la correspondance entre les variables SQL et les données du commercial
                uneCmde.Parameters.AddWithValue("@idAgentvente", idAgentvente);
               //Execution de la requete
                uneCmde.ExecuteNonQuery();

                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + requete);
            }
        }
        public void updateAgentvente(Agentvente unAgentvente)
        {

            {
                string requete = "call updateAgentvente(@idagent,@nom, @prenom, @email, @mdp, @departement, @commission); ";
                MySqlCommand uneCmde = null;
                try
                {
                    this.maConnexion.Open();
                    uneCmde = this.maConnexion.CreateCommand();
                    uneCmde.CommandText = requete;
                    //faire la correspondance entre les variables SQL et les données du commercial
                    uneCmde.Parameters.AddWithValue("@idagent", unAgentvente.IdAgent);
                    uneCmde.Parameters.AddWithValue("@nom", unAgentvente.Nom);
                    uneCmde.Parameters.AddWithValue("@prenom", unAgentvente.Prenom);
                    uneCmde.Parameters.AddWithValue("@email", unAgentvente.Email);
                    uneCmde.Parameters.AddWithValue("@mdp", unAgentvente.Mdp);
                    uneCmde.Parameters.AddWithValue("@departement", unAgentvente.Departement);
                    uneCmde.Parameters.AddWithValue("@commission", unAgentvente.Commission);
                    //Execution de la requete
                    uneCmde.ExecuteNonQuery();

                    this.maConnexion.Close();
                }
                catch (Exception exp)
                {
                    Debug.WriteLine("Erreur execution requete: " + requete);
                }

            }
        }
        public List<Agentvente> selectAllAgentvente()
        {
            List<Agentvente> lesAgentvente = new List<Agentvente>();
            string requete = "select * from v_liste_agentvente ; ";
            MySqlCommand uneCmde = null;
            try
            {
                this.maConnexion.Open();
                uneCmde = this.maConnexion.CreateCommand();
                uneCmde.CommandText = requete;
                //extraction des données des commerciaux
                DbDataReader unReader = uneCmde.ExecuteReader(); //creation d'un curseur
                try
                {
                    //s'il y a des données
                    if (unReader.HasRows)
                    {
                        //tant que il y a des lignes
                        while (unReader.Read())
                        {
                            Agentvente unAgentvente = new Agentvente(
                                unReader.GetInt16(0),
                                unReader.GetString(1), unReader.GetString(2),
                                unReader.GetString(3), unReader.GetString(4),
                                unReader.GetString(5), unReader.GetFloat(6)
                                );
                            //ajout de ce commercial à la liste lesCommerciaux
                            lesAgentvente.Add(unAgentvente);

                        }
                        unReader.Close(); 
                    }
                }
                catch (Exception exp)
                {
                    Debug.WriteLine("Impossible de lire les agents de vente "); 
                }

                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + requete);
            }

            return lesAgentvente; 
        }

        public List<Agentvente> selectLikeAgentvente(String filtre)
        {
            List<Agentvente> lesAgentvente = new List<Agentvente>();
            string requete = "select * from v_liste_agentvente where nom like @filtre or prenom like @filtre or email like @filtre or departement like @filtre; ";
            MySqlCommand uneCmde = null;
            try
            {
                this.maConnexion.Open();
                uneCmde = this.maConnexion.CreateCommand();
                uneCmde.CommandText = requete;
                uneCmde.Parameters.AddWithValue("@filtre", "%"+filtre+"%");
                //extraction des données des commerciaux
                DbDataReader unReader = uneCmde.ExecuteReader(); //creation d'un curseur
                try
                {
                    //s'il y a des données
                    if (unReader.HasRows)
                    {
                        //tant que il y a des lignes
                        while (unReader.Read())
                        {
                            Agentvente unAgentvente = new Agentvente(
                                unReader.GetInt16(0),
                                unReader.GetString(1), unReader.GetString(2),
                                unReader.GetString(3), unReader.GetString(4),
                                unReader.GetString(5), unReader.GetFloat(6)
                                );
                            //ajout de ce commercial à la liste lesCommerciaux
                            lesAgentvente.Add(unAgentvente);

                        }
                        unReader.Close();
                    }
                }
                catch (Exception exp)
                {
                    Debug.WriteLine("Impossible de lire les agents de ventes ");
                }

                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + requete);
            }

            return lesAgentvente;
        }
        public Agentvente selectWhereAgentvente (int idAgent)
        {
            Agentvente unAgentvente = null;
            string requete = "call avoiragent (@idagent); ";
            MySqlCommand uneCmde = null;
            try
            {
                this.maConnexion.Open();
                uneCmde = this.maConnexion.CreateCommand();
                uneCmde.Parameters.AddWithValue("@idagent", idAgent); 
                uneCmde.CommandText = requete;
                //extraction des données des commerciaux
                DbDataReader unReader = uneCmde.ExecuteReader(); //creation d'un curseur
                try
                {
                    //s'il y a des données
                    if (unReader.HasRows)
                    {
                        //s'il y a une ligne 
                        if (unReader.Read())
                        {
                            unAgentvente = new Agentvente(
                                unReader.GetInt16(0),
                                unReader.GetString(1), unReader.GetString(2),
                                unReader.GetString(3), unReader.GetString(4),
                                unReader.GetString(5), unReader.GetFloat(6)
                                );
                        }
                        unReader.Close();
                    }
                }
                catch (Exception exp)
                {
                    Debug.WriteLine("Impossible de lire les agents de ventes ");
                }

                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + requete);
            }
            return unAgentvente;
        }
        public Agentvente selectWhereAgentvente(string email, string mdp)
        {
            Agentvente unAgentvente = null;
            string requete = "select * from v_liste_agentvente where email = @email and mdp = @mdp; ";
            MySqlCommand uneCmde = null;
            try
            {
                this.maConnexion.Open();
                uneCmde = this.maConnexion.CreateCommand();
                uneCmde.Parameters.AddWithValue("@email", email);
                uneCmde.Parameters.AddWithValue("@mdp", mdp);
                uneCmde.CommandText = requete;
                //extraction des données des commerciaux
                DbDataReader unReader = uneCmde.ExecuteReader(); //creation d'un curseur
                try
                {
                    //s'il y a des données
                    if (unReader.HasRows)
                    {
                        //s'il y a une ligne 
                        if (unReader.Read())
                        {
                            unAgentvente = new Agentvente(
                               unReader.GetInt16(0),
                               unReader.GetString(1), unReader.GetString(2),
                               unReader.GetString(3), unReader.GetString(4),
                               unReader.GetString(5), unReader.GetFloat(6)
                               );
                        }
                        unReader.Close();
                    }
                }
                catch (Exception exp)
                {
                    Debug.WriteLine("Impossible de lire les agents ");
                }

                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + requete);
            }
            return unAgentvente;
        }
    }
}
 
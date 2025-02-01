using System;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Diagnostics;
namespace OrangeSD25
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
		/******************** Gestion des Commerciaux  ************/
		public void InsertCommercial(Commercial unCommercial)
		{
			string requete = "call insertCommercial(@nom, @prenom, @email, @mdp, @secteurAct, @commission); ";
			MySqlCommand uneCmde = null;
			try
			{
				this.maConnexion.Open();
				uneCmde = this.maConnexion.CreateCommand();
				uneCmde.CommandText = requete;
				//faire la correspondance entre les variables SQL et les données du commercial
				uneCmde.Parameters.AddWithValue("@nom", unCommercial.Nom);
                uneCmde.Parameters.AddWithValue("@prenom", unCommercial.Prenom);
                uneCmde.Parameters.AddWithValue("@email", unCommercial.Email);
                uneCmde.Parameters.AddWithValue("@mdp", unCommercial.Mdp);
                uneCmde.Parameters.AddWithValue("@secteurAct", unCommercial.SecteurAct);
                uneCmde.Parameters.AddWithValue("@commission", unCommercial.Commission);
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
        public void DeleteCommercial(int idCommercial)
        {
            string requete = "call deleteCommercial(@idCommercial); ";
            MySqlCommand uneCmde = null;
            try
            {
                this.maConnexion.Open();
                uneCmde = this.maConnexion.CreateCommand();
                uneCmde.CommandText = requete;
                //faire la correspondance entre les variables SQL et les données du commercial
                uneCmde.Parameters.AddWithValue("@idCommercial", idCommercial);
               //Execution de la requete
                uneCmde.ExecuteNonQuery();

                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + requete);
            }
        }
        public void UpdateCommercial(Commercial unCommercial)
        {
            string requete = "call updateCommercial(@idpersonne,@nom, @prenom, @email, @mdp, @secteurAct, @commission); ";
            MySqlCommand uneCmde = null;
            try
            {
                this.maConnexion.Open();
                uneCmde = this.maConnexion.CreateCommand();
                uneCmde.CommandText = requete;
                //faire la correspondance entre les variables SQL et les données du commercial
                uneCmde.Parameters.AddWithValue("@idpersonne", unCommercial.IdPersonne);
                uneCmde.Parameters.AddWithValue("@nom", unCommercial.Nom);
                uneCmde.Parameters.AddWithValue("@prenom", unCommercial.Prenom);
                uneCmde.Parameters.AddWithValue("@email", unCommercial.Email);
                uneCmde.Parameters.AddWithValue("@mdp", unCommercial.Mdp);
                uneCmde.Parameters.AddWithValue("@secteurAct", unCommercial.SecteurAct);
                uneCmde.Parameters.AddWithValue("@commission", unCommercial.Commission);
                //Execution de la requete
                uneCmde.ExecuteNonQuery();

                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + requete);
            }
        }
        public List<Commercial> SelectAllCommerciaux()
        {
            List<Commercial> lesCommerciaux = new List<Commercial>();
            string requete = "select * from v_liste_commerciaux ; ";
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
                            Commercial unCommercial = new Commercial(
                                unReader.GetInt16(0),
                                unReader.GetString(1), unReader.GetString(2),
                                unReader.GetString(3), unReader.GetString(4),
                                unReader.GetString(5), unReader.GetFloat(6)
                                );
                            //ajout de ce commercial à la liste lesCommerciaux
                            lesCommerciaux.Add(unCommercial);

                        }
                        unReader.Close(); 
                    }
                }
                catch (Exception exp)
                {
                    Debug.WriteLine("Impossible de lire les commerciaux "); 
                }

                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + requete);
            }

            return lesCommerciaux; 
        }

        public List<Commercial> SelectLikeCommerciaux(String filtre)
        {
            List<Commercial> lesCommerciaux = new List<Commercial>();
            string requete = "select * from v_liste_commerciaux where nom like @filtre or prenom like @filtre or email like @filtre or secteurAct like @filtre; ";
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
                            Commercial unCommercial = new Commercial(
                                unReader.GetInt16(0),
                                unReader.GetString(1), unReader.GetString(2),
                                unReader.GetString(3), unReader.GetString(4),
                                unReader.GetString(5), unReader.GetFloat(6)
                                );
                            //ajout de ce commercial à la liste lesCommerciaux
                            lesCommerciaux.Add(unCommercial);

                        }
                        unReader.Close();
                    }
                }
                catch (Exception exp)
                {
                    Debug.WriteLine("Impossible de lire les commerciaux ");
                }

                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + requete);
            }

            return lesCommerciaux;
        }
        public Commercial SelectWhereCommercial (int idPersonne)
        {
            Commercial unCommercial = null;
            string requete = "call avoirPersonne (@idpersonne); ";
            MySqlCommand uneCmde = null;
            try
            {
                this.maConnexion.Open();
                uneCmde = this.maConnexion.CreateCommand();
                uneCmde.Parameters.AddWithValue("@idpersonne", idPersonne); 
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
                             unCommercial = new Commercial(
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
                    Debug.WriteLine("Impossible de lire les commerciaux ");
                }

                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + requete);
            }
            return unCommercial; 
        }
        public Commercial SelectWhereCommercial(string email, string mdp)
        {
            Commercial unCommercial = null;
            string requete = "select * from v_liste_commerciaux where email = @email and mdp = @mdp; ";
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
                            unCommercial = new Commercial(
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
                    Debug.WriteLine("Impossible de lire les commerciaux ");
                }

                this.maConnexion.Close();
            }
            catch (Exception exp)
            {
                Debug.WriteLine("Erreur execution requete: " + requete);
            }
            return unCommercial;
        }
    }
}
 
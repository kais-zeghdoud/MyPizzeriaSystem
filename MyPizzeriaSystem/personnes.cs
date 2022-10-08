using System;
using System.Collections.Generic;
using Interactions;
using Menu;

namespace Personnes{

    public abstract class Personne{
        private string nom;
        private string prenom;
        List<Message>? messages; // '?' represents that this list of messages can be empty (null)

        public Personne(string nom, string prenom){
            this.nom = nom;
            this.prenom = prenom;
        }
    }

    public class Client : Personne{
        private readonly uint customerID;
        private string adresse;
        private string telephone;
        private DateTime firstOrder;
        private uint nbCommandes = 0;
        private double montantAchats = 0;

        public Client(string nom, string prenom, string adresse, string telephone) : base(nom, prenom)
        {
            this.customerID = (uint)PizzeriaController.getInstance().getClients().Count + 1;
            this.adresse = adresse;
            this.telephone = telephone;
            this.firstOrder = DateTime.Now;
        }

        public uint getClientID(){return customerID;}


        public void paieCommande(Commande commande){
            nbCommandes++;
            montantAchats += commande.getTotalPrice();
        }

        public void increaseNbCommandes(){
            nbCommandes ++;
        }

        public void increaseMontantAchats(double totalPrice){
            montantAchats += totalPrice;
        }

    }


    public class Commis : Personne{
        private readonly uint employeID;
        private uint nbCommandes = 0;

        public Commis(string nom, string prenom) : base(nom, prenom)
        {
            // appeler PizzeriaController pour avoir le nombre d'employés et attribuer une valeur à employeID

        }

        public uint getCommisID(){return employeID;}

        public uint getNbCommandes(){return nbCommandes;}

        public void addCustomer(){
            string nom, prenom, adresse;
            string telephone;
            PizzeriaController.getInstance().getClients().Add(new Client(nom, prenom, adresse, telephone));
        }

        public void modifyCustomer(){}

        public void deleteCustomer(uint ID){
            PizzeriaController.getInstance().getClients().RemoveAt((int)ID - 1);
        }

        public void addOrder(Client client){
            uint n_pizza, n_boissons;
            List<Pizza> pizzas;
            List<boissons> produitsAnnexes;

            for (int i = 0; i < n_pizza; i++)
            {
                Console.WriteLine("Enter pizza {0} : ", i+1);
                pizzas.Add(new Pizza(Console.ReadLine()));
            }
            for (int i = 0; i < n_boissons; i++)
            {
                Console.WriteLine("Enter drink {0}", i+1);
                int j = Console.ReadLine();
                produitsAnnexes.Add(new boissons{j});
            }

            PizzeriaController.getInstance().getCommandes().Add(new Commande(getCommisID(), client.getClientID(), pizzas, produitsAnnexes));
        }

        public void closeOrder(Commande commande){
            commande.setEtatCommande(statut.fermée);
            nbCommandes ++;
        }
    }


    public class Livreur : Personne{
        private readonly uint employeID;
        private uint nbLivraisons = 0;

        public Livreur(string nom, string prenom) : base(nom, prenom)
        {
            // appeler PizzeriaController pour avoir le nombre d'employés et attribuer une valeur à employeID
        }


        public uint getLivreurID(){return employeID;}

        public uint getNbLivraisons(){return nbLivraisons;}

        public void remiseCommande(Commande commande){
            commande.getClientID().paieCommande(commande);
            commande.setEtatPaiement(paiement.encaissé);
            nbLivraisons ++;
        }

        public void abandonCommande(Commande commande){
            commande.setEtatPaiement(paiement.perte);
        }
    }
}

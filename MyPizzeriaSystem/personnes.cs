using System;
using System.Collections.Generic;
using Interactions;
using Menu;

namespace Personnes{

    abstract class Personne{
        private string nom;
        private string prenom;
        List<Message> messages;
    }

    class Client : Personne{
        private readonly uint customerID;
        private string adresse;
        private string[10] telephone;
        private DateTime firstOrder;
        private uint nbCommandes = 0;
        private decimal montantAchats = 0;

        public Client(string nom, string prenom, string adresse, string telephone){
            this.nom = nom;
            this.prenom = prenom;
            this.customerID = PizzeriaController.getInstance.getClients.Count + 1;
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

        public void increaseMontantAchats(decimal totalPrice){
            montantAchats += totalPrice;
        }

    }


    class Commis : Personne{
        private readonly uint employeID;
        private uint nbCommandes = 0;

        public uint getCommisID(){return employeID;}

        public uint getNbCommandes(){return nbCommandes;}

        public void addCustomer(){
            string nom, prenom, adresse;
            string[10] telephone;
            PizzeriaController.getInstance().getClients().Add(new Client(nom, prenom, adresse, telephone));
        }

        public void modifyCustomer(){}

        public void deleteCustomer(uint ID){
            PizzeriaController.getInstance().getClients().RemoveAt(ID - 1);
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


    class Livreur : Personne{
        private readonly uint employeID;
        private uint nbLivraisons = 0;


        public uint getLivreurID(){return employeID;}

        public uint getNbLivraisons(){return nbLivraisons;}

        public void remiseCommande(Commande commande){
            commande.getClient().paieCommande(commande);
            commande.setEtatPaiement(paiement.encaissé);
            nbLivraisons ++;
        }

        public void abandonCommande(Commande commande){
            commande.setEtatPaiement(paiement.perte);
        }
    }
}

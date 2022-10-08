using System;
using System.Collections.Generic;
using Interactions;
using Menu;
using Process;

namespace Personnes{

    public abstract class Personne{
        private string nom;
        private string prenom;
        private List<Message> messages = new List<Message>();

        public Personne(string nom, string prenom){
            this.nom = nom;
            this.prenom = prenom;
        }

        public string getFullName(){return nom + ' ' +prenom;}

        public List<Message> getMessages(){return messages;}

        public void sendMessage(string text, Personne receiver){
            receiver.getMessages().Add(new Message(text, this, receiver));
        }
    }


    public abstract class Employe : Personne{
        private readonly uint employeID;

        public Employe(string nom, string prenom) : base(nom, prenom)
        {
            uint nbCommis = (uint) PizzeriaController.getInstance().getCommis().Count;
            uint nbLivreurs = (uint) PizzeriaController.getInstance().getLivreurs().Count;
            employeID = nbCommis + nbLivreurs + 1;
        }

        public uint getEmployeID(){return employeID;}
    }


    public class Commis : Employe{
        private uint nbCommandes = 0;

        public Commis(string nom, string prenom) : base(nom, prenom){}

        public uint getNbCommandes(){return nbCommandes;}

        public void addCustomer(){
            Console.Write("Entrez le nom du client : ");
            string nom = Console.ReadLine();

            Console.Write("Entrez le prénom du client : ");
            string prenom = Console.ReadLine();

            Adresse adresse = Fonctions.askAdresse();

            Console.Write("Entrez le numéro de téléphone de {0} {1} : ", prenom, nom);
            string telephone = Console.ReadLine();

            PizzeriaController.getInstance().getClients().Add(new Client(nom, prenom, adresse, telephone));
        }

        public void modifyCustomer(){}

        public void deleteCustomer(uint ID){
            PizzeriaController.getInstance().getClients().RemoveAt((int)ID - 1);
        }

        public void addOrder(Client client){
            List<Pizza> pizzas = new List<Pizza>();
            List<boissons> produitsAnnexes = new List<boissons>();
            int n_pizza = Fonctions.askNumber("pizza");
            int n_boissons = Fonctions.askNumber("boissons");

            for (int i = 0; i < n_pizza; i++){
                pizzas.Add(Fonctions.askPizza());
            }
            for (int i = 0; i < n_boissons; i++){
                produitsAnnexes.Add(Fonctions.askDrink());
            }

            client.setFirstOrder();
            PizzeriaController.getInstance().getCommandes().Add(new Commande(this, client, pizzas, produitsAnnexes));
        }

        public void closeOrder(Commande commande){
            commande.setEtatCommande(statut.fermée);
            nbCommandes ++;
        }
    }


    public class Livreur : Employe{
        
        private uint nbLivraisons = 0;

        public Livreur(string nom, string prenom) : base(nom, prenom){}

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


    public class Client : Personne{
        private readonly uint customerID;
        private Adresse adresse;
        private string telephone;
        private DateTime firstOrder;
        private uint nbCommandes = 0;
        private double montantAchats = 0;

        public Client(string nom, string prenom, Adresse adresse, string telephone) 
        : base(nom, prenom)
        {
            this.customerID = (uint)PizzeriaController.getInstance().getClients().Count + 1;
            this.adresse = adresse;
            this.telephone = telephone;
        }

        public uint getClientID(){return customerID;}
        public Adresse getAdresse(){return adresse;}
        public string getTelephone(){return telephone;}
        public DateTime getFirstOrder(){return firstOrder;}
        public void setFirstOrder(){firstOrder = DateTime.Now;}

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


    public class Adresse{
        private uint numRue;
        private string nomRue;
        private uint codePostal;
        private string ville;

        public Adresse(uint numRue, string nomRue, uint codePostal, string ville){
            this.numRue = numRue;
            this.nomRue = nomRue;
            this.codePostal = codePostal;
            this.ville = ville;
        }

        public string toString(){
            return numRue + " " + nomRue + " " + codePostal + " " + ville;
        }
    }

}

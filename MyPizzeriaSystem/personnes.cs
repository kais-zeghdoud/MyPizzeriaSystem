using System;
using System.Collections.Generic;
using Interactions;
using Menu;
using Process;
using System.Threading;
using System.Threading.Tasks;

namespace Personnes{

    public abstract class Personne{
        private string nom;
        private string prenom;
        private List<Message> messages = new List<Message>();

        public Personne(string nom, string prenom){
            this.nom = nom;
            this.prenom = prenom;
        }

        public string getFullName(){return prenom + ' ' +nom;}

        public List<Message> getMessages(){return messages;}

        public void sendMessage(string text, Personne receiver){
            receiver.getMessages().Add(new Message(text, this, receiver));
        }

        public void showMessages(){
            foreach (Message m in messages){m.toString();}
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

        public string toString(){
            return "\nID Employe : " + getEmployeID() + "\nNom : " + getFullName() + 
            "\nNb Commandes : " + nbCommandes;
        }

        public uint getNbCommandes(){return nbCommandes;}

        public void addCustomer(){
            Console.Write("Entrez le prénom du client : ");
            string prenom = Console.ReadLine();

            Console.Write("Entrez le nom du client : ");
            string nom = Console.ReadLine();

            Adresse adresse = Fonctions.askAdresse();

            Console.Write("Entrez le numéro de téléphone de {0} {1} : ", prenom, nom);
            string telephone = Console.ReadLine();

            PizzeriaController.getInstance().getClients().Add(new Client(nom, prenom, adresse, telephone));
        }

        public void modifyCustomer(){}

        public void deleteCustomer(){
            uint ID;
            do{
                Console.Write("Entrer le numéro de client à supprimer : ");
                ID = Convert.ToUInt16(Console.ReadLine());
            }while(ID > PizzeriaController.getInstance().getClients().Count + 1);
            PizzeriaController.getInstance().getClients().RemoveAt((int)ID - 1);
        }

        public void addOrder(){
            int IDClient;
            do{
                Console.Write("Entrer l'ID du client qui commande : ");
                IDClient = Convert.ToInt32(Console.ReadLine());
            }while(IDClient > PizzeriaController.getInstance().getClients().Count);
            Client client = PizzeriaController.getInstance().getClients().ElementAt(IDClient -1);
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

            Commande c = new Commande(this, client, pizzas, produitsAnnexes);
            PizzeriaController.getInstance().getCommandes().Add(c);
            client.setFirstOrder();
            nbCommandes ++;
            
            this.sendMessage("Cher client, votre commande a été prise en charge", client);
            getOrderReady(c);
        }

        public void closeOrder(){
            Commande c;
            int id;
            do{
                Fonctions.getDelivringOrders();
                Console.Write("Entrer l'ID de la commande à clôturer : ");
                id = Convert.ToInt32(Console.ReadLine());
            }while(id > PizzeriaController.getInstance().getCommandes().Where(c => c.getPaiement()== paiement.encaissé).Count());
            
            c = PizzeriaController.getInstance().getCommandes().ElementAt(id - 1);
            c.setEtatCommande(statut.fermée);
            this.sendMessage("Commande clôturée!\nMontant total : " + c.getTotalPrice() , c.getLivreur());
        }

        public async Task getOrderReady(Commande c){
            await Task.Run(()=> Thread.Sleep(10000 * c.getnbItems())); // 10sec per item
            c.setEtatCommande(statut.livraison);
        }
    }


    public class Livreur : Employe{
        
        private uint nbLivraisons = 0;

        public Livreur(string nom, string prenom) : base(nom, prenom){}

        public string toString(){
            return "\nID Employe : " + getEmployeID() + "\nNom : " + getFullName() + 
            "\nNb Commandes : " + nbLivraisons;
        }

        public uint getNbLivraisons(){return nbLivraisons;}

        public void remiseCommande(){
            Commande c;
            int id;
            do{
                Fonctions.getDelivringOrders();
                Console.Write("Entrer l'ID de la commande à livrer : ");
                id = Convert.ToInt32(Console.ReadLine());
            }while(id > PizzeriaController.getInstance().getCommandes().Where(c => c.getEtat()== statut.livraison).Count());
            
            c = PizzeriaController.getInstance().getCommandes().ElementAt(id - 1);
            c.getClient().paieCommande(c);
            c.setEtatPaiement(paiement.encaissé);
            c.setLivreur(this);
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
        public DateTime getFirstOrder(){return firstOrder;}
        public int getNbCommandes(){return (int)nbCommandes;}
        public double getAmount(){return montantAchats;}
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

        public string toString(){
            return "\nID Client : " + customerID + "\nNom : " + this.getFullName() + "\nAdresse : " + adresse.toString() +
            "\nTéléphone : " + telephone + "\nNombre de commandes : " + nbCommandes + "\nMontant total commandes : " + montantAchats;
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

        public string getCity(){return ville;}
    }


}

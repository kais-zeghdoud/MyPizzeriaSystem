using System;
using System.Collections.Generic;
using Personnes;
using Menu;

namespace Interactions{

    public class Message{
        private string text;
        private Personne sender;
        private Personne receiver;
        private DateTime heureDate;

        public Message(string text, Personne sender, Personne receiver){
            this.text = text;
            this.sender = sender;
            this.receiver = receiver;
            heureDate = DateTime.Now;
        }

        public string toString(){
            return "Sender : " + sender.getFullName() + "\nDestinataire : " + receiver.getFullName() + 
            "\nDate et heure : " + heureDate.ToString() + "\nMessage : " + text;
        }
    }

    public sealed class PizzeriaController{
        private static PizzeriaController instance;
        private static List<Client> clients;
        private static List<Commande> commandes;
        private static List<Commis> commis;
        private static List<Livreur> livreurs;

        private PizzeriaController()
        {
            clients = new List<Client>();
            commandes = new List<Commande>();
            commis = new List<Commis>();
            livreurs = new List<Livreur>();
        }

        public static PizzeriaController getInstance(){
            if(instance == null)
                instance = new PizzeriaController();
            return instance;
        }

        public List<Client> getClients(){return clients;}

        public List<Commande> getCommandes(){return commandes;}

        public List<Commis> getCommis(){return commis;}

        public List<Livreur> getLivreurs(){return livreurs;}

        public void addCommis(){
            string nom, prenom;
            Console.Write("Entrez le nom du commis : ");
            nom = Console.ReadLine();
            Console.Write("Entrez le prénom du commis : ");
            prenom = Console.ReadLine();
            commis.Add(new Commis(nom, prenom));
        }
        
        public void addLivreur(){
            string nom, prenom;
            Console.Write("Entrez le nom du livreur : ");
            nom = Console.ReadLine();
            Console.Write("Entrez le prénom du livreur : ");
            prenom = Console.ReadLine();
            livreurs.Add(new Livreur(nom, prenom));
        }

        public void showCommandes(){
            foreach (Commande c in commandes){c.toString();}
        }

    }
}
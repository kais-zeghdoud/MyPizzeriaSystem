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
        private static List<Client> clients = new List<Client>();
        private static List<Commande> commandes = new List<Commande>();
        private static List<Employe> employes = new List<Employe>();

        private PizzeriaController(){} // declare les listes de clients - commandes - 

        public static PizzeriaController getInstance(){
            if(instance == null)
                instance = new PizzeriaController();
            return instance;
        }

        public List<Client> getClients(){return clients;}

        public List<Commande> getCommandes(){return commandes;}

        public List<Employe> getEmployes(){return employes;}

        public void addCommis(string nom, string prenom){employes.Add(new Commis(nom, prenom));}
        
        public void addLivreur(string nom, string prenom){employes.Add(new Livreur(nom, prenom));}

    }
}
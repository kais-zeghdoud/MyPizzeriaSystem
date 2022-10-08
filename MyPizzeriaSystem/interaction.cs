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

    }

    public sealed class PizzeriaController{
        private static PizzeriaController instance;
        private static List<Client> clients;
        private static List<Commande> commandes;

        private PizzeriaController(){} // declare les listes de clients - commandes - 

        public static PizzeriaController getInstance(){
            if(instance == null)
                instance = new PizzeriaController();
            return instance;
        }

        public List<Client> getClients(){return clients;}

        public List<Commande> getCommandes(){return commandes;}


    }
}
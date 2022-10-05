using System;
using System.Collections.Generic;
using Personnes;
using Menu;

namespace Interactions{

    public class Message{
        private static string text;
        private static Personnes sender;
        private static Personnes receiver;
        private static DateTime heureDate;


    }

    public sealed class PizzeriaController{
        private static PizzeriaController instance;
        private static List<Client> clients;
        private static List<Commande> commandes;


        public static PizzeriaController getInstance(){
            if(instance == null)
                instance = new PizzeriaController();
            return instance;
        }

        public static List<Client> getClients(){return clients;}

        public static List<Commande> getCommandes(){return commandes;}


    }
}
using System;
using System.Collections.Generic;
using Personnes;
using Interactions;

namespace Menu{
    public enum base_pizza{tomate, crèmeFraîche, BBQ}
    public enum format{petit, moyen, grand}
    public enum boissons{Eau, JusOrange, CocaCola, Fanta, Sprite}
    public enum statut{préparation, livraison, fermée}
    public enum paiement{attente, encaissé, perte}


    public class Commande{
        private readonly uint numero;
        private DateTime heureDate;
        private uint commisID;
        private uint clientID;
        private uint nbItems;
        private List<Pizza> pizzas;
        private List<boissons> produitsAnnexes;
        private double totalPrice = 0;
        private statut etatCommande;
        private paiement etatPaiement;


        public Commande(uint commisID, uint clientID, List<Pizza> pizzas, List<boissons> produitsAnnexes){
            numero = (uint)PizzeriaController.getInstance().getCommandes().Count + 1;           
            heureDate = DateTime.Now;
            this.commisID = commisID;
            this.clientID = clientID;
            this.pizzas = pizzas;
            this.produitsAnnexes = produitsAnnexes;
            this.nbItems = (uint)(pizzas.Count + produitsAnnexes.Count);
            setTotalPrice();
            etatCommande = statut.préparation;
            etatPaiement = paiement.attente;
        }

        public void setTotalPrice(){
            foreach (var pizza in pizzas){totalPrice += pizza.getPrice();}
            totalPrice += (double)(produitsAnnexes.Count * 1.5);
        }

        public uint getCommisID(){return commisID;}
        
        public uint getClientID(){return clientID;}

        public double getTotalPrice(){return totalPrice;}

        public void setEtatCommande(statut etat){etatCommande = etat;}

        public void setEtatPaiement(paiement etat){etatPaiement = etat;}
    }


    public class Pizza{
        private string nom;
        private base_pizza type;
        private format taille;
        private double prix;

        public static Dictionary<string, double> pizzaPrices = new Dictionary<string, double>(){
            {"Marguerita", 6.50},
            {"Végétarienne", 7.90},
            {"Reine", 8.50},
            {"Fruits de mer", 10.90},
            {"Norvégienne", 9.50},
            {"Campione", 8.50},
            {"Régina", 8.50},
            {"Orientale", 8.50},
            {"Napolitaine", 8.00},
            {"Hawaïenne", 9.00}
        };

        public Pizza(string nom, base_pizza type, format taille){
            this.nom = nom;
            this.type = type;
            this.taille = taille;
            setPrice();
        }

        public void setPrice(){
            switch(taille)
            {
                case format.moyen:
                    prix = pizzaPrices[nom];
                    break;
                case format.petit:
                    prix = pizzaPrices[nom] - 1;
                    break;
                case format.grand:
                    prix = pizzaPrices[nom] + 1;
                    break;
            }
        }

        public double getPrice(){return prix;}

    }
}


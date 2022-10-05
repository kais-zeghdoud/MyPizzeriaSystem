using System;
using System.Collections.Generic;
using Personnes;
using Interactions;

namespace Menu{

    enum base_pizza{tomate, crèmeFraîche, BBQ}

    enum format{petit, moyen, grand}

    enum boissons{Eau, JusOrange, CocaCola, Fanta, Sprite}

    enum statut{préparation, livraison, fermée}

    enum paiement{attente, encaissé, perte}


    public class Commande{
        private readonly uint numero;
        private DateTime heureDate;
        private uint commisID;
        private uint clientID;
        private uint nbItems = 0;
        private List<Pizza> pizzas;
        private List<boissons> produitsAnnexes;
        private decimal totalPrice = 0;
        private statut etatCommande;
        private paiement etatPaiement;


        public Commande(uint commisID, uint clientID, List<Pizza> pizzas, List<boissons> produitsAnnexes){
            numero = PizzeriaController.getInstance().getCommandes().Count + 1;
            heureDate = DateTime.Now;
            this.commis = commis;
            this.client = client;
            this.pizzas = pizzas;
            this.produitsAnnexes = produitsAnnexes;
            this.totalPrice = computeTotalPrice();
            etatCommande = statut.préparation;
            etatPaiement = paiement.attente;
        }

        public void computeTotalPrice(){
            foreach (var pizza in pizzas){
                totalPrice += pizza.getPrice();
            }
            totalPrice += (produitsAnnexes.Count) * 1.5;
        }

        public Commis getCommis(){return commis;}
        
        public Client getClient(){return client;}

        public decimal getTotalPrice(){return totalPrice;}

        public void setEtatCommande(statut etat){etatCommande = etat;}

        public void setEtatPaiement(paiement etat){etatPaiement = etat;}
    }


    public class Pizza{
        private readonly string nom;
        private base_pizza type;
        private format taille;
        private decimal prix;

        public decimal getPrice(){return prix;}

    }
}


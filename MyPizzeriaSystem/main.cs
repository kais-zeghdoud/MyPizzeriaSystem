using System;
using Personnes;
using Interactions;
using Menu;
using Process;

public class MyPizzeriaSystem{
    public static void Main(string[] args){
        Console.Title = "MyPizzeriaSystem";
        bool run = true;
        int choice = 0;

        while(run)
        {
            Console.WriteLine("\nMENU\n\n1 - Module Effecif/Client\n2 - Module Commandes\n3 - ModuleStatistiques" +
            "\n4 - Module Communication \n5 - Quitter");
            choice = Convert.ToInt16(Console.ReadLine());

            switch(choice)
            {
                case 1 :
                    Console.WriteLine("\n1 - Ajouter un commis" +
                    "\n2 - Ajouter un livreur" +
                    "\n3 - Ajouter un compte client" +
                    "\n4 - Modifier un compte client" +
                    "\n5 - Supprimer un compte client" +
                    "\n6 - Afficher l'effectif Commis" +
                    "\n7 - Afficher l'effectif Livreur"+
                    "\n8 - Afficher les clients par ordre alphabétique"+
                    "\n9 - Afficher les clients par ville"+
                    "\n10 - Afficher les clients par montant des achats cumulés");
                    break;

                case 2 :
                    Console.WriteLine("\n1 - Créer une commande" +
                    "\n2 - Livrer une commande " +
                    "\n3 - Clôturer une commande" +
                    "\n4 - Afficher l'historique de l'ensemble des commandes"+
                    "\n5 - Afficher les commandes en cours"+
                    "\n6 - Afficher les commandes en livraison");
                    break;

                case 3 :
                    Console.WriteLine("\n1 - Afficher par commis le nombre de commandes gérées" +
                    "\n2 - Afficher par livreur le nombre de livraisons effectuees" +
                    "\n3 - Afficher les commandes selon une periode de temps" +
                    "\n4 - Afficher la moyenne des prix des commandes"+
                    "\n5 - Afficher la moyenne des comptes clients");
                    break;

                case 4 :
                Console.WriteLine("");
                    break;

                case 5:
                    run = false;
                    break;
            }
        }
        Console.ReadKey();
    }
}



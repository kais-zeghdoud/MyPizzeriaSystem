using System;
using Menu;
using Personnes;

namespace Process
{
    public class Fonctions
    {
        public static int askNumber(string item){
            int number = 0;
            do{
                Console.Write("Entrez le nombre de {0} : ", item);
                number = Convert.ToInt32(Console.ReadLine());
            }while(number > 0);
            return number;
        }


        public static Pizza askPizza(){
            string name;
            int typeIndex, sizeIndex;

            foreach (var pizza in Pizza.pizzaPrices)
                Console.WriteLine("{0} : {1}", pizza.Key, pizza.Value); // affichage des pizza et de leur prix
            do{
                Console.Write("Entrez le nom de la pizza : ");
                name = Console.ReadLine();
            }while(!Menu.Pizza.pizzaPrices.ContainsKey(name));

            foreach (var type in Enum.GetValues(typeof(base_pizza)))
                Console.WriteLine("{0} : {1}",(int)type, type); // affichage des bases de pizza
            do{
                Console.Write("Entrez le numéro de la base souhaitée : ");
                typeIndex = Convert.ToInt32(Console.ReadLine());
            }while(typeIndex < 0 || typeIndex > 2);

            foreach (var taille in Enum.GetValues(typeof(format)))
                Console.WriteLine("{0} : {1}",(int)taille, taille); // affichage des tailles de pizza
            do{
                Console.Write("Entrez le numéro de la taille souhaitée : ");
                sizeIndex = Convert.ToInt32(Console.ReadLine());
            }while(sizeIndex < 0 || sizeIndex > 2);

            return new Pizza(name, (base_pizza)typeIndex ,(format)sizeIndex);
        }


        public static boissons askDrink(){
            int drinkIndex;
            foreach (var boisson in Enum.GetValues(typeof(boissons))){
                Console.WriteLine("{0} : {1}",(int)boisson, boisson);
            }
            do{
                Console.Write("Entrez le numéro de la boisson souhaitée : ");
                drinkIndex = Convert.ToInt32(Console.ReadLine());
            }while(drinkIndex < 0 || drinkIndex > Enum.GetNames(typeof(boissons)).Length);

            return (boissons)drinkIndex;
        }


        public static Adresse askAdresse(){
            Console.Write("Entrez le numéro de rue : ");
            uint numRue = Convert.ToUInt16(Console.ReadLine());

            Console.Write("Entrez le nom de la rue : ");
            string nomRue = Console.ReadLine();

            Console.Write("Entrez le code postal : ");
            uint codePostal = Convert.ToUInt32(Console.ReadLine());

            Console.Write("Entrez la ville : ");
            string ville = Console.ReadLine();

            return new Adresse(numRue, nomRue, codePostal, ville);
        }


    }
}





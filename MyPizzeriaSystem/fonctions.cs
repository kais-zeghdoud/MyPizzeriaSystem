using System;
using Menu;
using Personnes;
using Interactions;

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


        public static string getClientsByAlphabeticOrder(){
            string clients = "";
            List<Client> sortedList = PizzeriaController.getInstance().getClients().OrderBy(c=>c.getFullName()).ToList();
            foreach (Client c in sortedList){
                clients += c.toString() + "\n";
            }
            return clients;
        }


        public static string getClientsByCity(string city){
            string clients = "";
            foreach (Client client in PizzeriaController.getInstance().getClients()){
                if (client.getAdresse().getCity().ToLower() == city.ToLower())
                    clients += client.toString() + "\n";
            }
            return clients;
        }


        public static string getClientsByAmount(){
            string clients = "";
            List<Client> sortedList = PizzeriaController.getInstance().getClients().OrderBy(c=>c.getAmount()).ToList();
            foreach (Client c in sortedList){
                clients += c.toString() + "\n";
            }
            return clients;
        }


        public static string getCommisByOrders(){
            string commis = "";
            foreach (Commis c in PizzeriaController.getInstance().getCommis()){
                commis += c.toString() + "\n";
            }
            return commis;
        }


        public static string getLivreursByDelivery(){
            string commis = "";
            foreach (Livreur l in PizzeriaController.getInstance().getLivreurs()){
                commis += l.toString() + "\n";
            }
            return commis;
        }


        public static string getMoyCommandes(){
            double prices = 0;
            foreach (Commande c in PizzeriaController.getInstance().getCommandes()){
                prices += c.getTotalPrice();
            }
            prices = prices / PizzeriaController.getInstance().getCommandes().Count;
            return "Moyenne des prix des commandes : " + prices;
        }
    }
}





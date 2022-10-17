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
            }while(number < 1);
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


        public static void getClientsByAlphabeticOrder(){
            List<Client> sortedList = PizzeriaController.getInstance().getClients().OrderBy(c=>c.getFullName()).ToList();
            foreach (Client c in sortedList){
                Console.WriteLine(c.toString());
            }
        }


        public static void getClientsByCity(){
            string city;
            Console.Write("Entrez le nom de la ville : ");
            city  = Console.ReadLine();

            foreach (Client client in PizzeriaController.getInstance().getClients()){
                if (client.getAdresse().getCity().ToLower() == city.ToLower())
                    Console.WriteLine(client.toString());
            }
        }


        public static void showClientsByAmount(){
            List<Client> sortedList = PizzeriaController.getInstance().getClients().OrderBy(c=>c.getAmount()).ToList();
            foreach (Client c in sortedList){
                Console.WriteLine(c.toString());
            }
        }


        public static void showCommisByOrders(){
            foreach (Commis c in PizzeriaController.getInstance().getCommis()){
                Console.WriteLine(c.toString());
            }
        }


        public static void showLivreursByDelivery(){
            foreach (Livreur l in PizzeriaController.getInstance().getLivreurs()){
                Console.WriteLine(l.toString());
            }
        }


        public static void showMoyCommandes(){
            double prices = 0;
            foreach (Commande c in PizzeriaController.getInstance().getCommandes()){
                prices += c.getTotalPrice();
            }
            prices = prices / PizzeriaController.getInstance().getCommandes().Count;

            Console.WriteLine("Moyenne des prix des commandes : " + prices);
        }

        public static void showMoyClients(){
            double moy;
            foreach (Client c in PizzeriaController.getInstance().getClients())
            {
                moy = c.getAmount() / c.getNbCommandes();
                c.toString();
                Console.WriteLine("Moyenne du compte client : {0}", moy);
            }
        }

        public static void showCommandesbyDate(){
            DateTime d;
            List<Commande> commandes = PizzeriaController.getInstance().getCommandes();
            do{
                Console.Write("Entrer une date minimale au format (year-month-day): ");
                d = Convert.ToDateTime(Console.ReadLine());
            }while(d > DateTime.Now);
            foreach (Commande c in commandes.Where(c => c.getHeureDate() > d)){
                c.toString();
            }
        }


        public static void getCurringOrders(){
            List<Commande> commandes = PizzeriaController.getInstance().getCommandes();
            foreach (Commande c in commandes.Where(c => c.getEtat()== statut.préparation)){c.toString();}
        }

        public static void getDelivringOrders(){
            List<Commande> commandes = PizzeriaController.getInstance().getCommandes();
            foreach (Commande c in commandes.Where(c => c.getEtat()== statut.livraison)){c.toString();}
        }


        public static void menuEffectifClient(int choice){
            var d = new Dictionary<int, System.Action>();
            d[1] = new Action(PizzeriaController.getInstance().addCommis);
            d[2] = new Action(PizzeriaController.getInstance().addLivreur);
            if (PizzeriaController.getInstance().getCommis().Count != 0){
                d[3] = new Action(PizzeriaController.getInstance().getCommis().ElementAt(0).addCustomer);
                d[4] = new Action(PizzeriaController.getInstance().getCommis().ElementAt(0).modifyCustomer);
                d[5] = new Action(PizzeriaController.getInstance().getCommis().ElementAt(0).deleteCustomer);
            }
            d[6] = new Action(showCommisByOrders);
            d[7] = new Action(showLivreursByDelivery);
            d[8] = new Action(getClientsByAlphabeticOrder);
            d[9] = new Action(getClientsByCity);
            d[10] = new Action(showClientsByAmount);

            d[choice].DynamicInvoke();
        }


        public static void menuCommandes(int choice, string employe){
            var d = new Dictionary<int, System.Action>();
            if(choice == 1 || choice == 3){
                d[1] = new Action(PizzeriaController.getInstance().getCommis().Find(c => c.getFullName() == employe).addOrder);
                d[3] = new Action(PizzeriaController.getInstance().getCommis().Find(c => c.getFullName() == employe).closeOrder);
            }
            if(choice == 2){
                d[2] = new Action(PizzeriaController.getInstance().getLivreurs().Find(c => c.getFullName() == employe).remiseCommande);
            }
            d[4] = new Action(PizzeriaController.getInstance().showCommandes);
            d[5] = new Action(getCurringOrders);
            d[6] = new Action(getDelivringOrders);

            d[choice].DynamicInvoke();
        }


        public static void menuStatistiques(int choice){
            var d = new Dictionary<int, System.Action>();
            d[1] = new Action(showCommisByOrders);
            d[2] = new Action(showLivreursByDelivery);
            d[3] = new Action(showCommandesbyDate);
            d[4] = new Action(showMoyCommandes);
            d[5] = new Action(showMoyClients);

            d[choice].DynamicInvoke();
        }


        public static void menuCommunication(int choice, string fullName){
            var d = new Dictionary<int, System.Action>();
            d[1] = new Action(PizzeriaController.getInstance().getClients().Find(c => c.getFullName() == fullName).showMessages);
            d[2] = new Action(PizzeriaController.getInstance().getLivreurs().Find(l => l.getFullName() == fullName).showMessages);
            d[3] = new Action(PizzeriaController.getInstance().getCommis().Find(c => c.getFullName() == fullName).showMessages);

            d[choice].DynamicInvoke();

        }
    }
}





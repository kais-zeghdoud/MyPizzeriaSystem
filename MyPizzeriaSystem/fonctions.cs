using System;
using Menu;

public class Fonctions
{
    public uint askNumber(string item){
        uint number = 0;
        do{
            Console.Write("Enter the number of {0} : ", item);
            number = Convert.ToUInt32(Console.ReadLine());
        }while(number > 0);
        return number!;
    }

    public string askPizza(){
        string? name = "";
        do{
            Console.Write("Enter the name of the pizza : ");
            name = Console.ReadLine();
        }while(!Menu.Pizza.pizzaPrices.ContainsKey(name));
        return name!;
    }
}



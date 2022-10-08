using System;
using Personnes;
using Interactions;
using Menu;

public class MyPizzeriaSystem{
    public static void Main(string[] args){
        Console.Title = "MyPizzeriaSystem";

        PizzeriaController.getInstance().addCommis("zeghdoud", "kais");

        foreach (var e in PizzeriaController.getInstance().getCommis())
        {
            e.sendMessage("first message", e);
            Console.WriteLine(e.getMessages().ElementAt(0).toString());
        }

        PizzeriaController.getInstance().getCommis().ElementAt(0).addCustomer();

        Console.WriteLine(PizzeriaController.getInstance().getClients().ElementAt(0).getFullName());
        Console.WriteLine(PizzeriaController.getInstance().getClients().ElementAt(0).getAdresse().toString());
        Console.WriteLine(PizzeriaController.getInstance().getClients().ElementAt(0).getFirstOrder());

        Console.ReadKey();
    }
}



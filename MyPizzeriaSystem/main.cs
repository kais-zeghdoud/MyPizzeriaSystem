using System;
using Personnes;
using Interactions;
using Menu;

public class MyPizzeriaSystem{
    public static void Main(string[] args){
        Console.Title = "MyPizzeriaSystem";

        PizzeriaController.getInstance().addCommis("zeghdoud", "kais");

        foreach (var e in PizzeriaController.getInstance().getEmployes())
        {
            e.sendMessage("first message", e);
            Console.WriteLine(e.getMessages().ElementAt(0).toString());
        }

        Console.ReadKey();
    }
}



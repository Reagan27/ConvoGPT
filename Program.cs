using System;
using ConvoGPT_console.GPT;

class Program
{


    public static async Task Main(string[] args)
    {
        Console.WriteLine("What is Your Name :");
        var myName = Console.ReadLine();

        Console.WriteLine("What can I act as :");
        var ActAsA = Console.ReadLine();


        await new GptService().sendRequest(myName, ActAsA);
    }
}
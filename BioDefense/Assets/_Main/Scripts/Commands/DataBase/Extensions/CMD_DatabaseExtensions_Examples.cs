using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMD_DatabaseExtensions_Examples : CMD_DatabaseExtension
{
    new public static void Extend(CommandDatabase database)
    {
        //Add command with no parameters
        database.AddCommand("print", new Action(PrintDefaultMessage));
        database.AddCommand("print_1p", new Action<string>(PrintUseermessage)); //This command takes a singule parameter
        database.AddCommand("print_mp", new Action<string[]>(PrintLines));

        //Add lambda with no parameters
        database.AddCommand("lambda", new Action(() => { Debug.Log("Printing a default message to console."); }));
        database.AddCommand("lambda_1p", new Action<string>((arg) => { Debug.Log($"User message: '{arg}'"); }));
        database.AddCommand("lambda_mp", new Action<string[]>((args) => { Debug.Log(string.Join(", ", args)); }));

        //add coroutine with no parameters
        database.AddCommand("processs", new Func<IEnumerator>(SimpleProcess));
        database.AddCommand("processs_1p", new Func<string, IEnumerator>(LineProcess));
        database.AddCommand("processs_mp", new Func<string[], IEnumerator>(MultiLineProcess));

        //Special Example

    }

    private static void PrintDefaultMessage()
    {
        Debug.Log("Printing a default message to console.");
    }

    private static void PrintUseermessage(string message)
    {
        Debug.Log($"User message: '{message}'");
    }

    private static void PrintLines(String[] lines)
    {
        int i = 1;
        foreach(string line in lines)
        {
            Debug.Log($"{i++}. '{line}'");
        }
    }

    private static IEnumerator SimpleProcess()
    {
        for(int i = 1; i <= 5; i++)
        {
            Debug.Log($"Process Running... [{i}]");
            yield return new WaitForSeconds(i);
        }
    }

    private static IEnumerator LineProcess(string data)
    {
        if(int.TryParse(data, out int num))
        {
            for (int i = 1; i <= num; i++)
            {
                Debug.Log($"Process Running... [{i}]");
                yield return new WaitForSeconds(i);
            }
        }
    }

    private static IEnumerator MultiLineProcess(string[] data)
    {
        foreach(string line in data)
        {
            Debug.Log($"Process Running... [{line}]");
            yield return new WaitForSeconds(0.5f);
        }
    }
}

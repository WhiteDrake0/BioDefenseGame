using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandingTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        yield return CommandManager.instance.Execute("print");
        yield return CommandManager.instance.Execute("print_1p", "Hello world!");
        yield return CommandManager.instance.Execute("print_mp", "line1", "line2", "line3");

        yield return CommandManager.instance.Execute("lambda");
        yield return CommandManager.instance.Execute("lambda_1p", "Hello lambda!");
        yield return CommandManager.instance.Execute("lambda_mp", "lambda1", "lambda2", "lambda3");

        yield return CommandManager.instance.Execute("processs");
        yield return CommandManager.instance.Execute("processs_1p", "3");
        yield return CommandManager.instance.Execute("processs_mp", "Process line 1", "Process line 2", "Process line 3");
    }
}

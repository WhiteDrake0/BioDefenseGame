using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class TestingArchitect : MonoBehaviour
    {
        DialogSystem ds;
        TextArchitect architect;

        string[] lines = new string[3]
        {
            "Good morning, Captain.",
            "Today is going to be a busy day for the both of has.",
            "So, lets get started."
        };
        // Start is called before the first frame update
        void Start()
        {
            ds = DialogSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.typewriter;
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("Hello world");
                architect.Build(lines[Random.Range(0, lines.Length)]);
            }
        }
    }
}


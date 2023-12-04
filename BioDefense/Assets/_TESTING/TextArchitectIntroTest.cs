using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace Testing
{
    public class TextArchitectIntroTest : MonoBehaviour
    {
        DialogSystem ds;
        TextArchitect architect;
        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        

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
            architect.speed = 0.05f;
        }

        // Update is called once per frame
        void Update()
        {
            if(bm != architect.buildMethod)
            {
                architect.buildMethod = bm;
                architect.Stop();
            }

            if (Input.GetKeyDown(KeyCode.S))
                architect.Stop();

            string longLine = "this is a very long line that makes no sense but I am just populating it with stuff because, you know, stuff is good right? I like stuff, you like stuff, we all like stufff and the turkey gets stuff.";

            if (Input.GetKeyDown(KeyCode.Space))
            {

                if (architect.isBuilding)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                }
                else
                    architect.Build(longLine);

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Append(longLine);
            }
        }

        
    }
}

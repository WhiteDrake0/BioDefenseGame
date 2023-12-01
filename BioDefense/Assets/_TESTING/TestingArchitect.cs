using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class TestingArchitect : MonoBehaviour
    {
        DialogSystem ds;
        TextArchitect architect;
        
        //GameObject Variables
        public GameObject characters;
        public GameObject dialogue;
        public GameObject btnStart;
        public GameObject btnCancel;

        //Bool variables
        private bool talking;
       // private bool startGame;
        private bool waitForDialogue;

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
            
            //initiate variables
            talking = false;
            //startGame = false;
            waitForDialogue = false;
            architect.OnTextBuildingComplete += OnTextBuildingComplete;


        }

        // Update is called once per frame
        void Update()
        {
            //Make buttons appear when archict is finishing building the syntice
            if(!architect.isBuilding && waitForDialogue)
            {
                characters.SetActive(true);
                dialogue.SetActive(false);
            }
        }

        private void OnTextBuildingComplete()
        {
            if (talking)
            {
                talking = false;
                btnCancel.SetActive(true);
                btnStart.SetActive(true);
                
            }
          
        }

        public void StartGame()
        {
            btnCancel.SetActive(false);
            btnStart.SetActive(false);
            architect.Build("Ok, lets start.");
            //startGame = true;

        }

        public void Cancel()
        {
            btnCancel.SetActive(false);
            btnStart.SetActive(false);
            architect.Build("I see, you need more time to prepare.");
            waitForDialogue = true;

        }

        public void StartCharacterDialogue()
        {
            if (!talking)
            {
                characters.SetActive(false);
                dialogue.SetActive(true);
                talking = true;
                architect.Build("Good morning captain. Ready to bigen todays tasks? (press Z for yes or X for no)");


            }
        }
    }
}


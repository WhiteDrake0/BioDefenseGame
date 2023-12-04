using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

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
        public GameObject btnContainerOptions;
        public GameObject btnContainerConfirmation;

        //Bool variables
        private bool talking;
       // private bool startGame;
        //private bool waitForDialogue;

        //Numeral Variables
        public int maxPeopleToSend = 5;

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
            /*waitForDialogue = false;
            architect.OnTextBuildingComplete += OnTextBuildingComplete;*/


        }

        // Update is called once per frame
        void Update()
        {
            //Make buttons appear when archict is finishing building the syntice
            /*if(!architect.isBuilding && waitForDialogue)
            {
                characters.SetActive(true);
                dialogue.SetActive(false);
            }*/
        }

        private void OnTextBuildingComplete()
        {
            if (talking)
            {
                talking = false;
                btnContainerConfirmation.SetActive(false);
                
            }
          
        }

        public void StartGame()
        {
            btnContainerConfirmation.SetActive(false);
            architect.Build("Ok, lets start.");
            //startGame = true;

        }

        public void Cancel()
        {
            btnContainerConfirmation.SetActive(false);
            btnContainerOptions.SetActive(true);
            architect.Build("I see, you need more time to prepare.");
            //waitForDialogue = true;

        }

        public void DefenseButton()
        {
            btnContainerOptions.SetActive(false);
            btnContainerConfirmation.SetActive(true);
            architect.Build("Do you wish to start the defense shieft? Just know that you won't have acess to this menu until the start of the next day.");
        }

        public void GetMaterials()
        {
            // Get the number of people the player wants to send 
            int peopleToSend = Random.Range(1, maxPeopleToSend + 1); 

            // Calculate the risk based on the number of people sent
            float riskFactor = Mathf.Clamp01((float)peopleToSend / maxPeopleToSend);

            // Determine the outcome (success or failure) based on the calculated risk
            bool success = Random.value > riskFactor;

            // If the gathering is successful, update resources and people accordingly
            if (success)
            {
                int materialsGathered = peopleToSend * Random.Range(1, 4); 
                                                                           

                int peopleLost = Mathf.RoundToInt((float)peopleToSend * riskFactor);
                Debug.Log("Gathering successful! " + materialsGathered + " materials gathered, " + peopleLost + " people lost.");
            }
            else
            {
                int peopleLost = Mathf.RoundToInt((float)peopleToSend * riskFactor);

                Debug.Log("Gathering failed! " + peopleLost + " people lost.");
            }
        }

        public void StartCharacterDialogue()
        {
            if (!talking)
            {
                characters.SetActive(false);
                dialogue.SetActive(true);
                talking = true;
                architect.Build("Good morning captain. Ready to bigen todays tasks?");


            }
        }
    }
}


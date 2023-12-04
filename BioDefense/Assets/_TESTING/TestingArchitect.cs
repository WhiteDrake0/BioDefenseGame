using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
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
        public GameObject background;
        public GameObject protrait;
        public GameObject btnContainerOptions;
        public GameObject btnContainerConfirmation;
        public GameObject nameText;
        

        //Bool variables
        private bool talking;
        private bool startGame;
        //private bool waitForDialogue;
        //private bool SeraSpeacking;

        //Numeral Variables
        public int maxPeopleToSend = 5;
        public int count = 0;

        string[] intro = new string[4]
        {
            "Fifty years have passed since an enigmatic viral outbreak swept the globe, transforming both humanity and the animal kingdom into grotesque delirious entities. (press space to continue)",
            "Following this peril, the uninfected sought refuge within Elpifloria's fortified bastion, erecting formidable barriers to ward off the relentless onslaught of mutated horrors. (press space to continue)",
            "The biohazard defense squad emerged as the last line of defense against the relentless tide of aberrations within the city's protective walls. (press space to continue)",
            "They stood as humanity's shield, warding off the encroaching nightmare that lurked beyond the city limits, tasked with neutralizing these monstrous threats. (press space to continue)"
        };

        
        // Start is called before the first frame update
        void Start()
        {
            ds = DialogSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.typewriter;
            
            //initiate variables
            talking = false;
            startGame = false;
           // waitForDialogue = false;
            //SeraSpeacking = false;

            StartIntro();

        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space) && count < intro.Length && !architect.isBuilding)
            {
                //Debug.Log(architect.isBuilding);
                StartIntro();
            }
            
            if(Input.GetKeyDown(KeyCode.Space) && count >= intro.Length && !architect.isBuilding && !background.activeSelf)
            {
                dialogue.SetActive(false);                
                background.SetActive(true);
                characters.SetActive(true);
                //waitForDialogue = false;
            }

            if(talking && !architect.isBuilding)
            {
                talking = false;
                btnContainerOptions.SetActive(true);
            }

            if(Input.GetKeyDown(KeyCode.Space) && startGame && !architect.isBuilding)
                SceneManager.LoadScene("BasicDesign");

        }

        void StartIntro()
        {
            architect.Build(intro[count]);
            count++;
           // waitForDialogue = true;
        }



        public void StartGame()
        {
            btnContainerConfirmation.SetActive(false);
            architect.Build("Ok, lets start. (press space to continue)");
            startGame = true;

        }

        public void Cancel()
        {
            btnContainerConfirmation.SetActive(false);
            btnContainerOptions.SetActive(true);
            architect.Build("I see, you need more time to prepare.");
            

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
            if (!talking) {

                
                characters.SetActive(false);
                dialogue.SetActive(true);
                if (!protrait.activeSelf)
                {
                    nameText.SetActive(true);
                    protrait.SetActive(true);
                }
                
                talking = true;
                architect.Build("Good morning captain. Ready to bigen todays tasks?");
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
using TMPro;

namespace Testing
{
    /// <summary>
    /// All of the logic of the buttons hare going to be here
    /// </summary>
    
    public class BtnManager : MonoBehaviour
    {
        //Gameobject variables
        public GameObject btnOptionsContainer;
        public GameObject btnGatherMaterialContainer;
        public GameObject btnSendPeople;
        public GameObject btnCloseGatherMaterial;
        public GameObject btnResources;

        //UI gameObjects
        public TMP_InputField inputNumber;
        public TextMeshProUGUI manPower;

        //Dialogue variables
        private TextArchitect architect;
        private DialogSystem ds;
        private bool clickedResources;
        private bool clickedGatherMaterial;
        private bool clickedSend;
        private bool sendWasClicked;

        // Start is called before the first frame update
        void Start()
        {
            //Set up Dialogue
            ds = DialogSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.typewriter;

            //Set variables
            clickedResources = false;
            clickedGatherMaterial = false;
            clickedSend = false;
            sendWasClicked = false;

        }

        // Update is called once per frame
        void Update()
        {
            
            if(Input.GetKeyDown(KeyCode.Space) && !architect.isBuilding && clickedResources)
            {
                btnOptionsContainer.SetActive(true);
                clickedResources = false;
            }

            if(clickedGatherMaterial && !architect.isBuilding && !btnGatherMaterialContainer.activeSelf)
            {
                btnGatherMaterialContainer.SetActive(true);
                clickedGatherMaterial = false;
            }

            if(clickedSend && !architect.isBuilding && !btnGatherMaterialContainer.activeSelf)
            {
                btnOptionsContainer.SetActive(true);
                clickedSend = false;
            }

            
        }

        #region Button Functions
        
        //open Send people to gather materials interface
        public void OpenSendPeopleUI()
        {
            if (!sendWasClicked)
            {
                btnOptionsContainer.SetActive(false);
                architect.Build("Do you wish to send a scounting team to gather resources? Just know you are putting the lives of your man at risk and will only know how well it went at the end of the day.");
                clickedGatherMaterial = true;
            } else
            {
                architect.Build("Captain!! We are already assembling a team for a material collection mission and need to be mindful about how many of our men we are putting at risk.");
            }
            
        }

        public void CloseGatherMaterial()
        {
            btnGatherMaterialContainer.SetActive(false);
            btnOptionsContainer.SetActive(true);
            architect.Build("");
        }

        public void SendPeople()
        {
            
            int nPeople = 0;
            int manPo = 0;

            if (inputNumber.text != string.Empty)
            {

                //Get input number
                nPeople = int.Parse(inputNumber.text);

                //Get manPower value
                manPo = int.Parse((manPower.text.Trim()).Substring(9));

                if(manPo < nPeople)
                {
                    architect.Build("We don't have that many people avalable.");
                }
                else
                {
                    int newValue = manPo - nPeople;

                    manPower.text = "ManPower: " + newValue;
                    //manPower.ForceMeshUpdate();
                    btnGatherMaterialContainer.SetActive(false);
                    architect.Build($"Roger! Assambling a team of {newValue} people for a material collection mission.");
                    clickedSend = true;
                    sendWasClicked = true;
                }
            }

            else
            {
                architect.Build("You need to give me number");
            }


        }

        // open resource allucation menu
        public void OpenResourcesAllucationUI()
        {
            
            architect.Build("Sorry, but science chiefs Andros and Jonas are still working on it. (press space to continue)");
            clickedResources = true;
            btnOptionsContainer.SetActive(false);


        }

        

        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
using TMPro;

public class InterfaceController : MonoBehaviour
{
    public RectTransform controls;
    public static InterfaceController instance;

    private GameObject btnConfirmationContainer;
    private GameObject btnOptionsContainer;
    private GameObject btnGatherMaterial;

    //UI gameObjects
    public TMP_InputField inputNumber;
    public TextMeshProUGUI manPower;

    //Dialogue variables
    private TextArchitect architect;
    private DialogSystem ds => DialogSystem.instance;
    private bool clickedResources;
    private bool clickedGatherMaterial;
    private bool clickedSend;
    private bool sendWasClicked;

    private void Awake()
    {
        instance = this;

        btnConfirmationContainer = controls.GetChild(2).gameObject;
        btnOptionsContainer = controls.GetChild(3).gameObject;
        btnGatherMaterial = controls.GetChild(4).gameObject;

        //ds = DialogSystem.instance;

        //Set variables
        clickedResources = false;
        clickedGatherMaterial = false;
        clickedSend = false;
        sendWasClicked = false;
    }

    public void ShowOptions()
    {
        btnOptionsContainer.SetActive(true);
    }

    

    #region btn Functions
    public void OpenSendPeopleUI()
    {
        if (!sendWasClicked)
        {
            btnOptionsContainer.SetActive(false);
            btnGatherMaterial.SetActive(true);
            ds.Say("Sera","Do you wish to send a scounting team to gather resources? Just know you are putting the lives of your man at risk and will only know how well it went at the end of the day.");
            
        }
        else
        {
            ds.Say("Sera", "Captain!! We are already assembling a team for a material collection mission and need to be mindful about how many of our men we are putting at risk.");
        }

    }

    public void OpenConfirmation()
    {
        btnOptionsContainer.SetActive(false);
        btnConfirmationContainer.SetActive(true);
        string line = "Sera \"Do you wish to start the defense shieft? Just know that you won't have acess to this menu until the start of the next day.\"";
        List<string> lines = new List<string>();
        lines.Add(line);

        DialogSystem.instance.Say(lines);
    }

    public void StartShift()
    {
        btnConfirmationContainer.SetActive(false);
        string line = "Sera \"Ok, lets start. (press space to continue)\" ChangeScene(BasicDesign)";
        List<string> lines = new List<string>();
        lines.Add(line);
        ds.Say(lines);
    }

    public void CloseGatherMaterial()
    {
        btnGatherMaterial.SetActive(false);
        btnOptionsContainer.SetActive(true);
        ds.Say("Sera", "");
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

            if (manPo < nPeople)
            {
                ds.Say("Sera", "We don't have that many people avalable.");
            }
            else
            {
                int newValue = manPo - nPeople;

                manPower.text = "ManPower: " + newValue;
                //manPower.ForceMeshUpdate();
                btnGatherMaterial.SetActive(false);
                ds.Say("Sera", $"Roger! Assambling a team of {newValue} people for a material collection mission.");
                clickedSend = true;
                sendWasClicked = true;
            }
        }

        else
        {
            ds.Say("Sera", "You need to give me number");
        }


    }

    // open resource allucation menu
    public void OpenResourcesAllucationUI()
    {

        ds.Say("Sera", "Sorry, but science chiefs Andros and Jonas are still working on it.");


    }
    #endregion
}

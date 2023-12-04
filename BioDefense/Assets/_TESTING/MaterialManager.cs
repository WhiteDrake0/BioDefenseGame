using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    //public Button gatherButton;
    public int maxPeopleToSend = 5; // Adjust the maximum number of people the player can send

    // Reference to the GameManager or a script managing game state
    //public GameManager gameManager;

    void Start()
    {
       /* // Assuming you have a reference to the GameManager or a script managing game state
        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is missing! Please assign it in the inspector.");
        }

        // Add a listener to the button click event
        gatherButton.onClick.AddListener(GatherMaterials);*/
    }

    void GatherMaterials()
    {
        // Get the number of people the player wants to send (you might have a UI input field for this)
        int peopleToSend = Random.Range(1, maxPeopleToSend + 1); // Example: send a random number between 1 and maxPeopleToSend

        // Calculate the risk based on the number of people sent
        float riskFactor = Mathf.Clamp01((float)peopleToSend / maxPeopleToSend);

        // Determine the outcome (success or failure) based on the calculated risk
        bool success = Random.value > riskFactor;

        // If the gathering is successful, update resources and people accordingly
        if (success)
        {
            int materialsGathered = peopleToSend * Random.Range(1, 4); // Example: gather a random amount of materials
            //gameManager.AddMaterials(materialsGathered);

            int peopleLost = Mathf.RoundToInt((float)peopleToSend * riskFactor);
            //gameManager.LosePeople(peopleLost);

            Debug.Log("Gathering successful! " + materialsGathered + " materials gathered, " + peopleLost + " people lost.");
        }
        else
        {
            int peopleLost = Mathf.RoundToInt((float)peopleToSend * riskFactor);
            //gameManager.LosePeople(peopleLost);

            Debug.Log("Gathering failed! " + peopleLost + " people lost.");
        }

        
    }
}

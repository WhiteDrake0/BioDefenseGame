using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class RootController : MonoBehaviour
    {

        //Variables
        private GameObject characters;
        private GameObject dialogue;
        private bool talking;

        // Start is called before the first frame update
        void Start()
        {

            //initiate variables
            talking = false;

            //Get children transform
            Transform ch = transform.Find("2 - Characters");
            Transform di = transform.Find("4 - Dialogue");

            //Cheack if children was found
            if (ch != null)
            {
                characters = ch.gameObject;
            }
            else
            {
                Debug.LogError("The Characters layer was not found!");
            }

            if (di != null)
            {
                dialogue = di.gameObject;
            }
            else
            {
                Debug.LogError("The dialogue layer was not found!");
            }
        }

        public void StartCharacterDialogue()
        {
            if (!talking)
            {
                characters.SetActive(false);
                dialogue.SetActive(true);
                talking = true;

            }
        }
    }
}
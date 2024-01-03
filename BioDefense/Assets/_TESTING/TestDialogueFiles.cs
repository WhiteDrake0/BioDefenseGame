using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
using TMPro;

namespace Testing
{
    public class TestDialogueFiles : MonoBehaviour
    {
        [SerializeField] private TextAsset fileToRead = null;

        public TextMeshProUGUI people;
        public TextMeshProUGUI material;
        public TextMeshProUGUI days;

        // Start is called before the first frame update
        void Start()
        {
            /* if(PlayerPrefs.HasKey("Materials"))
              material.text = "Material: " + PlayerPrefs.GetInt("Materials").ToString();*/
            material.text = "Material: 5000";
           
            SartConversation();

        }

        void SartConversation()
        {
            List<string> lines = FileManager.ReadTextAsset(fileToRead);

            DialogSystem.instance.Say(lines);
        }
    }
}

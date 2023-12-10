using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace Testing
{
    public class TestDialogueFiles : MonoBehaviour
    {
        [SerializeField] private TextAsset fileToRead = null;

        // Start is called before the first frame update
        void Start()
        {
            SartConversation();

        }

        void SartConversation()
        {
            List<string> lines = FileManager.ReadTextAsset(fileToRead);

            DialogSystem.instance.Say(lines);
        }
    }
}

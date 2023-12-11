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

            foreach(string line in lines)
            {
                DIALOGUE_LINE dl = DialogueParser.Parse(line);

                if (string.IsNullOrEmpty(line))
                    continue;

                for (int i = 0; i < dl.commandsData.commands.Count; i++)
                {
                    DL_COMMAND_DATA.Command command = dl.commandsData.commands[i];
                    Debug.Log($"Command [{i}] '{command.name}' has arguments [{string.Join(", ", command.arguments)}]");
                }
            }
            //DialogSystem.instance.Say(lines);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class ConversationManager
    {
        private DialogSystem dialogSystem => DialogSystem.instance;
        private Coroutine process = null;
        public bool isRunning => process != null;

        private TextArchitect architec = null;
        private bool userPrompt = false;

        public ConversationManager (TextArchitect architect)
        {
            this.architec = architect;
            dialogSystem.onUserPrompt_Next += OnUserPrompt_Next;
        }

        private void OnUserPrompt_Next()
        {
            userPrompt = true;
        }

        public void StartConversation (List<string> conversation)
        {
            StopConversation();
            process = dialogSystem.StartCoroutine(RunningConversation(conversation));
        }

        public void StopConversation()
        {
            if (!isRunning)
                return;

            dialogSystem.StopCoroutine(process);
            process = null;
        }

        IEnumerator RunningConversation(List<string> conversation)
        {
            for(int i = 0; i < conversation.Count; i++)
            {
                //Don't show any blank lines or try to run any logic on then.
                if (string.IsNullOrWhiteSpace(conversation[i]))
                    continue;

                DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);

                //Show dialogue
                if(line.hasDialogue)
                {
                    yield return Line_RunDialogue(line);
                }

                //Run any commands
                if (line.hasCommands)
                {
                    yield return Line_RunCommands(line);
                }

                //yield return new WaitForSeconds(1);
            }
        }

        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            //Show hide speaker name
            if (line.hasSpeaker)
                dialogSystem.ShowSpeakerName(line.speaker);
            else
                dialogSystem.HideSpeakerName();

            //Build dialogue
            yield return BuildDialogue(line.dialogue);

            //Wait for user input
            yield return WaitForUserInput();
        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            Debug.Log(line.commands);
            yield return null;
        }

        IEnumerator BuildDialogue(string dialogue)
        {
            //Build dialogue
            architec.Build(dialogue);

            while (architec.isBuilding)
            {
                if (userPrompt)
                {
                    if (!architec.hurryUp)
                        architec.hurryUp = true;
                    else
                        architec.ForceComplete();

                    userPrompt = false;
                }
                yield return null;
            }
        }

        IEnumerator WaitForUserInput()
        {
            while (!userPrompt)
                yield return null;

            userPrompt = false;
        }

    }
}

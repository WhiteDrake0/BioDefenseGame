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

                if(line.hasDialogue)
                   //Wait for user input
                   yield return WaitForUserInput();
            }
        }

        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            //Show hide speaker name
            if (line.hasSpeaker)
                dialogSystem.ShowSpeakerName(line.speakerData.displayName);

            //Build dialogue
            yield return BuildLineSegments(line.dialogueData);

        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            List<DL_COMMAND_DATA.Command> commands = line.commandsData.commands;

            foreach(DL_COMMAND_DATA.Command command in commands)
            {
                CommandManager.instance.Execute(command.name, command.arguments);
            }

            yield return null;
        }

        IEnumerator BuildLineSegments(DL_DIALOGUE_Data line)
        {
            for(int i = 0; i < line.segments.Count; i++)
            {
                DL_DIALOGUE_Data.DIALOGUE_SEGEMENT segment = line.segments[i];

                yield return WaitForDialogueSegmentSignalToBeTriggered(segment);

                yield return BuildDialogue(segment.dialogue, segment.appendText);
            }
        }

        IEnumerator WaitForDialogueSegmentSignalToBeTriggered(DL_DIALOGUE_Data.DIALOGUE_SEGEMENT segment)
        {
            switch (segment.startSignal)
            {
                case DL_DIALOGUE_Data.DIALOGUE_SEGEMENT.StartSignal.C:
                case DL_DIALOGUE_Data.DIALOGUE_SEGEMENT.StartSignal.A:
                    yield return WaitForUserInput();
                    break;
                case DL_DIALOGUE_Data.DIALOGUE_SEGEMENT.StartSignal.WC:
                case DL_DIALOGUE_Data.DIALOGUE_SEGEMENT.StartSignal.WA:
                    yield return new WaitForSeconds(segment.signalDelay);
                    break;
                default:
                    break;

            }
        }

        IEnumerator BuildDialogue(string dialogue, bool append = false)
        {
            //Build the dialogue
            if (!append)
                architec.Build(dialogue);
            else
                architec.Append(dialogue);

            //wait for the dialogue to complete.
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

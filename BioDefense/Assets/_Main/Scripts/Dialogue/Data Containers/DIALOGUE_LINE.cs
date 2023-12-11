using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{

    public class DIALOGUE_LINE
    {
        public DL_SPEAKER_DATA speakerData;
        public DL_DIALOGUE_Data dialogueData;
        public DL_COMMAND_DATA commandsData;

        public bool hasSpeaker => speakerData != null; 
        public bool hasDialogue => dialogueData != null; 
        public bool hasCommands => commandsData != null; 

        public DIALOGUE_LINE(string speaker, string dialogue, string commands)
        {
            this.speakerData = (string.IsNullOrEmpty(speaker) ? null : new DL_SPEAKER_DATA(speaker));
            this.dialogueData = (string.IsNullOrEmpty(dialogue) ? null : new DL_DIALOGUE_Data(dialogue));
            this.commandsData = (string.IsNullOrEmpty(commands) ? null : new DL_COMMAND_DATA(commands));
        }
    }
}
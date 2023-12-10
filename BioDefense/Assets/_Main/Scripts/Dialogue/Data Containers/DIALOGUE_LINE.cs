using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{

    public class DIALOGUE_LINE
    {
        public DL_SPEAKER_DATA speaker;
        public DL_DIALOGUE_Data dialogue;
        public string commands;

        public bool hasSpeaker => speaker != null; //speaker.hasSpeacker; //speaker != string.Empty;
        public bool hasDialogue => dialogue.hasDialogue; // dialogue != string.Empty;
        public bool hasCommands => commands != string.Empty;

        public DIALOGUE_LINE(string speaker, string dialogue, string commands)
        {
            this.speaker = (string.IsNullOrEmpty(speaker) ? null : new DL_SPEAKER_DATA(speaker));
            this.dialogue = new DL_DIALOGUE_Data(dialogue);
            this.commands = commands;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DIALOGUE
{
    public class DL_DIALOGUE_Data
    {

        public List<DIALOGUE_SEGEMENT> segments;
        private const string segmentIdentifierPattern = @"\{[ca]\}|\{w[ca]\s\d*\.?\d*\}";

        public DL_DIALOGUE_Data(string rawDialogue)
        {
            segments = RipSegments(rawDialogue);
        }

        public List<DIALOGUE_SEGEMENT> RipSegments(string rawDialogue)
        {
            List<DIALOGUE_SEGEMENT> segments = new List<DIALOGUE_SEGEMENT>();
            MatchCollection matchs = Regex.Matches(rawDialogue, segmentIdentifierPattern);

            int lastInddex = 0;

            //Find the first or only segment in the file
            DIALOGUE_SEGEMENT segment = new DIALOGUE_SEGEMENT();
            segment.dialogue = matchs.Count == 0 ? rawDialogue : rawDialogue.Substring(0, matchs[0].Index);
            segment.startSignal = DIALOGUE_SEGEMENT.StartSignal.NONE;
            segment.signalDelay = 0;
            segments.Add(segment);

            if (matchs.Count == 0)
                return segments;
            else
                lastInddex = matchs[0].Index;

            for (int i = 0; i < matchs.Count; i++)
            {
                Match match = matchs[i];
                segment = new DIALOGUE_SEGEMENT();

                //Get the start signal for the segment
                string signalMatch = match.Value;//{A}
                signalMatch = signalMatch.Substring(1, match.Length - 2);
                string[] signalSplit = signalMatch.Split(' ');

                segment.startSignal = (DIALOGUE_SEGEMENT.StartSignal)Enum.Parse(typeof(DIALOGUE_SEGEMENT.StartSignal), signalSplit[0].ToUpper());

                //Get the signal delay
                if (signalSplit.Length > 1)
                    float.TryParse(signalSplit[1], out segment.signalDelay);

                //Get the dialogue for the segment.
                int nextIndex = i + 1 < matchs.Count ? matchs[i + 1].Index : rawDialogue.Length;
                segment.dialogue = rawDialogue.Substring(lastInddex + match.Length, nextIndex - (lastInddex + match.Length));
                lastInddex = nextIndex;

                segments.Add(segment);

            }

            return segments;
        }


        public struct DIALOGUE_SEGEMENT
        {
            public string dialogue;
            public StartSignal startSignal;
            public float signalDelay;

            public enum StartSignal { NONE, C, A, WA, WC }

            public bool appendText => startSignal == StartSignal.A || startSignal == StartSignal.WA;
        }
    }
}
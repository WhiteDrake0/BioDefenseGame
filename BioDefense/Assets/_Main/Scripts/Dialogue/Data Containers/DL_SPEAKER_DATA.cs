using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DIALOGUE
{
    public class DL_SPEAKER_DATA
    {
        public string name, castName;

        /// <summary>
        /// This is the name that will display in the dialogue box to show who is speaking.
        /// </summary>
        public string displayName => castName != string.Empty ? castName : name;

        public Vector2 castPosition;
        public List<(int layer, string expression)> CastExpressions { get; set; }

        private const string NAMECAST_ID = " as ";
        private const string POSITION_ID = " at ";
        private const string EXPRESSION_ID = @" [";
        private const char AXISDELIMETER_ID = ':';
        private const char EXPRESSIONLAYER_JOINER = ',';
        private const char EXPRESSIONLAYER_DELIMITER = ':';

        public bool hasSpeacker => displayName != string.Empty;

        public DL_SPEAKER_DATA(string rawSpeaker)
        {
            string pattern = @$"{NAMECAST_ID}|{POSITION_ID}|{EXPRESSION_ID.Insert(EXPRESSION_ID.Length - 1, @"\")}";
            MatchCollection matchs = Regex.Matches(rawSpeaker, pattern);

            castName = "";
            castPosition = Vector2.zero;
            CastExpressions = new List<(int layer, string expression)>();

            if (matchs.Count == 0)
            {
                name = rawSpeaker;
                return;
            }

            int index = matchs[0].Index;

            name = rawSpeaker.Substring(0, index);

            for (int i = 0; i < matchs.Count; i++)
            {
                Match match = matchs[i];
                int startIndex = 0, endIndex = 0;

                if (match.Value == NAMECAST_ID)
                {
                    startIndex = match.Index + NAMECAST_ID.Length;
                    endIndex = i < matchs.Count - 1 ? matchs[i + 1].Index : rawSpeaker.Length;
                    castName = rawSpeaker.Substring(startIndex, endIndex - startIndex);
                }
                else if (match.Value == POSITION_ID)
                {
                    startIndex = match.Index + POSITION_ID.Length;
                    endIndex = i < matchs.Count - 1 ? matchs[i + 1].Index : rawSpeaker.Length;
                    string castPos = rawSpeaker.Substring(startIndex, endIndex - startIndex);

                    string[] axis = castPos.Split(AXISDELIMETER_ID, System.StringSplitOptions.RemoveEmptyEntries);

                    float.TryParse(axis[0], out castPosition.x);

                    if (axis.Length > 1)
                        float.TryParse(axis[1], out castPosition.y);
                }
                else if (match.Value == EXPRESSION_ID)
                {
                    startIndex = match.Index + EXPRESSION_ID.Length;
                    endIndex = i < matchs.Count - 1 ? matchs[i + 1].Index : rawSpeaker.Length;
                    string castExp = rawSpeaker.Substring(startIndex, endIndex - (startIndex + 1));

                    Debug.Log(i < matchs.Count - 1);

                    CastExpressions = castExp.Split(EXPRESSIONLAYER_JOINER)
                        .Select(x =>
                        {
                            var parts = x.Trim().Split(EXPRESSIONLAYER_DELIMITER);
                            return (int.Parse(parts[0]), parts[1]);

                        }).ToList();
                }
            }
        }

    }
}
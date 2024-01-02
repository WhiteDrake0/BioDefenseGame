using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using DIALOGUE;

namespace Testing
{
    public class TestingCharacters : MonoBehaviour
    {

        private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        // Start is called before the first frame update
        void Start()
        {
            //Character elen = CharacterManager.instance.CreateCharacter("Raelin");
            /*Character sera = CharacterManager.instance.CreateCharacter("sera");
            Character sera2 = CharacterManager.instance.CreateCharacter("sera");
            Character adam = CharacterManager.instance.CreateCharacter("Adam");*/

            StartCoroutine(Test());
        }


        IEnumerator Test()
        {
            Character_Sprite Raelin = CreateCharacter("Raelin") as Character_Sprite;
            Character_Sprite Guard = CreateCharacter("Guard as Generic") as Character_Sprite;

            Guard.SetPosition(Vector2.zero);
            Raelin.SetPosition(new Vector2(1, 0));

            yield return new WaitForSeconds(1);

            Raelin.TransitionSprite(Raelin.GetSprite("A2"));
            Raelin.TransitionSprite(Raelin.GetSprite("A_Shocked"), 1);
            Raelin.Animate("Hop");
            yield return Raelin.Say("Where did this wind chill come from");

            Guard.FaceRight();
            Guard.MoveToPosition(new Vector2(0.1f, 0));
            Guard.Animate("Shiver", true);
            yield return Guard.Say("I'don't know - but I hate it!{a} It's freezing!");

            Raelin.TransitionSprite(Raelin.GetSprite("B2"));
            Raelin.TransitionSprite(Raelin.GetSprite("B_Happy"), 1);
            yield return Raelin.Say("Oh, it's over!");

            Guard.Animate("Shiver", false);
            yield return Guard.Say("Thank the lord...{a} I'm not wearing enough clothes for that crap.");

            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
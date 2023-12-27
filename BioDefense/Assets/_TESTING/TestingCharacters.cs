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
            /* Character_Sprite Guard = CreateCharacter("Guard as Generic") as Character_Sprite;*/

            yield return new WaitForSeconds(1);

            yield return Raelin.UnHighlight();

            yield return new WaitForSeconds(1);

            yield return Raelin.TrantionColor(Color.red);

            yield return new WaitForSeconds(1);

            yield return Raelin.Highlight();

            yield return new WaitForSeconds(1);

            yield return Raelin.TrantionColor(Color.white);


            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
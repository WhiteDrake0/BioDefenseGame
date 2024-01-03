using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

public class HubManager : MonoBehaviour
{
    [SerializeField] private TextAsset fileToRead = null;

    private void Awake()
    {
        SartConversation();
    }

    private void SartConversation()
    {
        List<string> lines = FileManager.ReadTextAsset(fileToRead);
        Debug.Log(lines[0]);
        DialogSystem.instance.Say(lines);
    }
}

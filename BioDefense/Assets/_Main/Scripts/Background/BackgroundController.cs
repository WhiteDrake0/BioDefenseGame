using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Background
{
    public class BackgroundController : MonoBehaviour
    {
        public static BackgroundController instance { get; private set; }

       
        public RawImage backgroundPanel;

        private void Awake()
        {
            instance = this;

        }

        public void SetBackground(string name)
        {
            backgroundPanel.texture = Resources.Load<Texture2D>($"BackgroundImages/{name}");
        }



    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currentUI;
    [SerializeField] Animator animator;

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        animator.SetBool("MenuOpen", isMenuOpen);
    }

    private void OnGUI()
    {
        currentUI.text = LevelManager.main.materials.ToString();
    }

    public void SetSelected()
    {

    }

}

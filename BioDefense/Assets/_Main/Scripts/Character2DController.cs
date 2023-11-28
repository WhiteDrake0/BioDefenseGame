using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    private Animator anim;
    private bool talk;
    
    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponentInParent<Animator>();
        talk = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Debug.Log("Hello captain");

        if (talk)
        {
            anim.SetBool("Talk", true);
        }
    }
}

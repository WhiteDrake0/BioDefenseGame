using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;
    
    // Start is called before the first frame update
    private void Start()
    {
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

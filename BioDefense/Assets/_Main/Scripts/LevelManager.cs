using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int materials;

    // Start is called before the first frame update
    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        materials = 100;
    }

    public void IncreaseMaterials(int amount)
    {
        materials += amount;
    }
    
    public bool SpendMaterials(int amount)
    {
        if (amount <= materials)
        {
            //Buy items
            materials -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enogouht purchase");
            return false;
        }
    }
}

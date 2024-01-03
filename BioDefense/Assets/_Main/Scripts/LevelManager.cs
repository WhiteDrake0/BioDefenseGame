using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;
    public GameObject gameOverUI;
    public int materials;

    // Start is called before the first frame update
    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        materials = 20000;
    }

    public static void Toggle(GameObject gameOverUI, bool state)
    {

        gameOverUI.SetActive(state);
    }

    public void Update()
    {
        if (EnemyMovement.pathIndex1 == 16)
        {
            Time.timeScale = 0;
            gameOverUI.SetActive(true);
        }
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

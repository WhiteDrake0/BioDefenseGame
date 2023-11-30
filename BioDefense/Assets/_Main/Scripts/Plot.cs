using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;


    private GameObject tower;
    private Color startColor;

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
        
    }

    private void OnMouseDown()
    {
        if (tower != null)
        {
            return;
        }

        Tower towerToBeBuilt = BuildManager.main.GetSelectedTower();

        if (towerToBeBuilt.cost > LevelManager.main.materials)
        {
            return;
        }

        LevelManager.main.SpendMaterials(towerToBeBuilt.cost);

        tower = Instantiate(towerToBeBuilt.prefab, transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        startColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

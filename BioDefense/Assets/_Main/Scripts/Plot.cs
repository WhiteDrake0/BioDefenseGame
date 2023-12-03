using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    [SerializeField] public AudioClip buildSoundClip; // Reference to your sound clip
    [SerializeField] private AudioSource audioSource;

    private GameObject towerObj;
    public Turret turret;
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
        if (UIManager.main.isHoveringUI) return;

        if (towerObj != null)
        {
            turret.OpenUpgradeUI();
            return;
        }

        Tower towerToBeBuilt = BuildManager.main.GetSelectedTower();

        if (towerToBeBuilt.cost > LevelManager.main.materials)
        {
            return;
        }

        LevelManager.main.SpendMaterials(towerToBeBuilt.cost);

        towerObj = Instantiate(towerToBeBuilt.prefab, transform.position, Quaternion.identity);

        turret = towerObj.GetComponent<Turret>();


        if (buildSoundClip != null)
        {
            // Play the sound
            audioSource.PlayOneShot(buildSoundClip);
        }

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

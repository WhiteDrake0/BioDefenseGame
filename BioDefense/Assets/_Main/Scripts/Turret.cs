using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    [SerializeField]  public AudioClip yourSoundClip; // Reference to your sound clip
    [SerializeField]  private AudioSource audioSource;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float bps = 1f; //bullets per second
    [SerializeField] private int baseUpgradeCost = 100; //bullets per second

    private float bpsBase;
    private float targetingRangeBase;

    private Transform target;
    private float timeUntilFire;

    private int level = 1;

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);

    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length>0)
        {
            target = hits[0].transform;
        }
    }
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation,targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <=targetingRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        bpsBase = bps;
        targetingRangeBase = targetingRange;

        upgradeButton.onClick.AddListener(Upgrade);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return; 
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if(timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);

        if (yourSoundClip != null)
        {
            // Play the sound
            audioSource.PlayOneShot(yourSoundClip);
        }
    }


    public void Upgrade()
    {
        if(baseUpgradeCost > LevelManager.main.materials) return;

        LevelManager.main.SpendMaterials(CalculateCost());

        level++;

        bps = CalculateBPS();

        targetingRange = CalculateRange();

        CloseUpgradeUI();
    }

    private float CalculateRange()
    {
        return bpsBase * Mathf.Pow(level, 0.4f);
    }

    private float CalculateBPS()
    {
        return bpsBase * Mathf.Pow(level, 0.5f);
    }

    private int CalculateCost()
    {
        //every level makes the upgrades more expensive
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

}

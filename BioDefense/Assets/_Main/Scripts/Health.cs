using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int materialsWorth = 50;

    [SerializeField] private GameObject prefabToInstantiate;

    private bool isDestroyed = false;

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseMaterials(materialsWorth);
            isDestroyed = true;

            if (prefabToInstantiate!=null)
            {
                // Instantiate the prefab
                GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}

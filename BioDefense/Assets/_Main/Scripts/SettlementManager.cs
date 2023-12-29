// SettlementManager.cs
using UnityEngine;

public class SettlementManager : MonoBehaviour
{
    public GameObject settlementStructurePrefab;
    public int maxStructureLevel = 4; // Maximum upgrade level

    private int currentStructureLevel = 1;

    void Start()
    {
        SpawnInitialStructure();
    }

    void SpawnInitialStructure()
    {
        // Instantiate the initial structure
        Instantiate(settlementStructurePrefab, transform.position, Quaternion.identity);
    }

    public void UpgradeStructure()
    {
        if (currentStructureLevel < maxStructureLevel)
        {
            currentStructureLevel++;

            // Destroy the current structure
            Destroy(GameObject.FindGameObjectWithTag("SettlementStructure"));

            // Instantiate the upgraded structure
            Instantiate(settlementStructurePrefab, transform.position, Quaternion.identity);
        }
    }
}

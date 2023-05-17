using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Reference to the BuildManager script
    BuildManager buildManager;

    void Start()
    {
        // Get the instance of the BuildManager script
        buildManager = BuildManager.instance;
    }

    // Method for purchasing the standard turret
    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret Purchased");
        // Set the turret to build in the BuildManager to the standard turret prefab
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    // Method for purchasing the laser turret
    public void PurchaseLaserTurret()
    {
        Debug.Log("Laser Turret Purchased");
        // Set the turret to build in the BuildManager to the laser turret prefab
        buildManager.SetTurretToBuild(buildManager.laserTurretPrefab);
    }

    // Method for purchasing the missile turret
    public void PurchaseMissileTurret()
    {
        Debug.Log("Missile Turret Purchased");
        // Set the turret to build in the BuildManager to the missile turret prefab
        buildManager.SetTurretToBuild(buildManager.missileTurretPrefab);
    }
}
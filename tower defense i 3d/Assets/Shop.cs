using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }
    public void PurchaseLaserTurret()
    {
        Debug.Log("Laser Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.laserTurretPrefab);
    }
    public void PurchaseMissileTurret()
    {
        Debug.Log("Missile Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.missileTurretPrefab);
    }
}

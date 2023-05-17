using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Singleton instance of the BuildManager
    public static BuildManager instance;

    void Awake()
    {
        // Ensure there is only one instance of the BuildManager
        if (instance != null)
        {
            Debug.LogError("BuildManager called more than once");
            return;
        }
        instance = this;
    }

    // Prefabs for different types of turrets
    public GameObject standardTurretPrefab;
    public GameObject laserTurretPrefab;
    public GameObject missileTurretPrefab;

    private GameObject turretToBuild;

    // Get the currently selected turret to build
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    // Set the turret to build
    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
}
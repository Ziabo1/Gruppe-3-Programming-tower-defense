using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{   
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("BuildManager called more than once");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject laserTurretPrefab;
    public GameObject missileTurretPrefab;

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild ()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild (GameObject turret)
    {
        turretToBuild = turret;
    }   
}

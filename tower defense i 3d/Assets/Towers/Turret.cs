using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    //"public float range = 7.5f;" defines the range of the turret
    private float range = 7.5f;

    public string enemyTag = "Enemy";

    public Transform partToRotate;

    // Start is called before the first frame update.
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    //Following lines are used to search for enemies, that the turret can lock on to. It searches for enemies with the tag "enemies".
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
    }
    // Update is called once per frame
    void Update()
        {       
            if (target == null)
                return;
        //Following code makes the turret look at enemy entities
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
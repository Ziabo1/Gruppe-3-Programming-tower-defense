using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    //"public float range = 7.5f;" har til opgave og vise hvor lang rækkevide følgende turret har
    public float range = 7.5f;

    public string enemyTag = "Enemy";

    // Start is called before the first frame update.
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    //Følgende linjer bruges til at søge efter fjender som tårnet kan skyde på. Den søger efter fjender som har tagget "enemies".
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance);
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
        }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
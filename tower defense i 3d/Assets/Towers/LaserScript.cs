using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    private Transform target;

    [Header("Attributes")]

    //"public float range = x;" defines the range of the turret. By making it public, it can be adjusted in the unity editor.
    public float range = 30;
    //"fireRate" is the rate at which the tower shoots
    public float fireRate = 1f;
    //"fireCountdown" det ermines the cooldown of when the tower can fire.
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    private float turnSpeed = 30f;

    public GameObject bullePrefab;
    public Transform firePoint;

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
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        //Following block calculates the rate of fire. It tells the turret to fire after x ammount of time determidened by the equation "fireCountdown=1f/fireRate"
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        //following code makes sure the firecountdown is reduced by one every second
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bullePrefab, firePoint.position, firePoint.rotation);
        mgBulletScript bullet = bulletGO.GetComponent<mgBulletScript>();

        if (bullet != null)
            bullet.Seek(target);
    }

    //These codes can be deleted later. It just shows what the given range is in the unity editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

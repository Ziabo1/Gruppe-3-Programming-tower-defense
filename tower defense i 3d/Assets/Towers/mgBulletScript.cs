using UnityEngine;

public class mgBulletScript : MonoBehaviour
{
    private Transform target;
    public float speed = 0f;
    public float explosionRadius = 0f;
    public float damage = 0f;

    // Set the target for the bullet to seek
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the target is null or destroyed
        if (target == null)
        {
            // If the target is null, destroy the bullet
            Destroy(gameObject);
            return;
        }

        // Calculate the direction and distance to the target
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Check if the target is within reach
        if (dir.magnitude <= distanceThisFrame)
        {
            // If the target is reached, hit the target
            HitTarget();
            return;
        }

        // Move the bullet towards the target
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    // Handle hitting the target (either through direct collision or within explosion radius)
    void HitTarget()
    {
        if (explosionRadius > 0f)
        {
            // If there is an explosion radius, damage all enemies within the radius
            Explode();
        }
        else
        {
            // If there is no explosion radius, damage the target directly
            Damage(target);
        }

        // Destroy the bullet after hitting the target
        Destroy(gameObject);
    }

    // Handle damaging enemies within the explosion radius
    void Explode()
    {
        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Damage all enemies within the explosion radius
                Damage(collider.transform);
            }
        }
    }

    // Damage the enemy
    void Damage(Transform enemy)
    {
        // Get the FollowWP script component attached to the enemy
        FollowWP enemyScript = enemy.GetComponent<FollowWP>();

        // Check if the enemy script is found
        if (enemyScript != null)
        {
            // Call the TakeDamage method in the FollowWP script to apply damage
            enemyScript.TakeDamage(damage);
        }
    }

    // Draw the explosion radius gizmo in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    // Handle the collision events with trigger colliders
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Damage the enemy and destroy the projectile
            Damage(other.transform);
            Destroy(gameObject);
        }
    }
}
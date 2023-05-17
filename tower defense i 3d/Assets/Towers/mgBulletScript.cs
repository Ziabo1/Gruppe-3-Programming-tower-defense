using UnityEngine;

public class mgBulletScript : MonoBehaviour
{
    private Transform target;
    public float speed = 0;
    public float explosionRadius = 0f;
    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[]colliders=Physics.OverlapSphere(transform.position,explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag=="Enemy")
            {
                Damage(collider.transform);
            }
        }
    }
    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

    //These codes can be deleted later. It just shows what the given range is in the unity editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
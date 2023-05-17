using UnityEngine;

public class mgBulletScript : MonoBehaviour
{
    private Transform target;
    private float speed = 200;
    public int damage = 50;
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
    }

    void Damage (Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
    public 
    void HitTarget()
    {
        Debug.Log("WE HIT SOMETHING");
    }
}
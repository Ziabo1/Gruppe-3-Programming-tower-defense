using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    //Laver en array som hedder waypoints som fortæller hvilken vej objects skal føgle
    public GameObject[] waypoints;
    //laver en int variabel som holder styr på hvor mange waypoints der er
    int currentWP = 0;

    // laver så man i unity skal skifte farten, roteringsfarten og afstanden foran objektet, hvor det næste waypoint skal kontrolleres.
    public float speed = 100.0f;
    public float rotSpeed = 25.0f;
    public float lookAhead = 200.0f;

    public GameObject tracker;
    // Start is called before the first frame update
    public void Start()
    {
        // laver en cylinder gameobject hvor man fjerner collideren og gør den usynlig.   
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        //
        tracker.transform.position = this.transform.position;
        tracker.transform.rotation = this.transform.rotation;
    }
    public void ProgressTracker()
    {
        if (Vector3.Distance(tracker.transform.position, this.transform.position) > lookAhead) return;

        if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 3)
            currentWP++;

        if (currentWP >= waypoints.Length)
            currentWP = 0;

        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0, 0, (speed + 1) * Time.deltaTime);
    }

    // Update is called once per frame
    public void Update()
    {
        ProgressTracker();



        Quaternion lookatWP = Quaternion.LookRotation(tracker.transform.position - this.transform.position);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookatWP, rotSpeed * Time.deltaTime);

        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}

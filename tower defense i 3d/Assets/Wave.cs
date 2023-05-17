using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemy; // Prefab of the enemy object for the wave
    public int Count; // Number of enemies in the wave
    public int Health; // Health of each enemy in the wave

}

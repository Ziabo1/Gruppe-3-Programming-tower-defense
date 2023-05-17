using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    // Reference to the player's camera
    [SerializeField] private Camera PlayerCamera;

    // The currently selected turret for placement
    private GameObject CurrentPlacingTurret;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code goes here (if needed)
    }

    // Update is called once per frame
    void Update()
    {
        // Check if a turret is currently selected for placement
        if (CurrentPlacingTurret != null)
        {
            // Cast a ray from the player's camera to the mouse position on the screen
            Ray camRay = PlayerCamera.ScreenPointToRay(Input.mousePosition);

            // Check if the ray hits any objects within a maximum distance of 10000 units
            if (Physics.Raycast(camRay, out RaycastHit hitInfo, 10000f))
            {
                // Update the position of the current turret to the hit point
                CurrentPlacingTurret.transform.position = hitInfo.point;
            }

            // Check if the left mouse button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                // Set the currently placing turret to null, indicating placement is finished
                CurrentPlacingTurret = null;
            }
        }
    }

    // Method for setting the turret to place
    public void SetTurretToPlace(GameObject Turret)
    {
        // Instantiate the specified turret at position (0, 0, 0) with no rotation (identity)
        CurrentPlacingTurret = Instantiate(Turret, Vector3.zero, Quaternion.identity);
    }
}
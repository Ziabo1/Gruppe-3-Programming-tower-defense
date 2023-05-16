using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{

    [SerializeField] private Camera PlayerCamera;


    private GameObject CurrentPlacingTurret;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentPlacingTurret != null)
        {
            Ray camray = PlayerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camray, out RaycastHit hitInfo, 100f))
            {
                CurrentPlacingTurret.transform.position = hitInfo.point;
            }

            if (Input.GetMouseButtonDown(0))
            {
                CurrentPlacingTurret = null;
            }
        }
    }

    public void SetTurretToPlace(GameObject Turret)
    {
        CurrentPlacingTurret = Instantiate(Turret, Vector3.zero, Quaternion.identity);
    }

}

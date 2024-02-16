using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Camera mainCam;
    UnitManager unitManager;


    private void Start()
    {
        mainCam = Camera.main;
        unitManager = UnitManager.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Vector3 mousePos = Input.mousePosition;
            //mousePos.z = -mainCam.transform.position.z;

            //Vector3 worldPoint = mainCam.ScreenToWorldPoint(mousePos);
            //Debug.Log(mousePos);

            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                unitManager.MovePosition(hit.point);
            }


        }
    }
}

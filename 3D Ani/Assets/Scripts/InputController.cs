using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        moving();
    }

    private void moving()
    {
        anim.SetFloat("SpeedVertical", Input.GetAxis("Vertical"));
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    Animator anim;
    [SerializeField] Transform trsLookAt;
    [SerializeField, Range(0.1f, 1f)] float lookAyWeight;

    List<string>//채우기


    private void OnAnimatorIK(int layerIndex)
    {
        if(trsLookAt != null)
        {
            anim.SetLookAtWeight(0.1f);
            anim.SetLookAtPosition(trsLookAt.position);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        //버튼이 만들어지는 시스템 만들예정
    }


    // Update is called once per frame
    void Update()
    {
        moving();
        doDance();
    }

    private void moving()
    {
        anim.SetFloat("SpeedVertical", Input.GetAxis("Vertical"));
        anim.SetFloat("SpeedHorizontal", Input.GetAxis("Horizontal"));

        //anim.SetBool("front", Input.GetKey(KeyCode.W));
        //anim.SetBool("back", Input.GetKey(KeyCode.S));
        //anim.SetBool("right", Input.GetKey(KeyCode.D));
        //anim.SetBool("left", Input.GetKey(KeyCode.A));

    }
    
    private void doDance()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //anim.Play("Dance1");
            anim.CrossFade("Dance1", 0.2f);//절대값이 아님, 노말라이즈 값은 0~1까지
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //anim.Play("Dance2");
            anim.CrossFade("Dance2", 0.2f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //anim.Play("Dance3");
            anim.CrossFade("Dance3", 0.2f);
        }

        if(Input.GetAxis("Vertical") != 0.0 || Input.GetAxis("Horizontal") != 0.0f)
        {
            anim.Play("move");
        }
    }
}

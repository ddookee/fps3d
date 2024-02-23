using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    Animator anim;
    [SerializeField] Transform trsLookAt;
    [SerializeField, Range(0.1f, 1f)] float lookAyWeight;

    List<string>listDanceStateName = new List<string>();

    [SerializeField] GameObject objIven;
    [SerializeField] GameObject objButton;

    Dictionary<string, string> dicNameValue = new Dictionary<string, string>();

    [SerializeField, Range(0.0f, 1.0f)] float distanceToGround;


    private void OnAnimatorIK(int layerIndex)
    {
        if(trsLookAt != null)
        {
            anim.SetLookAtWeight(lookAyWeight);
            anim.SetLookAtPosition(trsLookAt.position);
        }

        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);


        if (Physics.Raycast(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, 
            Vector3.down, 
            out RaycastHit lefthit,
            distanceToGround + 1f, LayerMask.GetMask("Ground")))
        {
            Vector3 footPos = lefthit.point;
            footPos.y += distanceToGround;

            anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPos);

            anim.SetIKRotation(AvatarIKGoal.LeftFoot, 
                Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, lefthit.normal),
                lefthit.normal));
        }

        if (Physics.Raycast(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, 
            Vector3.down, 
            out RaycastHit righthit,
            distanceToGround + 1f, LayerMask.GetMask("Ground")))
        {
            Vector3 footPos = righthit.point;
            footPos.y += distanceToGround;

            anim.SetIKPosition(AvatarIKGoal.RightFoot, footPos);

            anim.SetIKRotation(AvatarIKGoal.RightFoot,
                Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, righthit.normal),
                righthit.normal));
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        dicNameValue.Add("Dance_1", "어떤춤1");
        dicNameValue.Add("Dance_2", "어떤춤2");
        dicNameValue.Add("Dance_3", "어떤춤3");
    }

    // Start is called before the first frame update
    void Start()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        //버튼이 만들어지는 시스템 만들예정
        initDance();
        createDanceUi();
    }


    // Update is called once per frame
    void Update()
    {
        moving();
        doDance();
        activeDanceIventory();
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

    private void activeDanceIventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = objIven.activeSelf;
            objIven.gameObject.SetActive(isActive);
        }
    }

    private void initDance()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        int count = clips.Length;
        for(int iNum = 0; iNum < count; ++iNum)
        {
            string animName = clips[iNum].name;
            if(animName.Contains("Dance_"))//Contains : 포함하는지
            {
                listDanceStateName.Add(animName);
            }
        }
    }

    private void createDanceUi()
    {
        Transform parent = objIven.transform;
        int count = listDanceStateName.Count;
        for(int iNum = 0;iNum < count; ++iNum)
        {
            int Number = iNum;

            GameObject obj = Instantiate(objButton, parent);


            TMP_Text objText = obj.GetComponent<TMP_Text>();
            string curName = listDanceStateName[iNum];
            objText.text = dicNameValue[curName];


            Button objBtn = obj.GetComponent<Button>();
            objBtn.onClick.AddListener(() =>
            {
                anim.CrossFade(listDanceStateName[Number], 0.1f);
            });
        }
    }
    
    private void doDance()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //anim.Play("Dance1");
            anim.CrossFade("Dance_1", 0.2f);//절대값이 아님, 노말라이즈 값은 0~1까지
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //anim.Play("Dance2");
            anim.CrossFade("Dance_2", 0.2f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //anim.Play("Dance3");
            anim.CrossFade("Dance_3", 0.2f);
        }

        if(Input.GetAxis("Vertical") != 0.0 || Input.GetAxis("Horizontal") != 0.0f)
        {
            anim.Play("move");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class FunctionLight : MonoBehaviour
{
    public enum TypeLight
    {
        Disable,//ºñ»ç¿ë
        Allaways,//Ç×»ó
        OnlyNight,//¹ã¿¡¸¸
        OnlyDays,//³·¿¡¸¸
    }

    public TypeLight typeLight;

    Material matWindow;
    GameObject objLight;


    public void init(bool _isNight)
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        matWindow = Instantiate(mr.material);
        mr.material = matWindow;

        objLight = GetComponentInChildren<Light>().gameObject;
        //°°À½
        //objLight = transform.GetChild(0).gameObject;
        //objLight = transform.Find("Point Light").gameObject;


        //matWindow.EnableKeyword("_EMISSION");//ÄÓ¶§
        if (typeLight == TypeLight.Disable)
        {

            matWindow.DisableKeyword("_EMISSION");//²¨Áü
            objLight.SetActive(false);
        }
        if ((_isNight == true && typeLight == TypeLight.OnlyNight) ||
           (_isNight == false && typeLight == TypeLight.OnlyDays) ||
           typeLight == TypeLight.Allaways)
        {
            TurnOnLight(true);
        }
        //else if(_isNight == false && typeLight == TypeLight.OnlyDays)
        //{
        //    matWindow.EnableKeyword("_EMISSION");
        //    objLight.SetActive(true);
        //}
        //else if(typeLight == TypeLight.Allaways)
        //{
        //    matWindow.EnableKeyword("_EMISSION");
        //    objLight.SetActive(true);
        //}
        else
        {
            TurnOnLight(false);
        }
    }

    public void TurnOnLight(bool _value)
    {
        if(_value == true)
        {
            matWindow.EnableKeyword("_EMISSION");
            objLight.SetActive(true);
        }
        else
        {
            matWindow.DisableKeyword("_EMISSION");//²¨Áü
            objLight.SetActive(false);
        }
    }
}

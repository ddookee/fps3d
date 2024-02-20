using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    [SerializeField] RectTransform rectTrs;

    Rect selectRect;
    Vector2 vecStart;//Ŭ�� ������
    Vector2 vecEnd;//Ŭ�� ������ �̵���

    UnitManager unitManager;

    private void Start()
    {
        rectTrs.gameObject.SetActive(false);
        unitManager = UnitManager.Instance;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0)))//Ŭ�� ����
        {
            unitManager.ClearAllSelectUnit();
            vecStart = Input.mousePosition;
            rectTrs.gameObject.SetActive (true);
            selectRect = new Rect();
        }

        if ((Input.GetKey(KeyCode.Mouse0)))//Ŭ�� ��
        {
            vecEnd = Input.mousePosition;
            drawSelector();
        }

        if ((Input.GetKeyUp(KeyCode.Mouse0)))//�巡�� ����
        {
            checkSelectedUnit();
            rectTrs.gameObject.SetActive(false);
        }

        
    }

    private void drawSelector()
    {
        Vector2 vecCenter = (vecStart + vecEnd) * 0.5f;
        rectTrs.position = vecCenter;

        float sizeX = Mathf.Abs(vecStart.x - vecEnd.x);
        float sizeY = Mathf.Abs(vecStart.y - vecEnd.y);

        rectTrs.sizeDelta = new Vector2(sizeX, sizeY);
    }

    private void checkSelectedUnit()
    {
        selectRect.xMin = vecEnd.x < vecStart.x ? vecEnd.x : vecStart.x;
        selectRect.xMax = vecEnd.x < vecStart.x ? vecStart.x : vecEnd.x;

        selectRect.yMin = vecEnd.y < vecStart.y ? vecEnd.y : vecStart.y;
        selectRect.yMax = vecEnd.y < vecStart.y ? vecStart.y : vecEnd.y;

        unitManager.SelectUnit(selectRect);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{

    public static UnitManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }
    }

    private List<Player> listPlayer = new List<Player>();

    public void AddUnit(Player _unit)
    {
        //if (listPlayer.Exists((x) => x == _unit) == false)
        //{
            listPlayer.Add(_unit);
        //}

    }

    public void RemoveUnit(Player _unit)
    {
        listPlayer.Remove(_unit);
    }

    public void MovePosition(Vector3 pos)
    {
        int count = listPlayer.Count;
        for(int iNum = 0; iNum < count; ++iNum)
        {
            Player unit = listPlayer[iNum];
            unit.SetDestination(pos);
        }

        //È¤Àº
        //foreach(Player unit in listPlayer)
        //{
        //    unit.SetDestination(pos);
        //}
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 vecDestination;
    Vector3 startPosition;
    [SerializeField] float randomRadiusRange = 30f;
    [SerializeField] bool selected = false;


    float waitingTime;//도착후에 잠깐 기다리는 시간
    [SerializeField] Vector2 vecWaitingMinMax;


    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, randomRadiusRange, NavMesh.AllAreas))
        {
            startPosition = hit.position;
        }
    }


    private void OnDestroy()
    {
        //1.동적으로 플레이중에 알고리즘에 의해서 삭제
        //UnitManager.Instance.RemoveUnit(this);

        //2.어떤조건에 의해서 데이터가 삭제되어야 할때 (ex 에디터에서 플레이가 끝났을떄)
        if(UnitManager.Instance != null)
        {
            UnitManager.Instance.RemoveUnit(this);
        } 
    }

    private void Start()
    {
        //setNewPath();
        //setNewWaitTime();
        if (selected == false) return;

        UnitManager.Instance.AddUnit(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(isArrive() == true)//새로운 이동 위치를 잡아줌
        {
            if (checkWaitTime() == true) return;

            setNewPath();
        }
    }

    /// <summary>
    /// true가 된다면 기달,false가 된다면 이동
    /// </summary>
    private void setNewWaitTime()
    {
        waitingTime = Random.Range(vecWaitingMinMax.x, vecWaitingMinMax.y);
    }

    private bool checkWaitTime()
    {
        if (waitingTime >= 0.0f)//기다려야 하는 시간이 0.0이 아니라면
        {
            waitingTime -= Time.deltaTime;//시간을 감소시킨다
            if (waitingTime < 0.0f)//만약 감소시킨 시간 0.0 이하가된다면
            {
                setNewWaitTime();//새 기다리는 시간을 정의
                return false;//이동하라고 전달
            }

            return true;
        }
        return false;
    }

    private void setNewPath()
    {
        vecDestination = getRandomPoint();
        agent.SetDestination(vecDestination);

        //agent.SetDestination(getRandomPoint()); 코드를 잘알면 이렇게 쳐도됨
    }

    /// <summary>
    /// Npc가 도착했는지 확인합니다.
    /// </summary>
    /// <returns></returns>
    private bool isArrive()
    {
        if(agent.velocity == Vector3.zero)//가만히 있는 상태, 이동불가능한 상황에 닥쳐서 멈춰있음
        {
            return true;
        }
        //if(Vector3.Distance(vecDestination, transform.position)== 0.0f)
        //{
        //    return true;
        //}
        
        return false;
    }


    /// <summary>
    /// 에이전트가 이동 가능한 위치를 스스로 체크해서 전달합니다.
    /// </summary>
    /// <returns></returns>
    private Vector3 getRandomPoint()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * randomRadiusRange;

        if(NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, randomRadiusRange, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return startPosition;
    }

    public void SetDestination(Vector3 _pos)
    {
        agent.SetDestination(_pos);
    }
}

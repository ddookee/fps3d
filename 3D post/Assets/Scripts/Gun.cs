using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject objHitHole;//ÇÁ¸®Æé
    [SerializeField] Transform trsTarget;
    [SerializeField] Light hitLight;
    [SerializeField] Light muzzleLight;
    LineRenderer lineRenderer;

    [SerializeField] Transform trsMuzzle; 

    short shootCount;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        beforeShooting();
        trsMuzzle = transform.GetChild(1);
    }

    void Update()
    {
        lookTarget();
        shootTarget();
        
        if(lineRenderer.enabled == true)
        {
            lineRenderer.SetPosition(0, trsMuzzle.position);
        }
    }

    private void lookTarget()
    {
        if (objHitHole != null) return;

        transform.LookAt(trsTarget);
    }

    private void shootTarget()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&& 
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 10.0f,LayerMask.GetMask("Target")))
        {
            createHole(hit);
        }
    }

    private void createHole(RaycastHit _hit)
    {
        GameObject obj = Instantiate(objHitHole, _hit.point + _hit.normal * 0.0001f, 
            Quaternion.FromToRotation(Vector3.forward, _hit.normal));
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.sortingOrder = shootCount++;

        if(shootCount >= 32767)
        {
            shootCount = 0;
        }

        
        lineRenderer.SetPosition(1,_hit.point);

        hitLight.transform.position = _hit.point + _hit.normal * 0.1f;
        hitLight.gameObject.SetActive(true);
        muzzleLight.gameObject.SetActive(true);
        lineRenderer.gameObject.SetActive(true);
        

        Invoke("beforeShooting", 0.1f);
    }

    private void beforeShooting()
    {
        hitLight.gameObject.SetActive(false);
        muzzleLight.gameObject.SetActive(false);
    }
}

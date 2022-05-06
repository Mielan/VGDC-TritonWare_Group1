using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Transform aim;

    [SerializeField]
    private float maxDist = 20;

    [SerializeField]
    private LineRenderer lazer;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private SoldierScript soldier;

    [SerializeField]
    private Material defaultLazerColor;

    [SerializeField]
    private Material hitLazerColor;

    private Camera cam;

    public bool setAim = false;

    private Vector3 aimDir;

    private Vector3 aimPos;

    private Selector selector;

    void Start()
    {
        cam = Camera.main;
        selector = soldier.selector;
    }

    public void Fire() 
    {
        RaycastHit hit;
        if (Physics.Raycast(aim.position, aimDir, out hit, maxDist, layerMask))
        {
            Transform rayHit = hit.transform;
            if (rayHit.tag.Equals("Soldier"))
            {
                rayHit.GetComponent<SoldierScript>().Die();
            }
        }
    }

    void Lazer() 
    {
        RaycastHit hit;
        lazer.SetPosition(0, aim.position);
        
        //print(cam.ScreenToWorldPoint(aimPos));
        if (Physics.Raycast(aim.position, aimDir, out hit, maxDist, layerMask))
        {
            lazer.SetPosition(1, aim.position + (aimDir * hit.distance));
            if (hit.transform.tag.Equals("Soldier"))
            {
                lazer.material = hitLazerColor;
            }
            else 
            {
                lazer.material = defaultLazerColor;

            }

        }
        else
        {
            lazer.SetPosition(1, aim.position + (aimDir * maxDist));

        }
    }



    private void FixedUpdate()
    {
        if (soldier.selected && setAim == false)
        {
            aimPos = Input.mousePosition;
            aimPos.z = 50;
            aimDir = (cam.ScreenToWorldPoint(aimPos) - aim.position).normalized;
            Lazer();
        }
        else if (setAim) 
        {
            Lazer();
        }
            
    }
    private void Update()
    {
        if (soldier.selected)
        {
            lazer.enabled = true;
            if (Input.GetButtonDown("Fire")) 
            {

                setAim = !setAim;
                aimDir = (cam.ScreenToWorldPoint(aimPos) - aim.position).normalized;
                soldier.ToggleLock();
                //soldier.readyToFire = setAim;
                if (setAim)
                {
                    selector.AddToFire(soldier);
                }
                else 
                {
                    selector.RemoveToFire(soldier);

                }

            }
        }
        else if (setAim == false)
        {
            lazer.enabled = false;
        }
    }
}

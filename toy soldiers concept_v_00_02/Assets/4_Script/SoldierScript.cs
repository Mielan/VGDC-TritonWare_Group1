using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour
{

    [SerializeField]
    private Transform body;

    [SerializeField]
    private ConfigurableJoint configJoint;

    [SerializeField]
    private Transform cameraPerspective;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private float moveSpeed;

    public Gun gun;

    public Selector selector;

    //public bool readyToFire = false;


    public bool team = false;


    public bool dead = false;

    //[SerializeField]
    //private int index;

    //[SerializeField]
    //private bool team;
    public bool selected;
    private float moveValue;
    private Camera cam;
    private float turnValue;
    private bool locked = false;

    [SerializeField]
    private MeshRenderer meshRend;

    void Start()
    {
        cam = Camera.main;
    }

    public void ToggleLock() 
    {
        locked = !locked;
        rb.isKinematic = locked;
    }

    public void Fire() 
    {
        gun.Fire();
    }

    public void Die() 
    {
        dead = true;
        selector.soldiersTeamA.Remove(this);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (selected)
        {
            if (locked == false)
            {
                turnValue += Input.GetAxis("Horizontal");
                configJoint.targetRotation = Quaternion.Euler(0, configJoint.targetRotation.y + (-turnSpeed * turnValue), 0);
                moveValue = Input.GetAxis("Vertical");
            }
            cam.transform.position = cameraPerspective.transform.position;
            cam.transform.rotation = cameraPerspective.transform.rotation;
            if (meshRend.enabled == false)
            {
                meshRend.enabled = true;
            }
        }
        else 
        {
            if (meshRend.enabled == true)
            {
                meshRend.enabled = false;
            }
        }

    }

    private void FixedUpdate()
    {
        if (selected && locked == false)
        {
            rb.velocity = body.rotation * new Vector3(0, rb.velocity.y, moveValue * moveSpeed);
        }
    }
}

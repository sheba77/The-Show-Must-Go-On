using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    
    public PlayerController thePlayer;
 

    public bool grounded = false;
    //private int resetDelta = 1;
    private Rigidbody2D myRb;
    private Transform myParent;
    private float PLAYERY;

    protected Collider2D myCollider;
    // Start is called before the first frame update
    protected void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        myRb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myParent = transform.parent;
        PLAYERY = thePlayer.transform.position.y;

    }

    // Update is called once per frame
    protected void Update()
    {
        
        if (grounded)
        {
            myRb.velocity = Vector3.zero;
        }

        if (transform.position.x < thePlayer.transform.position.x || 
            transform.position.y < PLAYERY - 2)
        {
            resetAtt();
        }
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("catcher"))
        {
            grounded = true;
            myRb.gravityScale = 0;
            myCollider.isTrigger = true;
            transform.SetParent(null);
        }
    }

    public void resetAtt()
    {
        grounded = false;
        myRb.velocity = Vector3.zero;
        myRb.angularVelocity = 0f;
        myRb.gravityScale = 0;
        myRb.simulated = true;
        myCollider.isTrigger = false;
        transform.SetParent(myParent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        myCollider.enabled = true;
        gameObject.SetActive(false);
    }

    public void drop()
    {
        if (!grounded)
        {
            myRb.gravityScale = 8;
        }
    }
}
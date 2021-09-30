using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public PlayerController thePlayer;
    public float playerHeight = 1.5f;
    public Transform generator;
    private int resetDelta = 15;

    private Rigidbody2D myRb;

    private BoxCollider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        myRb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < thePlayer.transform.position.x - resetDelta || 
            transform.position.y < thePlayer.transform.position.y - 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void turnOn()
    {
        resetEnemy();
        gameObject.SetActive(true);
        var pos = generator.position;
        transform.position = new Vector3(pos.x, pos.y + playerHeight, pos.z);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player-bird");
        }
        
        if (other.gameObject.CompareTag("bag"))
        {
            myCollider.enabled = false;
        }
    }

    public void resetEnemy()
    {
        Debug.Log("rb was called");
        myRb.velocity = Vector3.zero;
        myRb.angularVelocity = 0f;
        myRb.gravityScale = 0;
        transform.localRotation = Quaternion.identity;
        myCollider.enabled = true;
        gameObject.SetActive(false);
    }
}

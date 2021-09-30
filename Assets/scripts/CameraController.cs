using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController thePlayer;
    private Vector3 lastPlayerPosition;
    private float ditanceToMove;
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        lastPlayerPosition = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ditanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;
        transform.position = new Vector3(transform.position.x + ditanceToMove, transform.position.y, transform.position.z);
        lastPlayerPosition = thePlayer.transform.position;

    }
}

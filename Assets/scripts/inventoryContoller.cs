using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class inventoryContoller : MonoBehaviour
{
    public PlayerController thePlayer;
    private Vector3 lastPlayerPosition;
    private float ditanceToMove;
    private Vector3 inventoryStartPoint;
    private ToolController bagToolController;
    private ToolController jumperAToolController;
    private ToolController jumperBToolController;
    private bool useNext;

    private Animator paleAnimator;

    // Start is called before the first frame update
    void Start()
    {
        useNext = false;
        thePlayer = FindObjectOfType<PlayerController>();
        lastPlayerPosition = thePlayer.transform.position;
        inventoryStartPoint = transform.position;
        bagToolController = transform.GetChild(0).gameObject.GetComponent<ToolController>();
        
        jumperAToolController = transform.GetChild(1).gameObject.GetComponent<ToolController>();
        jumperBToolController = transform.GetChild(2).gameObject.GetComponent<ToolController>();


        bagToolController.gameObject.SetActive(false);
        
        jumperAToolController.gameObject.SetActive(false);
        jumperBToolController.gameObject.SetActive(false);


        paleAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        ditanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;
        transform.position = new Vector3(transform.position.x + ditanceToMove, transform.position.y, transform.position.z);
        lastPlayerPosition = thePlayer.transform.position;


        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!jumperAToolController.grounded && !useNext)
            {
                jumperAToolController.gameObject.SetActive(true);
                jumperAToolController.drop();
                useNext = true;


            }
            else
            {
                if (!jumperBToolController.grounded && useNext)
                {
                    jumperBToolController.gameObject.SetActive(true);
                    jumperBToolController.drop();
                    useNext = false;

                }
            }

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            bagToolController.gameObject.SetActive(true);
            StartCoroutine("pourPale");
            SoundManger.PlaySound("splash");

        }

    }

    IEnumerator pourPale()
    {
        
        yield return new WaitForSeconds(2.20f);
        bagToolController.gameObject.SetActive(false);

    }

    public void resetInventory()
    {
        transform.rotation = Quaternion.identity;
        lastPlayerPosition = thePlayer.transform.position;
        transform.position = inventoryStartPoint;
        bagToolController.resetAtt();
    }
}
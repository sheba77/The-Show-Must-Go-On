using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDistruct : MonoBehaviour
{
    public GameObject platformDistruct;
    void Start()
    {
        platformDistruct = GameObject.Find("PlatforDPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < platformDistruct.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}

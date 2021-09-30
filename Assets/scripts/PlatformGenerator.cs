using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform generatorPoint;
    public float distanceBetween;
    public float gapProb = 0.1f;
    private float yVal;

    private const int distanceBetweenMin = 17;
    private const int distanceBetweenhMax = 25;

    public GameObject[] platforms;
    private int platformSelector;
    private int preSelcted;

    void Start()
    {
        yVal = transform.position.y - 0.1f;
        preSelcted = 0;

    }

    void Update()
    {
        float currval = yVal;
        
        if (transform.position.x < generatorPoint.position.x)
       {
           distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenhMax);

           platformSelector = Random.Range(0, platforms.Length);
           
            if(preSelcted == platformSelector)
            {
                platformSelector = (preSelcted + 1) % 4;
            }

            preSelcted = platformSelector;

            if (platforms[platformSelector].tag == "bird")
            {
                currval = transform.position.y + 4.0f;
            }

         
            if (platforms[platformSelector].tag == "rock")
            {
                currval = yVal - 0.40f;
            }

            transform.position = new Vector3(transform.position.x + distanceBetween, currval, transform.position.z);

            Instantiate(platforms[platformSelector], transform.position, transform.rotation);
       } 
    }
}

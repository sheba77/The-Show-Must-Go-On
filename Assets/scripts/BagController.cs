using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : ToolController
{

    private GameObject hitObject; 


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "bird")
        {
            hitObject = other.gameObject;
            other.gameObject.GetComponent<Animator>().SetBool("isHit", true);
            StartCoroutine("waitforit",1.0f);
//            Destroy(other.gameObject);

        }

        if (other.gameObject.tag == "fire")
        {
            hitObject = other.gameObject;
            other.gameObject.GetComponent<Animator>().SetBool("isOut", true);
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine("waitforit", 2.0f);
//            Destroy(other.gameObject);
        }
    }

    IEnumerator waitforit(float num)
    {
        yield return new WaitForSeconds(num);
        Destroy(hitObject);
    }
}

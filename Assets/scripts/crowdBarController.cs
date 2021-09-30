using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crowdBarController : MonoBehaviour
{
    private Image barImage;

    private const int MAX_PLEASE = 100;

    private float crowdPlease;

    private float crowdPleaseDecrease;
    // Start is called before the first frame update
    void Start()
    {
        barImage = transform.Find("bar").GetComponent<Image>();
        crowdPlease = 33f;
        crowdPleaseDecrease = 0.7f;
        barImage.fillAmount = crowdPleaseNormal();
    }

    // Update is called once per frame
    void Update()
    {
        if (crowdPlease > 0)
        {
            crowdPlease -= crowdPleaseDecrease * Time.deltaTime;
        }

        if (crowdPlease < 1)
        {
            crowdPlease = 0;
        }

        barImage.fillAmount = crowdPleaseNormal();
    }

    public void addCrowdPlease(int amount)
    {
        crowdPlease += amount;
        if (crowdPlease > MAX_PLEASE)
        {
            crowdPlease = MAX_PLEASE;
        }
    }
    
    public void decreaseCrowdPlease(int amount)
    {
        crowdPlease -= amount;
        if (crowdPlease <= 0)
        {
            crowdPlease = 0;
        }
    }

    public bool lostPatience()
    {
        return crowdPlease <= 0;
    }
    
    public bool superPleased()
    {
        return crowdPlease >= MAX_PLEASE;
    }

    float crowdPleaseNormal()
    {
        return crowdPlease / MAX_PLEASE;
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RequirementController : MonoBehaviour
{
    public GameObject[] requirements;
    private int numChecked;
    // Start is called before the first frame update

    private SpriteRenderer renderer;
    Object[] sprites;
    Image img;

    void Start()
    {
        numChecked = 0;
    }
    

    public void addRequirements(string[] input)
    {
        uncheckAll();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
   
        sprites = Resources.LoadAll("missions");

        for (int i = 0; i < input.Length; i++)
        {

            requirements[i].GetComponent<Text>().text = input[i];
            requirements[i].transform.GetChild(0).gameObject.SetActive(true);
            img = requirements[i].transform.GetChild(0).GetComponent<Image>();
            Find_Sprite(i);
        }
    }

    public void checkRequirement(string req)
    {
        string formatReq = req;
        for (int i = 0; i < requirements.Length; i++)
        {
            if (requirements[i].GetComponent<Text>().text == formatReq)
            {
                requirements[i].transform.GetChild(1).gameObject.SetActive(true);
                numChecked += 1;
            }
        }
    }
    
    public void uncheckAll()
    {
        for (int i = 0; i < requirements.Length; i++)
        {
            requirements[i].transform.GetChild(1).gameObject.SetActive(false);
        }

        numChecked = 0;
    }

    private void Find_Sprite(int val)
    {
        int idx = 0 ;
        string name = requirements[val].GetComponent<Text>().text;

        if (name == "water Player")
        {
            idx = 1;
        }
        if (name == "rock Player")
        {
            idx = 2;
        }
        if (name == "bird Player")
        {
            idx = 3;
        }

        if (name == "fire Player")
        {
            idx = 4;
        }

        if (name == "water woman")
        {
            idx = 5;
        }
        if (name == "rock woman")
        {
            idx = 6;
        }
        if (name == "bird woman")
        {
            idx = 7;
        }
        if (name == "fire woman")
        {
            idx = 8;
        }

        if (name == "water devil")
        {
            idx = 9;
        }
        if (name == "rock devil")
        {
            idx = 10;
        }
        if (name == "bird devil")
        {
            idx = 11;
        }
        if (name  == "fire devil")
        {
            idx = 12;
        }

        if (name == "water donkey")
        {
            idx = 13;
        }
        if (name == "rock donkey")
        {
            idx = 14;
        }
        if (name == "bird donkey")
        {
            idx = 15;
        }
        if (name == "fire donkey")
        {
            idx = 16;
        }

        img.sprite = (Sprite)sprites[idx]; 

    }

    public bool isSetComplete()
    {
        return numChecked == requirements.Length;
    }

}

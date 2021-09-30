using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class StrartMenu : MonoBehaviour
{
    public GameObject Panle;
    private Animator anim;
    private  Animation animee;
    private bool isPushed;
    public GameObject instructions;
    bool instrucON = false;


    void Start()
    {
        Time.timeScale = 1.0f;
        if (Panle)
        {
            isPushed = false;
            instructions.SetActive(false);
            anim = Panle.GetComponent<Animator>();
            animee = GetComponent<Animation>();
            anim.SetBool("open", true);
            instructions.SetActive(false);
        }


    }

    public void pushed()
    {
        anim.SetBool("open", false);
        isPushed = true;
        anim.SetBool("StartGame", true);
        StartCoroutine("WaitForAnimation");
        
    }

    private IEnumerator WaitForAnimation()
    {
        //Debug.Log("now");
        yield return new WaitForSeconds(2.0f);
        //Debug.Log("later");
        StartGame();

    }
    public void StartGame()
    {
        SceneManager.LoadScene("endless");
    }
    public void LoseScene()
    {
        SceneManager.LoadScene("Lose");
    }
    public void LoadMenu()
    {
        
        SceneManager.LoadScene("Opening");
    }


    public void getInsc()
    {
        if(instrucON)
        {
            instructions.SetActive(false);
            instrucON = false;
        }

        else
        {
            instructions.SetActive(true);
            instrucON = true;
        }
    }

    public void win()
    {

        SceneManager.LoadScene("win");
    }
}


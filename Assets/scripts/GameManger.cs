using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{

    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayerController thePlayer;
    public RequirementController requirementController;
    public crowdBarController crowdController;
    private Vector3 PlayerStartPoint;
    public GameObject inventory;
    public GameObject PauseScene;

    public static bool GameIsPaused = false;
    
    private string[] coliderTags = { "bird", "fire", "water", "rock"};
    private string[] faceTags = { "Player","devil", "donkey", "woman"};

    private string[] requirements = new string[3];
    private const int CROWD_INCREASE_PLEASE = 15;
    private const int CROWD_DECREASE_PLEASE = 3;
    private const string NULL_REQIRMENT = "a b";
    private bool backup_update_req = false;

    private int winCounter;
    public StrartMenu Scripter;

    private PlatformDistruct[] platformDestroy;
    void Start()
    {
        platformStartPoint = platformGenerator.position;
        PlayerStartPoint = thePlayer.transform.position;
        addRequirements();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                pause();
            }
        }

        if (crowdController.lostPatience())
        {
            Scripter.LoseScene();
        }

        if (crowdController.superPleased())
        {
            Scripter.win();
        }

    }
    public void RestartGame()
    {

        StartCoroutine("RestartGameCo");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {

        //PauseScene.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseScene.SetActive(false);
    }

    void pause()
    {
        //PauseScene.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
        PauseScene.SetActive(true);
    }

    void addRequirements()
    {
        for (int i = 0; i < requirements.Length; i++)
        {
            int obstacleChoice = Random.Range(0, coliderTags.Length);
            int faceChoice = Random.Range(0, faceTags.Length);
            string obstacle = coliderTags[obstacleChoice];
            string face = faceTags[faceChoice];
            requirements[i] = obstacle + " " + face;
        }
//        requirementController.uncheckAll();
        requirementController.addRequirements(requirements);
    }
    
    public bool onPlayerHit(string colliderTag, string faceTag)
    {
        bool goodHit = false;

        for (int i = 0; i < requirements.Length; i++)
        {
            string[] req = requirements[i].Split(' ');

            if (req[0] == colliderTag && req[1] == faceTag)
            {
                requirementController.checkRequirement(requirements[i]);
                requirements[i] = NULL_REQIRMENT;
                crowdController.addCrowdPlease(CROWD_INCREASE_PLEASE);
                goodHit = true;
                if (requirementController.isSetComplete())
                {
                    addRequirements();
                }
            }
            
        }

        if (!goodHit)
        {
            crowdController.decreaseCrowdPlease(CROWD_DECREASE_PLEASE);
        }

        return goodHit;
    }

    public IEnumerator RestartGameCo()
    {
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        //destroy all of platforms - not we only set as inactive and not as false
        platformDestroy = FindObjectsOfType<PlatformDistruct>();
        for (int i =0; i < platformDestroy.Length; i++)
        {
            platformDestroy[i].gameObject.SetActive(false);
        }
        thePlayer.transform.position = PlayerStartPoint;
        platformGenerator.position = platformStartPoint;
//        inventory.transform.position = inventoryStartPoint;
        thePlayer.gameObject.SetActive(true);
        inventory.GetComponent<inventoryContoller>().resetInventory();
    }
    
    public string[] getColiderTags()
    {
        return coliderTags;
    }
    
    public string[] getFaceTags()
    {
        return faceTags;
    }

    public void win()
    {
        SceneManager.LoadScene("win");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Opening");
    }

}


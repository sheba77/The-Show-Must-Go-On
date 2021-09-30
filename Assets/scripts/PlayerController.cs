using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManger GM;

    public float Movespeed; //move speed
    private float MoveSpeedStore;
        
    private float WhenSpeedIncrease; //when should we increase the speed
    private float WhenSpeedIncreaseStore; //when should we increase the speed

    private float SpeedIncreaseMileStone; // how much to add each time 
    private float SpeedIncreaseMileStoneStore; 

    public float jumpForce; // the jump force

    private Rigidbody2D myRigid;
    private Collider2D myCollider;

    public bool grounded; // is the objects toching the ground
    public LayerMask whatIsGround; //should always be ground

    private Animator myAnimator;
    private Animator IconAnimator;

    public GameObject maskIconn;
    private string[] coliderTags;
    private string[] faceTags;

    private int maskNum; //face swapping 
    private string CurrMaskName;
    private bool freeToSwitch;
    private static readonly int ShouldDie = Animator.StringToHash("ShouldDie");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Restart = Animator.StringToHash("Restart");
    private static readonly int ShouldBird = Animator.StringToHash("ShouldBird");
    private static readonly int ShouldBurn = Animator.StringToHash("ShouldBurn");
    private static readonly int ShouldDrown = Animator.StringToHash("ShouldDrown");
    private static readonly int ShouldRock = Animator.StringToHash("ShouldRock");
    private static readonly int IsHuman = Animator.StringToHash("isHuman");
    private static readonly int IsDevil = Animator.StringToHash("isDevil");
    private static readonly int Devil = Animator.StringToHash("Devil");
    private static readonly int Human = Animator.StringToHash("Human");
    private static readonly int IsDonkey = Animator.StringToHash("isDonkey");
    private static readonly int Donkey = Animator.StringToHash("Donkey");
    private static readonly int IsPrincess = Animator.StringToHash("isPrincess");
    private static readonly int Princess = Animator.StringToHash("Princess");


    void Start()
    {
        coliderTags = GM.getColiderTags();
        faceTags = GM.getFaceTags();
        WhenSpeedIncrease = 150.0f;
        SpeedIncreaseMileStone = WhenSpeedIncrease;

        myRigid = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        IconAnimator = maskIconn.GetComponent<Animator>();

        //when dies to set to beggining
        MoveSpeedStore = Movespeed;
        SpeedIncreaseMileStoneStore = SpeedIncreaseMileStone;
        WhenSpeedIncreaseStore = WhenSpeedIncrease;

        myAnimator.SetBool(Restart, false);

        maskNum = 0;
        CurrMaskName = faceTags[0];
        freeToSwitch = true;


    }

    void Update()
    {
        if (myAnimator.GetBool(Restart) == true)
        {
            myAnimator.SetBool(Restart, false);
        }

//        if ((transform.position.x > WhenSpeedIncrease) & (Movespeed < 6.76f))
//        {
//            WhenSpeedIncrease += SpeedIncreaseMileStone;
//            Movespeed = Movespeed * SpeedMultiplier;
//            SpeedIncreaseMileStone = SpeedIncreaseMileStone * SpeedMultiplier;
//        }

        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        myRigid.velocity = new Vector2(Movespeed, myRigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
       

        if (Input.GetKeyDown(KeyCode.F) &&
            (myAnimator.GetBool(ShouldDie) == false) &&
            (myAnimator.GetBool(Jump) == false) && 
            freeToSwitch)
        {

            if(maskNum != 3)
            {
                maskNum += 1;
            }
            else
            {
                maskNum = 0;
            }
            
            SwitchFace();
        }

        myAnimator.SetFloat(Speed, Movespeed);

  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        freeToSwitch = false;
        if ((other.gameObject.CompareTag("jumper")) && (myAnimator.GetBool(ShouldDie) == false) && grounded)
        {
            
            myAnimator.SetBool("Jump", true);
            SoundManger.PlaySound("jump");
            myRigid.AddForce(transform.up * 400);
            StartCoroutine("WaitForAnimation", 1.0f);
            
        }

        if (coliderTags.Contains(other.gameObject.tag) & (grounded))
        {

            int value = 0;
            myAnimator.SetBool(ShouldDie, true);

            if (other.gameObject.CompareTag("bird"))
            {
                value = 1;
                myAnimator.SetBool(ShouldBird, true);
            }

            if (other.gameObject.CompareTag("fire"))
            {
                value = 2;
                myAnimator.SetBool(ShouldBurn, true);
            }

            if (other.gameObject.CompareTag("water"))
            {
                value = 3;
                myAnimator.SetBool(ShouldDrown, true);
            }

            if (other.gameObject.CompareTag("rock"))
            {
                value = 4;
                myAnimator.SetBool(ShouldRock, true);
            }

            bool isGoodHit = GM.onPlayerHit(other.gameObject.tag, CurrMaskName);

            StartCoroutine("WaitForDeath",value);

            if (isGoodHit)
            {
                SoundManger.PlaySound("cheer");
            }
            else
            {
                maskNum = 0;
                SoundManger.PlaySound("boo");

            }

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        freeToSwitch = true;
    }

    private IEnumerator WaitForAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        myAnimator.SetBool(Jump, false);

    }

    private IEnumerator WaitForDeath(int val)
    {
        yield return new WaitForSeconds(0.5f);
        Stop_animation(val);

    }

    private void Stop_animation(int val)
    {        
        if (val == 1)
        {
            myAnimator.SetBool(ShouldBird, false);
        }
        if (val == 2)
        {
            myAnimator.SetBool(ShouldBurn, false);
        }
        if (val == 3)
        {
            myAnimator.SetBool(ShouldDrown, false);
        }
        if (val == 4)
        {
            myAnimator.SetBool(ShouldRock, false);
        }

        myAnimator.SetBool(ShouldDie, false);
        Movespeed = 4;
    }

    private void SwitchFace()
    {
        //private string[] faceTags = { "Player","devil", "donkey", "woman"};
        SoundManger.PlaySound("mask");
        myAnimator.SetBool(Restart, false);


        if (faceTags[maskNum] == "devil")
        {
            CurrMaskName = faceTags[1];
            myAnimator.SetBool(IsHuman, false);
            myAnimator.SetBool(IsDevil, true);
            
            IconAnimator.SetBool(Devil, true);
            IconAnimator.SetBool(Human, false);

        }
        if (faceTags[maskNum] == "donkey")
        {
            CurrMaskName = faceTags[2];
            myAnimator.SetBool(IsDevil, false);
            myAnimator.SetBool(IsDonkey, true);
            
            IconAnimator.SetBool(Donkey, true);
            IconAnimator.SetBool(Devil, false);

        }
        if (faceTags[maskNum] == "woman")
        {
            CurrMaskName = faceTags[3];
            myAnimator.SetBool(IsDonkey, false);
            myAnimator.SetBool(IsPrincess, true);
            
            IconAnimator.SetBool(Princess, true);
            IconAnimator.SetBool(Donkey, false);
        }
        if (faceTags[maskNum] == "Player")
        {
            CurrMaskName = faceTags[0];
            myAnimator.SetBool(IsPrincess, false);
            myAnimator.SetBool(IsHuman, true);
            
            IconAnimator.SetBool(Human, true);
            IconAnimator.SetBool(Princess, false);
        }

    }

    void restartGame()
    {
        Movespeed = MoveSpeedStore;
        SpeedIncreaseMileStone = SpeedIncreaseMileStoneStore;
        WhenSpeedIncrease = WhenSpeedIncreaseStore;
        GM.RestartGame();
    }
}

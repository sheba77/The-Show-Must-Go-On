using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static AudioClip BooSound, YaySound, maskPop, jump, splash, Staring;
    static AudioSource src;
    void Start()
    {
        BooSound = Resources.Load<AudioClip>("Boo1");
        YaySound = Resources.Load<AudioClip>("cheer1");
        maskPop = Resources.Load<AudioClip>("pop");
        jump = Resources.Load<AudioClip>("boing");
        splash = Resources.Load<AudioClip>("splash");
        Staring = Resources.Load<AudioClip>("room");

        src = GetComponent<AudioSource>();
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "boo":
                src.PlayOneShot(BooSound);
                break;
            case "cheer":
                src.PlayOneShot(YaySound);
                break;
            case "mask":
                src.PlayOneShot(maskPop);
                break;
            case "jump":
                src.PlayOneShot(jump);
                break;
            case "splash":
                src.PlayOneShot(splash);
                break;
            case "start":
                src.PlayOneShot(Staring);
                break;


        }
    }
}

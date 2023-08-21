using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public GameObject BGMusic;
    private AudioSource audioSrc;
    public GameObject[] sounds;

    private void Awake()
    {
        sounds = GameObject.FindGameObjectsWithTag("Sound");

        if(sounds.Length == 0)
        {
            BGMusic = Instantiate(BGMusic);
            DontDestroyOnLoad(BGMusic.gameObject);
        }
        else
        {
            BGMusic = GameObject.Find("BGMusic");
        }
    }

    private void Start()
    {
        audioSrc= GetComponent<AudioSource>();
    }

}

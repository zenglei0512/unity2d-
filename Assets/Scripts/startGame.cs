using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class startGame : MonoBehaviour
{
    AudioSource buttonAudio;
    audioPlay audioPlay;
    // Start is called before the first frame update
    void Start()
    {
        audioPlay = GameObject.Find("Main Camera").GetComponent<audioPlay>();
        buttonAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameStart()
    {
        buttonAudio.Play();
        audioPlay.audioCallBack(buttonAudio, () =>
        {
            SceneManager.LoadScene("map1");
        });
    }

    public void gameQuit()
    {
        buttonAudio.Play();
        audioPlay.audioCallBack(buttonAudio, () =>
        {
           Application.Quit();
        });
    }
}

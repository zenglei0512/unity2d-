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
        audioPlay = gameObject.AddComponent<audioPlay>();
        buttonAudio = GetComponent<AudioSource>();
    }

    public void gameStart()
    {
        buttonAudio.Play();
        audioPlay.audioCallBack(buttonAudio, () =>
        {
            SceneManager.LoadScene("selectMaps");
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

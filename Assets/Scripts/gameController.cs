using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    audioPlay audioPlay;
    AudioSource buttonAudio;
    public GameObject button;
    bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        audioPlay = GameObject.Find("Main Camera").GetComponent<audioPlay>();
        buttonAudio = transform.Find("Button").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                pauseGame();
            } else
            {
                startGame();
            }
        }
    }

    public string getSceneName()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        return currentScene;
    }

    public void resetGame()
    {
        Time.timeScale = 1.0f;
        isPause = false;
        buttonAudio.Play();
        audioPlay.audioCallBack(buttonAudio, () =>
        {
            SceneManager.LoadScene(getSceneName());
        });
    }

    public void startGame()
    {
        Time.timeScale = 1.0f;
        isPause = false;
        buttonAudio.Play();
        audioPlay.audioCallBack(buttonAudio, () =>
        {
            button.SetActive(false);
        });
    }

    public void pauseGame()
    {
        button.SetActive(true);
        Time.timeScale = 0;
        isPause = true;
    }

    public void gotoMenu()
    {
        Time.timeScale = 1.0f;
        isPause = false;
        buttonAudio.Play();
        audioPlay.audioCallBack(buttonAudio, () =>
        {
            SceneManager.LoadScene("start");
        });
    }
}

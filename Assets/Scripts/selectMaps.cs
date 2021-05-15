using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class selectMaps : MonoBehaviour
{
    saveMapData saveMapData;
    AudioSource buttonAudio;
    audioPlay audioPlay;

    // Start is called before the first frame update
    void Start()
    {
        saveMapData = gameObject.AddComponent<saveMapData>();
        buttonAudio = gameObject.GetComponent<AudioSource>();
        audioPlay = gameObject.AddComponent<audioPlay>();
        initMaps();
    }

    void initMaps()
    {
        if(saveMapData.CurrentMapIndex == 0)
        {
            saveMapData.CurrentMapIndex = 1;
            saveMapData.MaxMapIndex = 1;
        }
        int mapCount = transform.childCount;
        for (int i = 0; i < mapCount; i++)
        {
            Transform map = transform.GetChild(i);
            Transform button = map.Find("Button");
            Image buttonBg = button.GetComponent<Image>();
            Button mapButton = button.GetComponent<Button>();
            int index = i + 1;
            mapButton.onClick.AddListener(delegate () {
                buttonAudio.Play();
                audioPlay.audioCallBack(buttonAudio, () =>
                {
                    SceneManager.LoadScene("map" + index.ToString());
                });
            });
            if (index > saveMapData.MaxMapIndex)
            {
                buttonBg.color = new Color(151f / 255f, 151f / 255f, 151f / 255f);
                mapButton.enabled = false;
            } else
            {
                buttonBg.color = new Color(1f, 1f, 1f);
                mapButton.enabled = true;
            }
        }
    }

    public void gotoStartScene()
    {
        buttonAudio.Play();
        audioPlay.audioCallBack(buttonAudio, () =>
        {
            SceneManager.LoadScene("start");
        });
    }
}

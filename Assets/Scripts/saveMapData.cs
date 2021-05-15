using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saveMapData : MonoBehaviour
{
    public int CurrentMapIndex
    {
        get { return PlayerPrefs.GetInt("curMapIndex"); }
        set { PlayerPrefs.SetInt("curMapIndex", value); }
    }

    public int MaxMapIndex
    {
        get { return PlayerPrefs.GetInt("maxMapIndex"); }
        set { PlayerPrefs.SetInt("maxMapIndex", value); }
    }

    public void saveCurrentMapIndex()
    {
        string currentMapName = SceneManager.GetActiveScene().name;
        CurrentMapIndex = int.Parse(currentMapName.Replace("map", ""));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoNextGame : MonoBehaviour
{
    saveMapData saveMapData;
    // Start is called before the first frame update
    void Start()
    {
        saveMapData = gameObject.AddComponent<saveMapData>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                saveMapData.saveCurrentMapIndex();
                saveMapData.CurrentMapIndex = saveMapData.CurrentMapIndex + 1;
                if (saveMapData.CurrentMapIndex > saveMapData.MaxMapIndex)
                {
                    saveMapData.MaxMapIndex = saveMapData.CurrentMapIndex;
                }
                if (saveMapData.CurrentMapIndex > 6)
                {
                    SceneManager.LoadScene("selectMaps");
                }
                else
                {
                    SceneManager.LoadScene("map" + saveMapData.CurrentMapIndex.ToString());
                }

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class audioPlay : MonoBehaviour
{
    public void audioCallBack(AudioSource audio, Action callBack)
    {
        StartCoroutine(AudioCallBack(audio, callBack));
    }

    IEnumerator AudioCallBack(AudioSource audio, Action callBack)
    {
        while (audio.isPlaying)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        callBack();
    }
}

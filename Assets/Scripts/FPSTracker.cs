using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSTracker : MonoBehaviour
{
    public int framerate;
    public Text framerateText;

    // Update is called once per frame
    void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        framerate = (int)current;
        framerateText.text = framerate.ToString();
    }
}

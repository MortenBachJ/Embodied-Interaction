using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeSpot : MonoBehaviour
{
    public Transform[] spots;
    public XRNode inputSource;
    public float desiredCooldown;

    private float cooldown;
    private int spotIndex = 0;
    private InputDevice device;
    private bool pressed;
    private bool pressed2;
    private bool allowed;

    private bool started = false;
    public GameObject startScreen;
    public GameObject videoPlayer;
    public GameObject continueScreen;
    public GameObject videoPlayer2;

    void Awake()
    {
        allowed = true;
    }

    void Update()
    {
        if(!allowed)
        {
            allowed = Cooldown(allowed);
        }

        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        if((device.TryGetFeatureValue(CommonUsages.triggerButton, out pressed) && pressed) && !started)
     
        if((device.TryGetFeatureValue(CommonUsages.triggerButton, out pressed) && pressed) && !started) 
        {
            started = true;
            startScreen.SetActive(true);
            videoPlayer.SetActive(true);

        }

        if((device.TryGetFeatureValue(CommonUsages.triggerButton, out pressed) && pressed) && allowed && started)
        {
            if(spotIndex <= spots.Length-1){
                transform.position = spots[spotIndex].position;
                cooldown = desiredCooldown;
                allowed = false;

                //remove first video clip
                startScreen.SetActive(false);
                videoPlayer.SetActive(false);
                //play next video clip
                continueScreen.SetActive(true);
                videoPlayer2.SetActive(true);
            }
            spotIndex++;
            if(spotIndex > spots.Length-1)
            {
                spotIndex = 0;
                cooldown = desiredCooldown;
                allowed = false;
            }
        }
    }

    public bool Cooldown(bool boolToChange)
    {
        boolToChange = false;
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            Debug.Log("Cooldown Left: " + cooldown);
        }
        if(cooldown <= 0)
        {
            boolToChange = true;
        }
        return boolToChange;
    }
}

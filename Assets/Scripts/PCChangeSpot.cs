using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PCChangeSpot : MonoBehaviour
{
    public Transform[] spots;
    public XRNode inputSource;
    public float desiredCooldown;

    private float cooldown;
    private int spotIndex = 0;
    private InputDevice device;
    private bool pressed;
    private bool allowed;

    private bool started = false;
    private bool first = false;
    public GameObject beforeScreen;
    public GameObject startScreen;
    public GameObject videoPlayer;
    public GameObject firstScreen;
    public GameObject videoPlayer2;
    public GameObject continueScreen;
    public GameObject videoPlayer3;
    public GameObject endScreen;
    public GameObject videoPlayer4;
    public GameObject chair;

    void Awake()
    {
        allowed = true;
        startScreen.SetActive(false);
        videoPlayer.SetActive(false);
        firstScreen.SetActive(false);
        videoPlayer2.SetActive(false);
        continueScreen.SetActive(false);
        videoPlayer3.SetActive(false);
        endScreen.SetActive(false);
        videoPlayer4.SetActive(false);
    }

    void Update()
    {
        if(!allowed)
        {
            allowed = Cooldown(allowed);
        }

        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);

        
        if((device.TryGetFeatureValue(CommonUsages.triggerButton, out pressed) && pressed))
        //if (Input.GetKey(KeyCode.Space))
        //if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out pressed) && pressed)
        {
            if (!started){
                beforeScreen.SetActive(false);
                startScreen.SetActive(true);
                videoPlayer.SetActive(true);

                //set cooldown
                cooldown = desiredCooldown+23;
                allowed = false;
                started = true;
                first = true;
            }
            else if(allowed && started)
            {
                if(spotIndex <= spots.Length-1){
                    chair.transform.position = spots[spotIndex].position;
                    cooldown = desiredCooldown;
                    allowed = false;
                    if(first){
                        //remove first video clip
                        startScreen.SetActive(false);
                        videoPlayer.SetActive(false);
                        //play first task screen
                        firstScreen.SetActive(true);
                        videoPlayer2.SetActive(true);
                        first = false;
                    } 
                    else{
                        //ensure that it is off before starting it again
                        firstScreen.SetActive(false);
                        videoPlayer2.SetActive(false);
                        continueScreen.SetActive(false);
                        videoPlayer3.SetActive(false);
                        //play next video clip
                        continueScreen.SetActive(true);
                        videoPlayer3.SetActive(true);
                    }
                }
                spotIndex++;
                if(spotIndex > spots.Length)
                {
                    continueScreen.SetActive(false);
                    videoPlayer3.SetActive(false);
                    //play final video clip
                    endScreen.SetActive(true);
                    videoPlayer4.SetActive(true);

                }
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

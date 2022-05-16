using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //to access Image

public class StatusIndicator : MonoBehaviour
{
    Player PlayerObject;
    Rotator[] RotatorObject;
    public static int slowCounter, fastCounter;

    public Image currentVisualImage, currentVoiceImage, currentSpeedImage; 

    [SerializeField]
    //SpriteRenderer cyanBase, yellowBase, magentaBase, pinkBase; //delete this
    Sprite visualIndicatorOff, voiceIndicatorOff, //base assets
        visualIndicatorOn, voiceIndicatorOn,
        defaultSpeed, slowSpeed1, slowSpeed2, fastSpeed1, fastSpeed2;  //custom assets with shapes

    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = GameObject.Find("Player").GetComponent<Player>();
        RotatorObject = FindObjectsOfType<Rotator>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Rotator rotator in RotatorObject)
        {
            slowCounter = rotator.slowCounter;
            fastCounter = rotator.fastCounter;
        }

        Debug.Log($"Slow Counter {slowCounter}, Fast Counter {fastCounter}");

        //Visual
        if (PlayerObject.colourBlindFriendly == true) //turn OFF Colour Blind-Friendly assets by changing to base assets
        {
            currentVisualImage.sprite = visualIndicatorOn;
        }
        else if (PlayerObject.colourBlindFriendly == false) //turn ON Colour Blind-Friendly assets by changing to base assets
        {
            currentVisualImage.sprite = visualIndicatorOff;
        }

        //Voice
        if (PlayerObject.voiceControl == true) //turn OFF Colour Blind-Friendly assets by changing to base assets
        {
            currentVoiceImage.sprite = voiceIndicatorOn;
        }
        else if (PlayerObject.voiceControl == false) //turn ON Colour Blind-Friendly assets by changing to base assets
        {
            currentVoiceImage.sprite = voiceIndicatorOff;
        }

        //Speed or Cognitive
        if (fastCounter == 0 && slowCounter == 0)
        {
            currentSpeedImage.sprite = defaultSpeed;
        }
        else if (fastCounter == 1 )
        {
            currentSpeedImage.sprite = fastSpeed1;
        }
        else if (fastCounter == 2)
        {
            currentSpeedImage.sprite = fastSpeed2;
        }
        else if (slowCounter == 1)
        {
            currentSpeedImage.sprite = slowSpeed1;
        }
        else if (slowCounter == 2)
        {
            currentSpeedImage.sprite = slowSpeed2;
        }
    }
}

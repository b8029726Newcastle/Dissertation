using System.Collections;
using System.Collections.Generic;
using System; //import for Action
using System.Linq; //ToArray
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Player PlayerObject;
    public Dictionary<string, Action> rotatorActions = new Dictionary<string, Action>();


    public float currentRotationSpeed = 100f; //100 degree rotation every second
    private float defaultRotationSpeed = 0;

    private int slowCounter, fastCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultRotationSpeed = currentRotationSpeed;
        PlayerObject = GameObject.Find("Player").GetComponent<Player>();

        rotatorActions = GameObject.Find("Player").GetComponent<Player>().actions;

        rotatorActions.Add("Test", DecreaseRotationSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, currentRotationSpeed * Time.deltaTime); //no rotation on x, y axes

        if (Input.GetKeyDown(KeyCode.P))
            IncreaseRotationSpeed();

    }

    public void DecreaseRotationSpeed()
     {
          //reduce rotation speed, MAYBE ALSO REDUCE JUMPFORCE or GravityScale?
         {
             //I think I need to RESET the COUNTERS back to 0 if the "currentRotationSpeed = defaultRotationSpeed";

             fastCounter = 0;
             if (slowCounter == 0)
             {
                 currentRotationSpeed = defaultRotationSpeed;
             }
             if (slowCounter < 2)
             {
                 currentRotationSpeed = currentRotationSpeed - 30f;
                 slowCounter++;
             }
             else if (slowCounter >= 2)
             {
                 currentRotationSpeed = defaultRotationSpeed;
                 slowCounter = 0;
             }
             Debug.Log("Current Rotation Speed = " + currentRotationSpeed + ". Slow Counter = " + slowCounter);
         }
    }

    public void IncreaseRotationSpeed()
    {
        slowCounter = 0;
        if (fastCounter == 0)
        {
            currentRotationSpeed = defaultRotationSpeed;
        }
        if (fastCounter < 2)
        {
            currentRotationSpeed = currentRotationSpeed + 30f;
            fastCounter++;
        }
        else if (fastCounter >= 2)
        {
            currentRotationSpeed = defaultRotationSpeed;
            fastCounter = 0;
        }
        Debug.Log("Current Rotation Speed = " + currentRotationSpeed + ". Fast Counter = " + fastCounter);
    }

    public void DefaultRotationSpeed()
    {
        slowCounter = 0;
        fastCounter = 0;
        currentRotationSpeed = defaultRotationSpeed;
        Debug.Log($"Current Rotation Speed = {currentRotationSpeed}. Fast Counter = {fastCounter}, Slow Counter = {slowCounter}");
    }

}

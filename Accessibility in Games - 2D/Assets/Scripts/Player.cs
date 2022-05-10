using System; //import for Action
using System.Collections;
using System.Collections.Generic;
using System.Linq; //ToArray
using UnityEngine;
using UnityEngine.Windows.Speech; //import for KeywordRecognizer
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rotator[] RotatorObject;

    public bool colourBlindFriendly = true; //Colour Blind-Friendly assets are ON by default
    public bool? increaseGameSpeed = null; //nullible, I don't want it to be true/false .... MAYBE just set false?
    public bool? decreaseGameSpeed = null;
    public bool? defaultGameSpeed = null; //MAYBE just set true?
    private bool voiceControl = false;

    public KeywordRecognizer keywordRecognizer;
    public Dictionary<string, Action> actions = new Dictionary<string, Action>(); //key = string, value = action

    [SerializeField]
    float jumpForce = 7.5f;
    //maybe different jumpforce  when you just go "steady" instead of "jump" in voice commands

    public string currentColour;
    public int slowCounter, fastCounter = 0;

    //reference to rigidbody
    [SerializeField]
    Rigidbody2D rigidBody2D;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    SpriteRenderer diamondSpriteRenderer;

    public Color replicaCyan;
    public Color replicaYellow;
    public Color replicaMagenta;
    public Color replicaPink;

    public Color colourRed;
    public Color colourGreen;
    public Color colourBlue;
    public Color colourYellow;

    SpriteSwapper SpriteSwapperObject;

    [SerializeField]
    Sprite playerRedBase, playerGreenBase, playerBlueBase, playerYellowBase,
        playerRedHeart, playerGreenLeaf, playerBlueRaindrop, playerYellowStar;

    // Start is called before the first frame update
    void Start()
    {
        RotatorObject = FindObjectsOfType<Rotator>(); ////access all other script as an instance
        SpriteSwapperObject = FindObjectOfType<SpriteSwapper>(); //access other script as an instance

        //game doesn't start or gravity doesn't apply until a key has been pressed or user says "START GAME"
        //add subtitles that give that instruction

        SetRandomColour();
        //diamondSpriteRenderer.color = ;
        actions.Add("Jump", Jump);
        actions.Add("Swap Assets", SwapAsset);
        actions.Add("Decrease Game Speed", DecreaseGameSpeed);
        actions.Add("Increase Game Speed", IncreaseGameSpeed);        
        actions.Add("Default Game Speed", DefaultGameSpeed);

        //initialise keywordRecognizer
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());

        //on events/callbacks like "OnPhraseRecognized" do +=
        keywordRecognizer.OnPhraseRecognized += RecognisedCommand;
        keywordRecognizer.Start(); //maybe make a button or option asking user "Turn on Voice Control/Commands" just so it doesn't always listen unless permission is given
        //keywordRecognizer.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SwapAsset();

        //if (Input.GetKeyDown(KeyCode.P))
        //    SpriteSwapperObject.SwapAsset();

        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
            Jump();

        if (Input.GetKeyDown(KeyCode.V))
            VoiceControl();

        if (Input.GetKeyDown(KeyCode.E))
            DecreaseGameSpeed();

        if (Input.GetKeyDown(KeyCode.R))
            IncreaseGameSpeed();

        if (Input.GetKeyDown(KeyCode.T))
            DefaultGameSpeed();

        //when player presses jump/space or left mouse button
        /*if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            //not rb.AddForce
            rigidBody2D.velocity = Vector2.up * jumpForce;
        }*/
    }
    private void OnDestroy()
    {
        if (keywordRecognizer != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
    private void Jump()
    {
        //REMOVE the ButtonDown if statement, replace with "if player said jump" or something like that
        //when player presses jump/space or left mouse button
        
        {
            //not rb.AddForce
            rigidBody2D.velocity = Vector2.up * jumpForce;
            //rigidBody2D.velocity = Vector2.up * jumpForce + Vector2.right; //make it go left and right  ---  prob map to "Q/E" buttons
        }
    }

    private void SwapAsset()
    {
        /*Debug.Log("Press Q Once");
        SpriteSwapperObject.SwapAsset();
        Debug.Log("Press Q Twice");*/

        {
            if (colourBlindFriendly == true) //turn OFF Colour Blind-Friendly assets by changing to base assets
            {
                switch (currentColour)
                {
                    case "Red": spriteRenderer.sprite = playerRedBase; break;
                    case "Green": spriteRenderer.sprite = playerGreenBase; break;
                    case "Blue": spriteRenderer.sprite = playerBlueBase; break;
                    case "Yellow": spriteRenderer.sprite = playerYellowBase; break;
                }
                colourBlindFriendly = false;
            }                
            else if (colourBlindFriendly == false) //turn ON Colour Blind-Friendly assets by changing to base assets
            {
                switch (currentColour)
                {
                    case "Red": spriteRenderer.sprite = playerRedHeart; break;
                    case "Green": spriteRenderer.sprite = playerGreenLeaf; break;
                    case "Blue": spriteRenderer.sprite = playerBlueRaindrop; break;
                    case "Yellow": spriteRenderer.sprite = playerYellowStar; break;
                }
                colourBlindFriendly = true;
            }
        }
    }

    private void VoiceControl()
    {        
        {
            if(voiceControl == false) //turn ON Voice Control and slow down game pace to accomodate for delays
            {
                jumpForce = 4;
                rigidBody2D.gravityScale = 0.5f;
                voiceControl = true;

                //maybe add anothger jump like a quick "dash" (jump force 5 --- add an if statement KeyDown.W) or a float like "steady" little jump
            }
            else if (voiceControl == true) //turn OFF Voice Control and set game pace back to normal
            {
                jumpForce = 7.5f;
                rigidBody2D.gravityScale = 1.5f;
                voiceControl = false;
            }
            //interpolated string
            Debug.Log($"Jumpforce is: {jumpForce} and the Gravity Scale is: {rigidBody2D.gravityScale}");
            Debug.Log("Voice Control Bool: " + voiceControl);
        }
    }

    private void DecreaseGameSpeed()
    {
        foreach (Rotator rotator in RotatorObject)
        {
            rotator.DecreaseRotationSpeed();
        }
    }

    private void IncreaseGameSpeed()
    {
        foreach (Rotator rotator in RotatorObject)
        {
            rotator.IncreaseRotationSpeed();
        }
    }
    
    private void DefaultGameSpeed()
    {
        foreach (Rotator rotator in RotatorObject)
        {
            rotator.DefaultRotationSpeed();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //@33:22 he  changed the radius to be smaller
        if(collision.tag == "ColourChanger")
        {
            SetRandomColour();
            Destroy(collision.gameObject);
            return;
        }

        //change to tags ONLY with NEW COLOURBLIND-FRIENDLY implementation
        if (collision.name != currentColour) //|| collision.tag != currentColour --- USE TAGS only after assets are changed
        {
            Debug.Log("GAME OVER or REDUCED HEALTH?");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reload current scene, player starts from scratch --- maybe add a checkpoint next time
            //You done fucked up
            //mayybe reduce health
        }
        
        Debug.Log(collision.tag);

        //change to tags ONLY with NEW COLOURBLIND-FRIENDLY implementation
        Debug.Log(collision.name);

    }
    public int SetRandomColour()
    {
        //there is a problem with this 0-3 range (count of 4  colours) when I only have 3 colours with the Triangle!
        int index = UnityEngine.Random.Range(0, 4); //Random.Range is minimum inclusive and maximum exclusive so (0, 1, 2, 3)
        //otherwise if Random.Range is (0, 4) it will only be (0, 1, 2) --- missing one colour in my switch cases


        //add if statement to check if colourBlindSafe is on or not
        /*if(colourBlindSafe == true)
        {
            //put the first switch in here
        }
        else  if (colourBlindSafe == false)
        {
            //put the second switch in here
        }*/

        if(colourBlindFriendly == true)
        {
            switch (index)
            {
                //maybe add check so it doesn't reuse "currentColour" twice
                /*case 0: currentColour = "Cyan"; spriteRenderer.color = replicaCyan; break;

                case 1: currentColour = "Yellow"; spriteRenderer.color = replicaYellow; break;

                case 2: currentColour = "Magenta"; spriteRenderer.color = replicaMagenta; break;

                case 3: currentColour = "Pink"; spriteRenderer.color = replicaPink; break;*/

                case 0: currentColour = "Red"; spriteRenderer.sprite = playerRedHeart; break;

                case 1: currentColour = "Green"; spriteRenderer.sprite = playerGreenLeaf; break;

                case 2: currentColour = "Blue"; spriteRenderer.sprite = playerBlueRaindrop; break;

                case 3: currentColour = "Yellow"; spriteRenderer.sprite = playerYellowStar; break;
            }
        }
        else if (colourBlindFriendly == false)
        {            
            switch (index)
            {
                //maybe add check so it doesn't reuse "currentColour" twice
                /*case 0: currentColour = "Cyan"; spriteRenderer.color = replicaCyan; break;

                case 1: currentColour = "Yellow"; spriteRenderer.color = replicaYellow; break;

                case 2: currentColour = "Magenta"; spriteRenderer.color = replicaMagenta; break;

                case 3: currentColour = "Pink"; spriteRenderer.color = replicaPink; break;*/

                case 0: currentColour = "Red"; spriteRenderer.sprite = playerRedBase; break;

                case 1: currentColour = "Green"; spriteRenderer.sprite = playerGreenBase; break;

                case 2: currentColour = "Blue"; spriteRenderer.sprite = playerBlueBase; break;

                case 3: currentColour = "Yellow"; spriteRenderer.sprite = playerYellowBase; break;
            }
        }
        return index;

        /*switch (index)
        {
            //maybe add check so it doesn't reuse "currentColour" twice
            /*case 0: currentColour = "Cyan"; spriteRenderer.color = replicaCyan; break;

            case 1: currentColour = "Yellow"; spriteRenderer.color = replicaYellow; break;

            case 2: currentColour = "Magenta"; spriteRenderer.color = replicaMagenta; break;

            case 3: currentColour = "Pink"; spriteRenderer.color = replicaPink; break;

            case 0: currentColour = "Red"; spriteRenderer.sprite = spriteRed; break;

            case 1: currentColour = "Green"; spriteRenderer.sprite = spriteGreen; break;

            case 2: currentColour = "Blue"; spriteRenderer.sprite = spriteBlue; break;

            case 3: currentColour = "Yellow"; spriteRenderer.sprite = spriteYellow; break;
        }
        return index;*/
    }

    private void RecognisedCommand(PhraseRecognizedEventArgs phrase)
    {
        Debug.Log("You said: " + phrase.text);
        //also maybe turn it into subtitles/cc

        actions[phrase.text].Invoke();
    }
}
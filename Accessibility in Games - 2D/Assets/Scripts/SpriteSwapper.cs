using System; //import for Action
using System.Collections;
using System.Collections.Generic;
using System.Linq; //ToArray
using UnityEngine;
using UnityEngine.Windows.Speech; //import for KeywordRecognizer
using UnityEngine.SceneManagement;

public class SpriteSwapper : MonoBehaviour
{
    Player PlayerObject;

    public string currentColour;
    public bool colourBlindFriendly = true; //Colour Blind-Friendly assets are ON by default

    [SerializeField]
    SpriteRenderer redSpriteRenderer, greenSpriteRenderer, blueSpriteRenderer, yellowSpriteRenderer;

    [SerializeField]
    //SpriteRenderer cyanBase, yellowBase, magentaBase, pinkBase; //delete this
    Sprite redBase, greenBase, blueBase, yellowBase, //base assets
        redHeart, greenLeaf, blueRaindrop, yellowStar;  //custom assets with shapes

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite playerRedBase, playerGreenBase, playerBlueBase, playerYellowBase,
        playerRedHeart, playerGreenLeaf, playerBlueRaindrop, playerYellowStar;

    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = GameObject.Find("Player").GetComponent<Player>();

        //PlayerObject.actions.Add("Test", SwapAsset);

        //actions.Add("Test", SwapAsset);

        /*
        //initialise keywordRecognizer
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());

        //on events/callbacks like "OnPhraseRecognized" do +=
        keywordRecognizer.OnPhraseRecognized += RecognisedCommand;
        keywordRecognizer.Start(); //maybe make a button or option asking user "Turn on Voice Control/Commands" just so it doesn't always listen unless permission is given
        //keywordRecognizer.Stop();
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SwapAsset();

        //Debug.Log($"Colour Blind Friendly = {PlayerObject.colourBlindFriendly}");
        
        if (PlayerObject.colourBlindFriendly == true) //turn OFF Colour Blind-Friendly assets by changing to base assets
        {
            redSpriteRenderer.sprite = redHeart;
            greenSpriteRenderer.sprite = greenLeaf;
            blueSpriteRenderer.sprite = blueRaindrop;
            yellowSpriteRenderer.sprite = yellowStar;

            //colourBlindFriendly = false;
        }
        else if (PlayerObject.colourBlindFriendly == false) //turn ON Colour Blind-Friendly assets by changing to base assets
        {            
            redSpriteRenderer.sprite = redBase;
            greenSpriteRenderer.sprite = greenBase;
            blueSpriteRenderer.sprite = blueBase;
            yellowSpriteRenderer.sprite = yellowBase;

            //colourBlindFriendly = true;
        }


    }
    private void OnDestroy()
    {
        /*if (PlayerObject.keywordRecognizer != null)
        {
            PlayerObject.keywordRecognizer.Stop();
            PlayerObject.keywordRecognizer.Dispose();
        }*/
    }

    public void SwapAsset() //don't need this anymore, just delete this method because everything is already in UPDATE METHOD
    {
        
        {
            if(colourBlindFriendly == true) //turn OFF Colour Blind-Friendly assets by changing to base assets
            {
                redSpriteRenderer.sprite = redBase;
                greenSpriteRenderer.sprite = greenBase;
                blueSpriteRenderer.sprite = blueBase;
                yellowSpriteRenderer.sprite = yellowBase;

                switch (currentColour)
                {
                    case "Red": spriteRenderer.sprite = playerRedBase; break;
                    case "Green": spriteRenderer.sprite = playerGreenBase; break;
                    case "Blue": spriteRenderer.sprite = playerBlueBase; break;
                    case "Yellow": spriteRenderer.sprite = playerYellowBase; break;
                }

                colourBlindFriendly = false;
            }
            else if(colourBlindFriendly == false) //turn ON Colour Blind-Friendly assets by changing to base assets
            {
                redSpriteRenderer.sprite = redHeart;
                greenSpriteRenderer.sprite = greenLeaf;
                blueSpriteRenderer.sprite = blueRaindrop;
                yellowSpriteRenderer.sprite = yellowStar;

                switch (currentColour)
                {
                    case "Red": spriteRenderer.sprite = playerRedHeart; break;
                    case "Green": spriteRenderer.sprite = playerGreenLeaf; break;
                    case "Blue": spriteRenderer.sprite = playerBlueRaindrop; break;
                    case "Yellow": spriteRenderer.sprite = playerYellowStar; break;
                }

                colourBlindFriendly = true;
            }
            
            Debug.Log("You pressed 'P' ");
            Debug.Log($"Current Colour is {currentColour}");
        }
    }

    private void RecognisedCommand(PhraseRecognizedEventArgs phrase)
    {
        Debug.Log("You said: " + phrase.text);
        //also maybe turn it into subtitles/cc

        PlayerObject.actions[phrase.text].Invoke();
    }
}

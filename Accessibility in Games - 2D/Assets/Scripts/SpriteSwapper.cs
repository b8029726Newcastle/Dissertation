using System.Collections;
using System.Collections.Generic;
using System.Linq; //ToArray
using UnityEngine;
using UnityEngine.Windows.Speech; //import for KeywordRecognizer

public class SpriteSwapper : MonoBehaviour
{
    Player PlayerObject;

    public bool colourBlindFriendly = true; //Colour Blind-Friendly assets are ON by default

    [SerializeField]
    SpriteRenderer redSpriteRenderer, greenSpriteRenderer, blueSpriteRenderer, yellowSpriteRenderer, colourSwapperSpriteRenderer;

    [SerializeField]
    //SpriteRenderer cyanBase, yellowBase, magentaBase, pinkBase; //delete this
    Sprite redBase, greenBase, blueBase, yellowBase, //base assets
        redHeart, greenLeaf, blueRaindrop, yellowStar,
        colourSwapperBase, colourSwapperWithShapes;  //custom assets with shapes


    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {        
        if (PlayerObject.colourBlindFriendly == true) //turn OFF Colour Blind-Friendly assets by changing to base assets
        {
            redSpriteRenderer.sprite = redHeart;
            greenSpriteRenderer.sprite = greenLeaf;
            blueSpriteRenderer.sprite = blueRaindrop;
            yellowSpriteRenderer.sprite = yellowStar;
            colourSwapperSpriteRenderer.sprite = colourSwapperWithShapes;
        }
        else if (PlayerObject.colourBlindFriendly == false) //turn ON Colour Blind-Friendly assets by changing to base assets
        {            
            redSpriteRenderer.sprite = redBase;
            greenSpriteRenderer.sprite = greenBase;
            blueSpriteRenderer.sprite = blueBase;
            yellowSpriteRenderer.sprite = yellowBase;
            colourSwapperSpriteRenderer.sprite = colourSwapperBase;
        }
    }
    private void OnDestroy()
    {
        if (PlayerObject.keywordRecognizer != null)
        {
            PlayerObject.keywordRecognizer.Stop();
            PlayerObject.keywordRecognizer.Dispose();
        }
    }

    private void RecognisedCommand(PhraseRecognizedEventArgs phrase)
    {
        Debug.Log("You said: " + phrase.text);
        //also maybe turn it into subtitles/cc

        PlayerObject.actions[phrase.text].Invoke();
    }
}

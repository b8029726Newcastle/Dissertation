using System.Collections;
using System.Collections.Generic;
using System.Linq; //ToArray
using UnityEngine;
using UnityEngine.Windows.Speech; //import for KeywordRecognizer

public class ColourChangeSwapper : MonoBehaviour
{
    Player PlayerObject;

    public bool colourBlindFriendly = true; //Colour Blind-Friendly assets are ON by default

    [SerializeField]
    SpriteRenderer colourSwapperSpriteRenderer;

    [SerializeField]
    //SpriteRenderer cyanBase, yellowBase, magentaBase, pinkBase; //delete this
    Sprite colourSwapperBase, //base assets
        colourSwapperWithShapes;  //custom assets with shapes


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
            colourSwapperSpriteRenderer.sprite = colourSwapperWithShapes;
        }
        else if (PlayerObject.colourBlindFriendly == false) //turn ON Colour Blind-Friendly assets by changing to base assets
        {
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

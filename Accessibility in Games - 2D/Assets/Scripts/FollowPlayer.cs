using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if player has went past the camera position
        if(player.position.y - 2 > transform.position.y) // - 2 means camera goes up if player is 2 units ABOVE the center
        {
            transform.position = new Vector3(transform.position.x, player.position.y - 2, transform.position.z);
        }
        else if (player.position.y + 2 < transform.position.y) //maybe not follow player going down like Brackeys?  Means they fuccked up
        {
            transform.position = new Vector3(transform.position.x, player.position.y + 2, transform.position.z);
        }
    }
}

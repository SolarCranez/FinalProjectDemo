using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFollow : MonoBehaviour
{
    // references to player listener and player
    public GameObject playerListener;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // update position to player camera's position
    private void UpdateListenerPos()
    {
        playerListener.transform.position = player.transform.position;
        playerListener.transform.rotation = player.transform.rotation;
    }


    // Update is called once per frame
    void Update()
    {
        UpdateListenerPos();
    }
}

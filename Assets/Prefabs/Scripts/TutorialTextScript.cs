using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextScript : MonoBehaviour
{
    //Destroys the gameobject the script is attached to when a key is pressed
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Destroy(this.gameObject);
        }
    }
}

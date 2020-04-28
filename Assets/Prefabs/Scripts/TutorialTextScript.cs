using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextScript : MonoBehaviour
{
    public ControllerInput controllerInput;

    //Destroys the gameobject the script is attached to when the player gives movement input
    void Update()
    {
        if (Input.anyKeyDown || controllerInput.state == 2 || controllerInput.state == 3)
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ControllerInput : MonoBehaviour
{
    //Sets up communication with the Arduino via the serial port
    SerialPort stream = new SerialPort("COM3", 9600);

    public int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Opens the serial stream
        stream.Open();
    }

    // Update is called once per frame
    void Update()
    {
        //Parses the Arduino output as an integer
            string output = stream.ReadLine();
            state = int.Parse(output);
    }
}

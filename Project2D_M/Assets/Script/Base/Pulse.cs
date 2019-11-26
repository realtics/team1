using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public void PulseOn()
    {
        Time.timeScale = 0.0f;
    }

    public void PulseOff()
    {
        Time.timeScale = 1.0f;
    }
}

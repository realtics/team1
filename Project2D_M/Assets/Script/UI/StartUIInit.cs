using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIInit : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1920, (1080/16)*9, false);
    }
}

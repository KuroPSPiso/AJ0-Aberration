using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSettings : MonoBehaviour
{
    public float InputXMultiplier = 1f;
    public float InputYMultiplier = 1f;
    public float FOV = 60f;

    public bool IsReady { get { return this.isReady; } }

    private bool isReady;

    private void Start()
    {
        isReady = true;
    }
}

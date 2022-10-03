using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        iTween.MoveFrom(gameObject, iTween.Hash("x", 15.0f, "y", 5.0f, "z", 4.0f, "time", 3.0f, "delay", 1.5f));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToControl : MonoBehaviour
{
    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", 15.0f, "y", 5.0f, "z", 4.0f, "time", 3.0f, "delay", 1.5f));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByControl : MonoBehaviour
{
    void Start()
    {
        iTween.MoveBy(gameObject, iTween.Hash("z", 15, "y", 9, "time", 1.8f, "delay", 5.1f, "easetype", iTween.EaseType.easeOutElastic));
    }
}

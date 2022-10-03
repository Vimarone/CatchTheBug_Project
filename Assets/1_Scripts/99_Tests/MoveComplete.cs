using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComplete : MonoBehaviour
{
    [SerializeField] GameObject _ball2;
    [SerializeField] GameObject _ball3;

    // 큐브 하나 : 5초간 회전(방향 자율)
    // 5초가 지나면 공(sphere)이 있는 위치로 이동
    // 이동이 끝나면 이동값의 반대값으로 공이 이동
    // 이동이 끝나면 캡슐이 크기가 커지면서 공 옆으로 이동
    //

    void Start()
    {
        iTween.MoveFrom(gameObject, iTween.Hash("x", -15, "z", -3, "delay", 2.8f, "time", 5, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "ObjectMoveTo", "oncompletetarget", gameObject));
    }

    void ObjectMoveTo()
    {
        iTween.MoveTo(_ball2, iTween.Hash("y", 8, "z", 10, "time", 4, "easetype", iTween.EaseType.easeOutElastic, "oncomplete", "ObjectMoveBy", "oncompletetarget", gameObject));
    }

    void ObjectMoveBy()
    {
        iTween.MoveBy(_ball3, iTween.Hash("y", -10, "z", 6, "time", 6, "easetype", iTween.EaseType.easeOutQuint));
    }
}

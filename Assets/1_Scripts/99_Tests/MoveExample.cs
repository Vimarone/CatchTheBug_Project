using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveExample : MonoBehaviour
{
    [SerializeField] GameObject _cube;
    [SerializeField] GameObject _sphere;
    [SerializeField] GameObject _capsule;

    Vector3 _rotAngle = new Vector3(720, 1080, 900 );
    Vector3 _zeroPos = new Vector3();

    // 큐브 하나 : 5초간 회전(방향 자율)
    // 5초가 지나면 공(sphere)이 있는 위치로 이동
    // 이동이 끝나면 이동값의 반대값으로 공이 이동
    // 이동이 끝나면 캡슐이 크기가 커지면서 공 옆으로 이동
    //

    void Start()
    {
        iTween.RotateTo(_cube, iTween.Hash("rotation", _rotAngle, "time", 5, "easetype", iTween.EaseType.easeInOutBounce, "oncomplete", "CubeMoveTo", "oncompletetarget", gameObject));
    }

    


    void CubeMoveTo()
    {
        _zeroPos = _cube.transform.position;
        iTween.MoveTo(_cube, iTween.Hash("position", _sphere.transform.position, "time", 4, "easetype", iTween.EaseType.easeInExpo, "oncomplete", "SphereMoveTo", "oncompletetarget", gameObject));
    }

    void SphereMoveTo()
    {
        iTween.MoveTo(_sphere, iTween.Hash("position", _zeroPos, "time", 3, "easetype", iTween.EaseType.easeInOutQuart, "oncomplete", "CapsuleScaleTo", "oncompletetarget", gameObject));
    }

    void CapsuleScaleTo()
    {
        iTween.ScaleTo(_capsule, iTween.Hash("scale", Vector3.one * 3, "time", 6, "easetype", iTween.EaseType.easeInOutCubic));
        iTween.MoveTo(_capsule, iTween.Hash("position", _sphere.transform.position, "time", 6, "easetype", iTween.EaseType.easeInElastic));
    }
}

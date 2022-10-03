using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSelect : MonoBehaviour
{
    SpriteRenderer _backSpriteRenderer;
    int _stageNum = 0;

    void Awake()
    {
        _stageNum = UserInfoManager._instance._nowStageNumber - 1;
        _backSpriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Debug.Log(_stageNum);
    }
    void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ResourcePoolManager._instance.GetBackgroundImage(_stageNum);
        _backSpriteRenderer.size = new Vector2(40, 40);
     }
}

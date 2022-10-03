using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectControl : MonoBehaviour
{
    
    [SerializeField] float _eraseTime = 1.8f;
    [SerializeField] float _turnDelayTime = 2;
    [SerializeField] float _changeDirStartTime = 5;

    DefineHelper.eInsectKind _kind;
    Animator _aniControl;
    bool _isDead = false;
    float _movSpeed = 0;
    float _angle = 0;

    void Awake()
    {
        _aniControl = GetComponent<Animator>();
        InvokeRepeating("randomRotateAt", _changeDirStartTime, _turnDelayTime); //함수명, 시작 시간, 반복 간격
    }

    void Update()
    {
        if (_isDead)
            return;

        transform.Translate(Vector3.up * _movSpeed * Time.deltaTime);

        //_passTime += Time.deltaTime;
        //if (_passTime >= _turnDelayTime)
        //{
        //    _passTime = 0;
        //    randomRotateAt();
        //}


    }

    //private void OnMouseDown() //콜라이더 필요
    //{
    //    if (_isDead)
    //    {
    //        return;
    //        GetComponent<CircleCollider2D>().enabled = false;
    //    }

    //    Debug.Log(gameObject.name + " Touch!!!");
    //    _isDead = true;
    //    _aniControl.SetBool("IsDead", _isDead);
    //    CancelInvoke("randomRotateAt");
    //    Destroy(gameObject, _eraseTime);
    //}

    IEnumerator OnMouseDown()
    {
        if (!DialogueManager.Paused)
        {
            GetComponent<CircleCollider2D>().enabled = false;

            _isDead = true;
            _aniControl.SetBool("IsDead", _isDead);
            CancelInvoke("randomRotateAt");
            IngameManager._instance.AddKillCount(_kind);    //직접 접근(어디에서나 사용 가능)
                                                            //SoundManager._instance.PlaySFXSound(DefineHelper.eSFXClipType.Die);
            SoundManager._instance.PlaySFXSoundOneShot(DefineHelper.eSFXClipType.Die);
            yield return new WaitForSeconds(_eraseTime);
            Destroy(gameObject, _eraseTime);
        }
    }
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        _isDead = true;
        _aniControl.SetBool("IsDead", _isDead);
        CancelInvoke("randomRotateAt");
        IngameManager._instance.AddKillCount(_kind);
        SoundManager._instance.PlaySFXSoundOneShot(DefineHelper.eSFXClipType.Die);
        yield return new WaitForSeconds(_eraseTime);
        Destroy(gameObject, _eraseTime);
    }

    void randomRotateAt()
    {
        int p = Random.Range(0, 3);
        float angle = 0.0f;
        switch (p)
        {
            case 1:
                angle = _angle;
                break;
            case 2:
                angle = -_angle;
                break;
        }
        //Debug.Log(p);
        transform.Rotate(0, 0, angle);
    }

    public void InitializeData(DefineHelper.eInsectKind k)
    {
        _kind = k;
        _movSpeed = DefineHelper._standardSpeed * DefineHelper._standardSpeedScalePerInsect[(int)k];
        _angle = DefineHelper._standardAngle * DefineHelper._standardAngleScalePerInsect[(int)k];
        _aniControl.speed = DefineHelper._standardSpeedScalePerInsect[(int)k];
    }
}

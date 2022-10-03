using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWindow : MonoBehaviour
{
    [SerializeField] GameObject _contentRoot;
    [SerializeField] Text _txtStaticLoading;
    [SerializeField] Slider _bar;
    [SerializeField] Text _txtTipString;

    Animator _aniController;

    int _limitCount = 6;
    float _checkTime = 0;
    int _count = 0;
    //float _loadingRate = 10.0f;

    void Awake()
    {
        _aniController = transform.GetChild(0).GetComponent<Animator>();
    }

    void Start()
    {
        //임시========
        //OpenWindow();
        //============
    }

    void Update()
    {
        // 로딩 문구에 0.5초마다 점 하나씩 추가
        // 로딩에 10초 소요

        if(_contentRoot.activeSelf){
            _checkTime += Time.deltaTime;
            if(_checkTime >= 0.5f)
            {
                _checkTime = 0;
                _txtStaticLoading.text = "Loading";
                for(int n = 0; n < _count; n++)
                {
                    _txtStaticLoading.text += ".";
                }
                if (++_count >= _limitCount)
                    _count = 0;
            }
            //SetLoadingProgress(_loadingRate += (Time.deltaTime / 10));
        }
    }

    

    public void OpenWindow()
    {
        _contentRoot.SetActive(false);

        _txtTipString.text = ResourcePoolManager._instance.GetRandomTipString();
        SetLoadingProgress(0);
        _txtStaticLoading.text = "Loading";
        _count++;
    }

    public void SetLoadingProgress(float pro)
    {
        _bar.value = pro;
        if (_bar.value == 1)
        {
            _contentRoot.SetActive(false);
            _aniController.SetTrigger("Open");
        }
    }

    public void EnableContents()
    {
        _contentRoot.SetActive(true);
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}

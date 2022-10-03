using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    static MainSceneManager _uniqueInstance;

    
    [SerializeField] GameObject _prefabSettingsWnd;
    [SerializeField] Sprite[] _slotTrans;
    [SerializeField] Color[] _changeColors;

    Text _txtTitle;
    Text _txtSubTitle;
    StageSelectWnd _selectWnd;
    SettingsWnd _settingWnd;



    int _idx = 0;
    //int _colorCnt = 4;
    float _colorChangeTime = 3f;
    float _alphaChangeTime = 3.5f;
    float _passTime = 0;
    float _passTime_alpha = 0;
    float _minAlphaValue = 0.05f;
    float _maxAlphaWalue = 1;
    float _changeOffsetTime = 0.5f;

    bool _isFaded = false;

    public static MainSceneManager _instance
    {
        get { return _uniqueInstance; }
    }

    private void Awake()
    {
        _uniqueInstance = this;
    }

    void Start()
    {
        // 임시
        InitSettingData();
        // =====
    }

    void LateUpdate()
    {
        _passTime += Time.deltaTime;
        _passTime_alpha += Time.deltaTime;


        if (_passTime_alpha >= _alphaChangeTime) 
        {
            _passTime_alpha = 0;
            _isFaded = !_isFaded;
            if (_isFaded) 
            {
                _txtTitle.CrossFadeAlpha(_minAlphaValue, _alphaChangeTime - _changeOffsetTime, false);
            }
            else 
            {
                _txtTitle.CrossFadeAlpha(_maxAlphaWalue, _alphaChangeTime - _changeOffsetTime, false);
            }
        }

        if(_passTime >= _colorChangeTime)
        {
            _passTime = 0;
            _txtSubTitle.CrossFadeColor(ChangeColor(_idx++), _colorChangeTime - _changeOffsetTime, false, true);
            if (_idx >= _changeColors.Length)
                _idx = 0;
        }
    }

    void InitSettingData()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UIMainTitle");
        _txtTitle = go.GetComponent<Text>();
        _txtSubTitle = go.transform.GetChild(0).GetComponent<Text>();
    }

    Color ChangeColor(int id)
    {
        return _changeColors[id];
    }

    public Sprite GetSpriteTrans(DefineHelper.eStageSelectColor color)
    {
        return _slotTrans[(int)color];
    }

    public void ClickOptionButton()
    {
        if (_settingWnd == null)
        {
            GameObject go = Instantiate(_prefabSettingsWnd);
            _settingWnd = go.GetComponent<SettingsWnd>();
        }
        _settingWnd.OpenSettingsWdn();
    }
    public void ClickStageButton()
    {
        
        if (_selectWnd == null)
        {
            GameObject go = Instantiate(ResourcePoolManager._instance.GetUIPrefabFromType(DefineHelper.eUIWindowType.StageInfoSelectWindow));
            _selectWnd = go.GetComponent<StageSelectWnd>();
        }
        _selectWnd.OpenMenuBtn(UserInfoManager._instance._clearStage);
        
    }
}

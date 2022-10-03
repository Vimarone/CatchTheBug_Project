using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreCounting : MonoBehaviour
{
    [SerializeField] Image _marker;
    [SerializeField] Text _txtCount;
    [SerializeField] Text _txtScore;

    DefineHelper.eResultCounting _state;

    int _targetScore = 0;
    float _drawScore = 0;
    float _countingTime = 1.5f;
    bool _isCount = false;

    public int _calcScore
    {
        get { return _targetScore; }
    }  

    public float _returnCountingTime
    {
        get { return _countingTime; }
    }

    void LateUpdate()
    {
        // 1. 시간으로 값이 카운팅
        // 2. 크기에 상관없이 1.5초 안에 카운팅이 끝남
        if (_isCount)
        {
            if(_targetScore <= _drawScore)
            {
                _txtScore.text = _targetScore.ToString();
                _isCount = false;
                
            }
            else
            {
                _drawScore += _targetScore * Time.deltaTime / _countingTime;
                //Time.deltaTime이 쌓여서(+=) 1이 되는 순간 _targetScore가 정상적으로 출력되는 것을 _countingTime으로 나눠 _countingTime이 되는 순간 _targetScore가 정상적으로 출력되게 함
                _txtScore.text = ((int)_drawScore).ToString();
            }
        }
    }

    public void InitDataSet(DefineHelper.eInsectKind kind, int killCnt)
    {
        _marker.sprite = ResourcePoolManager._instance.GetInsectTypeIcon(kind);
        _txtCount.text = killCnt.ToString();
        _txtScore.text = "0";
        _targetScore = killCnt * DefineHelper._baseScorePerInsect[(int)kind];
        _isCount = true;
    }
}

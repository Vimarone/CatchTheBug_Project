using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreCounting_00 : MonoBehaviour
{
    [SerializeField] Image _marker;
    [SerializeField] Text _txtCount;
    [SerializeField] Text _txtScore;

    int _targetScore = 0;
    float _drawScore = 0;
    float _countingTime = 1.5f;
    bool _isCount = false;


    ResultWindow_00 _ownRef;
    

    public int _calcScore
    {
        get { return _targetScore; }
    }

    public bool _endCounting
    {
        get { return !_isCount; }
    }

    void LateUpdate()
    {
        if (_isCount)
        {
            if (_targetScore <= _drawScore)
            {
                _txtScore.text = _targetScore.ToString();
                _isCount = false;
            }
            else
            {
                _drawScore += _targetScore * Time.deltaTime / _countingTime;
                _txtScore.text = ((int)_drawScore).ToString();
            }
        }
    }

    public void InitDataSet(DefineHelper.eInsectKind kind, int killCnt, ResultWindow_00 owner)
    {
        _ownRef = owner;
        _marker.sprite = ResourcePoolManager._instance.GetInsectTypeIcon(kind);
        _txtCount.text = killCnt.ToString();
        _txtScore.text = "0";
        _targetScore = killCnt * DefineHelper._baseScorePerInsect[(int)kind];
        _isCount = true;
    }
}

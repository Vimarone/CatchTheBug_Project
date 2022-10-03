using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultWindow : MonoBehaviour
{
    [SerializeField] GameObject _prefabScoreCounter;
    [SerializeField] RectTransform _Content;
    [SerializeField] Text _txtTotalScore;

    DefineHelper.eResultCounting _state;
    ScoreCounting sc;

    float _baseSize = 80;
    float _offsetY = 10;

    
    int _sumScore = 0;
    float _returnTime = 0;

    float _drawResScore = 0;
    float _countingTime = 1.5f;
    bool _isResultCount = false;

    void Start()
    {
        // 임시
        Dictionary<DefineHelper.eInsectKind, int> scoreData = new Dictionary<DefineHelper.eInsectKind, int>();
        scoreData.Add(DefineHelper.eInsectKind.GreenInsect, 1023);
        scoreData.Add(DefineHelper.eInsectKind.RedAnt, 1023);
        OpenResultWindow(scoreData);
        // ====
    }

    void LateUpdate()
    {
        switch (_state)
        {
            case DefineHelper.eResultCounting.none:
                break;
            case DefineHelper.eResultCounting.IndividualScore:
                break;
            case DefineHelper.eResultCounting.TotalScore:
                break;
            case DefineHelper.eResultCounting.Complete:
                break;
        }

        if (_isResultCount)
        {
            if (_sumScore <= _drawResScore)
            {
                _txtTotalScore.text = _sumScore.ToString("#,##0");
                _isResultCount = false;
            }
            else
            {
                _drawResScore += _sumScore * Time.deltaTime / (_countingTime + _returnTime);
                _txtTotalScore.text = ((int)_drawResScore).ToString("#,##0");
            }
        }
    }

    public void OpenResultWindow(Dictionary<DefineHelper.eInsectKind, int> scoreData)
    {
        // 개별 카운트가 멈추는 순간 토탈 카운트가 움직여야 함
        // 혹은 개별 카운트가 멈추고 좀 더 있다가 토탈 카운트가 멈춰야 함
        // 단, 심표 표시가 있어야 함

        for (int n = 0; n < scoreData.Count; n++)
        {
            GameObject go = Instantiate(_prefabScoreCounter, _Content);
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(_offsetY, -_offsetY - (_baseSize * n));
            sc = go.GetComponent<ScoreCounting>();
            sc.InitDataSet((DefineHelper.eInsectKind)n, scoreData[(DefineHelper.eInsectKind)n]);
            _sumScore += sc._calcScore;
        }
        _returnTime = sc._returnCountingTime;
        _isResultCount = true;
    }
}

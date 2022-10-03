using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultWindow_00 : MonoBehaviour
{
    [SerializeField] RectTransform _content;
    [SerializeField] Text _txtTotalScore;

    float _baseSize = 80;
    float _startPos = 20;

    List<ScoreCounting_00> _perScores;
    ItemScoreCount _itemScore;
    int _totalScore = 0;
    float _drawScore = 0;
    float _countingTime = 1.5f;

    DefineHelper.eResultCounting _state;


    void Start()
    {
        //    임시
        //    Dictionary<DefineHelper.eInsectKind, int> scoreData = new Dictionary<DefineHelper.eInsectKind, int>();
        //    scoreData.Add(DefineHelper.eInsectKind.GreenInsect, 1023);
        //    scoreData.Add(DefineHelper.eInsectKind.RedAnt, 1023);
        //    //scoreData.Add(DefineHelper.eInsectKind.BlackAnt, 81);
        //    OpenResultWindow(scoreData);
        //     ====
    }

    void LateUpdate()
    {
        switch (_state)
        {
            // 카운팅이 끝나는 시점을 감지하여 토탈 스코어 계산

            case DefineHelper.eResultCounting.IndividualScore:
                _state = DefineHelper.eResultCounting.ItemScore;
                for (int n = 0; n < _perScores.Count; n++)
                {
                    if (!_perScores[n]._endCounting)
                        _state = DefineHelper.eResultCounting.IndividualScore;
                }
                break;
            case DefineHelper.eResultCounting.ItemScore:
                _state = DefineHelper.eResultCounting.TotalScore;
                if (!_itemScore._endCounting)
                    _state = DefineHelper.eResultCounting.ItemScore;
                break;
            case DefineHelper.eResultCounting.TotalScore:
                if(_totalScore < 0)
                {
                    if (_totalScore >= _drawScore)
                    {
                        _txtTotalScore.text = string.Format("{0:#,###}", _totalScore);
                        _state = DefineHelper.eResultCounting.Complete;
                    }
                    else
                    {
                        _drawScore += _totalScore * (Time.deltaTime / _countingTime);
                        _txtTotalScore.text = string.Format("{0:#,###}", (int)_drawScore);
                    }
                }
                else{
                    if (_totalScore <= _drawScore)
                    {
                        _txtTotalScore.text = string.Format("{0:#,###}", _totalScore);
                        _state = DefineHelper.eResultCounting.Complete;
                    }
                    else
                    {
                        _drawScore += _totalScore * (Time.deltaTime / _countingTime);
                        _txtTotalScore.text = string.Format("{0:#,###}", (int)_drawScore);
                    }
                }
                
                break;
        }
    }
    
    public void OpenResultWindow(Dictionary<DefineHelper.eInsectKind, int> scoreData)
    {
        int cnt = 0;
        _perScores = new List<ScoreCounting_00>();
        _content.sizeDelta = new Vector2(_content.sizeDelta.x, _startPos + (_baseSize * scoreData.Count));

        GameObject props = ResourcePoolManager._instance.GetUIPropsPrefabFromType(DefineHelper.eUIPropsType.ResultProps);
        foreach (DefineHelper.eInsectKind key in scoreData.Keys)
        {
            GameObject go = Instantiate(props, _content);
            RectTransform rtf = go.GetComponent<RectTransform>();
            rtf.anchoredPosition = new Vector2(0, -_startPos - (_baseSize * cnt++));
            ScoreCounting_00 scg = go.GetComponent<ScoreCounting_00>();
            scg.InitDataSet(key, scoreData[key], this);

            _perScores.Add(scg);

            _totalScore += scg._calcScore;
        }
        _txtTotalScore.text = "0";
        _state = DefineHelper.eResultCounting.IndividualScore;
    }
    public void OpenItemScoreWindow(DefineHelper.eItemType type, int usedCount)
    {
        _itemScore = gameObject.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<ItemScoreCount>();
        _itemScore.InitDataSet(type, usedCount);

        _totalScore -= _itemScore._calcScore;
    }
    


    public void CommonDownButton(RectTransform rtf)
    {
        rtf.anchoredPosition = new Vector2(rtf.anchoredPosition.x, rtf.anchoredPosition.y - 15);
    }
    public void CommonUpButton(RectTransform rtf)
    {
        rtf.anchoredPosition = new Vector2(rtf.anchoredPosition.x, rtf.anchoredPosition.y + 15);
    }

    public void ClickHomeButton(string ex)
    {
        //Debug.Log(ex + "홈버튼 클릭됨");
        SceneControlManager._instance.StartMainScene();
    }

    public void ClickRegameButton()
    {
        //SceneManager.LoadScene("IngameScene");
        //Destroy(gameObject);
        SceneControlManager._instance.StartIngameScene();
    }

    void ClickExitButton()          // public이 아닐 때 실행시키는 방법 : OnClick에서 SendMessage(string) 선택 후 함수명 입력
    {
        //Debug.Log("나가기 버튼 클릭됨");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();         // application에서만 작동(editor 상에서는 적용되지 않음)
#endif
    }
}

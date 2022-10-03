using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectWnd : MonoBehaviour
{

    [SerializeField] GameObject _menu;
    [SerializeField] Transform _sPos;
    [SerializeField] Transform _ePos;
    [SerializeField] Transform _stageSlotParent;
    [SerializeField] RectTransform _content;
    [SerializeField] Text _stageLimitTime;
    [SerializeField] Text _stageNameTxt;
    [SerializeField] GameObject _selectItem;                //아이템 선택창


    List<StageSlot> _stageList = new List<StageSlot>();
    List<ScoreInfo> _infoList = new List<ScoreInfo>();


    float _basePosY = 100;
    float _offsetY = 10;

    public void OpenMenuBtn(int clearStage){
        iTween.MoveTo(_menu, iTween.Hash("x", _sPos.position.x, "time", 0.5f, "easetype", iTween.EaseType.easeOutBack));
        _stageNameTxt.text = string.Empty;
        _stageLimitTime.text = "0";
        GameObject props = ResourcePoolManager._instance.GetUIPropsPrefabFromType(DefineHelper.eUIPropsType.StageSlot);
        for (int n = 0; n < _stageSlotParent.childCount; n++)
        {
            GameObject _stage = Instantiate(props, _stageSlotParent.GetChild(n));
            StageSlot _stSlot = _stage.GetComponent<StageSlot>();

            //===========
            DefineHelper.eStageSelectColor stageColor = DefineHelper.eStageSelectColor.Locked;
            DefineHelper.stStageInfo info = ResourcePoolManager._instance.GetStageInfo(n + 1);
            if (n <= clearStage)
                stageColor = DefineHelper.eStageSelectColor.Free;
            _stSlot.InitDataSet(n + 1, stageColor, info._diffLevel, this);
            _stageList.Add(_stSlot);
            //===========

            /*
            if (n < clearStage)
                _stSlot.InitDataSet(n + 1, DefineHelper.eStageSelectColor.Free, 0, this);
            else
                _stSlot.InitDataSet(n + 1, DefineHelper.eStageSelectColor.Locked, 0, this);
            */
        }
    }
    public void CloseMenuBtn()
    {
        iTween.MoveTo(_menu, iTween.Hash("x", _ePos.position.x, "time", 0.25f, "easetype", iTween.EaseType.easeInBack));
    }

    public void ShowSelectStageInfo(int stageNum)
    {
        DefineHelper.stStageInfo info = ResourcePoolManager._instance.GetStageInfo(stageNum);
        UserInfoManager._instance._nowStageNumber = stageNum;
        for(int n = 0; n < _stageList.Count; n++)
        {
            if (_stageList[n]._myNumber == stageNum)
                continue;
            _stageList[n].NonSelect();
        }
        _stageNameTxt.text = info._name;
        _stageLimitTime.text = info._limitTime.ToString();
        /*
        _stageLimitTime.text = MainSceneManager._instance.GetStageInfo(stageNum)._limitTime + "";
        _stageNameTxt.text = MainSceneManager._instance.GetStageInfo(stageNum)._name;
        */

        //scoreInfo 생성
        for(int n = 0; n < _infoList.Count; n++)
        {
            Destroy(_infoList[n].gameObject);
        }
        _infoList.Clear();

        _content.sizeDelta = new Vector2(_content.sizeDelta.x, (_offsetY + _basePosY) * info._insectKinds.Length);

        GameObject props = ResourcePoolManager._instance.GetUIPropsPrefabFromType(DefineHelper.eUIPropsType.InsectScoreInfoProps);
        for (int n = 0; n < info._insectKinds.Length; n++)
        {
            GameObject go = Instantiate(props, _content);
            RectTransform rtf = go.GetComponent < RectTransform>();
            rtf.anchoredPosition += new Vector2(rtf.anchoredPosition.x, -(_offsetY + _basePosY) * n);
            ScoreInfo si = go.GetComponent<ScoreInfo>();
            si.InitDataSet(info._insectKinds[n]);
            _infoList.Add(si);
        }
    }

    public void ClickStartGameButton()
    {
        _selectItem.SetActive(true);
    }

    public void ClickSelectItemButton()
    {
        SceneControlManager._instance.StartIngameScene();
    }

    public void CloseSelectBtn()
    {
        _selectItem.SetActive(false);
    }
}

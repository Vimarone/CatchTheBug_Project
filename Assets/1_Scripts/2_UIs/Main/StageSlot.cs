using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSlot : MonoBehaviour
{
    [SerializeField] Image[] _star;
    [SerializeField] Image _lock;

    StageSelectWnd _ownerWnd;
    Image _bg;
    int _no;

    DefineHelper.eStageSelectColor _currentState;

    public int _myNumber
    {
        get { return _no; }
    }

    public void InitDataSet(int stageNum, DefineHelper.eStageSelectColor state, int clearDiffLevel, StageSelectWnd owner)       //InitDataSet : 이 객체가 생성되자 마자 실행
    {
        _ownerWnd = owner;
        _no = stageNum;
        _currentState = state;
        // 락 해제 여부 확인
        _bg = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        if(_currentState != DefineHelper.eStageSelectColor.Locked)
        {
            _lock.enabled = false;      //기본이 락 걸려있는 상태 = 락이 아니면 풀어줌
        }
        _bg.sprite = MainSceneManager._instance.GetSpriteTrans(_currentState);
        if (_currentState == DefineHelper.eStageSelectColor.Locked)
            return;

        // 클리어 레벨(별 개수) 확인 : 락이 걸려있으면 굳이 클리어할 이유가 없으므로 락이 걸려있을 때 리턴
        for (int i = 0; i < clearDiffLevel; i++)
        {
            _star[i].enabled = true;
        }
    }
    public void NonSelect()
    {
        if (_currentState != DefineHelper.eStageSelectColor.Locked)
            _currentState = DefineHelper.eStageSelectColor.Free;
        _bg.sprite = MainSceneManager._instance.GetSpriteTrans(_currentState);
    }

    public void ClickSelect()
    {
        if(_currentState == DefineHelper.eStageSelectColor.Free)
        {
            _ownerWnd.ShowSelectStageInfo(_no);
            _currentState = DefineHelper.eStageSelectColor.Selected;
            _bg.sprite = MainSceneManager._instance.GetSpriteTrans(_currentState);
        }
    }
}

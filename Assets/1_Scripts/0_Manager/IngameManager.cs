using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


// 게임 전체 흐름 제어
public class IngameManager : MonoBehaviour
{
    static IngameManager _uniqueInstance;

    // 참조 변수
    Generators _insectGenerator;
    MessageBox _msgBox;
    TimerBox _timerBox;
    CountBox _countBox;
    ItemBox _itemBox;

    [SerializeField] GameObject _bombEffect;
    [SerializeField] GameObject _bombObj;
    GameObject _bomb;

    // 정보 변수
    DefineHelper.eIngameState _currentState;
    DefineHelper.eItemType _currentItem;
    Dictionary<DefineHelper.eInsectKind, int> _killCounts;
    float _passTime = 0;
    int _startCount = 3;                            // 카운트다운 시간
    float _limitPlayTime = 0;                      // 플레이 제한 시간
    float _resetPlayTime = 0;
    float _endDelayTime = 2;                        // 타임오버 출력 시간
    int _itemCount = 0;

    DefineHelper.eInsectKind[] _genderInsects;

    int[] _genRateValues;

    // 임시
    // DefineHelper.eInsectKind[] k = { DefineHelper.eInsectKind.GreenInsect, DefineHelper.eInsectKind.RedAnt };
    // int[] r = { 35, 100 };
    // =====

   

    public static IngameManager _instance //정적 멤버변수
    {
        get { return _uniqueInstance; } 
    }

    public DefineHelper.eItemType GetCurrentItemType()
    {
        return _currentItem;
    }

    public int GetItemCount()
    {
        return _itemCount;
    }

    void Awake()
    {
        _uniqueInstance = this;
        _killCounts = new Dictionary<DefineHelper.eInsectKind, int>();
        _currentItem = SceneControlManager._instance.GetItem();
    }

    void Start()
    {
        // 임시
        //InitializeSettings(60);
        // ====
        //Debug.Log(_currentItem.ToString());
    }

    void Update()
    {        
        switch (_currentState)
        {
            case DefineHelper.eIngameState.COUNT:
                _passTime += Time.deltaTime;
                _msgBox.SetCounting((int)_passTime);
                if (_startCount - _passTime <= -1)
                    PlayGame();
                break;
            case DefineHelper.eIngameState.PLAY:
                _limitPlayTime -= Time.deltaTime;
                if (Input.GetButtonDown("UseItem"))
                {
                    switch (_currentItem)
                    {
                        case DefineHelper.eItemType.TimeStone:
                            StartCoroutine(UseTimeStone());
                            break;
                        case DefineHelper.eItemType.Bomb:
                            StartCoroutine(UseBomb());
                            break;
                        case DefineHelper.eItemType.None: 
                            //Debug.Log("뭐");
                            break;
                    }
                }
                if (_limitPlayTime <= 0)
                    EndGame();
                _timerBox.SettingTimer(_limitPlayTime);
                break;
            case DefineHelper.eIngameState.END:
                _passTime += Time.deltaTime;
                if (_passTime >= _endDelayTime)
                    ResultGame();
                break;
            case DefineHelper.eIngameState.RESULT:

                break;
        }
    }


    #region [State 함수]
    public void InitializeSettings(DefineHelper.stStageInfo stageInfo)
    {

#if UNITY_EDITOR
        _limitPlayTime = 10;
#else
        _limitPlayTime = stageInfo._limitTime;
#endif
        _genderInsects = stageInfo._insectKinds;
        _genRateValues = stageInfo._insectGenRate;
        _resetPlayTime = _limitPlayTime;
        GameObject go = null;

        _insectGenerator = GetComponent<Generators>();
        go = GameObject.FindGameObjectWithTag("UIMessageBox");
        _msgBox = go.GetComponent<MessageBox>();
        go = GameObject.FindGameObjectWithTag("UITimerBox");
        _timerBox = go.GetComponent<TimerBox>();
        go = GameObject.FindGameObjectWithTag("UICountBox");
        _countBox = go.GetComponent<CountBox>();
        go = GameObject.FindGameObjectWithTag("UIItemBox");
        _itemBox = go.GetComponent<ItemBox>();


        _msgBox.CloseMessageBox();
        _timerBox.InitSetData(_limitPlayTime);
        _countBox.InitSetData(_genderInsects);
        _itemBox.InitSetData(_currentItem);
        // 임시
        StartGameCount(_startCount);
        // ====
    }


    void StartGameCount(int count)
    {
        _currentState = DefineHelper.eIngameState.COUNT;
        _msgBox.OpenMessageBox("Game Start~!", DefineHelper.eMessageBoxKind.COUNTER, count);
        for (int n = 0; n < _genderInsects.Length; n++)
            _killCounts.Add(_genderInsects[n], 0);
    }
    void PlayGame()
    {
        _currentState = DefineHelper.eIngameState.PLAY;
        _msgBox.CloseMessageBox();
        _insectGenerator.StartInsectGenerate(_genderInsects, _genRateValues);
    }
    void EndGame()
    {
        _currentState = DefineHelper.eIngameState.END;

        _passTime = 0;      //테스트 타임 = 0; 
        _msgBox.OpenMessageBox("Time Over");
        _insectGenerator.EndInsectGenerate();
        UserInfoManager._instance._clearStage = (UserInfoManager._instance._clearStage < UserInfoManager._instance._nowStageNumber)
            ? ++UserInfoManager._instance._clearStage : UserInfoManager._instance._clearStage;
    }
    void ResultGame()
    {
        _currentState = DefineHelper.eIngameState.RESULT;
        
        _msgBox.CloseMessageBox();
        //종료창을 생성

        FileStream fs = new FileStream(UserInfoManager._instance._userInfo, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        int clearStage = UserInfoManager._instance._nowStageNumber;
        string temp = " ";
        sw.Write(clearStage + temp + SoundManager._instance.GetVolumes()[0] + temp + SoundManager._instance.GetVolumes()[1]);
        sw.Close();
        fs.Close();

        GameObject go = Instantiate(ResourcePoolManager._instance.GetUIPrefabFromType(DefineHelper.eUIWindowType.ResultWindow));
        ResultWindow_00 wnd = go.GetComponent<ResultWindow_00>();
        wnd.OpenResultWindow(_killCounts);
        wnd.OpenItemScoreWindow(_currentItem, _itemCount);
    }
    #endregion


    #region Item 함수

    IEnumerator UseTimeStone()
    {
        _limitPlayTime = _resetPlayTime;
        _timerBox.InitSetData(_limitPlayTime);
        _itemBox.SetCount(++_itemCount);
        _msgBox.OpenMessageBox("You just used TimeStone");
        yield return new WaitForSeconds(1.0f);
        _msgBox.CloseMessageBox();
        yield return null;
    }

    IEnumerator UseBomb()
    {
        _msgBox.OpenMessageBox("Click to drop the Bomb");
        yield return new WaitForSeconds(0.5f);
        _msgBox.CloseMessageBox();
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        Vector3 pos = Input.mousePosition;
        _bomb = Instantiate(_bombObj, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("UIItemBox").transform);
        yield return new WaitForSeconds(2.0f);
        Destroy(_bomb);
        _itemBox.SetCount(++_itemCount);

#if UNITY_EDITOR
        Vector3 mapPos = new Vector3(pos.x / 1070 * 34 - 17, pos.y / 600 * 20 - 10, 0);
        GameObject effect = Instantiate(_bombEffect, mapPos, Quaternion.identity, GameObject.FindGameObjectWithTag("UIItemBox").transform);
        effect.transform.localScale *= 15;
        Destroy(effect, 0.8f);
        yield return null;
#else
        Vector3 mapPos = new Vector3(pos.x / 1920 * 34 - 17, pos.y / 1080 * 20 - 10, 0);
        GameObject effect = Instantiate(_bombEffect, mapPos, Quaternion.identity, GameObject.FindGameObjectWithTag("UIItemBox").transform);
        effect.transform.localScale *= 15;
        Destroy(effect, 0.8f);
        yield return null;
#endif

    }

    void UseNone()
    {
        
    }
    #endregion

    

    public void AddKillCount(DefineHelper.eInsectKind kind)
    {
        _killCounts[kind]++;

        // 화면에 출력
        _countBox.SetCount(kind, _killCounts[kind]);
    }
}

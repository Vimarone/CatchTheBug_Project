using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
{
    static SceneControlManager _uniqueInstance;

    DefineHelper.eSceneIndex _prevScene;
    DefineHelper.eSceneIndex _currScene;        //Current Scene

    DialogWnd _wndExit;

    DefineHelper.eItemType _selectedItem;

    public static SceneControlManager _instance
    {
        get { return _uniqueInstance; }
    }

    void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 임시
        StartMainScene();
        //=====
    }


    public void StartMainScene()
    {
        _prevScene = _currScene;
        _currScene = DefineHelper.eSceneIndex.MainScene;

        StartCoroutine(LoadingScene(DefineHelper.eSceneIndex.MainScene.ToString()));

        SoundManager._instance.PlayBGMSound(DefineHelper.eBGMClipType.MainScene);
    }
    public void StartIngameScene()
    {
        _prevScene = _currScene;
        _currScene = DefineHelper.eSceneIndex.IngameScene;
        StartCoroutine(LoadingScene(DefineHelper.eSceneIndex.IngameScene.ToString()));

        SoundManager._instance.PlayBGMSound(DefineHelper.eBGMClipType.IngameScene);
    }

    IEnumerator LoadingScene(string sceneName)
    {
        _prevScene = _currScene;
        _currScene = DefineHelper.eSceneIndex.none;
        GameObject go = Instantiate(ResourcePoolManager._instance.GetUIPrefabFromType(DefineHelper.eUIWindowType.LoadingWindow), transform);
        LoadingWindow wnd = go.GetComponent<LoadingWindow>();
        wnd.OpenWindow();
        yield return new WaitForSeconds(2);
        AsyncOperation aOper = SceneManager.LoadSceneAsync(sceneName);
        while (!aOper.isDone)
        {
            //Debug.Log(_aOper.progress); 
            wnd.SetLoadingProgress(aOper.progress);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        wnd.SetLoadingProgress(aOper.progress);
        //Debug.Log("외부 : " + aOper.progress);
        while(go != null)
            yield return null;

        _currScene = _prevScene;
        // 씬 시작처리.....
        if(_currScene == DefineHelper.eSceneIndex.IngameScene)
        {
            int no = UserInfoManager._instance._nowStageNumber;
            DefineHelper.stStageInfo info = ResourcePoolManager._instance.GetStageInfo(no);
            IngameManager._instance.InitializeSettings(info);
        }
    }
    public DefineHelper.eSceneIndex NowScene()
    {
        return _currScene;
    }

    public void SetItem(DefineHelper.eItemType type)
    {
        _selectedItem = type;
    }

    public DefineHelper.eItemType GetItem()
    {
        return _selectedItem;
    }

}

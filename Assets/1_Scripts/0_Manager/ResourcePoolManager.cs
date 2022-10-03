using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoolManager : MonoBehaviour
{
    static ResourcePoolManager _uniqueInstance;

    [SerializeField] string[] _tipStrings;
    [SerializeField] GameObject[] _uiPrefabs;
    [SerializeField] GameObject[] _uiPropsPrefabs;
    [SerializeField] Sprite[] _insectTypeIcons;
    [SerializeField] Sprite[] _backgroundTypes;
    [SerializeField] AudioClip[] _bgmClips;
    [SerializeField] AudioClip[] _sfxClips;
    [SerializeField] Sprite[] _muteTypes;
    [SerializeField] Sprite[] _itemSprites;

    // 임시
    Dictionary<int, DefineHelper.stStageInfo> _dummyStageInfoList;
    // ======

    public static ResourcePoolManager _instance
    {
        get { return _uniqueInstance;  }
    }

    void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);

        DummyData();
    }

    public string GetRandomTipString()
    {
        int idx = Random.Range(0, _tipStrings.Length);

        return _tipStrings[idx];
    }


    public GameObject GetUIPrefabFromType(DefineHelper.eUIWindowType windowType)
    {
        return _uiPrefabs[(int)windowType];
    }

    public GameObject GetUIPropsPrefabFromType(DefineHelper.eUIPropsType propsType)
    {
        return _uiPropsPrefabs[(int)propsType];
    }

    public Sprite GetInsectTypeIcon(DefineHelper.eInsectKind kind)
    {
        return _insectTypeIcons[(int)kind];
    }

    public DefineHelper.stStageInfo GetStageInfo(int index)     //index : 스테이지 번호
    {
        return _dummyStageInfoList[index];
    }

    public Sprite GetBackgroundImage(int no)
    {
        return _backgroundTypes[no];
    }

    public AudioClip GetBgmClipFromType(DefineHelper.eBGMClipType type)
    {
        return _bgmClips[(int)type];
    }

    public AudioClip GetSFXClipFromType(DefineHelper.eSFXClipType type)
    {
        return _sfxClips[(int)type];
    }

    public Sprite GetMuteSpriteFromType(DefineHelper.eMuteType type)
    {
        return _muteTypes[(int)type];
    }

    public Sprite GetItemSpriteFromType(DefineHelper.eItemType type)
    {
        return _itemSprites[(int)type];
    }

    void DummyData()
    {
        
        _dummyStageInfoList = new Dictionary<int, DefineHelper.stStageInfo>();

        // 1.
        int[] rate = { 100 };
        DefineHelper.stStageInfo info = new DefineHelper.stStageInfo("조금 지저분한 다용도실", 60, 0, rate, DefineHelper.eInsectKind.GreenInsect);
        _dummyStageInfoList.Add(1, info);

        // 2. 
        rate[0] = 100;
        info = new DefineHelper.stStageInfo("깨끗해 보이는 응접실", 60, 0, rate, DefineHelper.eInsectKind.RedAnt);
        _dummyStageInfoList.Add(2, info);

        // 3. 
        rate = new int[] { 75, 100 };
        info = new DefineHelper.stStageInfo("복잡한 거실", 90, 0, rate, DefineHelper.eInsectKind.GreenInsect, DefineHelper.eInsectKind.RedAnt);
        _dummyStageInfoList.Add(3, info);

        // 4. 
        rate = new int[] { 55, 100 };
        info = new DefineHelper.stStageInfo("너저분한 책상 위", 100, 0, rate, DefineHelper.eInsectKind.RedAnt, DefineHelper.eInsectKind.GreenInsect);
        _dummyStageInfoList.Add(4, info);

    }
}

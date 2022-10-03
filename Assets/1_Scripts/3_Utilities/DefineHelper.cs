using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineHelper
{
    #region [Manager Info]

    public enum eSceneIndex
    {
        none,
        MainScene,
        IngameScene
    }
    public enum eIngameState
    {
        none                            = 0,
        COUNT,
        PLAY,
        END,
        RESULT,


        state_Count
    }
    public enum eStageSelectColor
    {
        Locked                          = 0,
        Free,
        Selected
    }
    public enum eUIWindowType
    {
        LoadingWindow                   = 0,
        StageInfoSelectWindow,
        ResultWindow,
        //EscapeWindow,
        OptionWindow,
        DialogueWindow,

        max_count
    }
    public enum eUIPropsType
    {
        CountingProps                   = 0,
        ResultProps,
        InsectScoreInfoProps,
        StageSlot,

        max_count
    }
    public enum eBGMClipType
    {
        MainScene,
        IngameScene
    }
    public enum eSFXClipType
    {
        Die,
        Click_tock,
        Click_tting,
        Counting_Nor,
        Counting_Fast
    }
    public enum eSliderType
    {
        BGM,
        SFX
    }
    public enum eMuteType
    {
        Mute,
        Unmute
    }
    public enum eItemType
    {
        TimeStone,
        Bomb,
        None
    }
    #endregion


    #region [UI Kind]
    public enum eMessageBoxKind
    {
        MESSAGE                         = 0,
        COUNTER
    }

    public enum eResultCounting
    {
        none = 0,
        IndividualScore,
        ItemScore,
        TotalScore,
        Complete
    }

    public enum eDialogType
    {
        Notification,
        Warning,
        Exit
    }
    #endregion

    
    #region  [InsectInfo]
    public enum eInsectKind
    {
        GreenInsect                     = 0,
        RedAnt,
        BlackAnt,


        max_cnt
    }

    //벌레 기본 이동 속도 & 회전 속도
    public static float _standardSpeed = 2;
    public static float _standardAngle = 45;

    //벌레별 이동 속도 & 회전 속도 계수
    public static float[] _standardSpeedScalePerInsect = { 1, 2 };
    public static float[] _standardAngleScalePerInsect = { 1, 1.5f };

    // 점수 계산용
    public static int[] _baseScorePerInsect = { 8, 12 };
    #endregion

    public struct stStageInfo
    {
        public string _name;
        public int _limitTime;
        public int _diffLevel;
        public eInsectKind[] _insectKinds;
        public int[] _insectGenRate;
        public stStageInfo(string name, int t, int level, int[] genRate, params eInsectKind[] kinds)
        {
            _name = name;
            _limitTime = t;
            _diffLevel = level;
            _insectGenRate = genRate;
            _insectKinds = kinds;
        }
    }

}

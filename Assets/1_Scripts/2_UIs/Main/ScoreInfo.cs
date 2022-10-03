using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreInfo : MonoBehaviour
{
    [SerializeField] Image _norIcon;
    //[SeiralizeField] Image _dieIcon;
    [SerializeField] Text _txtPerPoint;

    public void InitDataSet(DefineHelper.eInsectKind kind)
    {
        _norIcon.sprite = ResourcePoolManager._instance.GetInsectTypeIcon(kind);
        // _dieIcon.color = GetInsectKindColor(kind);
        _txtPerPoint.text = DefineHelper._baseScorePerInsect[(int)kind].ToString();


        /*
        switch (kind)
        {
            case DefineHelper.eInsectKind.GreenInsect:
                break;
            case DefineHelper.eInsectKind.RedAnt:
                break;
            case DefineHelper.eInsectKind.BlackAnt:
                break;

        }*/
    }

    Color GetInsectKindColor(DefineHelper.eInsectKind kind)
    {
        Color color = Color.white;
        switch (kind)
        {
            case DefineHelper.eInsectKind.RedAnt:
                color = Color.red;
                break;
            case DefineHelper.eInsectKind.BlackAnt:
                color = Color.gray;
                break;
        }
        return color;
    }
}

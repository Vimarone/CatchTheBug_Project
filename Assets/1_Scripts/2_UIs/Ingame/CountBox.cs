using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountBox : MonoBehaviour
{

    [SerializeField] RectTransform _boardBG;

    float _baseSize = 110;
    float _offsetY = 5;

    Dictionary<DefineHelper.eInsectKind, CountPoint> _ptnCounts;

    public void InitSetData(DefineHelper.eInsectKind[] kinds)
    {
        _ptnCounts = new Dictionary<DefineHelper.eInsectKind, CountPoint>();
        _boardBG.sizeDelta = new Vector2(_boardBG.sizeDelta.x, _offsetY + (_baseSize * kinds.Length));

        GameObject props = ResourcePoolManager._instance.GetUIPropsPrefabFromType(DefineHelper.eUIPropsType.CountingProps);
        for (int n = 0; n < kinds.Length; n++)
        {
            GameObject go = Instantiate(props, _boardBG);
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(_offsetY, -_offsetY - (_baseSize * n));

            CountPoint cp = go.GetComponent<CountPoint>();
            cp.InitSetData(ResourcePoolManager._instance.GetInsectTypeIcon(kinds[n]));
            _ptnCounts.Add(kinds[n], cp);
        }
    }

    public void SetCount(DefineHelper.eInsectKind kind, int count)
    {

        _ptnCounts[kind].SetCount(count); //Add로 받아놨기 때문에 딱댐

    }
}

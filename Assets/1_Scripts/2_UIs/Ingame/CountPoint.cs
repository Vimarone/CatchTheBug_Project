using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountPoint : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] Text _txtCount;

    public void InitSetData(Sprite s)
    {
        _icon.sprite = s;
        _txtCount.text = "0";
    }
    public void SetCount(int cnt)
    {
        _txtCount.text = cnt.ToString();
    }
    
}

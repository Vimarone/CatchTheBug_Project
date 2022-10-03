using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    Image _itemIcon;
    Text _itemName;
    Text _itemCount;
    int _itemNeverUsed = 0;

    void Awake()
    {
        _itemIcon = transform.GetChild(0).GetComponent<Image>();
        _itemName = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        _itemCount = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
    }
    public void InitSetData(DefineHelper.eItemType type)
    {
        _itemIcon.sprite = ResourcePoolManager._instance.GetItemSpriteFromType(type);
        _itemCount.text = _itemNeverUsed.ToString();
        switch (type)
        {
            case DefineHelper.eItemType.TimeStone:
                _itemName.text = "타임스톤";
                break;
            case DefineHelper.eItemType.Bomb:
                _itemName.text = "살충폭탄";
                break;
            case DefineHelper.eItemType.None:
                _itemName.text = "선택하지 않음";
                break;
        }
    }

    public void SetCount(int count)
    {
        _itemCount.text = count.ToString();
    }
}

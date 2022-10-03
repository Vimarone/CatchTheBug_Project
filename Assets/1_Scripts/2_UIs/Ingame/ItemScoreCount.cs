using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScoreCount : MonoBehaviour
{
    Image _itemIcon;
    Text _itemCount;
    Text _itemPoint;


    int _targetScore = 0;
    float _drawScore = 0;
    float _countingTime = 1.5f;
    bool _isCount = false;

    public int _calcScore
    {
        get { return _targetScore; }
    }


    public bool _endCounting
    {
        get { return !_isCount; }
    }

    void Awake()
    {
        _itemIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
        _itemCount = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        _itemPoint = gameObject.transform.GetChild(0).GetChild(1).GetComponent<Text>();
    }

    void LateUpdate()
    {
        if (_isCount)
        {
            if (_targetScore == 0)
            {
                _itemPoint.text = _targetScore.ToString();
                _isCount = false;
            }
            else
            {
                if (_targetScore <= _drawScore)
                {
                    _itemPoint.text = "-" + _targetScore.ToString();
                    _isCount = false;
                }
                else
                {
                    _drawScore += _targetScore * Time.deltaTime / _countingTime;
                    _itemPoint.text = "-" + ((int)_drawScore).ToString();
                }
            }
        }
    }

    public void InitDataSet(DefineHelper.eItemType type, int usedCount)
    {
        _itemIcon.sprite = ResourcePoolManager._instance.GetItemSpriteFromType(type);
        _itemCount.text = usedCount.ToString();
        _targetScore = (usedCount * 50);
        _itemPoint.text = "0";
        _isCount = true;
    }
}

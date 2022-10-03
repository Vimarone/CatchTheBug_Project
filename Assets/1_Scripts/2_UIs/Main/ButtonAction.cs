using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    Color _originColor;
    float _movementValue = 20;

    public void ButtonDownChangeBright(Graphic target)
    {
        _originColor = target.color;
        target.color = Color.gray;
    }

    public void ButtonUpChangeOrigin(Graphic target)
    {
        target.color = _originColor;
    }

    public void ButtonDownMoveDown(RectTransform target)
    {
        target.anchoredPosition += Vector2.down * _movementValue;

    }
    public void ButtonUpMoveUp(RectTransform target)
    {
        target.anchoredPosition += Vector2.up * _movementValue;
        
    }

    public void ButtonDownScaleShrink(RectTransform target)
    {
        target.localScale *= 0.9f;
    }
    public void ButtonUpScaleRestore(RectTransform target)
    {
        target.localScale = Vector3.one;
    }

    public void ItemSelectButton(int type)
    {
        SceneControlManager._instance.SetItem((DefineHelper.eItemType)type);
    }
}

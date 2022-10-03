using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    GameObject _bgObj;
    Text _txtMessage;
    Text _txtCounter;

    int _counter = 0;

    //float _tempSec = 0;

    void Awake()
    {
        _bgObj = transform.GetChild(0).gameObject;
        _txtMessage = _bgObj.GetComponentInChildren<Text>();
        _txtCounter = transform.GetChild(1).GetComponent<Text>();
    }

    public void OpenMessageBox(string msg, DefineHelper.eMessageBoxKind type = DefineHelper.eMessageBoxKind.MESSAGE, int count = 3)
    {
        gameObject.SetActive(true);
        switch (type)
        {
            case DefineHelper.eMessageBoxKind.MESSAGE:
                _bgObj.SetActive(true);
                _txtCounter.gameObject.SetActive(false);
                break;
            case DefineHelper.eMessageBoxKind.COUNTER:
                _bgObj.SetActive(false);
                _txtCounter.gameObject.SetActive(true);
                _counter = count;
                break;
        }
        _txtMessage.text = msg;
    }

    public void CloseMessageBox()
    {
        gameObject.SetActive(false);
    }


    public void SetCounting(int stime)
    {
        int number = _counter - stime;

        if (number <= 0)
        {
            _bgObj.SetActive(true);
            _txtCounter.gameObject.SetActive(false);
        }
        else
        {
            _txtCounter.text = number.ToString();
        }
    }
}

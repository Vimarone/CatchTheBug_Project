using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBox : MonoBehaviour

{ 
    [SerializeField] Text _txtSec;
    [SerializeField] Text _txtMiliSec;

    public void InitSetData(float timerTime)
    {
        SettingTimer(timerTime);
    }

    public void SettingTimer(float remainedTime)
    {
        int sec = (int)remainedTime;
        int msec = (int)((remainedTime - sec) * 100);
        _txtSec.text = sec.ToString();
        if (msec < 10)
            _txtMiliSec.text = "0" + msec.ToString();
        else
            _txtMiliSec.text = msec.ToString();

    }
}

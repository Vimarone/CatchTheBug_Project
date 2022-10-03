using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFuncs : MonoBehaviour
{
    public void CloseDoorEnd()
    {
        LoadingWindow wnd = transform.parent.GetComponent<LoadingWindow>();
        //if (owner != null)
        //{
        //    LoadingWindow wnd = owner.GetComponent<LoadingWindow>();
        //    if (wnd != null)
        //        wnd.EnableContents();
        //}
        if (wnd != null)
            wnd.EnableContents();
        else
            Debug.LogError("LoadingWindow를 찾을 수 없음");
    }

    public void OpenDoorEnd()
    {
        LoadingWindow wnd = transform.parent.GetComponent<LoadingWindow>();
        if (wnd != null)
            wnd.Close();
        else
            Debug.LogError("LoadingWindow를 찾을 수 없음");
    }
}

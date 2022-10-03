using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingMachineActive : MonoBehaviour
{
    Animator _aniCtrl;

    void Awake()
    {
        _aniCtrl = GetComponent<Animator>();
        
    }
    public void EndDance()      // 춤이 끝난 시점을 감지
    {
        int index = Random.Range(0, 3);
        _aniCtrl.SetInteger("SelectDance", index);
    }
}

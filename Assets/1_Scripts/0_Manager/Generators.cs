using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generators : MonoBehaviour
{
    [SerializeField] GameObject[] _insects;
    [SerializeField] float _spawnDelayStartTime = 0.5f;
    [SerializeField] float _spawnTime = 5;
    [SerializeField] int _perCount = 2;

    
    
    

    // 참조 변수
    List<InsectControl> _genInsects;
    DefineHelper.eInsectKind[] _insectKinds;
    int[] _genPerRate;
    Transform _rootPool;


    // 정보 변수
    float _checkListTime = 1;

    void Awake()
    {
        _genInsects = new List<InsectControl>();        
    }
    // Start is called before the first frame update
    void Start()
    {
        //=====
        //GenerateInsects(2);
        //InvokeRepeating("GenerateInsects", _spawnDelayStartTime, _spawnTime); //함수명, 시작 시간, 반복 간격
        //InvokeRepeating("CheckInsectList", 4, _checkListTime);
        GameObject go = GameObject.FindGameObjectWithTag("InsectsPool");
        if(go != null)
        {
            _rootPool = go.transform;
        }
        else
        {
            Debug.Log("InsectsPool을 찾지 못했습니다");
        }
        
    }

    void GenerateInsects()
    {
        float ScreenHalfH = Camera.main.orthographicSize;
        float ScreenHalfW = ScreenHalfH * Camera.main.aspect;

        Vector3 Pos = new Vector3();
        float Angle = 0.0f;
        int cnt = _perCount * 4;

        for (int n = 0; n < cnt; n++)
        {
            switch (n % 4)
            {
                case 0:     // 좌측
                    Pos.x = -ScreenHalfW;
                    Pos.y = Random.Range(-ScreenHalfH, ScreenHalfH);
                    Angle = -90.0f;
                    break;
                case 1:     // 우측
                    Pos.x = ScreenHalfW;
                    Pos.y = Random.Range(-ScreenHalfH, ScreenHalfH);
                    Angle = 90.0f;
                    break;
                case 2:     // 상단
                    Pos.x = Random.Range(-ScreenHalfW, ScreenHalfW);
                    Pos.y = ScreenHalfH;
                    Angle = 180.0f;
                    break;
                case 3:     // 하단
                    Pos.x = Random.Range(-ScreenHalfW, ScreenHalfW);
                    Pos.y = -ScreenHalfW;
                    Angle = 0f;
                    break;

            }
            
            int rd = Random.Range(0, _genPerRate[_genPerRate.Length - 1]);

            for (int i = 0; i < _genPerRate.Length; i++)
            {
                if (rd < _genPerRate[i])
                {
                    GameObject go = Instantiate(_insects[(int)_insectKinds[i]], _rootPool);

                    go.transform.position = Pos;
                    go.transform.Rotate(0, 0, Angle);

                    InsectControl insect = go.GetComponent<InsectControl>();
                    insect.InitializeData(_insectKinds[i]);

                    _genInsects.Add(insect);
                    break;
                }
            }
        }
    }

    void CheckInsectList()
    {
        _genInsects.Remove(null);
    }

    public void StartInsectGenerate(DefineHelper.eInsectKind[] kinds, int[] rate)
    {
        _insectKinds = kinds;
        _genPerRate = rate;
        InvokeRepeating("GenerateInsects", _spawnDelayStartTime, _spawnTime);
        InvokeRepeating("CheckInsectList", 4, _checkListTime);
    }
    public void EndInsectGenerate()
    {
        for (int n = 0; n < _genInsects.Count; n++)
        {
            if (_genInsects[n] != null)
                Destroy(_genInsects[n].gameObject);
        }
        _genInsects.Clear();
        CancelInvoke("GenerateInsects");
        CancelInvoke("CheckInsectList");
    }


}



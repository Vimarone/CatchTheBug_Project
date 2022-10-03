using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserInfoManager : MonoBehaviour
{
    static UserInfoManager _uniqueInstance;

    string _fileName = "userInfo.txt";
    string[] _infos;
    float _bgm, _eff;
    bool _bgmMute = false, _effMute = false;

    public int _nowStageNumber
    {
        get; set;
    }

    public static UserInfoManager _instance
    {
        get { return _uniqueInstance; }
    }

    public string _userInfo
    {
        get { return _fileName; }
    }
    public int _clearStage
    {
        get; set;
    }

    private void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(this.gameObject);

        // 임시======
        //_clearStage = 0;
        // ==========
        if (File.Exists(_fileName))
        {
            FileStream fs = new FileStream(_fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (sr.EndOfStream == false)
            {
                _infos = sr.ReadLine().Split(' ');
            }

            for(int i = 0; i < _infos.Length; i++)
            {
                switch (i % _infos.Length)
                {
                    case 0:
                        _clearStage = int.Parse(_infos[i]);
                        break;
                    case 1:
                        _bgm = float.Parse(_infos[i]);
                        break;
                    case 2:
                        _eff = float.Parse(_infos[i]);
                        break;
                }
            }
            Debug.Log(_clearStage);
            sr.Close();
            fs.Close();

            if (_bgm == 0)
                _bgmMute = true;
            if (_eff == 0)
                _effMute = true;

            SoundManager._instance.InitializeSet(_bgm, _bgmMute, _eff, _effMute);
        }
        else
        {
            FileStream fs = new FileStream(_fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            string temp = " ";
            sw.Write("0" + temp + "1" + temp + "1");
            sw.Close();
            fs.Close();
            _clearStage = 0;
            SoundManager._instance.InitializeSet(0.8f, false, 0.8f, false);
        }
    }
}

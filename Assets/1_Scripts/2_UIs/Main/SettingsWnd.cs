using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsWnd : MonoBehaviour
{
    AudioSource _bgmAudioSource;
    AudioSource _effAudioSource;
    [SerializeField] Slider _bgVolume;
    [SerializeField] Slider _effVolume;

    void Awake()
    {
        _bgmAudioSource = SoundManager._instance.gameObject.GetComponent<AudioSource>();
        _effAudioSource = SoundManager._instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>();

        _bgVolume.value = SoundManager._instance.GetVolumes()[(int)DefineHelper.eSliderType.BGM];
        _effVolume.value = SoundManager._instance.GetVolumes()[(int)DefineHelper.eSliderType.SFX];
    }

    public void ControlBGMSound(Slider s)
    {
        _bgmAudioSource.volume = s.value;
        SaveBGMVolume(s.value);
        if (s.value == 0)
            s.GetComponentInParent<Image>().sprite = ResourcePoolManager._instance.GetMuteSpriteFromType(DefineHelper.eMuteType.Mute);
        else
            s.GetComponentInParent<Image>().sprite = ResourcePoolManager._instance.GetMuteSpriteFromType(DefineHelper.eMuteType.Unmute);
    }

    public void ControlEFFSound(Slider s)
    {
        _effAudioSource.volume = s.value;
        SaveEFFVolume(s.value);
        if(s.value == 0)
            s.GetComponentInParent<Image>().sprite = ResourcePoolManager._instance.GetMuteSpriteFromType(DefineHelper.eMuteType.Mute);
        else
            s.GetComponentInParent<Image>().sprite = ResourcePoolManager._instance.GetMuteSpriteFromType(DefineHelper.eMuteType.Unmute);
    }

    public void MuteBGMSound(Slider s)
    {
        _bgmAudioSource.volume = 0;
        s.value = _bgmAudioSource.volume;
    }

    public void MuteEFFSound(Slider s)
    {
        _effAudioSource.volume = 0;
        s.value = _effAudioSource.volume;
    }

    public void OpenSettingsWdn()
    {
        gameObject.SetActive(true);
    }
    public void CloseSettingsWdn()
    {
        gameObject.SetActive(false);
    }

    public void SaveBGMVolume(float bgm)
    {
        FileStream fs = new FileStream(UserInfoManager._instance._userInfo, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        int clearStage = UserInfoManager._instance._nowStageNumber;
        string temp = " ";
        sw.Write(clearStage + temp + bgm.ToString() + temp + SoundManager._instance.GetVolumes()[1]);
        sw.Close();
        fs.Close();
    }
    public void SaveEFFVolume(float eff)
    {
        FileStream fs = new FileStream(UserInfoManager._instance._userInfo, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        int clearStage = UserInfoManager._instance._nowStageNumber;
        string temp = " ";
        sw.Write(clearStage + temp + SoundManager._instance.GetVolumes()[0] + temp + eff.ToString());
        sw.Close();
        fs.Close();
    }
}

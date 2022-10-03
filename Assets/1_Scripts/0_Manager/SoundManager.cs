using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 배경음 & 효과음
    // 2D는 사운드 위치가 고정적 & 평면적 : 오디오 소스를 카메라에 붙여놓는게 가장 좋음
    // 배경음 : 싱글
    // 효과음 : 멀티

    static SoundManager _uniqueInstance;

    AudioSource _bgmPlayer;
    // 2번
    AudioSource _sfxPlayer;

    float _bgmVolume;
    float _sfxVolume;
    bool _bgmMute;
    bool _sfxMute;

    // 1번
    List<AudioSource> _sfxPlayers = new List<AudioSource>();

    public static SoundManager _instance
    {
        get { return _uniqueInstance; }
    }

    

    void Awake() 
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);

        _bgmPlayer = GetComponent<AudioSource>();

        // 2번
        _sfxPlayer = transform.GetChild(0).GetComponent<AudioSource>();
        // 임시
        InitializeSet();

        // =====
    }

    void LateUpdate()
    {
        // 1번
        for (int n = 0; n < _sfxPlayers.Count; n++)
        {
            if (!_sfxPlayers[n].isPlaying)
            {
                Destroy(_sfxPlayers[n].gameObject);
                _sfxPlayers.RemoveAt(n);
                break;
            }
        }
    }

    public void InitializeSet(float bv = 1, bool bm = false, float fv = 1, bool fm = false)
    {
        _bgmVolume = bv;
        _bgmMute = bm;
        _sfxVolume = fv;
        _sfxMute = fm;
        //_bgmPlayer.mute = bm;
        // _bgmPlayer.volume = bv;
    }
    

    public void PlayBGMSound(DefineHelper.eBGMClipType type, bool isLoop = true)
    {
        _bgmPlayer.clip = ResourcePoolManager._instance.GetBgmClipFromType(type);
        _bgmPlayer.volume = _bgmVolume;
        _bgmPlayer.mute = _bgmMute;
        _bgmPlayer.loop = isLoop;
        _bgmPlayer.Play();
    }

    // 1번
    public AudioSource PlaySFXSound(DefineHelper.eSFXClipType type, bool isLoop = false)
    {
        GameObject go = new GameObject("SfxPlayer");
        go.transform.parent = transform;
        AudioSource sfxPlayer = go.AddComponent<AudioSource>();
        sfxPlayer.clip = ResourcePoolManager._instance.GetSFXClipFromType(type);
        sfxPlayer.volume = _sfxVolume;
        sfxPlayer.mute = _sfxMute;
        sfxPlayer.loop = isLoop;
        sfxPlayer.Play();

        _sfxPlayers.Add(sfxPlayer);

        return sfxPlayer;
    }

    public void PlaySFXSoundOneShot(DefineHelper.eSFXClipType type, bool isLoop = false)
    {
        _sfxPlayer.volume = _sfxVolume;
        _sfxPlayer.mute = _sfxMute;
        _sfxPlayer.loop = isLoop;
        _sfxPlayer.PlayOneShot(ResourcePoolManager._instance.GetSFXClipFromType(type));
    }

    public float[] GetVolumes()
    {
        float[] volumes = new float[2];
        volumes[0] = _bgmVolume;
        volumes[1] = _sfxVolume;
        return volumes;
    }

}

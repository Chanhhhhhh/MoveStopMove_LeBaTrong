using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    BtnClick = 0,
    WeaponThrow = 1,
    WeaponHit = 2,
    SizeUp = 3,
    Dead  = 4,
    Lose =5,
    Win = 6,
    ReviveCount = 7,
}
[System.Serializable]
public class SoundAudioClip
{
    public SoundType soundType;
    public AudioClip audioClip;
}
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private SoundAudioClip[] soundAudioClips;
    private bool IsMute;

    public void OnInit()
    {
        AudioListener.volume = SaveManager.Instance.OnSound ? 1 : 0;
    }
    public AudioClip GetAudioClip(SoundType soundType)
    {
        for(int i = 0; i < soundAudioClips.Length; i++)
        {
            if (soundAudioClips[i].soundType == soundType)
            {
                return soundAudioClips[i].audioClip;
            }
        }
        return null;
    }
    public void PlaySoundClip(SoundType soundType)
    {
        m_AudioSource.PlayOneShot(GetAudioClip(soundType));
    }

    public void PlaySoundClip(SoundType soundType, Vector3 pos)
    {
        float distance = Vector3.Distance(pos, LevelManager.Instance.player.TF.position);
        m_AudioSource.maxDistance = 50f;
        m_AudioSource.minDistance = 1f;
        float volume = Mathf.Clamp01((m_AudioSource.maxDistance - distance) / (m_AudioSource.maxDistance - m_AudioSource.minDistance));
        m_AudioSource.PlayOneShot(GetAudioClip((soundType)), volume);
    }
    public void MuteHandle()
    {      
        IsMute = !IsMute;
        SaveManager.Instance.OnSound = !IsMute;
        AudioListener.volume = IsMute ? 0 : 1;
    }
}

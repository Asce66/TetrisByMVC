using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingleIton<AudioManager>
{
    AudioSource As;
    bool isMute = false;
    [SerializeField] AudioClip btnClip, clearClip;

    private void Start()
    {
       As= gameObject.AddComponent<AudioSource>();
    }

    public void BtnClickAs()
    {
        if (isMute)
            return;
        As.PlayOneShot(btnClip);
    }
    
    public void ClearAs()
    {
        if (isMute)
            return;
        As.PlayOneShot(clearClip);
    }

    public bool MuteSwitch()
    {
        isMute = !isMute;
        return isMute;
    }
}

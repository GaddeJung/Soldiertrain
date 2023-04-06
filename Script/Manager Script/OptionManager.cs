using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject optioPanel;

    // 사운드 옵션
    public AudioClip clip;

    // 볼륨 조절

    public void OptionButton()
    {
        SFXSound.instance.SFXPlay("Button", clip);
        optioPanel.SetActive(true);
    }

    public void OptionExitButton()
    {
        SFXSound.instance.SFXPlay("Button", clip);
        optioPanel.SetActive(false);
    }
}
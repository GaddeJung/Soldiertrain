using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject optioPanel;

    // ���� �ɼ�
    public AudioClip clip;

    // ���� ����

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
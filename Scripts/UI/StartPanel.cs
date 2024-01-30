using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using static UnityEngine.Rendering.HighDefinition.ScalableSettingLevelParameter;

public class StartPanel : MonoBehaviour
{
    public AudioSource audioSource;

    public Button StartButton;
    public Button ExitButton;

    int sceneNum = 2;

    private void Start()
    {
        StartButton.onClick.AddListener(() =>
        {
            GameStart(sceneNum);
        });
        ExitButton.onClick.AddListener(ExitGame);

        audioSource = GetComponent<AudioSource>();
    }
        

    //�������ʼ��Ϸ����ť
    public void GameStart(int level)
    {
        audioSource.Play();
        GameObject.Destroy(this.gameObject);
        SceneManager.LoadScene("Scene1");
        //UIControl.GetInstance().SceneFindCanvas();
        UIControl.GetInstance().FindCanvas("PlayUI");
    }

    //������˳�����ť
    public void ExitGame()
    {
        audioSource.Play();
        AudioManager.Instance().SoundPlay("1");
        Application.Quit();
        Debug.Log("�˳���Ϸ");
    }
}

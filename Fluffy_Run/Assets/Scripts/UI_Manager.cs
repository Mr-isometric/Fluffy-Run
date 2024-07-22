using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu_Panel;
    [SerializeField] private GameObject Score_Panel;
    [SerializeField] private GameObject InGame_Panel;
    [SerializeField] private GameObject Ground_Holder;
    [SerializeField] private Button Start_Btn;
    [SerializeField] private Button Restart_Btn;
    [SerializeField] private Button MainMenu_Btn;
    [SerializeField] private Button Exit_Btn;
    [SerializeField] private TextMeshProUGUI Score_txt;
    [SerializeField] private DataHolder_SO _dataHolder;
    private float ScoreValue;
    public static UI_Manager instance;
    private void OnEnable()
    {
        Start_Btn.onClick.AddListener(GetStart_btn_OnClick);
        Restart_Btn.onClick.AddListener(Restart_Btn_OnClick);
        MainMenu_Btn.onClick.AddListener(MainMenu_Btn_OnClick);
        Exit_Btn.onClick.AddListener(Exit_Btn_OnClick);
    }
    private void OnDisable()
    {
        Start_Btn.onClick.RemoveListener(GetStart_btn_OnClick);
        Restart_Btn.onClick.RemoveListener(Restart_Btn_OnClick);
        MainMenu_Btn.onClick.RemoveListener(MainMenu_Btn_OnClick);
        Exit_Btn.onClick.RemoveListener(Exit_Btn_OnClick);
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        Time.timeScale = 0f;
    }
    private void MainMenu_Btn_OnClick()
    {
        MainMenu_Panel.SetActive(true);
        InGame_Panel.SetActive(false);
        SceneManager.LoadSceneAsync("Swapnil_Test");
    }

    private void Restart_Btn_OnClick()
    {
        SceneManager.LoadSceneAsync("Swapnil_Test");
    }

    public void PlayerGotHIT()
    {
        InGame_Panel.SetActive(true);
    }
   
    private void Exit_Btn_OnClick()
    {
        Application.Quit();
    }
    private void GetStart_btn_OnClick()
    {
        Time.timeScale = 1f;
        ScoreValue = 0;
        _dataHolder.isPlayerAlive = true;
        MainMenu_Panel.SetActive(false);

    }
    private void Update()
    {
        if (_dataHolder.isPlayerAlive)
        {
            ScoreValue += Time.deltaTime*10f;
            Score_txt.text = ScoreValue.ToString("0");
        }
    }

}

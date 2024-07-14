using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score_txt;
    [SerializeField] private Button Start_Btn;

    private float ScoreValue;

    private void OnEnable()
    {
        Start_Btn.onClick.AddListener(GetStart_btn_OnClick);
    }
    private void OnDisable()
    {
        Start_Btn.onClick.RemoveListener(GetStart_btn_OnClick);
    }
    private void Start()
    {
        Time.timeScale = 0f;
        Start_Btn.gameObject.SetActive(true);
        ScoreValue = 0;
    }
    private void GetStart_btn_OnClick()
    {
        Time.timeScale = 1f;
        Start_Btn.gameObject.SetActive(false);
    }
    private void Update()
    {
        ScoreValue += Time.deltaTime*10f;
        Score_txt.text = ScoreValue.ToString("0");
    }

}

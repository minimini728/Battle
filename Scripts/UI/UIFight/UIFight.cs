using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFight : MonoBehaviour
{
    public UIChange uiChange; //캐릭터 변경 UI
    public GameObject uiWin; //승리 UI
    public GameObject uiLose; //패배 UI

    public Button btnChange; //캐릭터 변경 버튼
    public Button btnPunch; //기본 공격 버튼
    public GameObject btnSkilGo; //스킬 공격 버튼

    private bool isActive; //버튼 활성화
    void Start()
    {
        this.btnChange.onClick.AddListener(() =>
        {
            this.uiChange.RefreshUI(1);

            this.uiChange.gameObject.SetActive(true);
            //player, enemy sprite renderer 변경 이벤트 전송 -> Player 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchPlayerLayer);
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchEnemyLayer);

        });

        this.btnPunch.onClick.AddListener(() =>
        {
            this.ToggleBtn(1);

            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.Punch);

        });

        this.btnSkilGo.GetComponent<Button>().onClick.AddListener(() =>
        {
            this.ToggleBtn(1);

            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.Skill);

        });

        //ShowUIChange 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ShowUIChange, this.ClickbtnChange);
        //ShowUIWin 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ShowUIWin, this.ShowUIWin);
        //ShowUILose 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ShowUILose, this.ShowUILose);
        //ActiveButton 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ActiveButton, this.ToggleBtn);

        this.isActive = true;
    }
    public void Init()
    {
        this.btnSkilGo.GetComponent<UISkillButton>().Init();
    }

    public void ClickbtnChange(short type)
    {            
        //UIChange ui refresh 미리 해놓기 -> UIChange 클래스로
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshUIChange);

        this.btnChange.onClick.Invoke();
    }
    private void ShowUIWin(short type)
    {
        StartCoroutine(this.ShowUIWin());
    }
    private void ShowUILose(short type)
    {
        StartCoroutine(this.ShowUILose());
    }
    IEnumerator ShowUIWin()
    {
        yield return new WaitForSeconds(1f);

        //player, enemy sprite renderer 변경 이벤트 전송 -> Player 클래스로
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchPlayerLayer);
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchEnemyLayer);

        this.uiWin.gameObject.SetActive(true);
    }
    IEnumerator ShowUILose()
    {
        yield return new WaitForSeconds(1f);

        //player, enemy sprite renderer 변경 이벤트 전송 -> Player 클래스로
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchPlayerLayer);
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchEnemyLayer);

        this.uiLose.gameObject.SetActive(true);
    }
    private void ToggleBtn(short type)
    {
        this.isActive = !isActive;

        if (this.isActive)
        {
            this.btnChange.interactable = true;
            this.btnPunch.interactable = true;
            this.btnSkilGo.GetComponent<Button>().interactable = true;
        }
        else
        {
            this.btnChange.interactable = false; ;
            this.btnPunch.interactable = false;
            this.btnSkilGo.GetComponent<Button>().interactable = false;
        }
    }
}

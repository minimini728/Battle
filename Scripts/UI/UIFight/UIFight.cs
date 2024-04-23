using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFight : MonoBehaviour
{
    public UIChange uiChange; //ĳ���� ���� UI
    public GameObject uiWin; //�¸� UI
    public GameObject uiLose; //�й� UI

    public Button btnChange; //ĳ���� ���� ��ư
    public Button btnPunch; //�⺻ ���� ��ư
    public GameObject btnSkilGo; //��ų ���� ��ư

    private bool isActive; //��ư Ȱ��ȭ
    void Start()
    {
        this.btnChange.onClick.AddListener(() =>
        {
            this.uiChange.RefreshUI(1);

            this.uiChange.gameObject.SetActive(true);
            //player, enemy sprite renderer ���� �̺�Ʈ ���� -> Player Ŭ������
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

        //ShowUIChange �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ShowUIChange, this.ClickbtnChange);
        //ShowUIWin �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ShowUIWin, this.ShowUIWin);
        //ShowUILose �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ShowUILose, this.ShowUILose);
        //ActiveButton �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ActiveButton, this.ToggleBtn);

        this.isActive = true;
    }
    public void Init()
    {
        this.btnSkilGo.GetComponent<UISkillButton>().Init();
    }

    public void ClickbtnChange(short type)
    {            
        //UIChange ui refresh �̸� �س��� -> UIChange Ŭ������
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

        //player, enemy sprite renderer ���� �̺�Ʈ ���� -> Player Ŭ������
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchPlayerLayer);
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchEnemyLayer);

        this.uiWin.gameObject.SetActive(true);
    }
    IEnumerator ShowUILose()
    {
        yield return new WaitForSeconds(1f);

        //player, enemy sprite renderer ���� �̺�Ʈ ���� -> Player Ŭ������
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

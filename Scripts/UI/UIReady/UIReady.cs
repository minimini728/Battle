using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReady : MonoBehaviour
{
    public UIPick uiPick;
    public Button btnStart; //���� ��ư

    private int count; //���� Ƚ�� -> start��ư Ȱ��ȭ
    void Start()
    {
        this.btnStart.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);

            //UIFightȰ��ȭ �̺�Ʈ -> UIFightDirector Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.FightStart);

            foreach (var item in InfoManager.instance.dicOrderCharacterInfo)
            {
                Debug.Log("(" + item.Key + ", " + item.Value.name + ")");
            }
        });

        //Start��ư Ȱ��ȭ �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ActiveStart, this.ActiveBtnStart);
    }

    void ActiveBtnStart(short type) //Start��ư Ȱ��ȭ �̺�Ʈ �޼���
    {
        this.count++;

        if(this.count >= 3)
        this.btnStart.interactable = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFightDirector : MonoBehaviour
{
    public UIReady uiReady; //�غ�â
    public UIFight uiFight; //����â
    void Start()
    {   
        //����â Ȱ��ȭ �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.FightStart, this.ShowUIFight);
    }

    void ShowUIFight(short type)
    {
        this.uiFight.gameObject.SetActive(true);
        this.uiFight.Init();
        //player �ʱ�ȭ �̺�Ʈ ���� -> FightMain Ŭ������
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.PlayerInit);
    }
}

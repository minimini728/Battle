using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFightDirector : MonoBehaviour
{
    public UIReady uiReady; //준비창
    public UIFight uiFight; //전투창
    void Start()
    {   
        //전투창 활성화 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.FightStart, this.ShowUIFight);
    }

    void ShowUIFight(short type)
    {
        this.uiFight.gameObject.SetActive(true);
        this.uiFight.Init();
        //player 초기화 이벤트 전송 -> FightMain 클래스로
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.PlayerInit);
    }
}

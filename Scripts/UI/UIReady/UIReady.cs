using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReady : MonoBehaviour
{
    public UIPick uiPick;
    public Button btnStart; //시작 버튼

    private int count; //선택 횟수 -> start버튼 활성화
    void Start()
    {
        this.btnStart.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);

            //UIFight활성화 이벤트 -> UIFightDirector 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.FightStart);

            foreach (var item in InfoManager.instance.dicOrderCharacterInfo)
            {
                Debug.Log("(" + item.Key + ", " + item.Value.name + ")");
            }
        });

        //Start버튼 활성화 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ActiveStart, this.ActiveBtnStart);
    }

    void ActiveBtnStart(short type) //Start버튼 활성화 이벤트 메서드
    {
        this.count++;

        if(this.count >= 3)
        this.btnStart.interactable = true;
    }
}

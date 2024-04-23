using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChange : MonoBehaviour
{
    public GameObject uiChangeCellGo; //선택 cell
    public Transform contentTrans; //cell 놓을 위치

    public Button btnClose; //닫기버튼
    void Start()
    {
        this.btnClose.onClick.AddListener(() =>
        {   
            this.gameObject.SetActive(false);

            //player, enemy sprite renderer 변경 이벤트 전송 -> Player 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchPlayerLayer);
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchEnemyLayer);

        });

        //UI refresh 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.RefreshUIChange, this.RefreshUI);

        //UI Change 닫기 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.CloseUIChange, this.CloseUIChange);

        this.ViewPortInit();
    }

    public void ViewPortInit()
    {
        this.RefreshUI(1);
    }

    public void RefreshUI(short type) //UI refresh
    {
        if(this.contentTrans != null)
        {
            //기존 제거
            foreach (Transform child in this.contentTrans)
            {
                Destroy(child.gameObject);
            }

            //다시 붙이기
            for (int i = 0; i < 3; i++)
            {
                var go = Instantiate(this.uiChangeCellGo, this.contentTrans);
                UIChangeCell changeCell = go.GetComponent<UIChangeCell>();
                changeCell.Init(i);
            }
            
        }
    }

    public void CloseUIChange(short type) //닫기
    {
        this.gameObject.SetActive(false);
    }

}

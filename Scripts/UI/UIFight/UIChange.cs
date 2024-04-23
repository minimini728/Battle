using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChange : MonoBehaviour
{
    public GameObject uiChangeCellGo; //���� cell
    public Transform contentTrans; //cell ���� ��ġ

    public Button btnClose; //�ݱ��ư
    void Start()
    {
        this.btnClose.onClick.AddListener(() =>
        {   
            this.gameObject.SetActive(false);

            //player, enemy sprite renderer ���� �̺�Ʈ ���� -> Player Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchPlayerLayer);
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchEnemyLayer);

        });

        //UI refresh �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.RefreshUIChange, this.RefreshUI);

        //UI Change �ݱ� �̺�Ʈ ���
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
            //���� ����
            foreach (Transform child in this.contentTrans)
            {
                Destroy(child.gameObject);
            }

            //�ٽ� ���̱�
            for (int i = 0; i < 3; i++)
            {
                var go = Instantiate(this.uiChangeCellGo, this.contentTrans);
                UIChangeCell changeCell = go.GetComponent<UIChangeCell>();
                changeCell.Init(i);
            }
            
        }
    }

    public void CloseUIChange(short type) //�ݱ�
    {
        this.gameObject.SetActive(false);
    }

}

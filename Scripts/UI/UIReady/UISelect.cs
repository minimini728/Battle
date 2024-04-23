using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelect : MonoBehaviour
{
    public UISelectCell uiSelectCell; //ĳ���� ī��

    public Button btnClose; //�ݱ� ��ư
    public Button btnSelect; //���� ��ư
    public Transform contentTrans; //��ũ�Ѻ� ��ġ

    private UISelectCell selectedCell; //���� ���õ� �� ����

    private UIPickCell uiPickCell; //���ȴ� pickcell ����
    void Start()
    {   
        //�ݱ� ��ư
        this.btnClose.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });

        //���� ��ư
        this.btnSelect.onClick.AddListener(() =>
        {
            if(selectedCell != null)
            {
                this.uiPickCell.ShowResult(selectedCell.characterData); //��� ������ �޼���
                this.gameObject.SetActive(false);

                var go = InfoManager.instance.RemainCharacterInfos.Find(x => x.id == selectedCell.characterData.id);
                InfoManager.instance.RemainCharacterInfos.Remove(go);

                //Start��ư Ȱ��ȭ -> UIReady Ŭ������
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ActiveStart);
            }

        });

    }
    public void Init(UIPickCell pickCell)
    {
        this.uiPickCell = pickCell;

        ScrollViewInit();
    }

    //��ũ�Ѻ� ����
    void ScrollViewInit()
    {
        for (int i = 0; i < InfoManager.instance.RemainCharacterInfos.Count; i++)
        {
            var go = Instantiate(this.uiSelectCell, this.contentTrans);
            UISelectCell selectCell = go.GetComponent<UISelectCell>();

            //���õ� ���� ���� �̺�Ʈ ó��
            selectCell.btnSelect.onClick.AddListener(() =>
            {
                SelectCell(selectCell);
            });

            selectCell.Init(InfoManager.instance.RemainCharacterInfos[i]);
        }

    }

    //���õ� �� ó��
    void SelectCell(UISelectCell cell)
    {
        if (selectedCell != null)
        {
            selectedCell.imgSelect.gameObject.SetActive(false); 
        }

        selectedCell = cell;
        selectedCell.imgSelect.gameObject.SetActive(true);
    }


    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
}

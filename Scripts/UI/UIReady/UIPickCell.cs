using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPickCell : MonoBehaviour
{
    private UIResult uiResult; //��� UI
    public GameObject uiResultGo;

    public GameObject uiSelectGo; //ĳ���� ���� UI

    public Button btnPlus; //�÷��� ��ư

    public int orderPickCell; //UIPickCell �ν��Ͻ� ���� ����
    void Start()
    {
        this.btnPlus.onClick.AddListener(() =>
        {   
            //����â
            var go = Instantiate(uiSelectGo, this.transform.parent.parent);
            go.GetComponent<UISelect>().Init(this);
            go.transform.position += new Vector3(0, 0, -3); //���̾� ���������� �̵�

        });

    }

    public void Init(int num)
    {
        this.orderPickCell = num;

        //���â
        var go = Instantiate(this.uiResultGo, transform);
        this.uiResult = go.GetComponent<UIResult>();
        this.uiResult.gameObject.SetActive(false);

    }

    ////��ü������ ������ UIResult�� ��ȯ�ϴ� �޼���
    //public UIResult GetUIResult()
    //{
    //    return uiResult;
    //}

    public void ShowResult(CharacterData data) //������ ĳ���� ���â�� �����ֱ�
    {
        this.uiResult.gameObject.SetActive(true);
        uiResult.Init(data);

        InfoManager.instance.dicOrderCharacterInfo.Add(orderPickCell, data);
        Debug.Log(InfoManager.instance.dicOrderCharacterInfo);

        this.btnPlus.interactable = false;
    }
}

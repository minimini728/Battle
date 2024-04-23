using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPickCell : MonoBehaviour
{
    private UIResult uiResult; //결과 UI
    public GameObject uiResultGo;

    public GameObject uiSelectGo; //캐릭터 선택 UI

    public Button btnPlus; //플러스 버튼

    public int orderPickCell; //UIPickCell 인스턴스 순서 저장
    void Start()
    {
        this.btnPlus.onClick.AddListener(() =>
        {   
            //선택창
            var go = Instantiate(uiSelectGo, this.transform.parent.parent);
            go.GetComponent<UISelect>().Init(this);
            go.transform.position += new Vector3(0, 0, -3); //레이어 순서때문에 이동

        });

    }

    public void Init(int num)
    {
        this.orderPickCell = num;

        //결과창
        var go = Instantiate(this.uiResultGo, transform);
        this.uiResult = go.GetComponent<UIResult>();
        this.uiResult.gameObject.SetActive(false);

    }

    ////자체적으로 생성된 UIResult를 반환하는 메서드
    //public UIResult GetUIResult()
    //{
    //    return uiResult;
    //}

    public void ShowResult(CharacterData data) //선택한 캐릭터 결과창에 보여주기
    {
        this.uiResult.gameObject.SetActive(true);
        uiResult.Init(data);

        InfoManager.instance.dicOrderCharacterInfo.Add(orderPickCell, data);
        Debug.Log(InfoManager.instance.dicOrderCharacterInfo);

        this.btnPlus.interactable = false;
    }
}

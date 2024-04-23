using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelect : MonoBehaviour
{
    public UISelectCell uiSelectCell; //캐릭터 카드

    public Button btnClose; //닫기 버튼
    public Button btnSelect; //선택 버튼
    public Transform contentTrans; //스크롤뷰 위치

    private UISelectCell selectedCell; //현재 선택된 셀 저장

    private UIPickCell uiPickCell; //눌렸던 pickcell 저장
    void Start()
    {   
        //닫기 버튼
        this.btnClose.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });

        //선택 버튼
        this.btnSelect.onClick.AddListener(() =>
        {
            if(selectedCell != null)
            {
                this.uiPickCell.ShowResult(selectedCell.characterData); //결과 보내는 메서드
                this.gameObject.SetActive(false);

                var go = InfoManager.instance.RemainCharacterInfos.Find(x => x.id == selectedCell.characterData.id);
                InfoManager.instance.RemainCharacterInfos.Remove(go);

                //Start버튼 활성화 -> UIReady 클래스로
                EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ActiveStart);
            }

        });

    }
    public void Init(UIPickCell pickCell)
    {
        this.uiPickCell = pickCell;

        ScrollViewInit();
    }

    //스크롤뷰 생성
    void ScrollViewInit()
    {
        for (int i = 0; i < InfoManager.instance.RemainCharacterInfos.Count; i++)
        {
            var go = Instantiate(this.uiSelectCell, this.contentTrans);
            UISelectCell selectCell = go.GetComponent<UISelectCell>();

            //선택된 셀에 대한 이벤트 처리
            selectCell.btnSelect.onClick.AddListener(() =>
            {
                SelectCell(selectCell);
            });

            selectCell.Init(InfoManager.instance.RemainCharacterInfos[i]);
        }

    }

    //선택된 셀 처리
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

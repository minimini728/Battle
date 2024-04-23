using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillButton : MonoBehaviour
{
    public Text txtSkillName; //스킬 이름
    public Image imgUp; //버프 화살표
    public Image imgDown; //너프 화살표

    public void Init()
    {
        var info = InfoManager.instance.dicOrderCharacterInfo[0].element_id;
        this.txtSkillName.text = DataManager.instance.dicElementData[info].skill_name;

        //스킬 버튼 초기화 이벤트 등록
        EventDispatcher.instance.AddEventHandler<int>((int)EventEnum.eEventType.SkillButtonInit, this.ActiveImage);

    }
    public void ActiveImage(short type, int num) //버프, 너프 표시
    {
        switch (num) //0 none, 1 up, 2 down
        {
            case 0:
                this.imgUp.gameObject.SetActive(false);
                this.imgDown.gameObject.SetActive(false);
                break;
            case 1:
                this.imgUp.gameObject.SetActive(true);
                this.imgDown.gameObject.SetActive(false);
                break;
            case 2:
                this.imgUp.gameObject.SetActive(false);
                this.imgDown.gameObject.SetActive(true);
                break;

        }

        var info = InfoManager.instance.dicOrderCharacterInfo[0].element_id;
        this.txtSkillName.text = DataManager.instance.dicElementData[info].skill_name;
    }


}

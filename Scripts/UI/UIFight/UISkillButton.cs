using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillButton : MonoBehaviour
{
    public Text txtSkillName; //��ų �̸�
    public Image imgUp; //���� ȭ��ǥ
    public Image imgDown; //���� ȭ��ǥ

    public void Init()
    {
        var info = InfoManager.instance.dicOrderCharacterInfo[0].element_id;
        this.txtSkillName.text = DataManager.instance.dicElementData[info].skill_name;

        //��ų ��ư �ʱ�ȭ �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler<int>((int)EventEnum.eEventType.SkillButtonInit, this.ActiveImage);

    }
    public void ActiveImage(short type, int num) //����, ���� ǥ��
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

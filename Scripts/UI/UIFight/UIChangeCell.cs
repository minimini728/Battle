using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeCell : MonoBehaviour
{
    public int orderCharacter; //UIChangeCell ���� - ĳ���� ���� ����

    public Image imgCharacterIcon;
    public Text txtCharacterName;
    public Image imgElementFrame;
    public Image imgElementIcon;
    public Text txtHp;
    public Text txtDamage;
    public Text txtSkillName;
    public Text txtSkillDamage;
    public Image imgElementFrame1;
    public Image imgElementIcon1;
    public Image deem;

    public UIUpDown uiArrow; //��ų �� ȭ��ǥ ui
    public Button btnSelect;

    void Start()
    {
        this.btnSelect.onClick.AddListener(() =>
        {   
            //���� �ٲٱ�
            var temp = InfoManager.instance.dicOrderCharacterInfo[0];
            InfoManager.instance.dicOrderCharacterInfo[0] = InfoManager.instance.dicOrderCharacterInfo[orderCharacter];
            InfoManager.instance.dicOrderCharacterInfo[orderCharacter] = temp;

            //Player UI Refresh -> UIPlayer Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshUIPlayer);
            //UIChange ui refresh �̸� �س��� -> UIChange Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshUIChange);
            //UIChange �ݱ� -> UIChange Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.CloseUIChange);
            //player, enemy sprite renderer ���� �̺�Ʈ ���� -> Player Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchPlayerLayer);
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchEnemyLayer);
            //player ĳ���� ���� �̺�Ʈ ���� -> Player Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchPlayerCharacter);
            //Ÿ�� �ٲٱ� �̺�Ʈ ���� -> FightMain Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ApplyTarget);

        });
    }

    public void Init(int num)
    {
        this.orderCharacter = num;

        this.CellInfoInit();
    }

    public void CellInfoInit()
    {        
        //������ �̹��� ��������
        var info = InfoManager.instance.dicOrderCharacterInfo;
        var atlas = AtlasManager.instance.GetAtlasByName("Total");
        ElementData elementData = DataManager.instance.dicElementData[info[orderCharacter].element_id]; //ù��° ĳ���� �Ӽ� ��������

        this.imgCharacterIcon.sprite = atlas.GetSprite(info[orderCharacter].sprite_name);
        this.txtCharacterName.text = info[orderCharacter].name;
        this.imgElementFrame.sprite = atlas.GetSprite(elementData.sprite_frame_name);
        this.imgElementIcon.sprite = atlas.GetSprite(elementData.sprite_name);
        this.txtHp.text = info[orderCharacter].hp.ToString();
        this.txtDamage.text = info[orderCharacter].damage.ToString();
        this.txtSkillName.text = elementData.skill_name;
        this.txtSkillDamage.text = info[orderCharacter].skill_damage.ToString();
        this.imgElementFrame1.sprite = atlas.GetSprite(elementData.sprite_frame_name);
        this.imgElementIcon1.sprite = atlas.GetSprite(elementData.sprite_name);

        if(info[orderCharacter].hp <= 0) //���� ĳ���� ó��
        {
            this.txtHp.text = 0.ToString();

            this.deem.gameObject.SetActive(true);

            this.btnSelect.interactable = false;
        }

        //��ų ȭ��ǥ
        var character = FindObjectOfType<Character>();
        var enemyId = InfoManager.instance.dicEnemyCharacterInfo[0].element_id;
        var num = character.SkillUIArrow(info[orderCharacter].element_id, enemyId);

        this.uiArrow.ActiveArrow(num);
    }

}

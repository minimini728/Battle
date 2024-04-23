using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeCell : MonoBehaviour
{
    public int orderCharacter; //UIChangeCell 순서 - 캐릭터 공격 순서

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

    public UIUpDown uiArrow; //스킬 상성 화살표 ui
    public Button btnSelect;

    void Start()
    {
        this.btnSelect.onClick.AddListener(() =>
        {   
            //순서 바꾸기
            var temp = InfoManager.instance.dicOrderCharacterInfo[0];
            InfoManager.instance.dicOrderCharacterInfo[0] = InfoManager.instance.dicOrderCharacterInfo[orderCharacter];
            InfoManager.instance.dicOrderCharacterInfo[orderCharacter] = temp;

            //Player UI Refresh -> UIPlayer 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshUIPlayer);
            //UIChange ui refresh 미리 해놓기 -> UIChange 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshUIChange);
            //UIChange 닫기 -> UIChange 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.CloseUIChange);
            //player, enemy sprite renderer 변경 이벤트 전송 -> Player 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchPlayerLayer);
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchEnemyLayer);
            //player 캐릭터 변경 이벤트 전송 -> Player 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.SwitchPlayerCharacter);
            //타겟 바꾸기 이벤트 전송 -> FightMain 클래스로
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
        //정보와 이미지 가져오기
        var info = InfoManager.instance.dicOrderCharacterInfo;
        var atlas = AtlasManager.instance.GetAtlasByName("Total");
        ElementData elementData = DataManager.instance.dicElementData[info[orderCharacter].element_id]; //첫번째 캐릭터 속성 가져오기

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

        if(info[orderCharacter].hp <= 0) //죽은 캐릭터 처리
        {
            this.txtHp.text = 0.ToString();

            this.deem.gameObject.SetActive(true);

            this.btnSelect.interactable = false;
        }

        //스킬 화살표
        var character = FindObjectOfType<Character>();
        var enemyId = InfoManager.instance.dicEnemyCharacterInfo[0].element_id;
        var num = character.SkillUIArrow(info[orderCharacter].element_id, enemyId);

        this.uiArrow.ActiveArrow(num);
    }

}

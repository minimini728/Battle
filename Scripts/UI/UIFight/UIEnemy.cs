using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemy : MonoBehaviour
{
    public Image imgCurrentCharacterIcon;
    public Text txtCurrentCharacterName;
    public Image imgCurrentElementFrame;
    public Image imgCurrentElementIcon;
    public Slider sliderCurrentCharaterHp;
    public Text txtHp;

    public Image imgSecondCharacterIcon;
    public Slider sliderSecondCharaterHp;
    public Image deem1;

    public Image imgThirdCharacterIcon;
    public Slider sliderThirdCharacterHp;
    public Image deem2;
    void Start()
    {
        //UI Refresh 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.RefreshUIEnemy, this.RefreshUI);

        this.InitInfo();
    }

    public void InitInfo() //UI 초기화
    {
        //정보와 이미지 가져오기
        var info = InfoManager.instance.dicEnemyCharacterInfo;
        var atlas = AtlasManager.instance.GetAtlasByName("Total");
        ElementData elementData = DataManager.instance.dicElementData[info[0].element_id]; //첫번째 캐릭터 속성 가져오기

        this.imgCurrentCharacterIcon.sprite = atlas.GetSprite(info[0].sprite_name);
        this.txtCurrentCharacterName.text = info[0].name;
        this.imgCurrentElementFrame.sprite = atlas.GetSprite(elementData.sprite_frame_name);
        this.imgCurrentElementIcon.sprite = atlas.GetSprite(elementData.sprite_name);
        this.sliderCurrentCharaterHp.value = Mathf.Clamp01((float)info[0].hp / 5000f);
        if(info[0].hp > 0)
        {
            this.txtHp.text = string.Format("{0}/5000", info[0].hp);
        }
        else
        {
            this.txtHp.text = string.Format("0/5000");
        }

        this.imgSecondCharacterIcon.sprite = atlas.GetSprite(info[1].sprite_name);
        this.sliderSecondCharaterHp.value = Mathf.Clamp01((float)info[1].hp / 5000f);
        if(info[1].hp <= 0)
        {
            this.deem1.gameObject.SetActive(true);
        }

        this.imgThirdCharacterIcon.sprite = atlas.GetSprite(info[2].sprite_name);
        this.sliderThirdCharacterHp.value = Mathf.Clamp01((float)info[2].hp / 5000f);
        if(info[2].hp <= 0)
        {
            this.deem2.gameObject.SetActive(true);
        }
    }

    public void RefreshUI(short type) //UI Refresh 이벤트 메서드
    {
        this.InitInfo();
    }

}

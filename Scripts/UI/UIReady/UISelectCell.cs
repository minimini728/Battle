using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectCell : MonoBehaviour
{
    public Image imgCharacter; //캐릭터 이미지
    public Text txtName; //캐릭터 이름
    public Image imgElementFrame; //속성 배경
    public Image imgElement; //속성 아이콘

    public GameObject imgSelect; //아웃라인
    public Button btnSelect;

    public CharacterData characterData; //캐릭터 정보 저장
    void Start()
    {
        this.btnSelect.onClick.AddListener(() =>
        {
            this.imgSelect.gameObject.SetActive(true);
        });
    }

    public void Init(CharacterData characterData)
    {
        this.characterData = characterData;

        //캐릭터에 맞는 속성 데이터 가져오기
        ElementData elementData = DataManager.instance.dicElementData[characterData.element_id];

        //오브젝트들 채우기
        var atlas = AtlasManager.instance.GetAtlasByName("Total"); //아틀라스 가져오기

        this.imgCharacter.sprite = atlas.GetSprite(characterData.sprite_name);
        this.txtName.text = characterData.name;
        this.imgElementFrame.sprite = atlas.GetSprite(elementData.sprite_frame_name);
        this.imgElement.sprite = atlas.GetSprite(elementData.sprite_name);

    }

}

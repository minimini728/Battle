using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResult : MonoBehaviour
{
    public Image imgElementFrame; //속성 배경
    public Image imgElement; //속성 아이콘
    public Text txtName; //캐릭터 이름


    public void Init(CharacterData data)
    {
        //캐릭터에 맞는 속성 데이터 가져오기
        ElementData elementData = DataManager.instance.dicElementData[data.element_id];

        //오브젝트들 채우기
        var atlas = AtlasManager.instance.GetAtlasByName("Total"); //아틀라스 가져오기

        GameObject prefab = Resources.Load<GameObject>("Prefab/Character/"+ data.prefab_name); //프리팹 로드

        if (prefab != null)
        {   
            //캐릭터 오브젝트 설정
            Vector3 localPosition = new Vector3(0f, -1.5f, -1f); //상대 좌표
            Vector3 scale = new Vector3(60f, 60f, 60f); //스케일 설정

            GameObject characterGo = Instantiate(prefab, transform);
            characterGo.transform.localPosition = localPosition; //상대 위치 설정
            characterGo.transform.localScale = scale; //스케일 설정

            //이미지와 텍스트 설정
            this.imgElementFrame.sprite = atlas.GetSprite(elementData.sprite_frame_name);
            this.imgElement.sprite = atlas.GetSprite(elementData.sprite_name);
            this.txtName.text = data.name;
        }
        else
        {
            Debug.LogError("캐릭터 오브젝트 못 찾음: " + data.prefab_name);
        }

    }
}

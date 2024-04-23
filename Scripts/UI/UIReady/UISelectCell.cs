using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectCell : MonoBehaviour
{
    public Image imgCharacter; //ĳ���� �̹���
    public Text txtName; //ĳ���� �̸�
    public Image imgElementFrame; //�Ӽ� ���
    public Image imgElement; //�Ӽ� ������

    public GameObject imgSelect; //�ƿ�����
    public Button btnSelect;

    public CharacterData characterData; //ĳ���� ���� ����
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

        //ĳ���Ϳ� �´� �Ӽ� ������ ��������
        ElementData elementData = DataManager.instance.dicElementData[characterData.element_id];

        //������Ʈ�� ä���
        var atlas = AtlasManager.instance.GetAtlasByName("Total"); //��Ʋ�� ��������

        this.imgCharacter.sprite = atlas.GetSprite(characterData.sprite_name);
        this.txtName.text = characterData.name;
        this.imgElementFrame.sprite = atlas.GetSprite(elementData.sprite_frame_name);
        this.imgElement.sprite = atlas.GetSprite(elementData.sprite_name);

    }

}

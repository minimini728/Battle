using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResult : MonoBehaviour
{
    public Image imgElementFrame; //�Ӽ� ���
    public Image imgElement; //�Ӽ� ������
    public Text txtName; //ĳ���� �̸�


    public void Init(CharacterData data)
    {
        //ĳ���Ϳ� �´� �Ӽ� ������ ��������
        ElementData elementData = DataManager.instance.dicElementData[data.element_id];

        //������Ʈ�� ä���
        var atlas = AtlasManager.instance.GetAtlasByName("Total"); //��Ʋ�� ��������

        GameObject prefab = Resources.Load<GameObject>("Prefab/Character/"+ data.prefab_name); //������ �ε�

        if (prefab != null)
        {   
            //ĳ���� ������Ʈ ����
            Vector3 localPosition = new Vector3(0f, -1.5f, -1f); //��� ��ǥ
            Vector3 scale = new Vector3(60f, 60f, 60f); //������ ����

            GameObject characterGo = Instantiate(prefab, transform);
            characterGo.transform.localPosition = localPosition; //��� ��ġ ����
            characterGo.transform.localScale = scale; //������ ����

            //�̹����� �ؽ�Ʈ ����
            this.imgElementFrame.sprite = atlas.GetSprite(elementData.sprite_frame_name);
            this.imgElement.sprite = atlas.GetSprite(elementData.sprite_name);
            this.txtName.text = data.name;
        }
        else
        {
            Debug.LogError("ĳ���� ������Ʈ �� ã��: " + data.prefab_name);
        }

    }
}

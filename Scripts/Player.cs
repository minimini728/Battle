using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public Character currentCharacter; //���� ĳ���� �ν��Ͻ�
    public GameObject currentCharacterObj; //���� ĳ���� ������Ʈ
    
    
    void Start()
    {
        //�÷��̾� sprite renderer layer ���� ���� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.SwitchPlayerLayer, this.SwitchLayer);
        //�÷��̾� ĳ���� ���� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.SwitchPlayerCharacter, this.SwitchCharacter);
    }

    public void Init()
    {
        this.SwitchCharacter(1);
    }

    public void SwitchCharacter(short type) //ĳ���� ������Ʈ ���� �޼���
    {   
        //������Ʈ ����
        var info = InfoManager.instance.dicOrderCharacterInfo;
        var data = DataManager.instance.dicCharacterData;

        GameObject prefab = Resources.Load<GameObject>("Prefab/Character/" + data[info[0].id].prefab_name); //������ �ε�
        Vector3 scale = new Vector3(0.55f, 0.55f, 0.55f); //������ ����

        if(this.currentCharacterObj != null)
        {
            Destroy(this.currentCharacterObj);
        }

        currentCharacterObj = Instantiate(prefab, this.transform);
        currentCharacterObj.transform.localScale = scale;
        Character currentCharacter = currentCharacterObj.GetComponent<Character>();
        currentCharacter.Init(this.SwitchElement(info[0].id), info[0].id);
        this.currentCharacter = currentCharacter;

    }

    public void SwitchLayer(short type) //ĳ���� ���̾� ���� �̺�Ʈ �޼���
    {
        int i = this.currentCharacterObj.GetComponent<SpriteRenderer>().sortingOrder;

        if (i == 0)
        {   
            i = -1;
        }
        else 
        { 
            i = 0; 
        }

        this.currentCharacterObj.GetComponent<SpriteRenderer>().sortingOrder = i;

    }

    public void Punch() //��ġ
    {
        this.currentCharacter.Punch();
    }
    public void Skill() //��ų
    {
        this.currentCharacter.SkillEffect(0);
    }

    public void Hit(int hitDamage) //����
    {
        this.currentCharacter.Hit(hitDamage + 1000); //�ִϸ��̼� ����

        this.currentCharacter.hp -= hitDamage; //hp ����

        if(hitDamage >= 500) //��ų ��������
        {
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ShakeCamera);
        }

        var info = InfoManager.instance.dicOrderCharacterInfo; //info hp ����
        info[0].hp -= hitDamage + 1000;

        if(info[0].hp <= 0 && info[1].hp <= 0 && info[2].hp <= 0) //��� ĳ���� ������
        {
            //ShowUILose �̺�Ʈ ���� -> UIFIght Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ShowUILose);
            //EndGame �̺�Ʈ ���� -> FightMain Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.EndGame);

        }
        else if(info[0].hp <= 0) //���� ĳ���� ������
        {
            StartCoroutine(this.ShowUIChange()); //�ٸ� ĳ���� ����
        }

        //UIPlayer refresh -> UIPlayer Ŭ������
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshUIPlayer);
    }

    public IEnumerator ShowUIChange()
    {
        yield return new WaitForSeconds(1.2f);

        //ShowUIChange �̺�Ʈ ���� -> UIFight Ŭ������
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ShowUIChange);

    }
    public IElement SwitchElement(int num)
    {
        IElement element = null;

        switch (num)
        {
            case 200: 
                element = new WaterElement();
                break;
            case 201:
                element = new FireElement();
                break;
            case 202:
                element = new ForestElement();
                break;
        }
        return element;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public Character currentCharacter; //���� ĳ���� �ν��Ͻ�
    public GameObject currentCharacterObj; //���� ĳ���� ������Ʈ

    private GameObject effectGo; //��ų ����Ʈ ������Ʈ
    void Start()
    {
        //�÷��̾� sprite renderer layer ���� ���� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.SwitchEnemyLayer, this.SwitchLayer);
        //�÷��̾� ĳ���� ���� �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.SwitchEnemyCharacter, this.SwitchCharacter);
    }

    public void Init()
    {
        this.SwitchCharacter(1);
    }

    public void SwitchCharacter(short type) //ĳ���� ������Ʈ ���� �޼���
    {
        //������Ʈ ����
        var info = InfoManager.instance.dicEnemyCharacterInfo;
        var data = DataManager.instance.dicCharacterData;

        GameObject prefab = Resources.Load<GameObject>("Prefab/Character/" + data[info[0].id].prefab_name); //������ �ε�
        Vector3 scale = new Vector3(0.55f, 0.55f, 0.55f); //������ ����

        if (this.currentCharacterObj != null)
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

    public void Punch()
    {
        this.currentCharacter.Punch();
    }

    public void Skill()
    {
        this.currentCharacter.SkillEffect(1);
    }
    public void Hit(int hitDamage)
    {   
        this.currentCharacter.Hit(hitDamage + 1000); //�ִϸ��̼� ����

        this.currentCharacter.hp -= hitDamage; //hp ����

        if (hitDamage >= 500) //��ų ��������
        {   
            //ShakeCamera �̺�Ʈ ���� -> ShakeCamera Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ShakeCamera);
        }

        var info = InfoManager.instance.dicEnemyCharacterInfo; //info hp ����
        info[0].hp -= hitDamage + 1000;

        if(info[0].hp <= 0 && info[1].hp <= 0 && info[2].hp <= 0) //��� ĳ���� ������
        {
            StartCoroutine(this.PlayDeadEffect()); //�״� ����Ʈ ���

            //ShowUIWin �̺�Ʈ ���� -> UIFight Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ShowUIWin);
            //EndGame �̺�Ʈ ���� -> FightMain Ŭ������
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.EndGame);

        }
        else if(info[0].hp <= 0) //���� ĳ���� ������
        {
            StartCoroutine(this.Dead());
        }

        //UIEnemy refresh -> UIEnemy Ŭ������
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshUIEnemy);

    }
    public IEnumerator Dead() //������ ���� ĳ���ͷ� �ٲٱ�
    {
        StartCoroutine(this.PlayDeadEffect()); //�״� ����Ʈ ���

        yield return new WaitForSeconds(1f);
        //���� �ٲٱ�
        var temp = InfoManager.instance.dicEnemyCharacterInfo[0];
        if(InfoManager.instance.dicEnemyCharacterInfo[1].hp > 0)
        {
            InfoManager.instance.dicEnemyCharacterInfo[0] = InfoManager.instance.dicEnemyCharacterInfo[1];
            InfoManager.instance.dicEnemyCharacterInfo[1] = temp;
        }
        else
        {
            InfoManager.instance.dicEnemyCharacterInfo[0] = InfoManager.instance.dicEnemyCharacterInfo[2];
            InfoManager.instance.dicEnemyCharacterInfo[2] = temp;
        }

        //UIEnemy refresh -> UIEnemy Ŭ������
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshUIEnemy);

        this.SwitchCharacter(1);

        //Ÿ�� �ٲٱ� �̺�Ʈ ���� -> FightMain Ŭ������
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ApplyTarget);

    }
    public IEnumerator PlayDeadEffect() //�״� ����Ʈ ���
    {
        yield return new WaitForSeconds(0.7f);
        this.currentCharacterObj.gameObject.SetActive(false);

        Vector3 scale = new Vector3(0.3f, 0.3f, 0.3f); //������ ����
        switch (InfoManager.instance.dicEnemyCharacterInfo[0].element_id) //�Ӽ��� ����Ʈ
        {
            case 100: //��
                effectGo = EffectManager.instance.GetEffect("Explosion_blue");
                var go = Instantiate(effectGo, this.transform);
                go.transform.localPosition = new Vector3(0f, 0f, -1f);
                go.transform.localScale = scale; //������ ����
                this.effectGo = go;
                break;
            case 101: //��
                effectGo = EffectManager.instance.GetEffect("Explosion_normal");
                var go1 = Instantiate(effectGo, this.transform);
                go1.transform.localPosition = new Vector3(0f, 0f, -1f);
                go1.transform.localScale = scale; //������ ����
                this.effectGo = go1;
                break;
            case 102: //��
                effectGo = EffectManager.instance.GetEffect("Explosion_green");
                var go2 = Instantiate(effectGo, this.transform);
                go2.transform.localPosition = new Vector3(0f, 0f, -1f);
                go2.transform.localScale = scale; //������ ����
                this.effectGo = go2;
                break;
        }

        var particle = this.effectGo.GetComponent<ParticleSystem>();
        particle.loop = false;

    }
    public IElement SwitchElement(int num) //�Ӽ� ��ġ
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

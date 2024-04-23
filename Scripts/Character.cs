using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character: MonoBehaviour
{   
    [SerializeField]
    public IElement element; //�Ӽ�
    public IElement target; //��� �Ӽ�

    public int id;
    public int hp;
    public int damage;

    public Animator anim;
    private ParticleSystem effectSkill;
    private GameObject effectGo;

    public void Init(IElement element, int id)
    {
        this.element = element;
        this.id = id;
        this.anim = GetComponent<Animator>();
        this.damage = DataManager.instance.dicCharacterData[id].damage;
    }

    public void Punch()
    {
        Debug.LogFormat("{0}: ��ġ", this.id);

    }
    public void SkillEffect(int euler)
    {

        if (this.transform.childCount == 0) //����Ʈ�� ������
        {
            this.CreateEffect(); //����Ʈ ����
            if(euler == 1) //enemy�� ����Ʈ z�� ����
            {
                Vector3 localPosition = this.effectGo.transform.localPosition;
                localPosition.z *= -1;
                this.effectGo.transform.localPosition = localPosition;
            }

            //flip
            var rendererModule = this.effectGo.GetComponent<ParticleSystemRenderer>();
            rendererModule.flip = new Vector3(euler, 0, 0);

            //particlesystem ����
            var particle = this.effectGo.GetComponent<ParticleSystem>();
            particle.playOnAwake = false;
            particle.loop = false;
            particle.Play();

            this.effectSkill = particle;
        }
        else //�̹� ����Ʈ�� ������
        {
            this.effectSkill.Play();
        }

    }
    public void CreateEffect() //�Ӽ��� ����Ʈ ����
    {
        GameObject effect = null;

        switch (this.element)
        {
            case WaterElement:
                effect = EffectManager.instance.GetEffect("Water_Splash_01_basic");
                var go = Instantiate(effect, this.transform);
                go.transform.localPosition = new Vector3(16.5f, 0f, -1f);
                this.effectGo = go;
                break;
            case ForestElement:
                effect = EffectManager.instance.GetEffect("Lazer_green");
                var go1 = Instantiate(effect, this.transform);
                go1.transform.localPosition = new Vector3(16.5f, 3f, -1f);
                this.effectGo = go1;
                break;
            case FireElement:
                effect = EffectManager.instance.GetEffect("flame_side");
                var go2 = Instantiate(effect, this.transform);
                go2.transform.localPosition = new Vector3(18f, 3f, -1f);
                this.effectGo = go2;
                break;
        }
    }

    public int CalculateDamage(IElement target) //��ų �� ������ ���
    {
        this.target = target;
        return element.CalculateDamage(this.target);
    }
    public void AssignTarget(IElement target) //��� �Ӽ� ����
    {
        this.target = target;
    }
    public void ApplySkillButton(IElement target)
    {
        //��ų ��ư �ʱ�ȭ �̺�Ʈ ���� -> UISkillButton Ŭ������
        Debug.Log(this.target);
        EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.SkillButtonInit, element.SkillButtonInit(target));
        
    }
    public void Hit(int damage)
    {
        Debug.LogFormat("{0}: ����", this.id);

        var txt = ObjectPoolManager.GetObject(); //������Ʈ Ǯ���� ������ �ؽ�Ʈ ��������
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        txt.Init(pos, damage); //������ �ؽ�Ʈ ��ġ, ���� �ʱ�ȭ

        StartCoroutine(this.PlayAnim()); //���� �ִϸ��̼�

    }
    IEnumerator PlayAnim() //���� �ִϸ��̼�
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true); //���� ������Ʈ Ȱ��ȭ
        }

        this.anim.SetInteger("State", 1);

        yield return new WaitForSeconds(0.9f); //0.9�� ���

        this.anim.SetInteger("State", 0);

    }
    public int SkillUIArrow(int id, int enemyId) //ĳ���� ���� UI ��ų �� ȭ��ǥ
    {
        IElement element = null;
        int num = 0;

        switch (id)
        {
            case 100:
                element = new WaterElement();
                break;
            case 101:
                element = new FireElement();
                break;
            case 102:
                element = new ForestElement();
                break;
        }

        return num = element.UISkillArrowInit(enemyId);

    }
}

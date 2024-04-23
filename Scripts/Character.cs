using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character: MonoBehaviour
{   
    public IElement element; //속성
    public IElement target; //상대 속성

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
        Debug.LogFormat("{0}: 펀치", this.id);

    }
    public void SkillEffect(int euler)
    {

        if (this.transform.childCount == 0) //이펙트가 없으면
        {
            this.CreateEffect(); //이펙트 생성
            if(euler == 1) //enemy는 이펙트 z값 반전
            {
                Vector3 localPosition = this.effectGo.transform.localPosition;
                localPosition.z *= -1;
                this.effectGo.transform.localPosition = localPosition;
            }

            //flip
            var rendererModule = this.effectGo.GetComponent<ParticleSystemRenderer>();
            rendererModule.flip = new Vector3(euler, 0, 0);

            //particlesystem 설정
            var particle = this.effectGo.GetComponent<ParticleSystem>();
            particle.playOnAwake = false;
            particle.loop = false;
            particle.Play();

            this.effectSkill = particle;
        }
        else //이미 이펙트가 있으면
        {
            this.effectSkill.Play();
        }

    }
    public void CreateEffect() //속성별 이펙트 생성
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

    public int CalculateDamage(IElement target) //스킬 상성 데이지 계산
    {
        this.target = target;
        return element.CalculateDamage(this.target);
    }
    public void AssignTarget(IElement target) //상대 속성 지정
    {
        this.target = target;
    }
    public void ApplySkillButton(IElement target)
    {
        //스킬 버튼 초기화 이벤트 전송 -> UISkillButton 클래스로
        Debug.Log(this.target);
        EventDispatcher.instance.SendEvent<int>((int)EventEnum.eEventType.SkillButtonInit, element.SkillButtonInit(target));
        
    }
    public void Hit(int damage)
    {
        Debug.LogFormat("{0}: 피해", this.id);

        var txt = ObjectPoolManager.GetObject(); //오브젝트 풀에서 데미지 텍스트 가져오기
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        txt.Init(pos, damage); //데미지 텍스트 위치, 글자 초기화

        StartCoroutine(this.PlayAnim()); //피해 애니메이션

    }
    IEnumerator PlayAnim() //피해 애니메이션
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true); //게임 오브젝트 활성화
        }

        this.anim.SetInteger("State", 1);

        yield return new WaitForSeconds(0.9f); //0.9초 대기

        this.anim.SetInteger("State", 0);

    }
    public int SkillUIArrow(int id, int enemyId) //캐릭터 변경 UI 스킬 상성 화살표
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

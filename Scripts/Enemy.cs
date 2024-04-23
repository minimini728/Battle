using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public Character currentCharacter; //현재 캐릭터 인스턴스
    public GameObject currentCharacterObj; //현재 캐릭터 오브젝트

    private GameObject effectGo; //스킬 이펙트 오브젝트
    void Start()
    {
        //플레이어 sprite renderer layer 순서 변경 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.SwitchEnemyLayer, this.SwitchLayer);
        //플레이어 캐릭터 변경 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.SwitchEnemyCharacter, this.SwitchCharacter);
    }

    public void Init()
    {
        this.SwitchCharacter(1);
    }

    public void SwitchCharacter(short type) //캐릭터 오브젝트 변경 메서드
    {
        //오브젝트 변경
        var info = InfoManager.instance.dicEnemyCharacterInfo;
        var data = DataManager.instance.dicCharacterData;

        GameObject prefab = Resources.Load<GameObject>("Prefab/Character/" + data[info[0].id].prefab_name); //프리팹 로드
        Vector3 scale = new Vector3(0.55f, 0.55f, 0.55f); //스케일 설정

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

    public void SwitchLayer(short type) //캐릭터 레이어 변경 이벤트 메서드
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
        this.currentCharacter.Hit(hitDamage + 1000); //애니메이션 실행

        this.currentCharacter.hp -= hitDamage; //hp 차감

        if (hitDamage >= 500) //스킬 데미지시
        {   
            //ShakeCamera 이벤트 전송 -> ShakeCamera 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ShakeCamera);
        }

        var info = InfoManager.instance.dicEnemyCharacterInfo; //info hp 차감
        info[0].hp -= hitDamage + 1000;

        if(info[0].hp <= 0 && info[1].hp <= 0 && info[2].hp <= 0) //모든 캐릭터 죽으면
        {
            StartCoroutine(this.PlayDeadEffect()); //죽는 이펙트 재생

            //ShowUIWin 이벤트 전송 -> UIFight 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ShowUIWin);
            //EndGame 이벤트 전송 -> FightMain 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.EndGame);

        }
        else if(info[0].hp <= 0) //현재 캐릭터 죽으면
        {
            StartCoroutine(this.Dead());
        }

        //UIEnemy refresh -> UIEnemy 클래스로
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshUIEnemy);

    }
    public IEnumerator Dead() //죽으면 다음 캐릭터로 바꾸기
    {
        StartCoroutine(this.PlayDeadEffect()); //죽는 이펙트 재생

        yield return new WaitForSeconds(1f);
        //순서 바꾸기
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

        //UIEnemy refresh -> UIEnemy 클래스로
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshUIEnemy);

        this.SwitchCharacter(1);

        //타겟 바꾸기 이벤트 전송 -> FightMain 클래스로
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ApplyTarget);

    }
    public IEnumerator PlayDeadEffect() //죽는 이펙트 재생
    {
        yield return new WaitForSeconds(0.7f);
        this.currentCharacterObj.gameObject.SetActive(false);

        Vector3 scale = new Vector3(0.3f, 0.3f, 0.3f); //스케일 설정
        switch (InfoManager.instance.dicEnemyCharacterInfo[0].element_id) //속성별 이펙트
        {
            case 100: //물
                effectGo = EffectManager.instance.GetEffect("Explosion_blue");
                var go = Instantiate(effectGo, this.transform);
                go.transform.localPosition = new Vector3(0f, 0f, -1f);
                go.transform.localScale = scale; //스케일 설정
                this.effectGo = go;
                break;
            case 101: //불
                effectGo = EffectManager.instance.GetEffect("Explosion_normal");
                var go1 = Instantiate(effectGo, this.transform);
                go1.transform.localPosition = new Vector3(0f, 0f, -1f);
                go1.transform.localScale = scale; //스케일 설정
                this.effectGo = go1;
                break;
            case 102: //숲
                effectGo = EffectManager.instance.GetEffect("Explosion_green");
                var go2 = Instantiate(effectGo, this.transform);
                go2.transform.localPosition = new Vector3(0f, 0f, -1f);
                go2.transform.localScale = scale; //스케일 설정
                this.effectGo = go2;
                break;
        }

        var particle = this.effectGo.GetComponent<ParticleSystem>();
        particle.loop = false;

    }
    public IElement SwitchElement(int num) //속성 매치
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public Character currentCharacter; //현재 캐릭터 인스턴스
    public GameObject currentCharacterObj; //현재 캐릭터 오브젝트
    
    
    void Start()
    {
        //플레이어 sprite renderer layer 순서 변경 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.SwitchPlayerLayer, this.SwitchLayer);
        //플레이어 캐릭터 변경 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.SwitchPlayerCharacter, this.SwitchCharacter);
    }

    public void Init()
    {
        this.SwitchCharacter(1);
    }

    public void SwitchCharacter(short type) //캐릭터 오브젝트 변경 메서드
    {   
        //오브젝트 변경
        var info = InfoManager.instance.dicOrderCharacterInfo;
        var data = DataManager.instance.dicCharacterData;

        GameObject prefab = Resources.Load<GameObject>("Prefab/Character/" + data[info[0].id].prefab_name); //프리팹 로드
        Vector3 scale = new Vector3(0.55f, 0.55f, 0.55f); //스케일 설정

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

    public void Punch() //펀치
    {
        this.currentCharacter.Punch();
    }
    public void Skill() //스킬
    {
        this.currentCharacter.SkillEffect(0);
    }

    public void Hit(int hitDamage) //피해
    {
        this.currentCharacter.Hit(hitDamage + 1000); //애니메이션 실행

        this.currentCharacter.hp -= hitDamage; //hp 차감

        if(hitDamage >= 500) //스킬 데미지시
        {
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ShakeCamera);
        }

        var info = InfoManager.instance.dicOrderCharacterInfo; //info hp 차감
        info[0].hp -= hitDamage + 1000;

        if(info[0].hp <= 0 && info[1].hp <= 0 && info[2].hp <= 0) //모든 캐릭터 죽으면
        {
            //ShowUILose 이벤트 전송 -> UIFIght 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ShowUILose);
            //EndGame 이벤트 전송 -> FightMain 클래스로
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.EndGame);

        }
        else if(info[0].hp <= 0) //현재 캐릭터 죽으면
        {
            StartCoroutine(this.ShowUIChange()); //다른 캐릭터 선택
        }

        //UIPlayer refresh -> UIPlayer 클래스로
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.RefreshUIPlayer);
    }

    public IEnumerator ShowUIChange()
    {
        yield return new WaitForSeconds(1.2f);

        //ShowUIChange 이벤트 전송 -> UIFight 클래스로
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

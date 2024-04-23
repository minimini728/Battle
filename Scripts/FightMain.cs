using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMain : MonoBehaviour
{
    public UIFightDirector director; //UI 캔버스
    public Player player;
    public Enemy enemy;
    public bool isPlayerTurn; //턴 추적
    public bool isEndGame; //게임 끝

    void Start()
    {
        //데이터 로드, 초기화
        DataManager.instance.LoadCharacterData();
        DataManager.instance.LoadElementData();
        InfoManager.instance.RemainCharacterInfoInit();
        InfoManager.instance.OrderCharacterInfoInit();
        InfoManager.instance.EnemyCharacterInfoInit();

        //player 초기화 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.PlayerInit, this.PlayerInif);
        //EndGame 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.EndGame, this.EndGame);

        this.isEndGame = false;
        this.isPlayerTurn = true;
    }

    public void PlayerInif(short type)
    {
        this.player.Init();
        this.enemy.Init();

        //puch 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.Punch, this.Punch);
        //skill 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.Skill, this.Skill);
        //target 이벤트 등록
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ApplyTarget, this.ApplyTarget);

        this.ApplyTarget(1);
    }

    public void Punch(short type) //펀치
    {
        if (isEndGame) return;

        if (isPlayerTurn)
        {
            this.player.Punch(); //플레이어 공격

            this.enemy.Hit(250); //적 피해

            this.isPlayerTurn = false;
            StartCoroutine(this.EnemyAttackTurn());

        }
        else
        {
            this.enemy.Punch();

            this.player.Hit(250);
        }

    }

    public void Skill(short type) //스킬
    {
        if (isEndGame) return;

        if (isPlayerTurn)
        {
            this.player.Skill(); //플레이어 공격

            this.enemy.Hit(this.ApplyElementInteraction()); //적 피해

            this.isPlayerTurn = false;
            StartCoroutine(this.EnemyAttackTurn());
        }
        else
        {
            this.enemy.Skill();

            this.player.Hit(this.ApplyElementInteraction());
        }

    }

    public IEnumerator EnemyAttackTurn() //적 공격 개시
    {
        yield return new WaitForSeconds(1.9f);

        int damage = this.enemy.currentCharacter.CalculateDamage(this.player.currentCharacter.element);

        if ( damage >= 500) //스킬
        {
            this.Skill(1);
        }
        else //펀치
        {
            this.Punch(1);
        }

        this.isPlayerTurn = true;

        //버튼 활성화 -> UIFight 클래스로
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ActiveButton);
    }
    public int ApplyElementInteraction() //속성 상성 데미지 계산
    {
        if (isPlayerTurn)
        {
            return this.player.currentCharacter.CalculateDamage(this.enemy.currentCharacter.element);
        }
        else
        {
            return this.enemy.currentCharacter.CalculateDamage(this.player.currentCharacter.element);
        }
    }
    public void ApplyTarget(short type)
    {
        this.player.currentCharacter.AssignTarget(this.enemy.currentCharacter.element);
        this.enemy.currentCharacter.AssignTarget(this.player.currentCharacter.element);

        //스킬 버튼 초기화
        this.player.currentCharacter.ApplySkillButton(this.enemy.currentCharacter.element);

    }

    private void EndGame(short type)
    {
        this.isEndGame = true;
    }
}

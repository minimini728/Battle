using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMain : MonoBehaviour
{
    public UIFightDirector director; //UI ĵ����
    public Player player;
    public Enemy enemy;
    public bool isPlayerTurn; //�� ����
    public bool isEndGame; //���� ��

    void Start()
    {
        //������ �ε�, �ʱ�ȭ
        DataManager.instance.LoadCharacterData();
        DataManager.instance.LoadElementData();
        InfoManager.instance.RemainCharacterInfoInit();
        InfoManager.instance.OrderCharacterInfoInit();
        InfoManager.instance.EnemyCharacterInfoInit();

        //player �ʱ�ȭ �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.PlayerInit, this.PlayerInif);
        //EndGame �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.EndGame, this.EndGame);

        this.isEndGame = false;
        this.isPlayerTurn = true;
    }

    public void PlayerInif(short type)
    {
        this.player.Init();
        this.enemy.Init();

        //puch �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.Punch, this.Punch);
        //skill �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.Skill, this.Skill);
        //target �̺�Ʈ ���
        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ApplyTarget, this.ApplyTarget);

        this.ApplyTarget(1);
    }

    public void Punch(short type) //��ġ
    {
        if (isEndGame) return;

        if (isPlayerTurn)
        {
            this.player.Punch(); //�÷��̾� ����

            this.enemy.Hit(250); //�� ����

            this.isPlayerTurn = false;
            StartCoroutine(this.EnemyAttackTurn());

        }
        else
        {
            this.enemy.Punch();

            this.player.Hit(250);
        }

    }

    public void Skill(short type) //��ų
    {
        if (isEndGame) return;

        if (isPlayerTurn)
        {
            this.player.Skill(); //�÷��̾� ����

            this.enemy.Hit(this.ApplyElementInteraction()); //�� ����

            this.isPlayerTurn = false;
            StartCoroutine(this.EnemyAttackTurn());
        }
        else
        {
            this.enemy.Skill();

            this.player.Hit(this.ApplyElementInteraction());
        }

    }

    public IEnumerator EnemyAttackTurn() //�� ���� ����
    {
        yield return new WaitForSeconds(1.9f);

        int damage = this.enemy.currentCharacter.CalculateDamage(this.player.currentCharacter.element);

        if ( damage >= 500) //��ų
        {
            this.Skill(1);
        }
        else //��ġ
        {
            this.Punch(1);
        }

        this.isPlayerTurn = true;

        //��ư Ȱ��ȭ -> UIFight Ŭ������
        EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.ActiveButton);
    }
    public int ApplyElementInteraction() //�Ӽ� �� ������ ���
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

        //��ų ��ư �ʱ�ȭ
        this.player.currentCharacter.ApplySkillButton(this.enemy.currentCharacter.element);

    }

    private void EndGame(short type)
    {
        this.isEndGame = true;
    }
}

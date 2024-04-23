using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnum
{
    public enum eEventType
    {
        NONE = -1,
        ActiveStart,
        FightStart,
        CloseUIChange,
        RefreshUIPlayer,
        RefreshUIChange,
        PlayerInit,
        SwitchPlayerLayer,
        SwitchPlayerCharacter,
        SwitchEnemyLayer,
        SwitchEnemyCharacter,
        Punch,
        Hit,
        RefreshUIEnemy,
        Skill,
        SkillButtonInit,
        ApplySkillButton,
        ApplyTarget,
        ShowUIChange,
        ShakeCamera,
        ShakeUIFight,
        ShowUIWin,
        ShowUILose,
        EndGame,
        ActiveButton,
    }
}

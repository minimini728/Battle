using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class InfoManager
{
    public static readonly InfoManager instance = new InfoManager();
    
    public List<CharacterData> RemainCharacterInfos { get; set; } //남은 캐릭터
    public Dictionary<int,CharacterData> dicOrderCharacterInfo { get; set; } //캐릭터 순서
    public Dictionary<int,CharacterData> dicEnemyCharacterInfo { get; set; } //적 캐릭터
    
    private InfoManager() { }

    public void RemainCharacterInfoInit()
    {
        this.RemainCharacterInfos = new List<CharacterData>(DataManager.instance.GetCharacterDatas());

    }

    public void OrderCharacterInfoInit()
    {
        this.dicOrderCharacterInfo = new Dictionary<int, CharacterData>();

        foreach (var item in DataManager.instance.GetCharacterDatas())
        {
            dicOrderCharacterInfo.Add(item.id, item.Clone());
        }
    }

    public void EnemyCharacterInfoInit()
    {
        this.dicEnemyCharacterInfo = new Dictionary<int, CharacterData>();

        dicEnemyCharacterInfo.Add(0, DataManager.instance.dicCharacterData[201].Clone());
        dicEnemyCharacterInfo.Add(1, DataManager.instance.dicCharacterData[202].Clone());
        dicEnemyCharacterInfo.Add(2, DataManager.instance.dicCharacterData[200].Clone());

        foreach (var item in InfoManager.instance.dicEnemyCharacterInfo)
        {
            Debug.Log("(" + item.Key + ", " + item.Value.name + ")");
        }
    }
}

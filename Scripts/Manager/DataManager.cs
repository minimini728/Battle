using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Linq;

public class DataManager
{
    public static readonly DataManager instance = new DataManager();

    private DataManager() { }

    public Dictionary<int, CharacterData> dicCharacterData; //플레이어 데이터
    public Dictionary<int, ElementData> dicElementData; //속성 데이터

    public string characterDataPath = "Data/character_data"; 
    public string elementDataPath = "Data/element_data";

    //플레이어 데이터 로드
    public void LoadCharacterData()
    {
        TextAsset asset = Resources.Load<TextAsset>(characterDataPath);

        var json = asset.text;

        CharacterData[] arrCharacterDatas = JsonConvert.DeserializeObject<CharacterData[]>(json);

        this.dicCharacterData = arrCharacterDatas.ToDictionary(x => x.id);
    }

    public List<CharacterData> GetCharacterDatas()
    {
        return this.dicCharacterData.Values.ToList();
    }

    //속성 데이터 로드
    public void LoadElementData()
    {
        TextAsset asset = Resources.Load<TextAsset>(elementDataPath);

        var json = asset.text;

        ElementData[] arrElementDatas = JsonConvert.DeserializeObject<ElementData[]>(json);

        this.dicElementData = arrElementDatas.ToDictionary(x => x.id);
    }

}

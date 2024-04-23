using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public GameObject[] arrEffects;
    public Dictionary<string, GameObject> dicEffects = new Dictionary<string, GameObject>();
    private void Awake()
    {
        EffectManager.instance = this;

        for (int i = 0; i < arrEffects.Length; i++)
        {
            var effect = this.arrEffects[i];
            this.dicEffects.Add(effect.name, effect);
        }
    }

    public GameObject GetEffect(string name)
    {
        if (this.dicEffects.ContainsKey(name))
        {
            return this.dicEffects[name];
        }
        else
        {
            Debug.LogFormat("<color=red>key: {0}을 못찾음.</color>", name);
            return null;
        }
    }
}

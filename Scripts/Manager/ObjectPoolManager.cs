using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;
    public Transform trans;

    Queue<TxtDamage> poolingObjectQueue = new Queue<TxtDamage>();

    private void Awake()
    {
        Instance = this;

        Initialize(2);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private TxtDamage CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<TxtDamage>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static TxtDamage GetObject()
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();

            obj.transform.SetParent(Instance.trans);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(Instance.trans);
            return newObj;
        }
    }

    public static void ReturnObject(TxtDamage obj)
    {
        obj.transform.SetParent(Instance.transform);
        obj.gameObject.SetActive(false);
        Instance.poolingObjectQueue.Enqueue(obj);
    }

}

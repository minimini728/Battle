using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPick : MonoBehaviour
{
    public GameObject uiPickCellGo;
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject pickCellGo = Instantiate(uiPickCellGo, transform);
            UIPickCell pickCell = pickCellGo.GetComponent<UIPickCell>();

            pickCell.Init(i);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuit : MonoBehaviour
{
    public Button btnQuit;
    void Start()
    {
        this.btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }


}

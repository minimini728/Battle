using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpDown : MonoBehaviour
{
    public GameObject uiUpGo;
    public GameObject uiDownGo;

    public void ActiveArrow(int num) //캐릭터 변경 UI 스킬 버프, 너프 표시
    {
        switch (num) //0 none, 1 up, 2 down
        {
            case 0: 
                this.uiUpGo.gameObject.SetActive(false);
                this.uiDownGo.gameObject.SetActive(false);
                break;
            case 1:
                this.uiUpGo.gameObject.SetActive(true);
                this.uiDownGo.gameObject.SetActive(false);
                break;
            case 2:
                this.uiUpGo.gameObject.SetActive(false);
                this.uiDownGo.gameObject.SetActive(true);
                break;

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

public class TxtDamage : MonoBehaviour
{
    private float moveSpeed = 2; //올라가는 속도
    private float alphaSpeed = 1; //알파값 속도
    public Text txtDamage; //텍스트
    private Color alpha; //알파값

    public void Init(Vector3 pos, int damage)
    {
        alpha = txtDamage.color;
        alpha.a = 1f;
        txtDamage.color = alpha;
        this.transform.position = pos;
        this.txtDamage.text = damage.ToString();
        Invoke("DestroyTxt", 1f);

    }

    void Update()
    {   
        //위로 올라가면서 흐려지게
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        txtDamage.color = alpha;

    }
    public void DestroyTxt()
    {
        ObjectPoolManager.ReturnObject(this);
    }
}

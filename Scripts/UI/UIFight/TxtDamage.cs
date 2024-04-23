using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

public class TxtDamage : MonoBehaviour
{
    private float moveSpeed = 2; //�ö󰡴� �ӵ�
    private float alphaSpeed = 1; //���İ� �ӵ�
    public Text txtDamage; //�ؽ�Ʈ
    private Color alpha; //���İ�

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
        //���� �ö󰡸鼭 �������
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        txtDamage.color = alpha;

    }
    public void DestroyTxt()
    {
        ObjectPoolManager.ReturnObject(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POPUP : MonoBehaviour
{  
    [SerializeField] private TextMesh textMesh;
    [SerializeField] private int minSize = 1;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int speedMinim = 1;
    [SerializeField] private float timeToChangeSize = 1f;

    private GameObject player;

    public delegate void OnPopupDieHelper(GameObject gameObject);
    public event OnPopupDieHelper OnPopupDie;


    private bool isInit = false;

    public void Init(PopupData data, float damageValue)
    {
        textMesh.text = damageValue.ToString();
        textMesh.color = data.Color;
        textMesh.fontSize = data.MaxSize;
        minSize = data.MinSize;
        speedMinim = data.MinimizeSpeed;
        speed = data.Speed;
        timeToChangeSize = data.TimeToChangeSize;
        isInit = true;
    }

    private void OnEnable()
    {
        if (isInit)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(PopupMove());
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(player.transform);
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    private IEnumerator PopupMove()
    {
        while (textMesh.fontSize > minSize)
        {
            textMesh.fontSize -= speedMinim;
            yield return new WaitForSeconds(timeToChangeSize);
        }
        isInit = false;
        OnPopupDie(gameObject);
    }
}

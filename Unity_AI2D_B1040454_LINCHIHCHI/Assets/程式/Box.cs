using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour {
    #region
    public enum state
    {
        normal, NotComplete, Complete
    }
    public state _state;

    [Header("對話")]
    public string sayStar = "給我錢我給你好康的";
    public string sayNotComplete = "錢!錢!錢!錢!錢!";
    public string sayComplete = "齁齁齁!感謝你~";
    [Header("對話速度"), Range(0,10)]
    public float sayspeed = 0.5f;
    [Header("任務相關")]
    public bool complete = false;
    public int countPlayer;
    public int countFinish = 10;
    [Header("介面")]
    public GameObject objCanvas;
    public Text textsay;
    public AudioClip soundSay;
    private AudioSource aud;
    public GameObject final;

    #endregion
    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "方塊人") //如果碰到物件為方塊人
        {
            Say();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "方塊人")
        {
            SayClose();
        }
    }
           

    private void Say()
    {

        objCanvas.SetActive(true);
        StopAllCoroutines();
        if (countPlayer >= countFinish) _state = state.Complete;

        switch (_state)
        {
            case state.normal:
                StartCoroutine(ShowDialog(sayStar));
                _state++;
                break;
            case state.NotComplete:
                StartCoroutine(ShowDialog(sayNotComplete));
                break;
            case state.Complete:
                StartCoroutine(ShowDialog(sayComplete));
                final.SetActive(true);
                break;
            
        }
    }
    private IEnumerator ShowDialog(string say)
    {
        textsay.text = "";

        for (int i = 0; i < say.Length; i++)
        {
            textsay.text += say[i].ToString();
;            aud.PlayOneShot(soundSay, 5f);
            yield return new WaitForSeconds(sayspeed);
        }
    }
    private void SayClose()
    {
        StopAllCoroutines();
        objCanvas.SetActive(false);
    }
    public void PlayerGet()
    {
        countPlayer++;
    }
}



   
	


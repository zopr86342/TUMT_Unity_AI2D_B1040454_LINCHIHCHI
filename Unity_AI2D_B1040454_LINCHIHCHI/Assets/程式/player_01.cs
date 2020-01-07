using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class player_01 : MonoBehaviour {
    public int speed = 50;
    public float jump = 2.5f;
    public string cubeMan = "方塊人";
    public bool pass = false;
    public bool isGround;
    [Header("血量"),Range(0,200)]
    public float hp = 100;

    public Image hpBar;
    private float hpMax;
    public GameObject final;


    public string parRun = "跑步開關";

    public UnityEvent onEat;

    private Rigidbody2D r2d;
    private Transform tra;
    private Animator ani;

    

    // Use this for initialization
    private void Start () {
        r2d = GetComponent<Rigidbody2D>();
        tra = GetComponent<Transform>();
        ani = GetComponent<Animator>();

        hpMax =hp;
	}

    // Update is called once per frame
    private void Update () {
        if (Input.GetKeyDown(KeyCode.A)) Turn(180);
        if (Input.GetKeyDown(KeyCode.D)) Turn(0);
    }
    private void FixedUpdate()
    {
        Walk();
        Jump();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
        Debug.Log("碰到東西了" + collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "coin")
        {
            Destroy(collision.gameObject);
            onEat.Invoke();
        }
    }
    private void Walk()
    {
        ani.SetBool(parRun, Input.GetAxis("Horizontal") != 0);//按下左右鍵時布林值不等於0
        r2d.AddForce(new Vector2(speed * Input.GetAxis("Horizontal"), 0));
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isGround = false;
            r2d.AddForce(new Vector2(0, jump));
        }
            
    }
    /// <summary>
    /// 轉彎
    /// </summary>
    /// <param name="direction">左轉為180,右轉為0</param>
    private void Turn(int direction)
    {
        tra.eulerAngles = new Vector3(0, direction, 0);
    }
    public void Damage(float damage)
    {
        hp -= damage;
        hpBar.fillAmount = hp / hpMax;
        if (hp <= 0) final.SetActive(true);
    }
}

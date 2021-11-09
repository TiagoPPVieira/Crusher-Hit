using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SecretBonus : MonoBehaviour
{
    public int bonus1 = 0;
    public int bonus2 = 0;
    public int bonus3 = 0;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;
    public CapsuleCollider2D capsuleCollider2D;
    public GameObject ballSecretCanvas_1;
    public GameObject ballSecretCanvas_2;
    public GameObject ballSecretCanvas_3;
    public Button BtTitle;

    private bool noMoveVerify = true;
    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("dataLevels[bonus_" + i + "].Balls") == 1)
            {
                switch (i)
                {
                    case 1:
                        boxCollider2D.enabled = false;
                        break;
                    case 2:
                        circleCollider2D.enabled = false;
                        break;
                }
                if (boxCollider2D.enabled == false && circleCollider2D.enabled == false)
                {
                    capsuleCollider2D.enabled = true;
                }

            }
            if (PlayerPrefs.GetInt("dataLevels[bonus_1].Balls") == 1)
            {
                ballSecretCanvas_1.SetActive(true);
            }
            if (PlayerPrefs.GetInt("dataLevels[bonus_2].Balls") == 1)
            {
                ballSecretCanvas_2.SetActive(true);
            }
            if (PlayerPrefs.GetInt("dataLevels[bonus_3].Balls") == 1)
            {
                ballSecretCanvas_3.SetActive(true);
            }
        }
        if (ballSecretCanvas_1.activeSelf == true && ballSecretCanvas_2.activeSelf == true && ballSecretCanvas_3.activeSelf == true)
        {
            BtTitle.enabled = false;
        }
    }

    void Update()
    {
        if (noMoveVerify)
        {
            BallControl.instance.noMove = true;
            noMoveVerify = false;
        }
        
        if (bonus1 == 6)
        {
            if (PlayerPrefs.GetInt("dataLevels[bonus_1].Balls") == 1)
            {

            }
            else
            {
                PlayerPrefs.SetInt("dataLevels[bonus_1].Balls", 1);
                ballSecretCanvas_1.SetActive(true);
            }
            ZerarParametros();
            bonus1 = 0;
        }
        if (bonus2 == 3)
        {
            if (PlayerPrefs.GetInt("dataLevels[bonus_2].Balls") == 1)
            {
                
            }
            else
            {
                PlayerPrefs.SetInt("dataLevels[bonus_2].Balls", 1);
                ballSecretCanvas_2.SetActive(true);
            }
            ZerarParametros();
            bonus2 = 0;
        }
        if(bonus3 == 3)
        {
            if (PlayerPrefs.GetInt("dataLevels[bonus_3].Balls") == 1)
            {
                
            }
            else
            {
                PlayerPrefs.SetInt("dataLevels[bonus_3].Balls", 1);
                ballSecretCanvas_3.SetActive(true);
            }
            ZerarParametros();
            bonus3 = 0;
        }
    }

    private void ZerarParametros()
    {
        BallControl.instance.noMove = true;
        BallControl.instance.numOfMoves = 4;
        BallControl.instance.transform.position = new Vector3(0f, 0f, 0f);
    }

    public void BonusLiberate()
    {
        StartCoroutine("WaitingCanvas");
    }
    IEnumerator WaitingCanvas()
    {
        yield return new WaitForSeconds(1f);
        BallControl.instance.noMove = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            if(PlayerPrefs.GetInt("dataLevels[bonus_1].Balls") == 1)
            {
                bonus3++;
            }
            else
            {
                bonus1++;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            bonus2++;
        }
    }
}

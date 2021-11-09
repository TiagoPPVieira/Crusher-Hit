using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasAnimControl : MonoBehaviour
{
    public Animator anim;
    public int level;
    public int balls;
    public int ballsInData;
    public int ballsInThisLevel;

    public GameObject TotalBallsAcum;
    public GameObject CanvasBonus;
    public GameObject[] ballCanvas;
    public Text textMissionComplete;

    void Start()
    {
        anim = GameObject.Find("Canvas").GetComponent<Animator>();
        level = SceneManager.GetActiveScene().buildIndex - 1;
        balls = BallControl.instance.numOfMoves;

        for(int level = 1; level < 120; level++)
        {
            if (PlayerPrefs.GetInt("dataLevels[" + level + "].levelComplete") == 1)
            {
                ballsInData += PlayerPrefs.GetInt("dataLevels[" + level + "].Balls");
            }
        }
        ballsInData += PlayerPrefs.GetInt("dataLevels[bonus_1].Balls");
        ballsInData += PlayerPrefs.GetInt("dataLevels[bonus_2].Balls");
        ballsInData += PlayerPrefs.GetInt("dataLevels[bonus_3].Balls");

        ballsInThisLevel = PlayerPrefs.GetInt("dataLevels[" + (SceneManager.GetActiveScene().buildIndex - 1) + "].Balls");
        
        StartCoroutine("AcumEachBall");
    }
    

IEnumerator AcumEachBall()
{
        anim.SetInteger("Transition", 2);
        textMissionComplete.text = "x" + ballsInData.ToString();
        yield return new WaitForSeconds(1f);

        if (ballsInThisLevel < balls)
        {
            if (ballsInThisLevel == 1)
            {
                ballCanvas[1].SetActive(false);
            }
            for (int i = 0; i < (balls-ballsInThisLevel); i++)
            {
            TotalBallsAcum.SetActive(true);
            
            textMissionComplete.text = "x" + (ballsInData + (i+1)).ToString();
                CanvasBonus.SetActive(true);
                for (int j=0; j<20; j++)
                {
                    ballCanvas[i].transform.localScale /= 1.1f;
                    CanvasBonus.transform.rotation = Quaternion.Euler(0f, 0f, 10f*j);
                    yield return new WaitForSeconds(0.06f);
                    TotalBallsAcum.gameObject.transform.localScale *= 1.025f;
                }
                CanvasBonus.SetActive(false);
                TotalBallsAcum.gameObject.transform.localScale /= 1.62f;
            
            ballCanvas[i].SetActive(false);
            
            yield return new WaitForSeconds(0.2f);
            }
        }
    }
}

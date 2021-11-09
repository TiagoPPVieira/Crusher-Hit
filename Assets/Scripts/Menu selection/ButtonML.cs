using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonML : MonoBehaviour
{
    public static ButtonML instance;

    public GameObject[] buttons;
    public GameObject transitionRightLocked;

    public SpriteRenderer sptRenderer;
    public Sprite level_win0Balls, level_win1Balls, level_win2Balls;

    public GameObject[] textBalls;
    public Text textBallsToNeed;

    public int totalBalls;
    public int BallsToUnlocked;
    public int page = 1;
    public bool animStartBalls = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        LoadLevelsUnLocked();

        StartCoroutine("AcumEachBall");
    }
    IEnumerator AcumEachBall()
    {
        for (int i = 0; i < totalBalls; i++)
        {
            textBallsToNeed.text = i.ToString() + "/" + BallsToUnlocked.ToString() + "X";
            textBallsToNeed.gameObject.transform.localScale *= 1.05f;
            yield return new WaitForSeconds(0.05f - i/200f);
            textBallsToNeed.gameObject.transform.localScale /= 1.05f;
            yield return new WaitForSeconds(0.05f - i / 200f);
        }
        animStartBalls = true;
    }

    private void Update()
    {
        switch (page)
        {
            case 1:
                BallsToUnlocked = 8;
                break;
            case 2:
                BallsToUnlocked = 16;
                break;
            case 3:
                BallsToUnlocked = 25;
                break;
            case 4:
                BallsToUnlocked = 32;
                break;
            case 5:
                BallsToUnlocked = 40;
                break;
            case 6:
                BallsToUnlocked = 50;
                break;
            case 7:
                BallsToUnlocked = 62;
                break;
            case 8:
                BallsToUnlocked = 120;
                break;
            default:
                Debug.Log("Page não encontrada!!!");
                break;
        }
        if (animStartBalls)
        {
            textBallsToNeed.text = totalBalls.ToString() + "/" + BallsToUnlocked.ToString() + "X";
        }
        
        if (totalBalls >= BallsToUnlocked || PlayerPrefs.GetInt("dataLevels[" + (page*15) + "].levelComplete") == 1)
        {
            textBallsToNeed.color = Color.white;
            transitionRightLocked.SetActive(false);
        }
        else
        {
            textBallsToNeed.color = Color.red;
            transitionRightLocked.SetActive(true);
        }
    }

    void LoadLevelsUnLocked()
    {
        for (int level = 0; level < buttons.Length; level++)
        {
            if (PlayerPrefs.GetInt("dataLevels[" + (level + 1) + "].levelComplete") == 1)
            {
                buttons[(level + 1)].gameObject.SetActive(false);
            }
        }

        for (int level = 0; level < buttons.Length; level++)
        {
            PlayerPrefs.GetInt("dataLevels[" + (level+1) + "].Balls");
            sptRenderer = textBalls[level].GetComponent<SpriteRenderer>();
            switch (PlayerPrefs.GetInt("dataLevels[" + (level + 1) + "].Balls"))
            {
                case 0:
                    sptRenderer.sprite =  level_win0Balls;
                    break;
                case 1:
                    sptRenderer.sprite = level_win1Balls;
                    break;
                case 2:
                    sptRenderer.sprite = level_win2Balls;
                    break;
            }

            PlayerPrefs.GetInt("dataLevels[" + level + "].levelComplete");
            
            if (PlayerPrefs.GetInt("dataLevels[" + (level + 1) + "].levelComplete") == 1)
            {
                totalBalls += PlayerPrefs.GetInt("dataLevels[" + (level + 1) + "].Balls");
            }
        }
        totalBalls += PlayerPrefs.GetInt("dataLevels[bonus_1].Balls");
        totalBalls += PlayerPrefs.GetInt("dataLevels[bonus_2].Balls");
        totalBalls += PlayerPrefs.GetInt("dataLevels[bonus_3].Balls");
    }
}


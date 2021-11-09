using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject tutorialObj;
    public GameObject NextLevelBt;

    public bool isPaused = false;
    public bool isComplete = false;

    public int objCount; 

    public Text movesText;
    public GameObject Balls_1;
    public GameObject Balls_2;
    public GameObject Balls_3;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Tutorial.instance.haveTutorial)
        {
            tutorialObj.SetActive(true);
            BallControl.instance.noMove = true;
        }
        else
        {
            tutorialObj.SetActive(false);
            BallControl.instance.noMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(objCount <= 0)
        {
            //level concluido
            if (!isComplete)
            {
                PlayerPrefs.SetInt("next level account to ads", PlayerPrefs.GetInt("next level account to ads") + 1);
                if (PlayerPrefs.GetInt("next level account to ads") % 7 == 6)
                {
                    GoogleAds.instance.DisplayAdsNextLevel();
                    BallControl.instance.numOfMoves--;
                    BallControl.instance.noMove = true;
                }
            }
            //PlayerPrefs.SetInt("next level account to ads", 0);
            isComplete = true;
            PlayerPrefs.SetInt("dataLevels[" + (SceneManager.GetActiveScene().buildIndex - 1) + "].levelComplete", 1);
            NextLevelBt.SetActive(true);
            BallControl.instance.noMove = true;
        }
    }

    

    public void UpdateMoves(int balls)
    {
        if (!BallControl.instance.noMove)
        {
            switch (balls)
            {
                case 3:
                    Balls_1.SetActive(true);
                    Balls_2.SetActive(true);
                    Balls_3.SetActive(true);
                    break;
                case 2:
                    Balls_1.SetActive(true);
                    Balls_2.SetActive(true);
                    Balls_3.SetActive(false);
                    break;
                case 1:
                    Balls_1.SetActive(true);
                    Balls_2.SetActive(false);
                    Balls_3.SetActive(false);
                    break;
                case 0:
                    Balls_1.SetActive(false);
                    Balls_2.SetActive(false);
                    Balls_3.SetActive(false);
                    break;

                default:
                    break;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class DataLevels : MonoBehaviour
{
    public int level;

    private void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex - 1;
    }
    private void Update()
    {
        if (GameController.instance.isComplete)
        {
            if(BallControl.instance.numOfMoves > PlayerPrefs.GetInt("dataLevels[" + level + "].Balls"))
            {
                PlayerPrefs.SetInt("dataLevels[" + level + "].Balls", BallControl.instance.numOfMoves);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextBGLevel : MonoBehaviour
{
    public Text text;

    void Start()
    {
        text = GetComponent<Text>();

        text.text = "LVL " + (SceneManager.GetActiveScene().buildIndex - 1);
    }
}

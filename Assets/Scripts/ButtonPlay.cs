
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlay : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
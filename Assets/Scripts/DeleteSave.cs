using UnityEngine;

public class DeleteSave : MonoBehaviour
{
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
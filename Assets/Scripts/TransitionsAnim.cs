using UnityEngine;
using System.Collections;

public class TransitionsAnim : MonoBehaviour
{
    public Animator animator;
    public float smoothMotion;
    public float velocity;
    public bool p = true;
    public GameObject[] buttonsTransition;
    public GameObject[] buttonsTransitionLocked;
    public GameObject rightButton;
    public GameObject leftButton;
    
    public void ClickToRight()
    {
        Debug.Log("direita" + ButtonML.instance.page);
        ButtonML.instance.page++;
        
        for (int i = 0; i < 9; i++)
        {
            if(ButtonML.instance.page - 1 == i)
            {
                buttonsTransition[ButtonML.instance.page - 1].transform.localPosition = new Vector3(0, 0, 0);
                buttonsTransitionLocked[ButtonML.instance.page - 1].transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                buttonsTransition[i].transform.localPosition = new Vector3(1000, 0, 0);
                buttonsTransitionLocked[i].transform.localPosition = new Vector3(1000, 0, 0);
            }
        }
    }

    public void ClickToLeft()
    {
        Debug.Log("esquerda");
        ButtonML.instance.page--;
        for (int i = 0; i < 9; i++)
        {
            if (ButtonML.instance.page - 1 == i)
            {
                buttonsTransition[ButtonML.instance.page - 1].transform.localPosition = new Vector3(0, 0, 0);
                buttonsTransitionLocked[ButtonML.instance.page - 1].transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                buttonsTransition[i].transform.localPosition = new Vector3(1000, 0, 0);
                buttonsTransitionLocked[i].transform.localPosition = new Vector3(1000, 0, 0);
            }
        }
    }

    private void Update()
    {
        CheckButtons();
        if(ButtonML.instance.transitionRightLocked.activeSelf == false && p)
        {
            ClickToRight();
            StartCoroutine(WaitingSideScroll());
        }
    }

    void CheckButtons()
    {
        if (ButtonML.instance.page == 1)
        {
            leftButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
        }
    }
    IEnumerator WaitingSideScroll()
    {
        yield return new WaitForSeconds(1f);
        p = false;
    }
}
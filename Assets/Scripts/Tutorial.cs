using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    public Animator anim;
    public bool haveTutorial = false;
    public static Tutorial instance;
    public GameObject OkBt;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (haveTutorial)
        {
            StartCoroutine(WaitingTransition());
        }
    }
    private void Update()
    {
        //Debug.Log("nomove: " + BallControl.instance.noMove);
        
        if (haveTutorial && OkBt.gameObject.activeInHierarchy)
        {
            BallControl.instance.noMove = true;
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("NextAnimTutorial");
                StartCoroutine(WaitingTransition());
            }
        }
        if (!haveTutorial)
        {
            StartCoroutine(WaitingToGame());
        }
    }
    IEnumerator WaitingTransition()
    {
        OkBt.SetActive(false); 
        //Debug.Log("okbt 1: " + OkBt.gameObject.activeInHierarchy);
        yield return new WaitForSeconds(2f);
        if (haveTutorial)
        {
            OkBt.SetActive(true);
        }
        //Debug.Log("okbt 2: " + OkBt.gameObject.activeInHierarchy);
    }
    IEnumerator WaitingToGame()
    {
        yield return new WaitForSeconds(1f);
        BallControl.instance.noMove = false;
    }
}

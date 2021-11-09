using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static Portal instance;
    
    public GameObject anotherPortal;

    private void Awake()
    {
        instance = this;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && Portal.instance.gameObject.layer == 10)
        {
            BallControl.instance.transform.position = anotherPortal.transform.position;
            StartCoroutine(Teleport());
        }
    }
    IEnumerator Teleport()
    {
        Portal.instance.gameObject.layer = 0;
        yield return new WaitForSeconds(0.2f);
        Portal.instance.gameObject.layer = 10;
    }
}

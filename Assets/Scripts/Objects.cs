using System.Collections;
using UnityEngine;

public class Objects : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            StartCoroutine("HitObject");
        }
    }
    IEnumerator HitObject()
    {
        anim.SetBool("HitObject", true);
        yield return new WaitForSeconds(0.5f);
        GameController.instance.objCount -= 1;
    }
}
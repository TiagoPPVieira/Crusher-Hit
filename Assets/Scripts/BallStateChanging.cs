using System.Collections;
using UnityEngine;

public class BallStateChanging : MonoBehaviour
{
    public int ballState;
    public SpriteRenderer spriteRenderer;
    public bool timer;
    private bool p;
    private void Start()
    {
        p = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (ballState)
        {
            case 1:
                spriteRenderer.color = Color.magenta;
                break;
            case 2:
                spriteRenderer.color = Color.green;
                break;
            case 3:
                spriteRenderer.color = Color.red;
                break;
            case 4:
                spriteRenderer.color = Color.blue;
                break;
            case 5:
                spriteRenderer.color = Color.yellow;
                break;
        }
    }
    private void Update()
    {
        if (timer)
        {
            StartCoroutine(TimerState());
        }
    }
    IEnumerator TimerState()
    {
        for(int i = 1; i <6; i++)
        {
            if (p)
            {
                p = false;
                ballState = i;
                switch (ballState)
                {
                    case 1:
                        spriteRenderer.color = Color.magenta;
                        break;
                    case 2:
                        spriteRenderer.color = Color.green;
                        break;
                    case 3:
                        spriteRenderer.color = Color.red;
                        break;
                    case 4:
                        spriteRenderer.color = Color.blue;
                        break;
                    case 5:
                        spriteRenderer.color = Color.yellow;
                        break;
                }
                yield return new WaitForSeconds(1f);
                p = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            switch (ballState)
            {
                case 1:
                    BallControl.instance.stateBall = 1;
                    break;
                case 2:
                    BallControl.instance.stateBall = 2;
                    break;
                case 3:
                    BallControl.instance.stateBall = 3;
                    break;
                case 4:
                    BallControl.instance.stateBall = 4;
                    break;
                case 5:
                    BallControl.instance.stateBall = 5;
                    break;
            }
        }
    }
}

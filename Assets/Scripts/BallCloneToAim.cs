using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCloneToAim : MonoBehaviour
{
    public static BallCloneToAim instance;
    public GameObject[] GOBallCloneToAim;
    public Rigidbody2D[] rig;
    public float limitOfVelocity;
    public bool verifBallAim = false;
    public bool p = true;
    private void Start()
    {
        instance = this;
    }
    private void FixedUpdate()
    {
        Physics2D.IgnoreLayerCollision(8, 13);
        Physics2D.IgnoreLayerCollision(9, 13);
        Physics2D.IgnoreLayerCollision(13, 13);

        if (verifBallAim)
        {
            Time.timeScale = 0.2f;
            for (int i = 0; i < 4; i++)
            {
                GOBallCloneToAim[i].SetActive(true);
            }
            StartCoroutine(AnimationBallPlaying());
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                GOBallCloneToAim[i].SetActive(false);
                GOBallCloneToAim[i].transform.position = BallControl.instance.transform.position;
            }
        }
    }
    IEnumerator AnimationBallPlaying()
    {
        Vector3 dragExitPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragExitPos.z = 0f;

        // Setar o fim da animação do dragging

        BallControl.instance.force = dragExitPos - BallControl.instance.gameObject.transform.position;
                
        Vector3 clampedForce = Vector3.ClampMagnitude(BallControl.instance.force, BallControl.instance.maxDrag) * BallControl.instance.power;
        
        if (p)
        {
            p = false;
            Vector3 checkAngle = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            
            for (int i = 0; i < 4; i++)
            {
                //Ball State
                rig[i].gravityScale = BallControl.instance.rig.gravityScale;
                rig[i].mass = BallControl.instance.rig.mass;
                rig[i].sharedMaterial = BallControl.instance.rig.sharedMaterial;

                //Setando a posição
                GOBallCloneToAim[i].transform.position = BallControl.instance.transform.position;
                rig[i].velocity = BallControl.instance.rig.velocity;
                rig[i].AddForce(clampedForce, ForceMode2D.Impulse);

                yield return new WaitForSeconds(0.1f);

                //Mudando o angulo da animação
                if (checkAngle != Camera.main.ScreenToWorldPoint(Input.mousePosition))
                {
                    //Inicializando as posições da animação
                    GOBallCloneToAim[0].transform.position = BallControl.instance.transform.position;
                    GOBallCloneToAim[1].transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - BallControl.instance.transform.position;
                    GOBallCloneToAim[2].transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    GOBallCloneToAim[3].transform.position = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - BallControl.instance.transform.position);

                    i = 4;
                }
            }
            p = true;
        }
    }
}

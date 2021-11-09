using System;
using System.Collections;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public static BallControl instance;
    public float power;
    public float maxDrag;
    public Vector3 force;
    public Rigidbody2D rig;
    public int stateBall = 1;
    public int numOfMoves;
    public bool noMove;
    //public GameObject[] draggingBallsBObj;
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;

    public float limitOfVelocity;

    Vector3 dragStartPos;

    public PhysicsMaterial2D materialBallStandart;
    public PhysicsMaterial2D materialBallNoGrip;
    public PhysicsMaterial2D materialBallBounciness;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        maxDrag = 5;
        power = 4;
    }

    private void Update()
    {
        chooseStateBall();
        GameController.instance.UpdateMoves(numOfMoves);
        if (numOfMoves > 0)
        {
            if (!noMove)
            {
                GetInput();
            }            
        }
        else
        {
            if (GameController.instance.isComplete)
            {
                GoogleAds.instance.ExtraBallBt.SetActive(false);
            }
            else
            {
                GoogleAds.instance.ExtraBallBt.SetActive(true);
            }
            if (rig.velocity.magnitude < 1 && !GameController.instance.isComplete)
            {
                //Debug.Log("Tente outra vez!");
            }
        }
    }

    void chooseStateBall()
    {
        switch (stateBall)
        {
            case 1: //BallStandart
                spriteRenderer.color = Color.magenta;
                rig.gravityScale = 1;
                rig.mass = 0.3f;
                rig.sharedMaterial = materialBallStandart;
                break;
            case 2: //BallNoGravity
                spriteRenderer.color = Color.green;
                rig.gravityScale = 0f;
                rig.mass = 0.3f;
                rig.sharedMaterial = materialBallNoGrip;
                break;
            case 3: //BallBounciness
                spriteRenderer.color = Color.red;
                rig.gravityScale = 1;
                rig.mass = 0.3f;
                rig.sharedMaterial = materialBallBounciness;
                break;
            case 4: //BallHeavy
                spriteRenderer.color = Color.blue;
                rig.gravityScale = 1;
                rig.mass = 2f;
                rig.sharedMaterial = materialBallStandart;
                break;
            case 5: //BallAntyGravity
                spriteRenderer.color = Color.yellow;
                rig.gravityScale = -0.5f;
                rig.mass = 0.3f;
                rig.sharedMaterial = materialBallStandart;
                break;
        }
    }

    void GetInput()
    {
        dragStartPos = instance.transform.position;
        dragStartPos.z = 0f;
        if (Input.GetMouseButtonDown(0))
        {
            DragStart();
        }
        if (Input.GetMouseButton(0))
        {
            Dragging();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 1f;
            DragExit();
            numOfMoves -= 1;
        }
    }

    void DragStart() {
        for (int i = 0; i < 8; i++)
        {
            if (noMove)
            {
                //draggingBallsBObj[i].gameObject.SetActive(false);
            }
            else
            {
                //draggingBallsBObj[i].gameObject.SetActive(true);
            }
        }
        // Setar o início da animação do dragging
        //Inicializando as posições da animação
        BallCloneToAim.instance.GOBallCloneToAim[0].transform.position = BallControl.instance.transform.position;
        BallCloneToAim.instance.GOBallCloneToAim[1].transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - BallControl.instance.transform.position;
        BallCloneToAim.instance.GOBallCloneToAim[2].transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        BallCloneToAim.instance.GOBallCloneToAim[3].transform.position = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - BallControl.instance.transform.position);

    }
    void Dragging() {
        
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        draggingPos.z = 0f;
        /*
        draggingBallsBObj[0].gameObject.transform.position = (dragStartPos);
        draggingBallsBObj[1].gameObject.transform.position = ((draggingPos + dragStartPos) / 2);
        draggingBallsBObj[2].gameObject.transform.position = (draggingPos);
        draggingBallsBObj[3].gameObject.transform.position = (draggingPos + (draggingPos - dragStartPos) / 2);
        draggingBallsBObj[4].gameObject.transform.position = (draggingPos + (draggingPos - dragStartPos));
        draggingBallsBObj[5].gameObject.transform.position = (draggingPos + (draggingPos - dragStartPos)*1.5f);
        draggingBallsBObj[6].gameObject.transform.position = ((draggingBallsBObj[1].gameObject.transform.position + dragStartPos) / 2);
        draggingBallsBObj[7].gameObject.transform.position = ((draggingPos + draggingBallsBObj[1].gameObject.transform.position) / 2);
        */
        BallCloneToAim.instance.verifBallAim = true;
    }

    void DragExit()
    {
        BallCloneToAim.instance.verifBallAim = false;

        Vector3 dragExitPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragExitPos.z = 0f;

        for (int i = 0; i < 8; i++)
        {
            //draggingBallsBObj[i].gameObject.SetActive(false);
        }
        // Setar o fim da animação do dragging

        force = dragExitPos - dragStartPos;
        
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        rig.AddForce(clampedForce, ForceMode2D.Impulse);

        limitOfVelocity = GetComponent<Rigidbody2D>().velocity.magnitude;

        if (limitOfVelocity > 10)
        {            
            rig.velocity.Set(1f, 1f);
        }
    }/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            StartCoroutine("audioHitObject");
        }
    }
    
    IEnumerator audioHitObject()
    {
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        audioSource.Stop();
    }

    IEnumerator SlowMotion()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
    }*/
}

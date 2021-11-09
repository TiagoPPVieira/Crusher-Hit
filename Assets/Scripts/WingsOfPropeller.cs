using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsOfPropeller : MonoBehaviour
{
    public GameObject[] wingsOfPropeller;
    public bool p = true;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            wingsOfPropeller[i].transform.localScale = new Vector3(Random.Range(-0.3f, 0.3f), 0.3f, 1f);
            wingsOfPropeller[i].transform.localPosition = new Vector3(Random.Range(0f, 2.5f), Random.Range(0f, 6f), 0f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (p)
        {
            p = false;
            for (int i = 0; i < 20; i++)
            {
                wingsOfPropeller[i].transform.localPosition += new Vector3( 0f, Time.deltaTime * speed, 0f);
                if(wingsOfPropeller[i].transform.localPosition.y > 7)
                {
                    wingsOfPropeller[i].transform.localPosition = new Vector3(Random.Range(0f, 2.5f), 0f, 0f);
                }
            }
            p = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < 20; i++)
        { 
            if(wingsOfPropeller[i].gameObject.layer == 8)
            {
                Debug.Log("Colidiu com o vento!");
            }
        }
    }
}

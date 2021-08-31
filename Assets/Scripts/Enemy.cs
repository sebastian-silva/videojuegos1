using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    [SerializeField] bool direction;
    [SerializeField] int hearths;
    [SerializeField] GameObject corazon;
    [SerializeField] Sprite corazonlleno;
    [SerializeField] Sprite corazonMedioLLeno;
    [SerializeField] Sprite corazonvacio;
    private float maxx;
    private float minx;
    private GameObject a;
    private GameObject[] corazones;
    private int count = 0;
    private float vida ;

    void Start()
    {
        vida = (float)hearths;
        corazones = new GameObject[hearths];

        Vector2 esqSupDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        maxx = esqSupDer.x;

        Vector2 esqInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minx = esqInfIzq.x;
        float mul = (float)hearths / 10;
        Debug.Log(mul);

        for (int i = 0; i < this.hearths; i++)
        {
            float n = 0.3f * i;
            corazones[i] = Instantiate(corazon, transform.position - new Vector3(-n + (mul), -0.7f, 0), transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            corazones[i].transform.SetParent(this.gameObject.transform);
        }
        this.movimiento();
    }

    private void movimiento()
    {

        if (direction)
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        else
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));

        if (transform.position.x >= maxx)
            direction = false;
        else if (transform.position.x <= minx)
            direction = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        float dano = 0;
        if (other.gameObject.CompareTag("bala"))
        {
            corazones[count].GetComponent<SpriteRenderer>().sprite = corazonvacio;
            count++;
            if (count == corazones.Length)
            {
                Destroy(this.gameObject);
            }
            dano = 1;
        }
        else if (other.gameObject.CompareTag("metra"))
        {
            if (corazones[count].GetComponent<SpriteRenderer>().sprite == corazonlleno)
            {
                corazones[count].GetComponent<SpriteRenderer>().sprite = corazonMedioLLeno;
            }
            else if (corazones[count].GetComponent<SpriteRenderer>().sprite == corazonMedioLLeno)
            {
                corazones[count].GetComponent<SpriteRenderer>().sprite = corazonvacio;
                count++;

            }
            
            if (count == corazones.Length)
            {
                Destroy(this.gameObject);
            }
        }


    }
}
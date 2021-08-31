using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {
    [SerializeField] int speed;
    [SerializeField] GameObject bala;
    [SerializeField] GameObject balaMetra;
    [SerializeField] float nextFire;
    [SerializeField] float metraFire;
    [SerializeField] Sprite normal;
    [SerializeField] Sprite metra;

    float minx, maxx, miny, maxy, tamx, tamy,canFire;

    bool estado = true;

    // Start is called before the first frame update
    void Start() {
        tamx = (GetComponent<SpriteRenderer>()).bounds.size.x;
        tamy = (GetComponent<SpriteRenderer>()).bounds.size.y;

        Vector2 esqSupDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        maxx = esqSupDer.x - (tamx / 2);
        maxy = esqSupDer.y - (tamy / 2);

        Vector2 esqInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minx = esqInfIzq.x + (tamx / 2);
        miny = esqInfIzq.y + 6 + (tamy / 2);
        // Debug.Log(esqSupDer.ToString());
    }

    // Update is called once per frame
    void Update() 
    {
        Movement();
        Fire();
        ChangeFire();
    }

    void Movement() 
    {
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(movH * Time.deltaTime * speed, movV * Time.deltaTime * speed));

        float newx = Mathf.Clamp(transform.position.x, minx, maxx);
        float newy = Mathf.Clamp(transform.position.y, miny, maxy);
        transform.position = new Vector2(newx, newy);

    }

    void Fire() 
    {
        if (estado)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= canFire)
            {
                Instantiate(bala, transform.position - new Vector3(0, tamy / 2, 0), transform.rotation);
                canFire = Time.time + nextFire;
            }
        }
        else {
            if (Input.GetKey(KeyCode.Space) && Time.time >= canFire) 
            {
                Instantiate(balaMetra, transform.position - new Vector3(0, tamy / 2, 0), transform.rotation);
                canFire = Time.time + metraFire;
            }
            
        }

    }

    void ChangeFire() 
    {   
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (estado)
            {
                estado = false;
                print(estado);
                this.gameObject.GetComponent<SpriteRenderer>().sprite = metra;
            }
            else 
            {
                estado = true;
                this.gameObject.GetComponent<SpriteRenderer>().sprite = normal;
                
            }
        }
    }

}

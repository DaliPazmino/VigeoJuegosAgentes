using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerLogic1 : MonoBehaviour
{
    public HealthBarLogic healthBarLogic;
    public float moveVelocity = 10.0f;
    public float rotationVelocity = 100.0f;
    private Animator anim;
    public float x, y;

    public Rigidbody rb;
    public float fuerzaDeSalto = 8f;
    public bool puedoSaltar;
    public int score = 0;

    public Transform Objetivo;
    public int minDistanceAtack = 10;

    public bool estoyAtacando;
    public float impulsoDeGolpe = 0f;

    // Start is called before the first frame update
    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>(); 
    }

    private void FixedUpdate()
    {
         transform.Rotate(0, x * Time.deltaTime * rotationVelocity, 0);
         transform.Translate(0, 0, y * Time.deltaTime * moveVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(1) && puedoSaltar && !estoyAtacando)
        {
            anim.SetTrigger("Golpeo");
            estoyAtacando = true;
            Atacar(); // Llama a la función de ataque que definiste
        }

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        if (puedoSaltar)
        {
            if (!estoyAtacando)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetBool("jump", true);
                    rb.AddForce(new Vector3(0, fuerzaDeSalto, 0), ForceMode.Impulse);
                }
            }
            anim.SetBool("touchground", true);
        }
        else
        {
            EstoyCayendo();
        }
    }

    public void EstoyCayendo()
    {
        anim.SetBool("touchground", false);
        anim.SetBool("jump", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            other.gameObject.SetActive(false);
            score += 100;
            if(score >= 500)
            {
                SceneManager.LoadScene(0);
            }
        }

        if (other.gameObject.tag == "areaImpacto")
        {
            healthBarLogic.vidaActual -= 5;
        }
    }

    private void Atacar()
    {
        if (Vector3.Distance(transform.position, Objetivo.position) < minDistanceAtack)
        {
            Debug.Log("Atacando Enemigo aaaa");
            GlobalVariables.miVariableGlobal = true;
        }
    }

    public void DejeDeGolpear()
    {
        estoyAtacando = false;
    }


}

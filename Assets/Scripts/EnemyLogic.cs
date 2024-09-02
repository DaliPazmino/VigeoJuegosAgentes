using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public HealthBarLogic healthBarLogic;
    public float velocidad = 2.5f;
    public float rotationVelocity = 100.0f;
    private Animator anim;
    public float x, y;
    // Variables de control de movimiento aleatorio
    public float cronometro;

    public float descanso = 4;
    public int minDistance = 10;
    public int minDistanceAtack = 10;

    public Transform Enemigo;
    private int colisionoCon = 0;

    public bool estoyAtacando;
    public bool estoyBloqueando;
    public float impulsoDeGolpe = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Player")
            colisionoCon = 1;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    [Task]
    private void JugadorEnRango()
    {
        if (Vector3.Distance(transform.position, Enemigo.position) < minDistance)
        {
            Debug.Log("Jugador en Rango");
            Task.current.Succeed();
        }
        else if (GlobalVariables.miVariableGlobal)
        {
            GlobalVariables.miVariableGlobal = false;
            Task.current.Fail();
        }
    }

    [Task]
    private void CalcularRuta()
    {
        if (colisionoCon == 1)
        {
            Debug.Log("Calculando Ruta");
            Task.current.Succeed();
            colisionoCon = 0;
        }
        else if (Vector3.Distance(transform.position, Enemigo.position) < minDistance)
        {
            // Calculamos la dirección hacia el enemigo
            Vector3 direccion = (Enemigo.position - transform.position).normalized;

            // Calculamos la velocidad de movimiento
            Vector3 velocidadMovimiento = direccion * velocidad;

            float x = Mathf.Clamp(velocidadMovimiento.z / velocidad, -1f, 1f);
            float y = Mathf.Clamp(velocidadMovimiento.x / velocidad, -1f, 1f);
            // Actualizamos las variables en el Animator
            anim.SetFloat("VeloX", x);
            anim.SetFloat("VeloY", y);

            transform.LookAt(Enemigo);

            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        }
    }

    [Task]
    private void Atacar()
    {
        if (!estoyAtacando)
        {
            anim.SetTrigger("Golpeo");
            estoyAtacando = true;
        }
        if (GlobalVariables.miVariableGlobal)
        {
            GlobalVariables.miVariableGlobal = false;
            Task.current.Fail();
        }
        if (cronometro > descanso)
        {
            Debug.Log("Atacando");
            cronometro = 0;
            Task.current.Succeed();
        }
        cronometro += 1 * Time.deltaTime;
    }

    [Task]
    private void JugadorAtaca()
    {
        if (Vector3.Distance(transform.position, Enemigo.position) < minDistanceAtack)
        {
            Debug.Log("Me esta atacando");
            cronometro = 0;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    private void Cubrirse()
    {
        if (!estoyBloqueando)
        {
            anim.SetTrigger("Bloqueo");
        }
        if (Vector3.Distance(transform.position, Enemigo.position) > minDistance || cronometro > descanso)
        {

            Debug.Log("Bloqueando");
            estoyBloqueando = true;
            cronometro = 0;
            Task.current.Succeed();
        }
        cronometro += 1 * Time.deltaTime;
    }

    public void DejeDeGolpear()
    {
        estoyAtacando = false;
    }

    public void DejeDeBloquear()
    {
        estoyBloqueando = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "areaImpacto")
        {
            healthBarLogic.vidaActual -= 5;
        }
    }
}

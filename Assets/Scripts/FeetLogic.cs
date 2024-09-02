using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetLogic : MonoBehaviour
{
    public PlayerLogic1 PlayerLogic1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        PlayerLogic1.puedoSaltar = true;
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerLogic1.puedoSaltar = false;
    }
}

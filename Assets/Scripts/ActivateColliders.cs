using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateColliders : MonoBehaviour
{
    public BoxCollider pu�oBoxCol;
    // Start is called before the first frame update
    void Start()
    {
        DesactivarCollidersarmas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarCollidersArmas()
    {
        pu�oBoxCol.enabled = true;
    }

    public void DesactivarCollidersarmas()
    {
        pu�oBoxCol.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveForce : MonoBehaviour
{
    public float force = 5f;
    public bool automaticTrigger = false;
    public LayerMask receiverLayers;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        if (automaticTrigger) Explode();
    }

    // Update is called once per frame
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, receiverLayers);

        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            if (rb == null) continue;
            rb.AddExplosionForce(force, transform.position, radius);
        }
    }
}

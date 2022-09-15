using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// This class handles determining if a player is killed by configuring kill conditions
/// </summary>
[RequireComponent(typeof(Collider))]
public class PlayerKiller : MonoBehaviour
{
    /// <summary>Unity Event for what else will happen on kill such as a death sound playing</summary>
    public UnityEvent onKill;

    /// <summary>True if the player is dead</summary>
    public bool dead;

    /// <summary>
    /// Minimum distance between object to each opposite collider to trigger a kill
    /// X represents horizontal tolerance
    /// Y represents vertical tolerance
    /// </summary>
    public Vector2 squishTolerances;

    /// <summary>
    /// Layers which the object can be squished by
    /// </summary>
    public LayerMask validSquishLayers;


    /// <summary>
    /// The minimum distance the object needs to be inside of a collider by to trigger a kill
    /// </summary>
    public float penetrationTolerance;

    /// <summary>
    /// Layers which the object can be penetrating inside of can trigger a kill
    /// </summary>
    public LayerMask validPenetrateLayers;

    private Collider myCollider;

    /// <summary>Sets dead back to false</summary>
    public void Reset()
    {
        dead = false;
    }
    private void Kill()
    {
        dead = true;
        onKill.Invoke();
    }

    private void Start()
    {
        dead = false;
        myCollider = GetComponent<Collider>();
    }

    private void CheckForDeath(Collision col)
    {
        CheckForSquish(col);
        CheckForPenetration(col);
    }

    private void CheckForSquish(Collision col)
    {
        if (dead) return;
        if (!validSquishLayers.HasLayer(col.collider.gameObject.layer)) return;

        Vector2 direction = col.GetContact(0).normal;
        direction = direction.normalized; // re-normalize the vector is it is 2D not 3D
        Vector2 relativeSquishTolernaces = direction * squishTolerances;

        RaycastHit hit;
        Physics.Raycast(transform.position, direction, out hit, relativeSquishTolernaces.magnitude, validSquishLayers);

        if (hit.collider != null && hit.collider != col.collider && hit.collider.isTrigger == false)
        {
            Kill();
        }
    }

    private void CheckForPenetration(Collision col)
    {
        if (dead) return;
        if (!validPenetrateLayers.HasLayer(col.collider.gameObject.layer)) return;

        Vector3 direction;
        float distance;

        Physics.ComputePenetration(
            col.collider,
            col.collider.transform.position,
            col.collider.transform.rotation,

            myCollider,
            myCollider.transform.position,
            myCollider.transform.rotation,

            out direction,
            out distance
        );
        if (distance > penetrationTolerance)
        {
            Kill();
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        CheckForDeath(col);
    }

    private void OnCollisionStay(Collision col)
    {
        CheckForDeath(col);
    }
}

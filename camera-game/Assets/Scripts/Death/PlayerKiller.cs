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

    /// <summary>
    /// triggers the death UnityEvent when dead changes from false to true
    /// </summary>
    public bool automaticallyKill = true;

    /// <summary>
    /// continue to check if player shoudl be dead, even if it is already dead
    /// </summary>
    public bool checkDespiteDead = false;

    /// <summary>
    /// boolean represents whether the player should be dead, only valid if checkDespiteDead is true
    /// </summary>
    public bool deadTrigger = false;

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
        myCollider = GetComponent<Collider>();
    }

    public void HandleDeath(Collision col)
    {
        Debug.Log("3. Collision Events Occurred, Checking for death");
        if (dead)
        {
            if (checkDespiteDead)
            {
                deadTrigger = CheckForDeath(col);
            }
        }
        else
        {
            deadTrigger = CheckForDeath(col);
            if (deadTrigger && automaticallyKill)
            {
                Kill();
            }
        }
    }

    public bool CheckForDeath(Collision col)
    {
        if (col == null) return false;
        if (!enabled) return false;

        if (CheckForSquish(col)) return true;
        if (CheckForPenetration(col)) return true;

        return false;
    }

    private bool CheckForSquish(Collision col)
    {
        if (!validSquishLayers.HasLayer(col.collider.gameObject.layer)) return false;

        ContactPoint contact = col.GetContact(0);
        Vector2 direction = contact.normal;
        direction = direction.normalized; // re-normalize the vector is it is 2D not 3D
        Vector2 relativeSquishTolernaces = direction * squishTolerances;

        RaycastHit hit;
        Physics.Raycast(contact.point, direction, out hit, relativeSquishTolernaces.magnitude, validSquishLayers);

        return hit.collider != null && hit.collider != col.collider && hit.collider.isTrigger == false;
    }

    private bool CheckForPenetration(Collision col)
    {
        if (!validPenetrateLayers.HasLayer(col.collider.gameObject.layer)) return false;

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
        return distance > penetrationTolerance;
    }

    private void OnCollisionEnter(Collision col)
    {
        HandleDeath(col);
    }

    private void OnCollisionStay(Collision col)
    {
        HandleDeath(col);
    }
}

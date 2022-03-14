using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPackage : MonoBehaviour
{
    public Vector3 offset = new Vector3(0,0,0);
    GameObject currentPackage = null;

    void pickUpPackage(GameObject package){
        currentPackage = package;

        currentPackage.transform.SetParent(gameObject.transform);
        Destroy(currentPackage.GetComponent<Rigidbody>());

        currentPackage.transform.localPosition = offset;
        currentPackage.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    void dropPackage(){
        if (currentPackage == null) return;

        currentPackage.AddComponent<Rigidbody>();
        currentPackage.transform.parent = null;
        currentPackage = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            Debug.Log("Drop");
            dropPackage();
        }
    }
    void FixedUpdate(){

    }
    void OnCollisionEnter(Collision collision)
    {
        if (currentPackage == null){
            if (collision.gameObject.GetComponent<Package>() != null){
                pickUpPackage(collision.gameObject);
            }
        }
    }
}
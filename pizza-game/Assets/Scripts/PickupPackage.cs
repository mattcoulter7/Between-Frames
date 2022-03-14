using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPackage : MonoBehaviour
{
    GameObject currentPackage = null;
    void pickUpPackage(GameObject package){
        currentPackage = package;
        package.transform.SetParent(gameObject.transform);
    }

    void dropPackage(){
        if (currentPackage == null) return;
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
    void OnCollisionEnter(Collision collision)
    {
        if (currentPackage == null){
            if (collision.gameObject.GetComponent<Package>() != null){
            pickUpPackage(collision.gameObject);
            }
        }
    }
}
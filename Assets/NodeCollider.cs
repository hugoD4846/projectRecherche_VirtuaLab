using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCollider : MonoBehaviour
{
    public GameObject breadBoardNode = null;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "BreadboardElement"){
            breadBoardNode = other.gameObject.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "BreadboardElement"){
            breadBoardNode = null;
        }
    }
}

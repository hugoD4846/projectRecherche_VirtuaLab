using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragAndDropComponent : MonoBehaviour
{
    private float _initial_height;
    private Quaternion _defaultRotation;
    public Rigidbody rigid;
    public GameObject rightNode;
    public GameObject leftNode;
    public Renderer componentRenderer;
    public Material wrongPlacementMaterial;
    private Material initialMaterial;
    private bool wrongPlace = false;
    void Start()
    {
        this._defaultRotation = transform.rotation;
        this.initialMaterial = componentRenderer.sharedMaterial;
    }
    private void OnMouseDown() {
        this.rigid.isKinematic = true;
        resetRotation();
        GameObject rightAttachedHole = rightNode.GetComponent<NodeCollider>().breadBoardNode;
        GameObject leftAttachedHole = leftNode.GetComponent<NodeCollider>().breadBoardNode;
        if (rightAttachedHole.GetComponent<breadboardItemScript>().attachedComponent == gameObject)
        {
            rightAttachedHole.GetComponent<breadboardItemScript>().attachedComponent = null;
        }
        if(leftAttachedHole.GetComponent<breadboardItemScript>().attachedComponent == gameObject)
        {
            leftAttachedHole.GetComponent<breadboardItemScript>().attachedComponent = null;
        }
    }

    private void OnMouseUp() {
        GameObject rightAttachedHole = rightNode.GetComponent<NodeCollider>().breadBoardNode;
        GameObject leftAttachedHole = leftNode.GetComponent<NodeCollider>().breadBoardNode;
        bool attachCondition = (
            rightAttachedHole != null
            &&
            leftAttachedHole != null
            &&
            rightAttachedHole.GetComponent<breadboardItemScript>().attachedComponent == null
            &&
            leftAttachedHole.GetComponent<breadboardItemScript>().attachedComponent == null
        );
        this.rigid.isKinematic = attachCondition;
        if (attachCondition){
            rightAttachedHole.GetComponent<breadboardItemScript>().attachedComponent = gameObject;
            leftAttachedHole.GetComponent<breadboardItemScript>().attachedComponent = gameObject;
            transform.parent = rightAttachedHole.transform.parent.parent;
            transform.position = new Vector3(rightAttachedHole.transform.position.x + 1f,1.4f,rightAttachedHole.transform.position.z);
        }
    }
    private void resetRotation() {
        transform.rotation = this._defaultRotation;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "ComponentElement")
        {
            this.wrongPlace = true;
            componentRenderer.sharedMaterial = wrongPlacementMaterial;
        }   
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "ComponentElement")
        {
            this.wrongPlace = false;
            componentRenderer.sharedMaterial = initialMaterial;
        }   
    }
    private void OnMouseDrag() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1000f, ~(LayerMask.GetMask("Component") | LayerMask.GetMask("Ignore Raycast"))))
        {
            transform.position = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);
        }
    }
}

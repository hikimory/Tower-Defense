using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    GameObject turret;

    private Renderer rend;
    private Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown() {
        if(turret != null){
            Debug.Log("Can't build there!");
            return;
        }

        //Build a turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation) as GameObject;
    }

    // called while the mouse stays over the object 
    void OnMouseEnter() {
        rend.material.color = hoverColor;
    }

    // the mesh turns white when the mouse moves away.
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    void OnMouseDown() {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        if(buildManager.GetTurretToBuild() == null)
            return;
        
        if(turret != null){
            Debug.Log("Can't build there!");
            return;
        }

        //Build a turret
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation) as GameObject;
    }

    // called while the mouse stays over the object 
    void OnMouseEnter() {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        if(buildManager.GetTurretToBuild() == null)
            return;
        rend.material.color = hoverColor;
    }

    // the mesh turns white when the mouse moves away.
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}

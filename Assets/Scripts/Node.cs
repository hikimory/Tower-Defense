using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition(){
        return transform.position + positionOffset;
    }

    void OnMouseDown() {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        
        if(turret != null){
            buildManager.SelectNode(this);
            return;
        }

        if(!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint){
        if(PlayerStats.Money < blueprint.cost){
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity) as GameObject;
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity) as GameObject;
        Destroy(effect, 5f);
        Debug.Log("Turret build!");
    }

    public void UpgradeTurret(){
        if(PlayerStats.Money < turretBlueprint.upgradeCost){
            Debug.Log("Not enough money to upgrate that!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        // Get rid of the old turret
        Destroy(turret);

        //Build a new one
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity) as GameObject;
        turret = _turret;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity) as GameObject;
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret upgraded!");
    }

    // called while the mouse stays over the object 
    void OnMouseEnter() {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        if(!buildManager.CanBuild)
            return;
        
        if(buildManager.HasMoney){
            rend.material.color = hoverColor;
        }else {
            rend.material.color = notEnoughMoneyColor;
        }
        
    }

    // the mesh turns white when the mouse moves away.
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}

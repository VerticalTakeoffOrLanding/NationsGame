using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManagerScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> forceList = new List<GameObject>(); //List of all forces ingame, independent of ownership or type

    [SerializeField] private GameObject forcePrefab; //The force prefab that will be instantiated


    // Needs a list of orders for units somehow? each order must contain the unit reference, the start and end tile and the action (move, attack, etc)


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateForce(TileScript currentTile, int forceType)
    {
        GameObject newForce = Instantiate(forcePrefab, currentTile.transform.position, Quaternion.identity); //Change spawn pos so that the force is "above" the tile (can do that in the force script instead if easier)
        newForce.GetComponent<ForceScript>().SetForceType(forceType);
        forceList.Add(newForce);
    } // Creates one force, applies the correct details, adds to the forceList
}

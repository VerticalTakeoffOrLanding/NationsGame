using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceScript : MonoBehaviour
{
    [SerializeField] private int forceType;
    TileScript currentTileScript;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetForceType(int type)
    {
        forceType = type;
    } //Tell force what type it is

    public void SetCurrentTile(TileScript newTileScript)
    {
        currentTileScript = newTileScript;
    } //Tell force what tile it is attached to
}

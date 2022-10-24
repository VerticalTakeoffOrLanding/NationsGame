using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSelectionUIScript : MonoBehaviour
{
    [SerializeField] GameObject GameBoard;
    GameBoardScript gameBoardScript;

    private TileScript selectedTileScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SelectTile(TileScript tileScript)
    {
        //Debug.Log(tileScript.GetLabour());
        selectedTileScript = tileScript;
        gameObject.GetComponent<Text>().text = selectedTileScript.GetTitle() + "\n" +
        "Food: " + selectedTileScript.GetFood() +"\n" +
        "Labour " + selectedTileScript.GetLabour();
  




        




    } // Called by cursor on click.
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollectionScript : MonoBehaviour //Manages the list of tile collections (eg 3 desert tiles)
{

    [SerializeField] public GameObject GameBoard;

    private GameBoardScript gameBoardScript;


    // Start is called before the first frame update
    void Start()
    {
        gameBoardScript = GameBoard.GetComponent<GameBoardScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameBoardScript : MonoBehaviour
{
    private const float tileFlatRadius = 52.1f; //Distance from centre to closest point (perpendicular to any flat face)

    [SerializeField] public GameObject profileLoader;
    [SerializeField] public GameObject forceManager;

    private ProfileLoaderScript profileLoaderScript;
    private ForceManagerScript forceManagerScript;

    [SerializeField] public GameObject tilePrefab;

    [SerializeField] private int xTileNum;
    [SerializeField] private int yTileNum;
    private static GameObject[,] TileGrid;

    // Start is called before the first frame update
    void Start()
    { 
        TileGrid = new GameObject[xTileNum, yTileNum];

        // Caching // (is that how its spelled?)
        profileLoaderScript = profileLoader.GetComponent<ProfileLoaderScript>();
        forceManagerScript = forceManager.GetComponent<ForceManagerScript>();

        //LoadBoard("Assets/Resources/textLoad.txt");
        CreateBoard(xTileNum,yTileNum);
        forceManagerScript.CreateForce(TileGrid[0, 0].GetComponent<TileScript>(), 0);
        forceManagerScript.CreateForce(TileGrid[30, 8].GetComponent<TileScript>(), 1);
        forceManagerScript.CreateForce(TileGrid[16, 2].GetComponent<TileScript>(), 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector2Int LoopPos (int x, int y)
    {
        Vector2Int loopedPos = new Vector2Int(x, y);
        if (x < 0)
        {
            loopedPos.x = xTileNum-1;
        }
        if (x > xTileNum-1)
        {
            loopedPos.x = 0;
        }
        if (y > yTileNum-1)
        {
            loopedPos.y = 0;
        }
        if (y < 0)
        {
            loopedPos.y = yTileNum-1;
        }
        //Debug.Log("The coords of this adjacent are: " + loopedPos.x + ":" + loopedPos.y);
        return loopedPos;
    }

    private void FirstGeneration()
    {
        foreach (GameObject tile in TileGrid)
        {
            TileScript thisTile = tile.GetComponent<TileScript>();
            float tileTemp = 0;
            float tileTempAdj = 0; //Temp from adjacent tiles
            float tileTempY = 0; //Temp from y position
            float thisY = thisTile.GetGridlistPosition().y;
            float maxY = yTileNum;

            for (int i = 0; i < 6; i++)
            {
                tileTempAdj += thisTile.GetAdjacentTile(i).GetHeat();
            }
            tileTempAdj = tileTempAdj / 6; //Gets average temperature of adjacent tiles

            // (-40/m^2 * y^2) + (40/m * y) -5
            tileTempY = (-40 / (maxY * maxY)) * (thisY * thisY) + ((40 / maxY) * thisY) - 5;
            
            tileTemp = tileTempAdj*1.2f + tileTempY - Random.Range(0,5);
            //Debug.Log("tileTemp is: " + tileTemp);
            tileTemp = Mathf.Clamp(tileTemp, -5, 5);
            int tileTempInt = Mathf.RoundToInt(tileTemp);

            Debug.Log("My Y position is : " + thisY + " and my temp is " + tileTempInt);
            thisTile.SetTileType(profileLoaderScript.GetTileHeatType(tileTempInt));
        }
    }

    private void CreateBoard(int width, int height)
    {
        float yPos = 0;
        float xPos = 0;
        for (int i = 0; i < width; i++)
        {
            xPos += tileFlatRadius;
            yPos = 0;
            for (int o = 0; o < height; o++)
            {
                yPos += tileFlatRadius * Mathf.Cos((30 * Mathf.PI) / 180);
                if ((o % 2) == 0) // Checks for either odd or even column for stagger
                {
                    xPos += tileFlatRadius * Mathf.Sin((30 * Mathf.PI) / 180);
                }
                else
                {
                    xPos -= tileFlatRadius * Mathf.Sin((30 * Mathf.PI) / 180);
                }

                GameObject newTile = Instantiate(tilePrefab, new Vector3 (xPos,yPos,0), Quaternion.identity); //Instantiate tile
                TileGrid[i, o] = newTile; //Add tile to the tileGrid (2d array)
                TileGrid[i, o].GetComponent<TileScript>().MakeRandomTile(); //Gives tile random surface details (used to retrieve profiles to fill in other details)
                TileGrid[i, o].GetComponent<TileScript>().SetGridListPos(new Vector2(i, o)); //Tells the tile where it is in the TileGrid
            }
        }
        foreach (GameObject tile in TileGrid) //Looping through the list again to set adjacents (cant do this on first pass) adjacent 0 is the top left tile, continues clockwise
        {
            TileScript thisTile = tile.GetComponent<TileScript>(); //Reference this tiles script
            Vector2 thisTilePos = thisTile.GetGridlistPosition(); //Get the gridlist position of the tile
            int thisX = Mathf.RoundToInt(thisTilePos.x);
            int thisY = Mathf.RoundToInt(thisTilePos.y);
            Vector2Int adjTilePos;

            if (thisY % 2 == 0)
            {
                adjTilePos = LoopPos(thisX, thisY + 1); //upleft
                thisTile.SetAdjacentTile(0, TileGrid[adjTilePos.x, adjTilePos.y].GetComponent<TileScript>());
                adjTilePos = LoopPos(thisX + 1, thisY + 1); //upright
                thisTile.SetAdjacentTile(1, TileGrid[adjTilePos.x, adjTilePos.y].GetComponent<TileScript>());
                adjTilePos = LoopPos(thisX + 1, thisY - 1); //downright
                thisTile.SetAdjacentTile(3, TileGrid[adjTilePos.x, adjTilePos.y].GetComponent<TileScript>());
                adjTilePos = LoopPos(thisX, thisY - 1); //downleft
                thisTile.SetAdjacentTile(4, TileGrid[adjTilePos.x, adjTilePos.y].GetComponent<TileScript>());
            }
            else
            {
                adjTilePos = LoopPos(thisX - 1, thisY + 1); //upleft
                thisTile.SetAdjacentTile(0, TileGrid[adjTilePos.x, adjTilePos.y].GetComponent<TileScript>());
                adjTilePos = LoopPos(thisX, thisY + 1); //upright
                thisTile.SetAdjacentTile(1, TileGrid[adjTilePos.x, adjTilePos.y].GetComponent<TileScript>());
                adjTilePos = LoopPos(thisX, thisY - 1); //downright
                thisTile.SetAdjacentTile(3, TileGrid[adjTilePos.x, adjTilePos.y].GetComponent<TileScript>());
                adjTilePos = LoopPos(thisX - 1, thisY - 1); //downleft
                thisTile.SetAdjacentTile(4, TileGrid[adjTilePos.x, adjTilePos.y].GetComponent<TileScript>());
            }

            adjTilePos = LoopPos(thisX+1, thisY); //right
            thisTile.SetAdjacentTile(2, TileGrid[adjTilePos.x, adjTilePos.y].GetComponent<TileScript>());
            adjTilePos = LoopPos(thisX-1, thisY); //left
            thisTile.SetAdjacentTile(5, TileGrid[adjTilePos.x, adjTilePos.y].GetComponent<TileScript>());
        }
        SaveBoard();
        RefreshBoard();
        FirstGeneration();
        FirstGeneration();
        RefreshBoard();

    } //CreateBoard: Generates the tiles

    private void RefreshBoard()
    {
        foreach (GameObject i in TileGrid) //Check through each tile.
        {
            string[] tempTypeProfile = profileLoaderScript.GetTileTypeProfile(i.GetComponent<TileScript>().GetTileType()); //For each tile, check its type... //Look at the appropriate profiles.
            i.GetComponent<TileScript>().GiveTypeProfile(tempTypeProfile); //Set the tile details accordingly
            //Debug.Log(tempTypeProfile[1]);
        }

        //For each tile, check its modifier and upgrade.
        //Look at the appropriate profiles.
        //Set the tile details accordingly
    }

    public TileScript getTileScript(Vector2 position)
    {
        return TileGrid[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)].GetComponent<TileScript>();
    }








































    private void SaveBoard() //Saves the board to a CSV
    {
        string path = "Assets/Resources/text.txt";
        StreamWriter writer = new StreamWriter(path, true); //Creates a txt file if none exist
        writer.WriteLine(xTileNum+","+yTileNum); //First line preamble for game details
        foreach (GameObject i in TileGrid) //For every tile
        {
            TileScript tempScript = i.GetComponent<TileScript>();
            writer.WriteLine(tempScript.GetTileType()+","+ tempScript.GetModifier() + ","+ tempScript.GetUpgrade()); //This line must store every tile property we need to save
        }
        writer.Close();
    } //SaveBoard: Saves the board details to CSV

    private void LoadBoard(string saveLoc)
    {
        string path = saveLoc;
        StreamReader reader = new StreamReader(path);
        string line = reader.ReadLine();
        string[] lineSplit = line.Split(',');
        CreateBoard(int.Parse(lineSplit[0]), int.Parse(lineSplit[1])); //Creates the board using the details in the preamble        
        foreach (GameObject i in TileGrid)
        {
            TileScript tempScript = i.GetComponent<TileScript>();
            line = reader.ReadLine();
            lineSplit = line.Split(',');
            tempScript.SetTileType(int.Parse(lineSplit[0])); // Set all the tile details
            tempScript.SetModifier(int.Parse(lineSplit[1]));
            tempScript.SetUpgrade(int.Parse(lineSplit[2]));
        }
        reader.Close();
    } //LoadBoard: Loads board details manually

   
}

//when moving between columns: (+/- depending on direction)
/// x = tileFlatRadius*Cos(30)
/// y = tileFlatRadius*Sin(30) 
//
//xPos += tileFlatRadius*Mathf.Cos((30 * Mathf.PI) / 180); //Shift to next column x

//// TileGrid[i, o].GetComponent<TileScript>().MakeRandomType(); // how to change the details of an instance in the list
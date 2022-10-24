using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ProfileLoaderScript : MonoBehaviour
{
    private int numTileTypes = 10; //Change me when adding new tiles (highest tileindex +1)
    private int numTileProperties = 8; //Change me when adding new tile properties
    private string[,] TileTypes;





    // Start is called before the first frame update
    void Start()
    {
        TileTypes = new string[numTileTypes, numTileProperties];
        LoadAllProfiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadAllProfiles()
    {
        LoadTileTypeProfiles();
    } //Loads all the profiles into this object, converting text file into useful datastructures (sounds cool)

    private void LoadTileTypeProfiles()
    {
        string path = "Assets/Resources/Profiles/TileTypes.txt";
        StreamReader reader = new StreamReader(path);
        string fullText = reader.ReadToEnd();
        string[] fullTextSplit = fullText.Split(',');
        int u = 0;
        for (int i = 0; i < numTileTypes; i++)
        {
            for (int o = 0; o < numTileProperties; o++)
            {
                u++;
                TileTypes[i, o] = fullTextSplit[u];
                //Debug.Log(TileTypes[i, o] + " " + i + "" + o);
            }
        }
        reader.Close();
    } //Populates a 2D array with all the details of the different types of tile

    public int GetTileHeatType(int heat)
    {
        for (int i = 0; i < numTileTypes; i++)
        {
            //Debug.Log(TileTypes[i, 7] + " : " + heat.ToString());
            if (int.Parse(TileTypes[i,7]) == heat)
            {
                Debug.Log("Found a heat match");
                Debug.Log("Heat is: " + i + " and TileType is now: " + TileTypes[i, 0]);
                return int.Parse(TileTypes[i, 0]);
            }
        }
        return 0;
    }

    public string[] GetTileTypeProfile(int typeIndex)
    {
        string[] TypeProfile = new string[numTileProperties];
        for (int i = 0; i < numTileProperties; i++)
        {
            //Debug.Log(TileTypes[i, typeIndex]);
            TypeProfile[i] = TileTypes[typeIndex, i];
        }
        //Debug.Log("Reaching point of return (GetTileTypeProfile)");
        return TypeProfile;
    } //GetTileTypeProfile: Returns an array of numbers, corresponding to different tile properties
}

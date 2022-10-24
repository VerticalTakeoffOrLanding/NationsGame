using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    // PROPERTIES //
    [SerializeField] private string Title;

    [SerializeField] private int type;
    [SerializeField] private int modifier;
    [SerializeField] private int upgrade;

    [SerializeField] private int food;
    [SerializeField] private int labour;
    [SerializeField] private int science;
    [SerializeField] private int culture;
    [SerializeField] private int worship;
    [SerializeField] private int heat;


    [SerializeField] private Vector2 gridlistPosition;
    private bool selected = false;


    [SerializeField] private string collectionName;
    [SerializeField] private TileScript[] adjacents = new TileScript[6];

    // Start is called before the first frame update
    void Start()
    {
        RefreshTileProperties();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeRandomTile()
    {
        type = Random.Range(0, 10); // Change me when adding new tile types
    } //Generates a random tile (mostly for early development)

    public void MakeTile(int newType,int newModifier,int newUpgrade)
    {
        type = newType;
        modifier = newModifier;
        upgrade = newUpgrade;
    } //For setting the details of a newly instantiated tile

    public void RefreshTileProperties()
    {
        
    }

    public void GiveTypeProfile(string[] profile)
    {
        Title = profile[1]; //Positions of detail in a profile are hardcoded, 1 is the title index
        SetColour(int.Parse(profile[0]));
        type = int.Parse(profile[0]);
        food = int.Parse(profile[2]);
        labour = int.Parse(profile[3]);
        science = int.Parse(profile[4]);
        culture = int.Parse(profile[5]);
        worship = int.Parse(profile[6]);
        heat = int.Parse(profile[7]);
    } //Send a tile profile to this tile

    //////////////////////////////////////////// Getters /////////////////////////////////////////////////////
    public int GetTileType()
    {
        return type;
    }
    public int GetModifier()
    {
        return modifier;
    }
    public int GetUpgrade()
    {
        return upgrade;
    }
    public int GetFood()
    {
        return food;
    }
    public int GetLabour()
    {
        return labour;
    }
    public int GetScience()
    {
        return science;
    }
    public int GetCulture()
    {
        return culture;
    }
    public int GetWorship()
    {
        return worship;
    }
    public int GetHeat()
    {
        return heat;
    }

    public string GetTitle()
    {
        return Title;
    }

    public Vector2 GetGridlistPosition()
    {
        return gridlistPosition;
    }

    public string GetTileCollection()
    {
        return collectionName;
    }

    public TileScript GetAdjacentTile(int index)
    {
        return adjacents[index];
    }

    //////////////////////////////////////////// Setters /////////////////////////////////////////////////////
    public void SetTileType(int input)
    {
        type = input;
    }
    public void SetModifier(int input)
    {
        modifier = input;
    }
    public void SetUpgrade(int input)
    {
        upgrade = input;
    }
    public void SetColour(int input) //To find the colours, go to the temphex prefab and change the colour /// This is for early dev
    {
        if (input == 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.47f, 0.55f, 0.4f, 1);
        }
        else if (input == 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.48f, 0.48f, 0.45f, 1);
        }
        else if (input == 2)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.35f, 0.55f, 0.28f, 1);
        }
        else if (input == 3)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.18f, 0.18f, 0.65f, 0.75f);
        }
        else if (input == 4)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.39f, 0.38f, 0.67f, 0.9f);
        }
        else if (input == 5)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.94f, 0.90f, 0.43f, 1);
        }
        else if (input == 6)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.42f, 0.38f, 0.32f, 1);
        }
        else if (input == 7)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.95f, 0.95f, 0.95f, 1);
        }
        else if (input == 8)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.99f, 1);
        }
        else if (input == 9)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.77f, 0.62f, 0.46f, 1);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0.01f, 0.36f, 0.95f, 1);
        }
    }

    public void SetGridListPos(Vector2 position)
    {
        gridlistPosition = position;
    }
    public void SetTileCollection(string collection)
    {
        collectionName = collection;
    }

    public void SetAdjacentTile(int index, TileScript adjacentTile)
    {
        //Debug.Log("Index is: " + index);
        adjacents[index] = adjacentTile;
    }
}
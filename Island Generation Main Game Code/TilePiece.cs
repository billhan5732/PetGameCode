using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePiece : MonoBehaviour
{
    // Start is called before the first frame update

    public static float size = 12f;
    public static float groundHeightScale;
    public static float heightDifference = 1f;
    public static Transform terrainParent;

    public int elevation;
    public bool hasInteractableOfTile = false;
    public bool hasHostile = false;

    public static Color landColor = new Color(30f/255f, 190f/255f, 20f/255f);
    

    public GameObject interactableOfTile;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Start() 
    {
        transform.SetParent(terrainParent);
    }

    public void SetLocationAndElevation(int elevation, int posX, int posY) 
    {
        transform.SetParent(terrainParent);
        this.elevation = elevation;

        if (elevation <= 0) { gameObject.SetActive(false); return; }

        float yScale = groundHeightScale + elevation * heightDifference;

        transform.localScale = new Vector3(size, yScale, size);
        transform.position = new Vector3(size*posX, 0,size*posY);

        SetTerrainColor();
    }

    public void SetTerrainColor() 
    {
        if (elevation <= 1) 
        {
            return;
        }

        Color grassColor = new Color(landColor.r, (landColor.g*255 - 4 * elevation)/255f, landColor.b);//new Color(30 / 255f, (190 - elevation * 4) / 255f, 20 / 255f);
        //grassColor.g = (landColor.g - (elevation * 4)) / 255f;
        transform.GetComponent<MeshRenderer>().material.color = grassColor;
    }

    public void SetInteractable(GameObject newInteractable) 
    {
        hasInteractableOfTile = true;
        this.interactableOfTile = newInteractable;

        float yPos = transform.position.y + elevation * heightDifference / 2 + groundHeightScale / 2;
        Vector3 newPosition = new Vector3(transform.position.x, yPos, transform.position.z);

        newInteractable.transform.position = newPosition;

    }

    public Vector3 GetTopPosition() 
    {
        float yPos = transform.position.y + elevation * heightDifference / 2 + groundHeightScale / 2;
        Vector3 newPosition = new Vector3(transform.position.x, yPos, transform.position.z);
        return newPosition;
    }
}

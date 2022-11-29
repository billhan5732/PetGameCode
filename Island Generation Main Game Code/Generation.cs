using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
	public GameObject tile;
	//public GameObject testPalm;
	public static string islandType;
	public float extremity = 1f;

	public float propCoverage;

	public static int ocean = 0;
	public static int beach = 1;
	public static int landE1 = 2;

	public static int border = 5;
	public static int rimBorder = 3;
	public static int test = 0;

	public static readonly int tileSize = 12;

	public Material oceanMat;
	public Material grassMat;


	public Transform nonStaticInteractablesParent;
	public static Transform interactablesParent;

	public GameObject objectiveTestOBJ;

	#region grid
	public static int rows = 36;
	public static int width = rows;

	public static int[][] map = new int[rows][];
	public static int[][] emptyMap = new int[rows][];
	#endregion
	// = emptyMap[0].Length;
	public List<GameObject> tileList = new List<GameObject>();
	//public List<GameObject> tileList = new List<GameObject>();
	public static GameObject[] commonResources;

	public static void LoadMaps() 
	{
		int[] row = new int[width];
		for (int i = 0; i < row.Length; i++) { row[i] = 0; }
		for (int i = 0; i < rows; i++) { emptyMap[i] = row.Clone() as int[]; }
	}

	void Awake() 
	{ 
		LoadMaps();
		TilePiece.terrainParent = GameObject.FindWithTag("TerrainParent").transform; 
		TilePiece.groundHeightScale = tile.transform.localScale.y;
		TilePiece.heightDifference = extremity;
		interactablesParent = nonStaticInteractablesParent; 
	}

	void Start() 
	{
		
		//TilePiece.groundHeightScale = tile.transform.localScale.y;
		//TilePiece.terrainParent = GameObject.FindWithTag("TerrainParent").transform;
		//display();
		//loadEmptyMap();



		//commonResources = Resources.LoadAll<GameObject>("IslandResourcePrefabs");

	}

	public void LOAD_MAP() 
	{
		commonResources = Resources.LoadAll<GameObject>("IG/IslandResourcePrefabs/" + islandType);
		ClearMap();
		generate2(1);
		loadTiles();

		GenerateObjective();
	}

	void ClearMap() 
	{
		map = emptyMap.Clone() as int[][];
	}

	void loadTiles() 
	{
		int size = 12;

		float heightScale = this.tile.transform.localScale.y;
		this.tile.transform.localScale = new Vector3(size,heightScale,size);

		for (int y =0; y<rows;y++) 
		{
			for (int x = 0; x < width; x++) 
			{
				GameObject newTile = Instantiate(this.tile,TilePiece.terrainParent);
				tileList.Add(newTile);

				newTile.GetComponent<TilePiece>().SetLocationAndElevation(map[y][x], x, y);
				loadTileResources(newTile);
			}
		}

		if (map[0][0] == 0)
		{
			this.tile.SetActive(false);
		}
	}

	public void SpawnGameObjectOnRandomTile(GameObject thing, bool isHostile = false) 
	{
		int random = Random.Range(1, tileList.Count);
		if (isHostile) 
		{
			while (tileList[random].GetComponent<TilePiece>().hasHostile || tileList[random].GetComponent<TilePiece>().elevation < 2)
			{
				random = Random.Range(0, tileList.Count);
			}
			tileList[random].GetComponent<TilePiece>().hasHostile = true;
		}
		
		if (tileList[random].GetComponent<TilePiece>().hasInteractableOfTile) { tileList[random].GetComponent<TilePiece>().interactableOfTile.SetActive(false); }
        tileList[random].GetComponent<TilePiece>().SetInteractable(thing);
	}

	void GenerateObjective() 
	{
		int random = Random.Range(0, tileList.Count);

		while (tileList[random].GetComponent<TilePiece>().elevation < 2) 
		{
			random = Random.Range(0, tileList.Count);
		}
		if (tileList[random].GetComponent<TilePiece>().hasInteractableOfTile) { tileList[random].GetComponent<TilePiece>().interactableOfTile.SetActive(false); }

		tileList[random].GetComponent<TilePiece>().SetInteractable(objectiveTestOBJ);

	}

	public static void loadTileResources(GameObject newTile)
	{
		GameObject resource;
		resource = commonResources[UnityEngine.Random.Range(1, commonResources.Length)];
		if ( resource.GetComponent<Interactable>().canSpawnInElevation(newTile.GetComponent<TilePiece>().elevation) && (UnityEngine.Random.Range(0f, 1f) < resource.GetComponent<Interactable>().spawnChance * 0.5f))
		{

			GameObject inworldR = Instantiate(resource) as GameObject;
			inworldR.transform.SetParent(interactablesParent);

			float rngScale = UnityEngine.Random.Range(-0.5f, 0.1f);
			Vector3 randomScale = new Vector3(rngScale + inworldR.transform.localScale.x, rngScale + inworldR.transform.localScale.y, rngScale + inworldR.transform.localScale.z);

			inworldR.transform.Rotate(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
			inworldR.transform.localScale = randomScale;
			newTile.GetComponent<TilePiece>().SetInteractable(inworldR);
		}
	}

	public static void flat(int x, int y)
	{
		if ((x <= rimBorder || x >= rows - rimBorder) || (y <= rimBorder || y >= width - rimBorder))
		{
			return;
		}

		map[y][x + 1] += 1;
		map[y][x - 1] += 1;
		map[y + 1][x] += 1;
		map[y - 1][x] += 1;

		map[y - 1][ x - 1] += 1;
		map[y - 1][ x + 1] += 1;
		map[y + 1][ x - 1] += 1;
		map[y + 1][ x + 1] += 1;
	}
	
	public static void tileUpPoint(int x, int y)
	{

		if (x <= 1 || x >= rows - 2)
		{
			return;
		}
		if (y <= 1 || y >= width - 2)
		{
			return;
		}


		map[y][x] += 2;

		map[y + 1][x] += 1;
		map[y - 1][x] += 1;
		map[y][x + 1] += 1;
		map[y][x - 1] += 1;

		map[y + 1][x + 1] += 1;
		map[y - 1][x + 1] += 1;

		map[y + 1][x - 1] += 1;
		map[y - 1][x - 1] += 1;
	}

	public static bool checkIfInBOunds(int x, int y) 
	{
		if (x <= border || x >= rows - border)
		{
			return false;
		}
		if (y <= border || y >= width - border)
		{
			return false;
		}

		return true;
	}

	public static bool checkIfInRimBOunds(int x, int y)
	{
		if (x <= rimBorder || x >= rows - rimBorder)
		{
			return false;
		}
		if (y <= rimBorder || y >= width - rimBorder)
		{
			return false;
		}

		return true;
	}

	public static void createBigFlat(int x, int y, int size) 
	{
		if ((x <= border || x >= rows - border) || (y <= border || y >= width - border))
		{
			return;
		}


		int[] centerCoord = { y, x };
		//map[centerCoord[0]][centerCoord[1]] = 3;
		for (int i = -1 * size; i <= size; i++)
		{
			//tileUpPoint(centerCoord[1], centerCoord[0]);

			int change = 1;
			if (i == 0 || i == 1 || i == -1) { change = 0; }

			map[y + i][x] += change;
			flat(x, y + i);
			map[y + i][x + i] += change;

			map[y][x + i] += change;
			flat(x + i, y);
			map[y + i][x - i] += change;

			//Corners
			if (i != -1 * size && i != size)
			{
				flat(x + i, y + i);
				flat(x - i, y + i);
			}
		}
	}

	public static void generate2(int density)
	{
		int lowerChunk = 30 * density;
		int upperChunk = 40 * density;
		int chunks = (int)(Random.Range(lowerChunk, upperChunk));//(int)(Math.random() * (upperChunk - lowerChunk)) + lowerChunk;

		int lowerSize = 1;
		int upperSize = 3;

		createBigFlat((int)(rows / 2), (int)(width / 2), 3);
		
		for (int i = 0; i < chunks; i++)
		{
			int x =(int)(Random.Range(border, rows - border));//(int)(Math.random() * (map[0].Length - border));
			int y = (int)(Random.Range(border, width - border)); //border + (int)(Math.random() * (map.Length - border));
			int size = (int)(Random.Range(lowerSize, upperSize));//(Math.random() * (upperSize - lowerSize)) + lowerSize;
			createBigFlat(x, y, size);
		}

		cleanUpHoles(3);
		beachify(1);
		
	}

	public static void beachify(int lenience)
	{
		for (int y = 0; y < width; y++)
		{
			for (int x = 0; x < rows; x++)
			{
				if ((x > 1 && x < rows - 1) && (y > 1 && y < width - 1))
				{
					if (isOutCrop(x, y, lenience) && map[y][x] > 1)
					{
						map[y][x] = 1;
					}
				}
			}
		}
	}

	public static void ditchFill(int x, int y) 
	{
		int lowest = 50;
		bool isDitch = true;
		int point = map[y][ x];


		List<int> listOfELVN = new List<int>();
		for (int i = -1; i <= 1; i += 2)
		{
			
			if (map[y + i][ x] <= point)
			{
				isDitch = false;
				
			}
			if (map[y][ x + i] <= point)
			{
				isDitch = false;
				
			}

			if (map[y + i][ x + i] <= point)
			{
				isDitch = false;
				
			}
			if (map[y + i][ x - i] <= point)
			{
				isDitch = false;
				
			}
			listOfELVN.Add(map[y + i][ x]);
			listOfELVN.Add(map[y][ x + i]);
			listOfELVN.Add(map[y + i][ x + i]);
			listOfELVN.Add(map[y + i][ x - i]);
		}
		listOfELVN.Sort();
		int delta = Mathf.Abs(listOfELVN[7] - map[y][x]);
		//if(delta > )

		int changeTo = listOfELVN[0] + (int)(delta/2);
		if (isDitch) 
		{
			map[y][ x] = changeTo;
		}
		
	}

	public static void cleanUpHoles(int reps)
	{
		for (int m = 0; m < reps; m++)
		{
			for (int y = 0; y < width; y++)
			{
				for (int x = 0; x < rows; x++)
				{
					
					
					if (checkIfInBOunds(x, y))
					{
						ditchFill(x, y);
						//holes
						if (isHole(x, y) && map[y][ x] == 0)
						{
							createBigFlat(x, y, 3);
							//test++;
						}
						//outcrops
						if (isOutCrop(x, y, 7) && map[y][ x] == 1)
						{
							map[y][ x] = 0;
						}
						//Spikes
						/*
						if (map[y, x] >= 5)
						{
							map[y, x] = 5;
							for (int i = 0; i < map[y, x]; i++) 
							{
								flatV2(x, y);
							}
							
						}
						*/
						
					}
					
					
				}
			}
		}
		beachify(2);
	}

	public static bool isHole(int x, int y)
	{
		bool returnBool = false;
		int surroundingLand = 0;

		for (int i = -1; i <= 1; i += 2)
		{
			if (map[y + i][x] > 0)
			{
				surroundingLand++;
			}
			if (map[y][x + i] > 0)
			{
				surroundingLand++;
			}
			if (map[y + i][x + i] > 0)
			{
				surroundingLand++;
			}
			if (map[y + i][x - i] > 0)
			{
				surroundingLand++;
			}
		}

		if (surroundingLand > 6)
		{
			returnBool = true;
		}

		return returnBool;
	}

	public static void display() 
	{
		string line = "";
		for (int y = 0; y < width; y++) 
		{
			for (int x = 0; x < rows; x++) 
			{
				line = line + map[y][x].ToString() + ", ";
			}
			Debug.Log(line);
			line = "";
		}
	}

	public static bool isOutCrop(int x, int y, int surround)
	{
		bool returnBool = false;
		int surroundingLand = 0;

		for (int i = -1; i <= 1; i += 2)
		{
			if (map[y + i][x] == 0)
			{
				surroundingLand++;
			}
			if (map[y][x + i] == 0)
			{
				surroundingLand++;
			}

			if (map[y + i][x + i] == 0)
			{
				surroundingLand++;
			}
			if (map[y + i][x - i] == 0)
			{
				surroundingLand++;
			}
		}

		if (surroundingLand >= surround)
		{
			returnBool = true;
		}

		return returnBool;
	}

}




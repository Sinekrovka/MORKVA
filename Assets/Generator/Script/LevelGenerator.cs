using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private LevelData _levelsData;
    [SerializeField] private LevelItemData _levelPalete;

    private LevelData.Level currentLevelParams;
    private GameObject levelContainer;
    private List<List<int>> configLevel;

    private NavMeshSurface _meshSurface;
    private int plotX;
    private int plotY;

    public static LevelGenerator Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateLevel()
    {
        int levelID;
        if (PlayerPrefs.HasKey("Level"))
        {
            levelID = PlayerPrefs.GetInt("Level");
        }
        else
        {
            levelID = 0;
            PlayerPrefs.SetInt("Level", 0);
            PlayerPrefs.Save();
        }
        currentLevelParams = _levelsData.Levels[levelID];
        levelContainer = new GameObject("Level_" + levelID);
        configLevel = new List<List<int>>();
        
        GenerateLevelPlot();
        EnviromentGenerate();
        WriteLevel();
        CreateNavMesh();
    }

    public void Rebuild(bool win)
    {
        if (win)
        {
            
        }
        else
        {
            
        }
    }

    public void ClearLevel()
    {
        
    }

    private void CreatePlayer(int posX, int posY)
    {
        Instantiate(_levelPalete.GetPaleteOnName("Player").prefabGeneration, new Vector3(posX*5+1f, 1, posY*5+1f),
            quaternion.identity);
    }

    private void CreateNavMesh()
    {
        levelContainer.transform.localScale = Vector3.one*5;
        _meshSurface = GetComponent<NavMeshSurface>();
        _meshSurface.BuildNavMesh();
        
        LevelItemData.ItemGeneration data = _levelPalete.GetPaleteOnName("Enemy");

        for (int i = 0; i < currentLevelParams.countEnemies; i++)
        {
            Vector3 pos = levelContainer.transform.GetChild(0).
                GetChild(Random.Range(1, plotX-1)).GetChild(Random.Range(1,plotY-1)).position;
            pos.x += 0.5f;
            pos.z += 0.5f;
            Instantiate(data.prefabGeneration, pos, quaternion.identity);
        }
    }

    private void EnviromentGenerate()
    {
        WayCreator wayCreator = new WayCreator();
        wayCreator.GenerateWays(this);
        WallGenerator wallGenerator = new WallGenerator();
        wallGenerator.GenerateWalls(this);
    }

    private void GenerateLevelPlot()
    {
        plotX = Mathf.RoundToInt(currentLevelParams.levelSize.x);
        plotY = Mathf.RoundToInt(currentLevelParams.levelSize.y);

        GameObject container = new GameObject("Floor");
        container.transform.SetParent(levelContainer.transform);

        LevelItemData.ItemGeneration data = _levelPalete.GetPaleteOnName("Floor");

        for (int i = 0; i < plotX; i++)
        {
            List<int> dataString = new List<int>(); 
            GameObject containerX = new GameObject("containerX");
            containerX.transform.SetParent(container.transform);
            for (int j = 0; j < plotY; j++)
            {
                dataString.Add(data.numberOnPalete);
                Instantiate(data.prefabGeneration, new Vector3(i, 0, j), 
                    Quaternion.identity, containerX.transform);
            }
            configLevel.Add(dataString);
        }
    }

    public void SetConfigData(int xCoordination, int yCoordination, int data)
    {
       configLevel[xCoordination][yCoordination] = data;
    }

    public int GetPlotX => plotX;
    public int GetPlotY => plotY;

    public int GetCountExits => currentLevelParams.countExits;

    public int GetPaleteIndex(string name)
    {
        return _levelPalete.GetPaleteOnName(name).numberOnPalete;
    }

    public void CreateWall(Vector3 position, Vector3 rotation, int coorX, int coorY, bool roundWall)
    {
        bool floor = configLevel[coorX][coorY].Equals(_levelPalete.GetPaleteOnName("Floor").numberOnPalete);
        bool way = configLevel[coorX][coorY].Equals(-1);

        if (roundWall)
        {
            if (floor || way)
            {
                Instantiate(_levelPalete.GetPaleteOnName("Wall").prefabGeneration, 
                    position, Quaternion.Euler(rotation), levelContainer.transform);
            }
            else
            {
                Debug.LogError(coorX+"!!!" +coorY);
            }
        }
        else
        {
            if (floor)
            {
                Instantiate(_levelPalete.GetPaleteOnName("Wall").prefabGeneration, 
                    position, Quaternion.Euler(rotation), levelContainer.transform);
            }
        }
    }

    private void WriteLevel()
    {
        string l = "";
        foreach (var str in configLevel)
        {
            foreach (var VARIABLE in str)
            {
                l += VARIABLE + ",";
            }

            l += "\n";
        }
        Debug.LogError(l);
    }

    public void SpawnDoor(bool exit, int posX, int posY)
    {
        string name;
        
        if (exit)
        {
            name = "Exit";
        }
        else
        {
            name = "Enter";
            CreatePlayer(posX, posY);
        }
        
        Vector3 rotation;
        if (posX.Equals(0))
        {
            rotation = new Vector3(0, 270, 0);
        }
        else
        {
            if (posX.Equals(plotX-1))
            {
                rotation = new Vector3(0, 90, 0);
                posY += 1;
                posX += 1;
            }
            else
            {
                if (posY.Equals(0))
                {
                    rotation = new Vector3(0, 0, 0);
                }
                else
                {
                    rotation = new Vector3(0, 0, 0);
                    posY += 1;
                }
            }
        }
        
        Instantiate(_levelPalete.GetPaleteOnName(name).prefabGeneration, new Vector3(posX, 0, posY),
            Quaternion.Euler(rotation), levelContainer.transform);
    }
    
    public Transform GetLevelContainer  => levelContainer.transform;
}

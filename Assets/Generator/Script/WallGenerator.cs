using UnityEngine;

public class WallGenerator
{
    private LevelGenerator _generator;
    private int plotX;
    private int plotY;
    public void GenerateWalls(LevelGenerator generate)
    {
        _generator = generate;
        plotX = _generator.GetPlotX;
        plotY = _generator.GetPlotY;
        GenerateWallsRoundLevel();
        GenerateWallsInsideLevel();
        
    }

    private void GenerateWallsRoundLevel()
    {
        for (int i = 0; i < plotX; ++i)
        {
            _generator.CreateWall(new Vector3(i+1, 0,0), new Vector3(-90,180),i, 0, true);
            _generator.CreateWall(new Vector3(i,0, plotY), new Vector3(-90,0), i, plotY-1, true);
        }
        
        for(int i=0; i < plotY; ++i)
        {
            _generator.CreateWall(new Vector3(0,0, i), new Vector3(-90, 270), 0,i, true);
            _generator.CreateWall(new Vector3(plotX,0, i+1), new Vector3(-90, 90), plotX-1,i, true);
        }
    }

    private void GenerateWallsInsideLevel()
    {
        for(int i=1; i<plotX-1; ++i)
        {
            for (int j = 1; j < plotY-1; ++j)
            {
                GenerateRandomWall(i, j);
            }
        }
    }

    private void GenerateRandomWall(int xCoor, int yCoor)
    {
        int gen = Random.Range(0, 4);
        if (gen.Equals(0))
        {
            int angleY = Random.Range(0, 4);
            if (angleY.Equals(0) || angleY.Equals(3))
            {
                _generator.CreateWall(new Vector3(xCoor, 0, yCoor), new Vector3(-90, angleY* 90), xCoor, yCoor, false);
            }
            else
            {
                if (angleY.Equals(1))
                {
                    _generator.CreateWall(new Vector3(xCoor, 0, yCoor+1), new Vector3(-90, angleY* 90), xCoor, yCoor, false);
                }
                else
                {
                    _generator.CreateWall(new Vector3(xCoor+1, 0, yCoor+1), new Vector3(-90, angleY* 90), xCoor, yCoor, false);
                }
            }
        }
        else if (gen.Equals(2))
        {
            for (int i = 0; i < gen; ++i)
            {
                _generator.CreateWall(new Vector3(xCoor, 0, yCoor), new Vector3(-90, i*90), xCoor, yCoor, false);
            }
        }
    }
}

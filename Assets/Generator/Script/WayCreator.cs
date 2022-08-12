using System.Collections.Generic;
using UnityEngine;

public class WayCreator
{
    private List<Vector2> exitCoordinates;
    private Vector2 enterCoordinates;

    private int XPlot;
    private int YPlot;

    private LevelGenerator _generator;
    
    public void GenerateWays(LevelGenerator generator)
    {
        _generator = generator;
        XPlot = _generator.GetPlotX;
        YPlot = _generator.GetPlotY;
        GenerateDoors();

        int exitCode = _generator.GetPaleteIndex("Exit");

        foreach (var exitCoor in exitCoordinates)
        {
            int xPos = Mathf.RoundToInt(exitCoor.x);
            int yPos = Mathf.RoundToInt(exitCoor.y);
            _generator.SetConfigData(xPos, yPos, exitCode);
            CalculateWay(exitCoor);
            _generator.SpawnDoor(true, xPos, yPos);
        }
    }

    private void CalculateWay(Vector2 exitCoor)
    {
        int startX = Mathf.RoundToInt(enterCoordinates.x);
        int startY = Mathf.RoundToInt(enterCoordinates.y);

        int endX = Mathf.RoundToInt(exitCoor.x);
        int endY = Mathf.RoundToInt(exitCoor.y);

        int indexX = CalculateIndexMovement(startX, endX);
        int indexY = CalculateIndexMovement(startY, endY);
        

        while(!startX.Equals(endX) || !startY.Equals(endY))
        {
            if (startX.Equals(endX))
            {
                if (startY + indexY != endY)
                {
                    startY += indexY;
                }
                else
                {
                    break;
                }
            }
            else if (startY.Equals(endY))
            {
                if (startX + indexX != endX)
                {
                    startX += indexX;
                }
                else
                {
                    break;
                }
            }
            else
            {
                int direction = Random.Range(0, 2);
                if (direction.Equals(0))
                {
                    startX += indexX;
                }
                else
                {
                    startY += indexY;
                }
            }
           
            _generator.SetConfigData(startX, startY, -1);
        }
    }

    private int CalculateIndexMovement(int startX, int endX)
    {
        if (endX - startX != 0)
        {
            return (endX-startX)/Mathf.Abs(endX-startX);
        }
        return 0;
    }

    private void GenerateDoors()
    {
        enterCoordinates = CreateDoor();
        int xPos = Mathf.RoundToInt(enterCoordinates.x);
        int yPos = Mathf.RoundToInt(enterCoordinates.y);
        _generator.SetConfigData(xPos, yPos, _generator.GetPaleteIndex("Enter"));
        _generator.SpawnDoor(false, xPos, yPos);

        int countExits = _generator.GetCountExits;

        exitCoordinates = new List<Vector2>();
        for (int i = 0; i < countExits; i++)
        {
            Vector2 exitCoor = CreateDoor();
            if (exitCoor.Equals(enterCoordinates) || Mathf.Abs(exitCoor.x-enterCoordinates.x)<5 || Mathf.Abs(exitCoor.y-enterCoordinates.y)<5) /// В ДАННЫЕ МИН ДИСТАНЦИЮ
            {
                i--;
            }
            else
            {
                exitCoordinates.Add(exitCoor);
            }
        }
    }

    private Vector2 CreateDoor()
    {
        int direction = Random.Range(0, 2);
        
        int[] coor;

        if (direction.Equals(0))
        {
            coor = GenerateCoordinates(XPlot, YPlot);
        }
        else
        {
            coor = GenerateCoordinates(YPlot, XPlot);
        }
        Vector2 coordination = new Vector2(coor[direction], coor[Mathf.Abs(direction-1)]);
        return coordination;
    }

    private int[] GenerateCoordinates(int firstCount, int secondCount)
    {
        int first = Random.Range(0, firstCount);
        int second;
        if (first.Equals(0) || first.Equals(firstCount-1))
        {
            second = Random.Range(1, secondCount-1);
        }
        else
        {
            second = Random.Range(0, 2);
            if (!second.Equals(0))
            {
                second = secondCount-1;
            }
            else
            {
                second = 0;
            }
        }

        int[] mass = {first, second};
        return mass;
    }
}

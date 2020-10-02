using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCounter
{

    public delegate void PointCounterHelper(int value);
    public event PointCounterHelper PointEvent;

    private int points;

    public int Points
    {
        get
        {
            return points;
        }
        private set
        {
            points = value;
        }
    }

    private static PointCounter instance;
    private static object synchRoot = new object();

    protected PointCounter()
    {
        points = 0;
    }

    

    public static PointCounter GetPointCounter()
    {
        lock (synchRoot)
        {
            if (instance == null)
            {
                instance = new PointCounter();
            }
        }
        return instance;
    }

    public void AddPoints(int value)
    {
        points += value;
        if (points < 0)
        {
            points = 0;
        }
        Debug.Log("Поинты из поинт каунтера: " + points);
        PointEvent?.Invoke(points);
    }

    public void RefreshPoints()
    {
        points = 0;
        PointEvent?.Invoke(points);
    }
}

using UnityEngine;


[System.Serializable]
public class MinMax
{
    public float min;
    public float max;
    public MinMax(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public float GetLerpValue(float ratio) => Mathf.Lerp(min, max, ratio);
    public float GetRandom() => Random.Range(min, max);
}

[System.Serializable]
public class MinMaxInt
{
    
    public int min;
    public int max;
    public MinMaxInt(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    public float GetRandom()
    {
        return Random.Range(min, max + 1);
    }
}
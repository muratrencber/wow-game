using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DadLine
{
    public float threshold;
    public string key;
    public string line;
    public float duration;

    public DadLine(string line, float duration)
    {
        this.line = line;
        this.duration = duration;
        threshold = 0;
        key = "";
    }

    public static DadLine GetOptimalLine(DadLine[] lines)
    {
        if(lines == null)
            return null;
        bool isNegative = Dad.NegativeTolerance > Dad.Tolerance;
        float tolerance = isNegative ? Dad.NegativeTolerance : Dad.Tolerance;
        List<DadLine> finalLines = GetList(lines);
        if(finalLines.Count == 0)
            return null;
        finalLines.Sort((a,b) => (Mathf.Abs(a.threshold).CompareTo(Mathf.Abs(b.threshold))));
        int index = Mathf.FloorToInt((isNegative ? Dad.NegativeTolerance : Dad.Tolerance) * finalLines.Count);
        if(index > finalLines.Count - 1 || index < 0)
            return null;
        return finalLines[index];
    }

    public static List<DadLine> GetList(DadLine[] lines)
    {
        bool isNegative = Dad.NegativeTolerance > Dad.Tolerance;
        List<DadLine> finalLines = new List<DadLine>();
        for(int i = 0; i < lines.Length; i++)
        {
            DadLine l = lines[i];
            if(l.threshold != 0 && l.threshold > 0 == isNegative)
                continue;
            if(l.key != Dad.CurrentNeed)
                continue;
            finalLines.Add(l);
        }
        return finalLines;
    }
}

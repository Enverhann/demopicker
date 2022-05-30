using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreValues", menuName = "Values")]

[System.Serializable]
public class ScriptableScore : ScriptableObject
{
    public int necessaryScore;
}

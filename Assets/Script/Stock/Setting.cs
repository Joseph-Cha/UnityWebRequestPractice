using UnityEngine;

[CreateAssetMenu(fileName = "Setting", menuName = "ScriptableObjects/Setting", order = 1)]
public class Setting : ScriptableObject
{
    public int Balance;
    public string StockCode;
    public string ShowCount;
    public float k;
}
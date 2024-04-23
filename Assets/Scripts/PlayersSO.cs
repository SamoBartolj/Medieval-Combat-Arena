using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Players")]
public class PlayersSO : ScriptableObject
{
    public string playerName;
    public GameObject playerPrefab;
    public Sprite playerImage;
}

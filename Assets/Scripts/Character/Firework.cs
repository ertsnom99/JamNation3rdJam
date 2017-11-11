using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    [SerializeField]
    private GameManager.Colors fireworkColor;
    public GameManager.Colors FireworkColor
    {
        get { return fireworkColor; }
        set { fireworkColor = value; }
    }
}

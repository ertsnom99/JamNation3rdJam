using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointsManager : MonoBehaviour
{
    [Serializable]
    public class CheckPointGroupe
    {
        public CheckPoint[] checkPoints;
    }

    [SerializeField]
    private CheckPointGroupe[] checkPointGroupes;
    
    public CheckPointGroupe GetCheckPointGroupeByCheckPoint(CheckPoint checkPoint)
    {
        foreach(CheckPointGroupe checkPointGroupe in checkPointGroupes)
        {
            if (Array.IndexOf(checkPointGroupe.checkPoints, checkPoint) != -1) return checkPointGroupe;
        }

        return null;
    }
}

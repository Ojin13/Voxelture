using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemySerializable
{
    public float[] originalSpawnPosition = new float[3]; //0=x 1=y 2=z
    public float[] enemyPosition = new float[3]; //0=x 1=y 2=z
    public int ID;
    public bool isDead = false;
    public bool playerDetected;
    public string enemyName;
    public string enemyType;
    public string enemyDescription;
    public int HP;
    public int MAXHP;
    public int DMG;
    public int LEVEL;
    public int[] DROPS;
    public int EXP;
    public int spawnerID;
}

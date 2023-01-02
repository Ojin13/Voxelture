using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatureUI : MonoBehaviour
{
    public GameObject Enemy;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI HP;
    public GameObject ProgressBar;
    private float startingWidth;
    public GameObject DesiredRotation;
    public bool isCanvas;
    public static Canvas canvas;
    public bool EnemiesLoaded;
    public Image selectedIndicator;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (!isCanvas)
        {
            DesiredRotation = CameraContol.CameraObject;
            startingWidth = ProgressBar.GetComponent<RectTransform>().sizeDelta.x;
        }
        else
        {
            canvas = gameObject.GetComponent<Canvas>();
        }
        
        Invoke("CheckForInitialDestruction",1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCanvas)
        {
            Vector3 x = new Vector3(DesiredRotation.transform.eulerAngles.x, DesiredRotation.transform.eulerAngles.y, DesiredRotation.transform.eulerAngles.z);
            GetComponent<RectTransform>().rotation = Quaternion.Euler(x);
            
            if (Enemy != null)
            {
                transform.position = new Vector3(Enemy.transform.position.x,Enemy.GetComponentInChildren<SkinnedMeshRenderer>().bounds.max.y+0.5f,Enemy.transform.position.z);

                if (Enemy.GetComponent<EnemyController>().isDead)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Name.text = Enemy.GetComponent<EnemyController>().enemyName+" Lvl "+Enemy.GetComponent<EnemyController>().LEVEL;
                    HP.text = Enemy.GetComponent<EnemyController>().HP + "/" + Enemy.GetComponent<EnemyController>().MAXHP;
                    
                    float currentValue;
                    float maxValue;


                    currentValue = Enemy.GetComponent<EnemyController>().HP;
                    maxValue = Enemy.GetComponent<EnemyController>().MAXHP;
                    float currentBarValue = currentValue;
                    float maxBarValue = maxValue;
                    float barPercentage = (currentBarValue / maxBarValue) * 100;
                
                    ProgressBar.GetComponent<RectTransform>().sizeDelta = new Vector2((startingWidth/100)*barPercentage, ProgressBar.GetComponent<RectTransform>().sizeDelta.y);

                    if (PlayerController.Player.GetComponent<GetClosestEnemy>().isClosest(Enemy))
                    {
                        selectedIndicator.enabled = true;
                    }
                    else
                    {
                        selectedIndicator.enabled = false;
                    }
                }
            }
            else
            {
                if (EnemiesLoaded)
                {
                    Destroy(gameObject);
                }
            }
            
            
        }
    }

    public void CheckForInitialDestruction()
    {
        EnemiesLoaded = true;
    }
}

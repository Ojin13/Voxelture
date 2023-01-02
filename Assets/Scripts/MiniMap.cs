using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{

    public RawImage playerSymbol;
    public RawImage MapTexture;
    public Texture advancedteMap; 
    public Texture defaultMap;
    public Skill advancedMap;

    public int Xcons;
    public int Ycons;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setCorrectMap();
        
        float estimatedX = -(-120 * PlayerController.Player.transform.position.x) / Xcons;//-415;
        float estimatedY = -(550 * PlayerController.Player.transform.position.z) / Ycons;//2620;
        playerSymbol.GetComponent<RectTransform>().anchoredPosition = new Vector3(estimatedX, estimatedY,0);
    }



    public void setCorrectMap()
    {
        if (advancedMap.unlocked)
        {
            MapTexture.texture = advancedteMap;
        }
        else
        {
            MapTexture.texture = defaultMap;
        }
    }
}

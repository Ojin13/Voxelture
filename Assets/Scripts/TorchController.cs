using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TorchController : MonoBehaviour
{
    public float torchRadius;
    public bool holdingTorch;
    public GameObject torch;
    public Light light;
    public static TorchController TorchControl;
    
    void Awake()
    {
        TorchControl = this;
    }
    
    void Update()
    {

        string torchRarity = GetComponent<Equipment>().Accessory.GetComponent<ItemUI>().invItem.rarityLevel;

        switch (torchRarity)
        {
            case "Common":
                torchRadius = 15;
                break;
            case "Rare":
                torchRadius = 25;
                break;
            case "Antic":
                torchRadius = 50;
                break;
            case "Legendary":
                torchRadius = 75;
                break;
            case "God Like":
                torchRadius = 100;
                break;
            default:
                torchRadius = 15;
                break;
        }
        light.range = torchRadius;

        if (GetComponent<Equipment>().isEquipped(0,"Torch"))
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (holdingTorch)
                {
                    hideTorch();
                }
                else
                {
                    useTorch();
                }
            }
        }
    }

    public void useTorch()
    {
        holdingTorch = true;
        torch.SetActive(true);
    }
    
    public void hideTorch()
    {
        holdingTorch = false;
        torch.SetActive(false);
    }
}

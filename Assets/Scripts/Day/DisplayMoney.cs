using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayMoney : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyDisplay;
    [SerializeField] TextMeshProUGUI priceDisplay;
    [SerializeField] TextMeshProUGUI dayDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyDisplay.text = ("Money: £" + GameManager.money);
        priceDisplay.text = ("Tile Price: £" + TileBuyer.getPrice());
        dayDisplay.text = ("Day: 1");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinText : MonoBehaviour
{
    Text cointext;
    [SerializeField]
    GameObject gamecontroller;
    // Start is called before the first frame update
    void Start()
    {
        cointext = this.gameObject.GetComponent<Text>();
        if(cointext != null)
            cointext.text = "this works";
        //if(gamecontroller != null)
        //gamecontroller.GetComponent<CoinCollectionScript>();    
        gamecontroller = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if(cointext != null)
            cointext.text = "Number Of Coins: " + gamecontroller.GetComponent<CoinCollectionScript>().GetNumberOfCoins();
    }
}

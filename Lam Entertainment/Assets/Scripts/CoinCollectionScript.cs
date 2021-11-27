using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectionScript : MonoBehaviour
{
    static int numberOfCoins = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void increaseCoins()
    {
        numberOfCoins++;
        Debug.Log(numberOfCoins);
    }

    public int GetNumberOfCoins()
    {
        return numberOfCoins;
    }
}

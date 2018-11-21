using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private GameObject puntajetxt;
    private int puntos = 0;
    private int puntosPorMoneda = 10;

	// Use this for initialization
	void Start () {
        puntajetxt= GameObject.Find("puntaje");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
      
            Debug.Log("entered");
        if (other.gameObject.name.Contains("Moneda"))
        {
            Destroy(other.gameObject);
            puntos += puntosPorMoneda;
            puntajetxt.GetComponent<Text>().text = ""+puntos;
        }

    }

}

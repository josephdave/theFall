using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaciondeMonedas : MonoBehaviour {

    // Use this for initialization
    private GameObject moneda;
    private GameObject player;
    private GameObject rotatePlayer;
    private float espaciadoVertical = 1.0f;
    private float rotacion = 10f;
    private int CantidadPisos = 400;
    private int monedasPiso = 5;


    void Start () {

        moneda = GameObject.Find("baseMoneda");
        player = GameObject.Find("Player");
        rotatePlayer = GameObject.Find("Rotacion");



        crearMonedas();


    }
    public void crearMonedas()
    {
        float rango = 360f / monedasPiso;

        for (int i = 0; i < CantidadPisos; i++)
        {
            for (int j = 0; j < monedasPiso; j++)
            {
                GameObject mon = Instantiate(moneda, new Vector3(moneda.transform.position.x, moneda.transform.position.y - (i * espaciadoVertical), moneda.transform.position.z), Quaternion.identity);
                
                Vector3 euler = transform.eulerAngles;
                euler.y = moneda.transform.eulerAngles.y + Random.Range(j * rango, (j + 1) * rango);
                mon.transform.eulerAngles = euler;

                Vector3 euler2 = transform.eulerAngles;
                euler2.y = Random.Range(0, 360f);
                euler2.z = 90;

                mon.transform.GetChild(0).transform.eulerAngles = euler2;


            }
        }
    }

    public void resetGame()
    {
        player.transform.position = new Vector3(0, 0, -6);
        rotatePlayer.transform.eulerAngles = new Vector3(0, 0, 0);
        borrarMonedas();
        crearMonedas();
    }

    public void borrarMonedas()
    {
       GameObject[] monedas = GameObject.FindGameObjectsWithTag("moneda");

        foreach (GameObject moneda in monedas)
        {
            if(moneda.name!="baseMoneda")
            GameObject.Destroy(moneda);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

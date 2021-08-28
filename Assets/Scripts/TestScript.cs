using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class TestScript : MonoBehaviour 
{



    [SerializeField]public Vectors_01 test = new Vectors_01(2f, 12f);
    [SerializeField] public Vectors_01 test2 = new Vectors_01(6f, 4f);
    [SerializeField] public Vectors_01 test3 = new Vectors_01(1f, 1f);
    [SerializeField] Vectors_01 test4 = new Vectors_01(0f, 0f);

    [SerializeField][Range(0,1)]float scalar = 0.5f;


    Vectors_01 SubsVector = new Vectors_01();
    Vectors_01 midVector = new Vectors_01();
    Vectors_01 sumVector = new Vectors_01();
    // Start is called before the first frame update
    void Start()
    {


        
        Debug.Log("test");
        //Debug.Log(test.compX + " . " + test.compY);
        Debug.Log(test.ToString());


        Debug.Log("test2");
        Debug.Log(test2.ToString());


        /*Debug.Log("Test  suma test2");
        test.Addition(test2);
        Debug.Log(test.ToString());


        Debug.Log("Test  resta test2");
        test.Substraction(test2);
        Debug.Log(test.ToString());

        Debug.Log("Test  modulo");
        Debug.Log(test.Module());

        Debug.Log("Test  normalizar");
        test.Normalize();
        Debug.Log(test.ToString());

        Debug.Log("Test2  Escalar");
        test2.ScalarMultiply(2);
        Debug.Log(test2.ToString());*/

        
        Debug.Log(SubsVector.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        //test.DrawVector();
        //test.DrawVector(test3);
        //Debug.DrawLine(Vector3.zero, Vector3.one);
        test.DrawVector();
        test2.DrawVector(Color.red);
        //test2.DrawVector(test, Color.yellow);


        
        /*SubsVector = test2.Substraction2(test);
        midVector = SubsVector.ScalarMultiply2(scalar);

        SubsVector.DrawVector(Color.yellow);
        SubsVector.DrawVector(test,Color.yellow);

        midVector.DrawVector(test,Color.green);

        sumVector = test.Addition2(midVector);

        sumVector.DrawVector(Color.blue);*/


        Vectors_01 lerpTest= test.Lerp(test2, scalar);
        lerpTest.DrawVector(Color.yellow);
    }
}

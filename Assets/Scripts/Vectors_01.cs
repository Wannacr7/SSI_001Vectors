using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;





[Serializable]

public class Vectors_01 
{

    public float compX, compY;

    public Vectors_01()
    {
       
    }
    public Vectors_01(float x,float y)
    {
        compX = x;
        compY = y;
    }


    public void Addition(Vectors_01 another)
    {
        
        compX += another.compX;
        compY += another.compY;


    }
    public Vectors_01 Addition2(Vectors_01 another)
    {

        float _compX = compX + another.compX;
        float _compY = compY + another.compY;

        Vectors_01 sumVector = new Vectors_01();
        sumVector.compX = _compX;
        sumVector.compY = _compY;
        return sumVector;
    }

    public void Substraction(Vectors_01 another)
    {
        compX -= another.compX;
        compY -= another.compY;
    }
    public Vectors_01 Substraction2(Vectors_01 another)
    {
        float _compX = compX - another.compX;
        float _compY = compY - another.compY;

        Vectors_01 retVector = new Vectors_01();
        retVector.compX = _compX;
        retVector.compY = _compY;
        return retVector;
    }

    public void ScalarMultiply(float scalar)
    {
        compX *= scalar;
        compY *= scalar;
    }
    public Vectors_01 ScalarMultiply2(float scalar)
    {

        float _compX = compX;
        float _compY = compY;

        _compX *= scalar;
        _compY *= scalar;

        Vectors_01 multyVector = new Vectors_01();
        multyVector.compX = _compX;
        multyVector.compY = _compY;
        return multyVector;
    }

    public float Module()
    {
        float mod = (float)Math.Sqrt(Math.Pow(compX,2)+ Math.Pow(compY,2));
        return mod;
    }

    public void Normalize()
    {
        float _mod = Module();
        if(_mod != 0)
        {
            compX /= _mod;
            compY /= _mod;
        }
        else
        {
            compX = 0;
            compY = 0;
        }
       
    }


    public  void DrawVector()
    {
        Vector3 finalDraw = new Vector3(compX, compY, 0);
        Debug.DrawLine(Vector3.zero, finalDraw, Color.cyan);
    }
    public void DrawVector(Color _color)
    {
        Vector3 finalDraw = new Vector3(compX, compY, 0);
        Debug.DrawLine(Vector3.zero, finalDraw, _color);
    }
    public void DrawVector(Vectors_01 another)
    {
        Vector3 finalDraw = new Vector3(compX+another.compX, compY+another.compY);
        Vector3 initialDraw = new Vector3(another.compX,another.compY);
        Debug.DrawLine(initialDraw, finalDraw, Color.red);
    }
    public void DrawVector(Vectors_01 another, Color _color)
    {
        Vector3 finalDraw = new Vector3(compX + another.compX, compY + another.compY);
        Vector3 initialDraw = new Vector3(another.compX, another.compY);
        Debug.DrawLine(initialDraw, finalDraw, _color);
    }
    public Vectors_01 Lerp(Vectors_01 another, float scalar)
    {
        /*Vectors_01 firstVector = new Vectors_01();
        firstVector.compX = compX;
        firstVector.compY = compY;*/

        Vectors_01 lerpVector;
        Vectors_01 midVector;
        Vectors_01 subsVector;

        //subsVector = firstVector.Substraction2(another);
        subsVector = another.Substraction2(this);
        midVector = subsVector.ScalarMultiply2(scalar);
        lerpVector = Addition2(midVector);

        

        return lerpVector;
    }






    public override string ToString()
    {
        return ("(" + compX.ToString() + "  " + compY.ToString() + ")");
    }

    public static implicit operator Vector3(Vectors_01 v)
    {
         return new Vector2(v.compX,v.compY);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [SerializeField] Vectors_01 firstPosition;
    [SerializeField] Vectors_01 velocity;
    [SerializeField] Vectors_01 Aceleration;

    Vectors_01 sum;
    Vectors_01 rest;
    Vectors_01 tool = new Vectors_01();

    [SerializeField]GameObject target;
    [SerializeField] float maxSpeed;
    [SerializeField] bool Automatic = false;
    // Start is called before the first frame update
    void Start()
    {
        target.transform.position = new Vector2(0f, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Automatic)
        {
            firstPosition = new Vectors_01(target.transform.position.x, target.transform.position.y);
            firstPosition.DrawVector();

            sum = firstPosition.Substraction2(velocity);

            velocity.DrawVector(firstPosition, Color.red);

            target.transform.position = target.transform.position + velocity.ScalarMultiply2(Time.deltaTime);
            velocity.Addition(Aceleration.ScalarMultiply2(Time.deltaTime));


            UpdatePosition();
            CheckSpeed();
        }
        
    }
    public void UpdatePosition()
    {


        if(target.transform.position.x  >= 4.5f)
        {
            
            velocity.compX *= -1f ;
            Aceleration.compX *= -1f;
            target.transform.position = new Vector3(4.5f,0);
        }
        else if (target.transform.position.x  <= -4.5f)
        {
            velocity.compX *= -1f ;
            Aceleration.compX *= -1f;
            target.transform.position = new Vector3(-4.5f, 0);
        }
        else if (target.transform.position.y >= 4.5f)
        {
            velocity.compY *= -1f ;
            Aceleration.compY *= -1f;
            target.transform.position = new Vector3(0, 4.5f);
        }
        else if (target.transform.position.y <= -4.5f)
        {
            velocity.compY *= -1f ;
            Aceleration.compY *= -1f;
            target.transform.position = new Vector3(0, -4.5f);
        }
        
        Debug.Log("Update position");
    }

    private void CheckSpeed()
    {
        if (velocity.Module() > maxSpeed)
        {
            velocity.Normalize();
            velocity.ScalarMultiply(maxSpeed);
        }
    }
}

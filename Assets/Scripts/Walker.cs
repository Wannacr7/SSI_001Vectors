using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [SerializeField] Vectors_01 firstPosition;
    [SerializeField] Vectors_01 worldPosition;
    [SerializeField] Vectors_01 velocity;
    [SerializeField] Vectors_01 Aceleration;
    [SerializeField] Vectors_01 World;

    Vectors_01 sum;
    Vectors_01 rest;
    Vectors_01 tool = new Vectors_01();

    [SerializeField]GameObject target;
    [SerializeField] GameObject worldTarget;

    [SerializeField] float maxSpeed;
    [SerializeField] [Range(0f, 1f)] float ran;

    [SerializeField] bool Automatic = false;
    [SerializeField] bool noBorders = false;


    
    // Start is called before the first frame update
    void Start()
    {
        //target.transform.position = new Vector2(0f, 0f);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Automatic)
        {
            
            firstPosition = new Vectors_01(target.transform.position.x, target.transform.position.y);
            SetVectorInto(worldPosition, worldTarget.transform.position);
            sum = firstPosition.Substraction2(velocity);

            firstPosition.DrawVector(Color.blue);
            velocity.DrawVector(firstPosition, Color.red);
            Aceleration.DrawVector(firstPosition, Color.yellow);

            UpdatePosition();
            CheckSpeed();
        }
        
    }
    public void UpdatePosition()
    {
        target.transform.position = target.transform.position + velocity.ScalarMultiply2(Time.deltaTime);
        velocity.Addition(Aceleration.ScalarMultiply2(Time.deltaTime));
        
        Aceleration = worldPosition.Substraction2(firstPosition);

        if (!noBorders)
        {
            if (target.transform.position.x >= 4.5f)
            {

                velocity.compX *= -1f;
                //Aceleration.compX *= -1f;
                target.transform.position = new Vector3(4.5f, target.transform.position.y);

            }
            else if (target.transform.position.x <= -4.5f)
            {
                velocity.compX *= -1f;
                //Aceleration.compX *= -1f;
                target.transform.position = new Vector3(-4.5f, target.transform.position.y);

            }
            if (target.transform.position.y >= 4.5f)
            {
                velocity.compY *= -1f;
                //Aceleration.compY *= -1f;
                target.transform.position = new Vector3(target.transform.position.x, 4.5f);

            }
            else if (target.transform.position.y <= -4.5f)
            {
                velocity.compY = -velocity.compY;

                velocity.compY *= ran;

                //Aceleration.compY *= -1f;
                target.transform.position = new Vector3(target.transform.position.x, -4.5f);

            }

        }

        Debug.Log("Update position");
    }

    private void CheckSpeed()
    {
        if (Mathf.Abs(velocity.Module()) > maxSpeed)
        {
            velocity.Normalize();
            velocity.ScalarMultiply(maxSpeed);
            
        }
    }

    private void SetVectorInto(Vectors_01 a, Vector3 b)
    {
        a.compX = b.x;
        a.compY = b.y;
    }


}

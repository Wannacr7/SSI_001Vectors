using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [SerializeField] Vectors_01 firstPosition;
    [SerializeField] Vectors_01 velocity;

    Vectors_01 sum;
    Vectors_01 rest;
    Vectors_01 tool = new Vectors_01();

    [SerializeField]Transform target;
    [SerializeField] int seed;
    [SerializeField] bool Automatic = false;
    // Start is called before the first frame update
    void Start()
    {
        target.position = new Vector2(0f, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        firstPosition = new Vectors_01(target.position.x, target.position.y);
        firstPosition.DrawVector();

        sum = firstPosition.Substraction2(velocity);

        velocity.DrawVector(firstPosition,Color.red);

        UpdatePosition();
    }
    public void UpdatePosition()
    {

        target.position += velocity.ScalarMultiply2(Time.deltaTime);

        if(target.position.x  > 4.5f)
        {
            velocity.compX *= -1f;
        }
        if (target.position.x  < -4.5f)
        {
            velocity.compX *= -1f;
        }
        if (target.position.y > 4.5f)
        {
            velocity.compY *= -1f;
        }
        if (target.position.y < -4.5f)
        {
            velocity.compY *= -1f;
        }
        Debug.Log("Update position");
    }
}

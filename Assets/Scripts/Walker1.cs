using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker1 : MonoBehaviour
{
    [SerializeField] Vectors_01 firstPosition;
    //[SerializeField] Vectors_01 worldPosition;
    [SerializeField] Vectors_01 velocity;
    [SerializeField] Vectors_01 Aceleration;
    //[SerializeField] Vectors_01 World;
    [SerializeField] Vectors_01 Force;
    [SerializeField] Vectors_01 Force2;
    [SerializeField] Vectors_01 Forces;
    private Vectors_01 fluidVector;

    Vectors_01 sum;
    Vectors_01 rest;
    Vectors_01 tool = new Vectors_01();

    [SerializeField]GameObject target;
    [SerializeField] GameObject worldTarget;
    [SerializeField] GameObject fluid;

    [SerializeField] float maxSpeed;
    [SerializeField] [Range(0f, 1f)] float ran;
    [SerializeField] float mass;
    [SerializeField] float gravity;
    [SerializeField][Range(0,1)] float coeFricction;
    [SerializeField] [Range(0, 1)] float dragCoef;

    [SerializeField] bool Automatic = false;
    [SerializeField] bool noBorders = false;
    



    // Start is called before the first frame update
    void Start()
    {
        //target.transform.position = new Vector2(0f, 0f);
        fluidVector = new Vectors_01(fluid.transform.position.x, fluid.transform.position.y);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Automatic)
        {
            Forces.compX = 0;
            Forces.compY = 0;
            //AddForce(Force);
            AddForce(Force2);
            AddForce(GravityForce(gravity).ScalarMultiply2(mass));
            AddForce(SetFriction(coeFricction));
            if(target.transform.position.y <= fluidVector.compY + 5)
            {
                
                AddForce(SetResistance(dragCoef));
            } 










            //Forces = AddForce(Force).Addition2(AddForce(Force2));
            firstPosition = new Vectors_01(target.transform.position.x, target.transform.position.y);
            //SetVectorInto(worldPosition, worldTarget.transform.position);
            sum = firstPosition.Substraction2(velocity);

            firstPosition.DrawVector(Color.blue);
            velocity.DrawVector(firstPosition, Color.red);
            Aceleration.DrawVector(firstPosition, Color.yellow);

            fluidVector = new Vectors_01(fluid.transform.position.x, fluid.transform.position.y);
            Vectors_01 fluidchange = new Vectors_01(fluidVector.compX, fluidVector.compY+5);
            fluidchange.DrawVector(Color.white);
            

            UpdatePosition();
            //CheckSpeed();
        }
        
    }
    public void UpdatePosition()
    {

        Aceleration = Forces.ScalarMultiply2(1 / mass);
        velocity.Addition(Aceleration.ScalarMultiply2(Time.deltaTime));
        target.transform.position = target.transform.position + velocity.ScalarMultiply2(Time.deltaTime);
        

        //Aceleration = worldPosition.Substraction2(firstPosition);
        

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
    private bool AddForce(Vectors_01 _force)
    {

        Forces.compX += _force.compX;
        Forces.compY += _force.compY;

        return true;
    }

    private Vectors_01 GravityForce(float gravForce)
    {
        return new Vectors_01(0, gravForce);
    }

    private Vectors_01 SetFriction(float _coef)
    {
        Vectors_01 frictionVector = -_coef * 4 * velocity.Normalize2();
        Debug.Log(frictionVector);
        return frictionVector;
    }
    private Vectors_01 SetResistance(float _dragCoef)
    {
        float module = velocity.Module();
        float resistence = -.5f * 1f * Mathf.Pow(module, 2) * 1 * _dragCoef;
        Vectors_01 resintenceVector = velocity.Normalize2().ScalarMultiply2(resistence);

        return resintenceVector;
    }
    private void SetVectorInto(Vectors_01 a, Vector3 b)
    {
        a.compX = b.x;
        a.compY = b.y;
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker1 : MonoBehaviour
{

    [SerializeField] bool Automatic = false;
    [SerializeField] bool noBorders = false;

    #region Fisica
    [SerializeField] private Vectors_01 _Position;
    [SerializeField] private Vectors_01 _Velocity;
    [SerializeField] Vectors_01 _Aceleration;
    #endregion

    #region Forces
    [SerializeField] private bool firstForce = false;
    [SerializeField] private Vectors_01 Force;
    [SerializeField] private bool SecondForce = false;
    [SerializeField] private Vectors_01 Force2;
    public Vectors_01 Forces;
    #endregion

    private Vectors_01 fluidVector;
    private Vectors_01 sum;
    private Vectors_01 rest;
    private Vectors_01 tool = new Vectors_01();

    [SerializeField] float maxSpeed;
    [SerializeField] float mass;
    [SerializeField] float gravity;
    [SerializeField][Range(0,1)] float coeFricction;
    [SerializeField] [Range(0, 1)] float dragCoef;

    [SerializeField] GameObject target;
    [SerializeField] GameObject worldTarget;
    [SerializeField] GameObject fluid;




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
            if(firstForce)AddForce(Force);
            if(SecondForce)AddForce(Force2);
            AddForce(GravityForce(gravity).ScalarMultiply2(mass));
            AddForce(SetFriction(coeFricction));
            if(target.transform.position.y <= fluidVector.compY + 5) AddForce(SetResistance(dragCoef));
     










            //Forces = AddForce(Force).Addition2(AddForce(Force2));
            _Position = new Vectors_01(target.transform.position.x, target.transform.position.y);
            //SetVectorInto(worldPosition, worldTarget.transform.position);
            sum = _Position.Substraction2(_Velocity);

            _Position.DrawVector(Color.blue);
            _Velocity.DrawVector(_Position, Color.red);
            _Aceleration.DrawVector(_Position, Color.yellow);

            fluidVector = new Vectors_01(fluid.transform.position.x, fluid.transform.position.y);
            Vectors_01 fluidchange = new Vectors_01(fluidVector.compX, fluidVector.compY+5);
            fluidchange.DrawVector(Color.white);
            

            UpdatePosition();
            //CheckSpeed();
        }
        
    }
    public void UpdatePosition()
    {

        _Aceleration = Forces.ScalarMultiply2(1 / mass);
        _Velocity.Addition(_Aceleration.ScalarMultiply2(Time.deltaTime));
        target.transform.position = target.transform.position + _Velocity.ScalarMultiply2(Time.deltaTime);
        

        //Aceleration = worldPosition.Substraction2(firstPosition);
        

        if (!noBorders)
        {
            if (target.transform.position.x >= 4.5f)
            {

                _Velocity.compX *= -1f;
                //Aceleration.compX *= -1f;
                
                target.transform.position = new Vector3(4.5f, target.transform.position.y);

            }
            else if (target.transform.position.x <= -4.5f)
            {
                _Velocity.compX *= -1f;
                //Aceleration.compX *= -1f;
                
                target.transform.position = new Vector3(-4.5f, target.transform.position.y);

            }
            if (target.transform.position.y >= 4.5f)
            {
                _Velocity.compY *= -1f;
                //Aceleration.compY *= -1f;
                
                target.transform.position = new Vector3(target.transform.position.x, 4.5f);

            }
            else if (target.transform.position.y <= -4.5f)
            {
                _Velocity.compY = -_Velocity.compY;

                _Velocity.compY *= -1f;

                //Aceleration.compY *= -1f;
                
                target.transform.position = new Vector3(target.transform.position.x, -4.5f);

            }

        }

        Debug.Log("Update position");
    }

    private void CheckSpeed()
    {
        if (Mathf.Abs(_Velocity.Module()) > maxSpeed)
        {
            _Velocity.Normalize();
            _Velocity.ScalarMultiply(maxSpeed);
            
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
        Vectors_01 frictionVector = -_coef * 4 * _Velocity.Normalize2();
        Debug.Log(frictionVector);
        return frictionVector;
    }
    private Vectors_01 SetResistance(float _dragCoef)
    {
        float module = _Velocity.Module();
        float resistence = -.5f * 1f * Mathf.Pow(module, 2) * 1 * _dragCoef;
        Vectors_01 resintenceVector = _Velocity.Normalize2().ScalarMultiply2(resistence);

        return resintenceVector;
    }
    private void SetVectorInto(Vectors_01 a, Vector3 b)
    {
        a.compX = b.x;
        a.compY = b.y;
    }
    

}

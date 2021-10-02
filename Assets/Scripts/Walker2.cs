using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker2 : MonoBehaviour
{

    [SerializeField] bool Automatic = false;
    [SerializeField] bool noBorders = false;


    #region Fisica
    [SerializeField] private Vectors_01 _Position;
    [SerializeField] private Vectors_01 _Velocity;
    [SerializeField] Vectors_01 _Aceleration;
    #endregion

    #region Forces
    public Vectors_01 Forces;
    #endregion

    private Vectors_01 sum;
    private Vectors_01 rest;
    private Vectors_01 tool = new Vectors_01();
    private Vectors_01 gravitationalforce;
    private Vector3 radio;
    private Vector3 positionHere;
    private Vector3 positionOther;


    [SerializeField] bool gravitation;
    [SerializeField] public float GRAV_CONSTANT;
    [SerializeField] float maxSpeed;
    [SerializeField] public float mass;
    [SerializeField] private float massOfWorld;
    [SerializeField] float gravity;
    [SerializeField] [Range(0, 1)] float bouncing;

    //[SerializeField] GameObject target;
    [SerializeField] GameObject worldTarget;
    





    // Start is called before the first frame update
    void Start()
    {
        //GRAV_CONSTANT = 6.674f * (float)Mathf.Pow(10, -11);
        GRAV_CONSTANT = 1;
        massOfWorld = worldTarget.GetComponent<Walker2>().mass;

        UpdateRadio();

        _Position = new Vectors_01(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        _Position = new Vectors_01(gameObject.transform.position.x, gameObject.transform.position.y);

        if (Automatic)
        {
            
            
            Forces.compX = 0;
            Forces.compY = 0;
            
            
            if (gravitation) AddForce(Gratitation(massOfWorld, mass, radio));
            //radio = gameObject.transform.position - worldTarget.transform.position;


            UpdateRadio();


            sum = _Position.Substraction2(_Velocity);

            _Position.DrawVector(Color.blue);
            _Velocity.DrawVector(_Position, Color.red);
            _Aceleration.DrawVector(_Position, Color.yellow);
            Debug.DrawLine(gameObject.transform.position, worldTarget.transform.position, Color.white);


            UpdatePosition();
            CheckSpeed();
        }

    }
    public void UpdatePosition()
    {
        _Aceleration = Forces.ScalarMultiply2(1 / mass);
        _Velocity.Addition(_Aceleration.ScalarMultiply2(Time.deltaTime));
        transform.position = transform.position + _Velocity.ScalarMultiply2(Time.deltaTime);

        if (!noBorders)
        {
            if (gameObject.transform.position.x >= 4.5f)
            {
                _Velocity.compX *= -1f;
                gameObject.transform.position = new Vector3(4.5f, gameObject.transform.position.y);
            }
            else if (gameObject.transform.position.x <= -4.5f)
            {
                _Velocity.compX *= -1f;
                gameObject.transform.position = new Vector3(-4.5f, gameObject.transform.position.y);
            }
            if (gameObject.transform.position.y >= 4.5f)
            {
                _Velocity.compY *= -1f;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 4.5f);
            }
            else if (gameObject.transform.position.y <= -4.5f)
            {
                _Velocity.compY = -_Velocity.compY;
                _Velocity.compY *= bouncing;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, -4.5f);

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

    private Vectors_01 GravityForce(Vector3 _position)
    {
        return new Vectors_01(_position.x, _position.y);
    }


    private Vectors_01 Gratitation(float _mass1, float _mass2, Vector3 radio)
    {
        float module = radio.magnitude;
        float scalaComponents = (GRAV_CONSTANT * _mass1 * _mass2) / Mathf.Pow(module,2);
        Vector3 gravitation = radio.normalized * scalaComponents;
        Vectors_01 temp = new Vectors_01();
        SetVectorInto(temp, gravitation);
        Debug.Log("Lawea");
        return temp;
    }
    private void UpdateRadio()
    {
        positionHere = gameObject.transform.position;
        positionOther = worldTarget.transform.position;
        radio = positionHere - positionOther;
    }
    private void SetVectorInto(Vectors_01 a, Vector3 b)
    {
        a.compX = b.x;
        a.compY = b.y;
    }


}

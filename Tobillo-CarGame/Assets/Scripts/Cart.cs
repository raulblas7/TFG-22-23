using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    // Variables en las que guardamos las ruedas (colliders, transforms y sus giros)
    public WheelCollider[] wheels;
    public Transform[] wheels_mesh;
    public float wheel_torque = 200;
    public float brake_torque = 500;
    public float max_steerangle = 30;

    // Vector para controlar la posicion de las ruedas
    private Vector3 wheel_position;
    // Vector para controlar la rotacion de las ruedas mediante un quaternion
    private Quaternion wheel_rotation;

    // Velocidad actual del coche
    public float current_speed;

    [HideInInspector]
    public Rigidbody _rigidbody; // TODO: Posible cambio de public a private con SerailizeField

    private Vector3 savedPauseVelocity;
    private Vector3 savedPauseAngularVelocity;

    public bool isAICart;

    // Velocidad maxima a la que puede ir el coche (Seteable desde el inspector)
    public float maxSpeed = 30;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        maxSpeed = 30;
    }

    public void AccelerateCart(float v, float h, float b)
    {
        current_speed = Mathf.RoundToInt(_rigidbody.velocity.magnitude * 3.6f);

        // Aceleracion de las ruedas
        if (v > 0)
        {
            if (current_speed < maxSpeed)
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = Mathf.Clamp(v, -1f, 1f) * wheel_torque;
                }
            }
            else
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = 0;
                }
            }
        }
        // Frenado de las ruedas
        else if (v < 0)
        {
            if (current_speed > -5)
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = Mathf.Clamp(v, -1f, 1f) * wheel_torque;
                }
            }
            else
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = 0;
                }
            }
        }

        if (v == 0 && !isAICart)
        {
            b = 0.3f;
        }

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].brakeTorque = Mathf.Clamp(b, 0f, 1f) * brake_torque;
        }

        wheels[0].steerAngle = Mathf.Clamp(h, -1f, 1f) * max_steerangle;
        wheels[1].steerAngle = Mathf.Clamp(h, -1f, 1f) * max_steerangle;
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetWorldPose(out wheel_position, out wheel_rotation);
            wheels_mesh[i].position = wheel_position;
            wheels_mesh[i].rotation = wheel_rotation;
        }
    }

    public void OnPause(bool pause)
    {
        if (pause)
        {
            savedPauseVelocity = _rigidbody.velocity;
            savedPauseAngularVelocity = _rigidbody.angularVelocity;
            if (_rigidbody.isKinematic == false)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }
        else
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = savedPauseVelocity;
            _rigidbody.angularVelocity = savedPauseAngularVelocity;
        }
    }
}

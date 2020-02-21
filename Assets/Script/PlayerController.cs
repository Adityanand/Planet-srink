using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public GameObject PopUp;
    public ParticleSystem BurnEffect;
    public GameObject deathEffect;
    public GameObject BurnToAsh;
    public int Health;
    public int Points;

    private float rotation;
    private Rigidbody RBody;
    private ObjectPool Pool;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Pool = ObjectPool.instance;
        RBody = GetComponent<Rigidbody>();
        Health = 100;
        BurnEffect.Stop();
        StartCoroutine(PlayerSpeed());
    }
    IEnumerator PlayerSpeed()
    {
        moveSpeed += .05f;
        yield return new WaitForSeconds(2);
        StartCoroutine(PlayerSpeed());
    }
    void Update()
    {
        rotation = Input.GetAxisRaw("Horizontal");
        if(Health==0)
        {
            Instantiate(BurnToAsh, transform.position, transform.rotation);
            Destroy(gameObject);
            PopUp.SetActive(true);
        }
        Debug.Log("Health=" + Health);
    }

    void FixedUpdate()
    {
        RBody.MovePosition(RBody.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        Vector3 yRotation = Vector3.up * rotation * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(yRotation);
        Quaternion targetRotation = RBody.rotation * deltaRotation;
        RBody.MoveRotation(Quaternion.Slerp(RBody.rotation, targetRotation, 50f * Time.deltaTime));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Health")
        {
            if (Health != 100)
                Health += 10;
            other.gameObject.SetActive(false);
            Pool.poolDictionary["Health"].Enqueue(gameObject);
        }
        if (other.tag == "Point")
        {
            Points += 10;
            other.gameObject.SetActive(false);
            Pool.poolDictionary["Point"].Enqueue(gameObject);
        }
        if (other.tag == "Fire")
        {
            Health -= 10;
            BurnEffect.Play();
        }
        if (other.tag == "Spike")
        {
            Health = 0;
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            PopUp.SetActive(true);
        }
    }
}

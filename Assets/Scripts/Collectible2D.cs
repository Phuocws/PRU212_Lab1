using UnityEngine;

public class Collectible2D : MonoBehaviour
{

    public float rotationSpeed = 0.5f;
    public GameObject onCollectEffect;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
         // Check if the other object has a PlayerController2D component
        if (other.GetComponent<RoombaController>() != null) {
            
            // Destroy the collectible
            Destroy(gameObject);

            // Instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);
            //Destroy(onCollectEffect, 1f); // Destroy the effect after 2 seconds
		}
    }
}



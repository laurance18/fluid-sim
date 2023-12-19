using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class FluidSim : MonoBehaviour
{

    public Sprite circleSprite;
    private GameObject circle;

    float particleSize = 1f;
    public float gravity;

    Vector2 velocity;
    Vector2 position;

    void Start()
    {
        DrawCircle(position, particleSize, Color.cyan);
    }

    void Update()
    {
        velocity += Vector2.down * gravity * Time.deltaTime;
        position += velocity * Time.deltaTime;
        ResolveCollisions();   

        DrawCircle(position, particleSize, Color.cyan);
    }
    void DrawCircle(Vector2 position, float particleSize, Color color)
    {
        // Check if the circle object already exists
        if (circle == null)
        {
            // Create a new circle object
            circle = new GameObject("Circle");

            // Add a SpriteRenderer component to the circle object
            SpriteRenderer spriteRenderer = circle.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = circleSprite;

            // Set the color of the circle
            spriteRenderer.color = color;

            // Set the size of the circle
            circle.transform.localScale = new Vector2(particleSize, particleSize);
        }

        // Set the position of the circle
        circle.transform.position = new Vector2(position.x, position.y);
    }

    void ResolveCollisions()
    {
        SpriteRenderer renderer = circle.GetComponent<SpriteRenderer>();
        Vector2 boundsSize = new Vector2(renderer.bounds.size.x, renderer.bounds.size.y);
        Vector2 halfBoundsSize = (boundsSize / 2);


        float collisionDamping = 0.7f;

        // Get the screen bounds
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // Check if the circle is outside the screen bounds in the x direction
        if (position.x + halfBoundsSize.x > screenBounds.x)
        {
            position.x = screenBounds.x - halfBoundsSize.x;
            velocity.x *= -collisionDamping;
        }
        else if (position.x - halfBoundsSize.x < -screenBounds.x)
        {
            position.x = -screenBounds.x + halfBoundsSize.x;
            velocity.x *= -collisionDamping;
        }

        // Check if the circle is outside the screen bounds in the y direction
        if (position.y + halfBoundsSize.y > screenBounds.y)
        {
            position.y = screenBounds.y - halfBoundsSize.y;
            velocity.y *= -collisionDamping;
        }
        else if (position.y - halfBoundsSize.y < -screenBounds.y)
        {
            position.y = -screenBounds.y + halfBoundsSize.y;
            velocity.y *= -collisionDamping;
        }

    }

}


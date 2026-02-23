using UnityEngine;

public class CoinUpAndDown : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    public float Amplitude = 1.0f;
    private Vector3 StartPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float VerticalMovement = Mathf.Sin(Time.time * MoveSpeed) * Amplitude;

        Vector3 NewPosition = StartPos + Vector3.up * VerticalMovement;
        transform.position = NewPosition;
    }
}

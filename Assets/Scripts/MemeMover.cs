using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemeMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float changeDirectionInterval;
    [SerializeField] private float stopDuration;

    private Vector3 direction;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(MoveRandomly());
    }

    private IEnumerator MoveRandomly()
    {
        while (true)
        {
            direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;

            float timer = 0f;
            while (timer < changeDirectionInterval)
            {
                MoveInCameraBounds();
                timer += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(stopDuration);
        }
    }

    private void MoveInCameraBounds()
    {
        Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float objectWidth = GetComponent<Renderer>().bounds.size.x;
        float objectHeight = GetComponent<Renderer>().bounds.size.y;

        newPosition.x = Mathf.Clamp(newPosition.x, -cameraWidth / 2 + objectWidth / 2, cameraWidth / 2 - objectWidth / 2);
        newPosition.y = Mathf.Clamp(newPosition.y, -cameraHeight / 2 + objectHeight / 2, cameraHeight / 2 - objectHeight / 2);
        transform.position = newPosition;
    }
}

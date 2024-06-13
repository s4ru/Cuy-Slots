using UnityEngine;

public class GuineaPig : MonoBehaviour
{
    public float speed = 5f;
    public int index; // Index del conejillo de indias
    private bool running = false;
    private GameManager gameManager;

    // Parámetros
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (running)
        {
            MoveAlongTrack();
        }
    }

    private void MoveAlongTrack()
    {
        // Si el Cuy llega al waypoint actual, pasa al siguiente
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                running = false;
                gameManager.FinishRace(this);
                return;
            }
        }

        // Mover hacia el waypoint actual
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);
    }

    public void StartRunning()
    {
        running = true;
        currentWaypointIndex = 0; // Reiniciar el índice del waypoint
    }

    public void ResetPosition()
    {
        running = false;
        transform.position = waypoints[11].position; // Volver al inicio de la pista
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; // Ajustar la velocidad del conejillo de indias
    }
}

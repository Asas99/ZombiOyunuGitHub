using UnityEngine;

public class OpenDoor : MonoBehaviour
{
   // public Animator animator;
    public GameObject Door;
    public int Iterations,MaxIterations;
    public float TurnSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Iterations = MaxIterations;
       // animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Door != null)
        {
            if (Iterations < MaxIterations)
            {
                Door.transform.Rotate(0, TurnSpeed, 0);
                Iterations++;
            }
        }
    }

    public void OnMouseDown()
    {
        if(Iterations == MaxIterations)
        {   
            TurnSpeed = -TurnSpeed;
            Iterations = 0;
        }
    }
}

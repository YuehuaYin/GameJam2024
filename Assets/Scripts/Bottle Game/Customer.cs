using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using Random = System.Random;

public abstract class Customer : MonoBehaviour
{
    [SerializeField]
    private MeshFilter meshFilter;
    private Mesh mesh;

    protected BottleGame bottleGame;

    [SerializeField]
    private CustomerTimer timer;

    public int targetLiquid = 30;
    public int currectLiquid;
    
    public float time;
    public float maxtime = 20;

    public bool start;

    private Random random = new Random();

    [SerializeField]
    protected GameObject bigDaddyWholeThing;

    [SerializeField] private AudioClip arrive;
    [SerializeField] protected AudioClip fill1;
    [SerializeField] protected AudioClip fill2;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        mesh = new Mesh();
        
        Vector3[] verticies = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        verticies[0] = new Vector3(-0.283f, -0.49f);
        verticies[1] = new Vector3(-0.283f, -0.49f);
        verticies[2] = new Vector3(0.283f, -0.49f);
        verticies[3] = new Vector3(0.283f, -0.49f);

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = verticies;
        mesh.uv = uv;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;

        bottleGame = GameObject.Find("BottleGame Quadrent").GetComponent<BottleGame>();
    }

    private void FixedUpdate()
    {
        if (currectLiquid < targetLiquid && start)
        {
            if (time < maxtime)
            {
                time += Time.deltaTime;
                timer.updateTimer(time, maxtime);
            }
            else
            {
                fail();
            }
        }
    }

    protected void AddLiquid()
    {
        float x = ((float) currectLiquid / targetLiquid) * (0.45f - 0.283f);
        float y = ((float) currectLiquid / targetLiquid) *(0.26f + 0.49f);

        Vector3[] vertices = new Vector3[4];
        
        vertices[0] = new Vector3(-0.283f, -0.49f);
        vertices[1] = new Vector3(-x - 0.283f, y - 0.49f);
        vertices[2] = new Vector3(x + 0.283f, y - 0.49f);
        vertices[3] = new Vector3(0.283f, -0.49f);

        mesh.vertices = vertices;
        
        meshFilter.mesh = mesh;
        
        PlayFill();
    }

    public virtual void Fill()
    {
        if (currectLiquid < targetLiquid)
        {
            currectLiquid++;
            AddLiquid();
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);
    }

    protected virtual void fail()
    {
    }

    private void PlayFill()
    {
        if (!audioSource.isPlaying)
        {
            if (random.NextDouble() > 0.5)
            {
                audioSource.clip = fill1;
            }
            else
            {
                audioSource.clip = fill2;
            }

            audioSource.pitch = (float) (0.95 + (random.NextDouble() * 0.1));
            audioSource.Play();
        }
    }

    private void PourMeADrinkBartender()
    {
        audioSource.clip = arrive;
        
        audioSource.pitch = (float) (0.95 + (random.NextDouble() * 0.1));
        audioSource.Play();
    }

    public void begin()
    {
        start = true;
        PourMeADrinkBartender();
    }
}

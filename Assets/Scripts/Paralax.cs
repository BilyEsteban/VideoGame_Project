using UnityEngine;

public class Paralax : MonoBehaviour
{

    public float parallaxPlayer = 0.1f;
    private Transform player;
    private Material parallaxMaterial;
    private float lastPlayerX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parallaxMaterial = GetComponent<Renderer>().material;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPlayerX = player.position.x;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     float deltaX = player.position.x - lastPlayerX;
    //     parallaxMaterial.mainTextureOffset += new Vector2(deltaX * parallaxPlayer, 0f);
    //     lastPlayerX = player.position.x;

        
    // }

    void LateUpdate()
    {
        float deltaX = player.position.x - lastPlayerX;

        Vector2 offset = parallaxMaterial.mainTextureOffset;

        offset.x += deltaX * parallaxPlayer;

        parallaxMaterial.mainTextureOffset = offset;

        lastPlayerX = player.position.x;
    }
}

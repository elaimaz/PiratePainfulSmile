using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private Sprite[] explosionSprites;
    private int currentIndex = 0;
    private float timer;
    [SerializeField]
    private float animationRate = 0.1f;
    private bool runAnimation = true;

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= animationRate && runAnimation != false)
        {
            timer -= animationRate;
            GetComponentInChildren<SpriteRenderer>().sprite = explosionSprites[currentIndex];
            currentIndex = (currentIndex + 1);
            if (currentIndex >= explosionSprites.Length)
                runAnimation = false;
        }
        else if (timer >= animationRate && runAnimation == false)
        {
            Destroy(gameObject);
        }
    }
}

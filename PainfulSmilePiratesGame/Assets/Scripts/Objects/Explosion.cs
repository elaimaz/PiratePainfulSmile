using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private Sprite[] explosionSprites = null;
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
            currentIndex++;
            if (currentIndex >= explosionSprites.Length)
                runAnimation = false;
            else
                GetComponentInChildren<SpriteRenderer>().sprite = explosionSprites[currentIndex];
        }
        else if (timer >= animationRate && runAnimation == false)
        {
            Destroy(gameObject);
        }
    }
}

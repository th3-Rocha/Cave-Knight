using UnityEngine;

public class Effector : MonoBehaviour
{
    public float selfDestructTime = 5f;
    public Animator animator;
    public string selectedAnimation;
    private SpriteRenderer spriteRenderer;
    public string[] animationNames;
    public bool mirrorX;
    private void Reset()
    {
        
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        PopulateAnimationList();
    }


    void PopulateAnimationList()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator component not found.");
            return;
        }

        // Get all animations from the Animator
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        // Populate the animationNames array with the names of all animations
        animationNames = new string[clips.Length];
        for (int i = 0; i < clips.Length; i++)
        {
            animationNames[i] = clips[i].name;
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null || string.IsNullOrEmpty(selectedAnimation))
        {
            Debug.LogWarning("Animator or selectedAnimation not set. Cannot play animation.");
            return;
        }

        // Play the selected animation
        animator.Play(selectedAnimation);

        Invoke("DestroyGameObject", selfDestructTime);
    }
    void Update(){
        if(!mirrorX){
            Vector3 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;

        }

    }
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}

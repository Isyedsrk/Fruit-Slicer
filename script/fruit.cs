
using UnityEngine;

public class fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffect;

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position , float force)
   {

    FindObjectOfType<GameManager>().IncreaseScore();
    whole.SetActive(false);
    sliced.SetActive(true);

    fruitCollider.enabled = false;
    juiceParticleEffect.Play();

    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    sliced.transform.rotation = Quaternion.Euler(0f , 0f , angle);

    Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

    foreach(Rigidbody slice in slices)
    {
        slice.velocity = fruitRigidbody.velocity;
        slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
    }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) {
            blade blade = other.GetComponent<blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }

}

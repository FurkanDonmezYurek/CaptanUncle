using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed; // Bulutun hareket hızı
    public float loopRange;
    private Vector3 endPoint;

    private void Start()
    {
        endPoint = transform.position + new Vector3(-loopRange, 0f, 0f);
    }

    void Update()
    {
        if (this.gameObject.tag == "SmallCloud")
        {
            // Bulutu sağa doğru hareket ettir
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            // Bulut ekranın dışına çıktığında tekrar başa al
            if (transform.position.x < endPoint.x) // Ekrandan taşan bir değer seçebilirsiniz
            {
                Destroy(this.gameObject);
            }
        }
        if (this.gameObject.tag == "BigCloud")
        {
            this.gameObject.GetComponent<SpriteRenderer>().size += new Vector2(0.002f, 0f);
        }
    }
}

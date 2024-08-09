using UnityEngine;

interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    //Etkileşim noktası
    public Transform InteractCheck;

    //Etkileşim Alan Büyüklüğü
    public float InteractRadius;

    //Etkileşime girilecek objenin layeri
    public LayerMask InteractLayer;

    void Update()
    {
        Interaction();
    }

    public void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Etkileşim alanı
            var interactObj = Physics2D.OverlapCircle(
                InteractCheck.position,
                InteractRadius,
                InteractLayer
            );

            if (interactObj == null)
            {
                Debug.Log("null");
            }
            else
            { //Objenin içinde IInteractable interfacesi var mı diye bakar
                if (interactObj.gameObject.TryGetComponent(out IInteractable intObj))
                {
                    intObj.Interact();
                }
            }
        }
    }

    //Etkileşime girilebilen alanı yeşille gösterir
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(InteractCheck.position, InteractRadius);
    }

    public void InteractionButton()
    {
        //Etkileşim alanı
        var interactObj = Physics2D.OverlapCircle(
            InteractCheck.position,
            InteractRadius,
            InteractLayer
        );

        if (interactObj == null)
        {
            Debug.Log("null");
        }
        else
        { //Objenin içinde IInteractable interfacesi var mı diye bakar
            if (interactObj.gameObject.TryGetComponent(out IInteractable intObj))
            {
                intObj.Interact();
            }
        }
    }
}

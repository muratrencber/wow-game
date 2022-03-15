using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DadReceiver : MonoBehaviour
{
    [SerializeField] LayerMask _validLayers;
    [SerializeField] Vector2 _size, _offset;

    void Update()
    {
        Vector2 center =(Vector2)transform.position + _offset;
        Vector3 tdSize = new Vector3(_size.x, _size.y, Mathf.Infinity);
        Collider2D[] colls = Physics2D.OverlapAreaAll(center + _size * -.5f, center + _size * .5f, _validLayers);
        foreach(Collider2D coll in colls)
        {
            IDadItem dadItem = coll.GetComponent<IDadItem>();
            if(dadItem == null)
                dadItem = coll.GetComponentInChildren<IDadItem>();
            if(dadItem == null)
                dadItem = coll.GetComponentInParent<IDadItem>();
            if(dadItem == null || !dadItem.AvailableForConsumption)
                continue;
            dadItem.OnConsumption();
            //SEND DAD MESSAGE
            break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + (Vector3)_offset, _size);
    }
}

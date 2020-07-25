using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    /// <summary>
    /// Prevent the Player Dragger from interacting with this.
    /// </summary>
    public bool Locked = false;

    public PurchaseStack Purchase = null;
    CardManager _manager = null;

    static float HeightAboveBoard = 3.0f;

    [SerializeField]
    public const float Width = 2.0f;
 

    public Dropzone _parent = null;
    public Dropzone Parent {
        get { return _parent; }
        set
        {
            if (value) {
                if (_parent == null)
                {
                    _parent = value;
                }
                else
                {
                    _parent.RemoveDraggable(this);
                    _parent = value;
                }
                //_parent.Reorganize();
            }
        }
    }

    bool _selected = false;
    public bool Selected { get { return _selected; }
        set {
            if (_selected == false && value == true)
            {
                OldSpot = transform.position;
            }
            else if (_selected == true && value == false)
            {
                if (Parent) { Parent.Reorganize(); }
                else if (_manager && _manager.Purchase) { _manager.Purchase.Redisplay(); }
            }
            _selected = value;
        }
    }

    Vector3 OldSpot = Vector3.zero;
    public Vector3 MoveTarget = Vector3.zero;

    public void MoveToTarget()
    {
        transform.position = MoveTarget + new Vector3(0, HeightAboveBoard, 0);
    }

    private void Start()
    {
        _manager = GetComponent<CardManager>();
    }
}

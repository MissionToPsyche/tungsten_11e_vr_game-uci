using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class RockPieceBrokenEvent : UnityEvent { }

public class RockController : MonoBehaviour
{
    public List<RockPieceControler> basePieces;
    public List<RockPieceControler> targetPieces;
    public RockPieceBrokenEvent RockPieceBroken;

    private XRGrabInteractable gip;

    private Dictionary<GameObject, int> dictBFS = new Dictionary<GameObject, int>();
    private Queue<GameObject> frontier = new Queue<GameObject>();

    private Transform originalParent;

    private void Start()
    {
        gip = GetComponent<XRGrabInteractable>();
        originalParent = transform.parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider thisCollider = collision.GetContact(0).thisCollider;
        Collider otherCollider = collision.GetContact(0).otherCollider;
        if (thisCollider.transform.parent != null && otherCollider.gameObject.CompareTag("PickHead") && collision.relativeVelocity.magnitude > 2)
        {
            BreakPiece(thisCollider.gameObject);
        }
    }

    void BreakPiece(GameObject piece)
    {
        if (piece.transform.parent != transform)
        {
            return;
        }

        gip.interactionManager.UnregisterInteractable(gip.GetComponent<IXRInteractable>());

        ParticleSystem ps = piece.GetComponent<ParticleSystem>();
        ps.Play();

        AudioSource audioSource = piece.GetComponent<AudioSource>();
        audioSource.Play();

        Unparent(piece);

        RockPieceBroken.Invoke();

        RockPieceControler rpc = piece.GetComponent<RockPieceControler>();
        dictBFS.Clear();
        frontier.Clear();
        for (int i = 0; i < rpc.neighbors.Count; i++)
        {
            GameObject neighbor = rpc.neighbors[i];
            dictBFS.Add(neighbor, i);
            frontier.Enqueue(neighbor);
        }
        BFS();

        gip.interactionManager.RegisterInteractable(gip.GetComponent<IXRInteractable>());
    }

    void Unparent(GameObject obj)
    {
        obj.transform.parent = originalParent;
        gip.colliders.Remove(obj.GetComponent<MeshCollider>());

        obj.gameObject.AddComponent<Rigidbody>();
        obj.gameObject.AddComponent<XRGrabInteractable>();

        RockPieceControler rpc = obj.GetComponent<RockPieceControler>();
        if (targetPieces.Contains(rpc))
        {
            rpc.isPersistant = true;
        }
        
        foreach (var neighbor in rpc.neighbors)
        {
            if (neighbor != null) neighbor.GetComponent<RockPieceControler>().neighbors.Remove(obj);
        }
    }

    void BFS()
    {
        // While we have items in frontier and we don't have an overall piece.
        while (frontier.Count > 0 && dictBFS.Values.Distinct().Count() != 1)
        {
            GameObject piece = frontier.Dequeue();
            int sourcePool = dictBFS[piece];
            RockPieceControler rpc = piece.GetComponent<RockPieceControler>();
            foreach (var neighbor in rpc.neighbors)
            {
                // If we haven't seen it before, add to frontier and set pool
                if (dictBFS.TryAdd(neighbor, sourcePool))
                {
                    frontier.Enqueue(neighbor);
                }
                // If we have seen it before, check if it's from a different pool
                else
                {
                    // If it is from a different pool, conduct a merge
                    int otherPool = dictBFS[neighbor];
                    if (sourcePool != otherPool)
                    {
                        foreach (var key in dictBFS.Keys.ToList())
                        {
                            if (dictBFS[key] == otherPool) dictBFS[key] = sourcePool;
                        }
                    }
                }
            }
        }
        List<int> pools = dictBFS.Values.Distinct().ToList();
        // If we have one pool, return.
        if (pools.Count <= 1) return;
        // Pulled from https://stackoverflow.com/questions/355945/find-the-most-occurring-number-in-a-listint
        int largestPool = dictBFS.Values.GroupBy(i => i).OrderByDescending(grp => grp.Count())
                            .Select(grp => grp.Key).First();
        foreach (GameObject obj in dictBFS.Keys) {
            int pool;
            if (dictBFS.TryGetValue(obj, out pool)) {
                if (pool == largestPool) {
                    continue;
                }
            }
            Unparent(obj);
        }
    }

    public void Explode()
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform) {
            children.Add(child);
        }
        children.ForEach(delegate (Transform child)
        {
            child.parent = originalParent;
            Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
            rb.AddExplosionForce(10.0f, transform.position, 0.0f, 3.0f);
        });
        Destroy(gameObject);
    }
}

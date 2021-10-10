using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private GameObject player = null;

    private Animator animator = null;

    [SerializeField] Transform leftRaycastPoint = null;
    [SerializeField] Transform rightRaycastPoint = null;

    private IAstarAI pathfinding = null;
    private AIDestinationSetter destinationSetter = null;
    [SerializeField] private float attackRadius = 10f;
    private bool withinRadius = false;

    [SerializeField] private int enemyGridIndex = 0;
    private BasePlayer playerInfo = null;
    public bool playerIsInGrid = false;

    private Vector2 rayDir;
    private float rayLength;
    private string layerMask = "Walls";

    void Start()
    {
        player = GameObject.Find("Player");
        destinationSetter = GetComponent<AIDestinationSetter>();
        destinationSetter.target = player.transform;
        pathfinding = GetComponent<AILerp>();
        pathfinding.isStopped = true;
        playerInfo = player.GetComponent<BasePlayer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator == null)
        {
            //Debug.Log("Animator missing!");
            return;
        }
        if (pathfinding == null)
        {
            Debug.Log("No pathfinding script was found!");
            return;
        }     

        if (pathfinding.isStopped) animator.SetBool("IsMoving", false);
        else animator.SetBool("IsMoving", true);

        animator.SetFloat("Horizontal", pathfinding.velocity.x);
        animator.SetFloat("Vertical", pathfinding.velocity.y);

        if (playerInfo.currentGridIndex == enemyGridIndex) playerIsInGrid = true;
        else playerIsInGrid = false;

        withinRadius = (player.transform.position - gameObject.transform.position).sqrMagnitude < attackRadius * attackRadius;
    }

    void FixedUpdate()
    {
        if (pathfinding == null)
        {
            Debug.Log("No pathfinding script was found!");
            return;
        }
        if (leftRaycastPoint == null || rightRaycastPoint == null)
        {
            Debug.Log("Raycast point(s) missing!");
            return;
        }

        if (playerIsInGrid && withinRadius)
        {
            rayDir = (pathfinding.destination - transform.position).normalized;
            rayLength = Vector2.Distance(pathfinding.destination, transform.position);

            RaycastHit2D hitsFromLeft = Physics2D.Raycast(leftRaycastPoint.position, rayDir, rayLength, LayerMask.GetMask(layerMask));
            RaycastHit2D hitsFromRight = Physics2D.Raycast(rightRaycastPoint.position, rayDir, rayLength, LayerMask.GetMask(layerMask));

            if (hitsFromLeft.collider != null || hitsFromRight.collider != null) // if there are obstacles, start moving
            {
                pathfinding.isStopped = false;
            }
            else // if there are no obstacles, stop moving
            {
                pathfinding.isStopped = true;           
            }
        }
        else
        {
            pathfinding.isStopped = true;
        }
    }

    public bool CanEnemyShoot()
    {
        return playerIsInGrid && withinRadius;
    }
}

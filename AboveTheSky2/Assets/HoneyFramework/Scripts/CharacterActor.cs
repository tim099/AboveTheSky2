using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

namespace HoneyFramework
{
    /*
     * simple structure so store data required by unit to move
     */
    struct MovementData
    {
        //path data which unit need to follow
        public List<Vector3> path;
        //laziness defines offset for unity to start later tan order received. this way formations would move irregularly
        public float laziness;
    }

    /*
     * Character version of the actor which have ability to animate from one position to another. Works well with mechanim
     */
    public class CharacterActor : Actor
    {
        public Formation parent;
        public float speed = 2.5f;
        Coroutine currentAction;
        Animator animator;

        /// <summary>
        /// Create character instance and scale it. Scale modifies size and speed of the character.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        static public CharacterActor CreateCharacter(string path, float scale)
        {
            GameObject source = Resources.Load(path, typeof(GameObject)) as GameObject;
            if (source == null)
            {
                Debug.LogError("Missing resource: " + path);
            }
            GameObject displayObject = GameObject.Instantiate(source) as GameObject;
            if (displayObject == null) return null;

            displayObject.transform.parent = World.instance.transform;
            displayObject.transform.localScale = Vector3.one * scale;

            CharacterActor a = displayObject.AddComponent<CharacterActor>();
            a.speed  *= scale;

            return a;
        }

        /// <summary>
        /// finds animator and sets its basic state
        /// </summary>
        /// <returns></returns>
        void Start()
        {
            animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("Alive", true);
            }
        }

        /// <summary>
        /// Asks actor to animate through path. 
        /// </summary>
        /// <param name="pathData">path to travel through</param>
        /// <param name="laziness">delay to execute path</param>
        /// <returns></returns>
        public void AnimateViaPath(List<Vector3> pathData, float laziness)
        {
            if (currentAction != null)
            {
                //In unity 4.6 or later change it to:
                //StopCoroutine(currentAction);
                StopCoroutine("Movement");
            }
            MovementData md = new MovementData();
            md.path = pathData;
            md.laziness = laziness;

            currentAction = StartCoroutine("Movement", md);
        }

        /// <summary>
        /// Coroutine which handles update of the position during character actor animation
        /// </summary>
        /// <param name="pathData"></param>
        /// <returns></returns>
        private IEnumerator Movement(MovementData pathData)
        {
            //wait for the laziness to ware off
            while (true)
            {
                if (pathData.laziness <= 0f) break;

                pathData.laziness -= Time.deltaTime;
                yield return null;
            }
            //start animation of the character into movement 
            if (animator != null)
            {
                animator.SetFloat("Movement", speed);
            }

            //movement loop
            List<Vector3> navPath = pathData.path;
            int pathIndex = 1;
            while (pathIndex < navPath.Count)
            {
                Vector3 start = transform.localPosition;
                start.y = 0;
                float moveRange = Time.deltaTime * speed;
                Vector3 position = start;
                Vector3 direction = Vector3.zero;
                Vector3 target = Vector3.zero;

                //movement will continue over many control points if after reaching one point character still have some spare movement range
                while (moveRange > 0f)
                {
                    if (pathIndex < navPath.Count)
                    {
                        target = navPath[pathIndex];
                        direction = target - position;
                    }
                    else
                    {
                        moveRange = 0f;
                        break;
                    }

                    float distance = direction.magnitude;

                    if (distance <= moveRange)
                    {
                        position = target;
                        moveRange -= distance;
                        pathIndex++;
                    }
                    else
                    {
                        position += direction.normalized * moveRange;
                        moveRange = 0f;
                    }
                }

                SetWorldPosition(position);                

                //if movement direction was non 0 we would like to rotate body to movement direction
                direction.y = 0;
                if (direction != Vector3.zero)
                {
                    Quaternion lookAt = Quaternion.LookRotation(direction);
                    transform.rotation = lookAt;
                }

                if (moveRange > 0f)
                {
                    //destination reached!
                    break;
                }

                yield return null;
            }

            //animation finished. Inform animation about this change and ensure character is looking at the right direction
            currentAction = null;
            if (animator != null)
            {
                animator.SetFloat("Movement", 0f);
            }
            Quaternion finalLookAt = Quaternion.LookRotation(parent.direction);
            transform.rotation = finalLookAt;
        }
    }
}
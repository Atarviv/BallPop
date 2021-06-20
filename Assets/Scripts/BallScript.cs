using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Transform targetPos;
    public List<Color> Colors;
    public Color color;
    // Start is called before the first frame update
    void Start()
        {
        color = Colors[Random.Range(0, Colors.Count)];
        GetComponent<SpriteRenderer>().color = color;
        }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator NewLocation(float yPos)
        {
        while (transform.position.y - yPos > 0.01f)
            {
            transform.Translate(0, -5*Time.deltaTime, 0);
            yield return new WaitForSeconds(0);
            }
        }
}

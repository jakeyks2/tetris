using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public Grid grid;

    public float moveInterval;

    private Rigidbody2D rb;

    private float moveTimer = 0.0f;

    private bool isOverEdge = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Move"))
        {
            Vector3 oldPos = rb.position;
            Vector3Int cellPos = grid.LocalToCell(transform.localPosition);
            Vector3Int movement = new((int)Input.GetAxisRaw("Move"), 0, 0);
            cellPos += movement;
            rb.MovePosition(grid.CellToLocalInterpolated(cellPos + grid.GetLayoutCellCenter()));
            if (isOverEdge)
            {
                rb.MovePosition(oldPos);
            }
        }

        if (Input.GetButtonDown("Rotate"))
        {
            transform.Rotate(0.0f, 0.0f, -90.0f);
        }

        moveTimer += Time.deltaTime;

        if (moveTimer > moveInterval)
        {
            moveTimer = 0.0f;
            Vector3Int cellPos = grid.LocalToCell(transform.localPosition);
            cellPos += new Vector3Int(0, -1, 0);
            rb.MovePosition(grid.CellToLocalInterpolated(cellPos + grid.GetLayoutCellCenter()));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("out2");
        if (collision.gameObject.name == "GridEdge")
        {
            isOverEdge = true;
        }
    }
}

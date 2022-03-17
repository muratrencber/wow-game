using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    [SerializeField] Transform bookContainer;
    public static Vector3[] bookPositions;
    public static int CurrentBookCount;
    public static BookOnDesk[] books;

    void Awake()
    {
        CurrentBookCount = 0;
        bookPositions = new Vector3[bookContainer.childCount];
        books = new BookOnDesk[bookContainer.childCount];
        for(int i = 0; i < bookContainer.childCount; i++)
        {
            Transform bookt = bookContainer.GetChild(i);
            bookPositions[i] = bookt.position;
            books[i] = bookt.GetComponent<BookOnDesk>();
            books[i].index = i;
            bookt.gameObject.SetActive(false);
        }
    }

    public static void AddedNewBook()
    {
        CurrentBookCount++;
        for(int i = 0; i < books.Length; i++)
        {
            books[i].gameObject.SetActive(i < CurrentBookCount);
        }
    }

    public static void RemovedLastBook()
    {
        CurrentBookCount--;
        for(int i = 0; i < books.Length; i++)
        {
            books[i].gameObject.SetActive(i < CurrentBookCount);
        }
    }
}

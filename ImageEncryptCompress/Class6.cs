using System;
using System.Collections.Generic;

public class CircularQueue
{
    private int size, front, rear;
    public List<int> queue = new List<int>();

    public CircularQueue()
    {
        this.size = 0;
        this.front = this.rear = -1;
    }

    public int Size()
    {
        return size;
    }

    public void Enqueue(int data)
    {
        if ((front == 0 && rear == size - 1) || (rear == (front - 1) % (size - 1)))
        {
            Console.WriteLine("Queue is Full");
        }
        else if (front == -1)
        {
            front = 0;
            rear = 0;
            queue.Add(data);
        }
        else if (rear == size - 1 && front != 0)
        {
            rear = 0;
            queue[rear] = data;
        }
        else
        {
            rear = (rear + 1) % size;
            if (front <= rear)
            {
                queue.Add(data);
            }
            else
            {
                queue[rear] = data;
            }
        }
    }

    public int Dequeue()
    {
        int temp;
        if (front == -1)
        {
            Console.WriteLine("Queue is Empty");
            return -1;
        }
        temp = queue[front];
        if (front == rear)
        {
            front = -1;
            rear = -1;
        }
        else if (front == size - 1)
        {
            front = 0;
        }
        else
        {
            front = front + 1;
        }
        return temp;
    }

    public int Peek()
    {
        if (front == -1)
        {
            Console.WriteLine("Queue is Empty");
            return -1;
        }
        return queue[front];
    }

    public int ElementAt(int index)
    {
        if (index < 0 || index >= size || front == -1)
        {
            Console.WriteLine("Invalid index or Queue is Empty");
            return -1;
        }
        return queue[(front + index) % size];
    }

    public void DisplayQueue()
    {
        if (front == -1)
        {
            Console.WriteLine("Queue is Empty");
            return;
        }
        Console.Write("Elements in the circular queue are: ");
        if (rear >= front)
        {
            for (int i = front; i <= rear; i++)
            {
                Console.Write(queue[i] + " ");
            }
            Console.WriteLine();
        }
        else
        {
            for (int i = front; i < size; i++)
            {
                Console.Write(queue[i] + " ");
            }
            for (int i = 0; i <= rear; i++)
            {
                Console.Write(queue[i] + " ");
            }
            Console.WriteLine();
        }
    }
}

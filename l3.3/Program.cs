using System;
public class Node
{
    public string Data { get; set; }
    public Node Next { get; set; }

    public Node(string data)
    {
        Data = data;
        Next = null;
    }
}

public class Queue
{
    private Node head = null; // указатель на первый элемент
    private Node last = null; // указатель на последний элемент
    private int length = 0;

    public int Length => length;

    // Функция создания элемента (аналог get_struct)
    private Node CreateNode()
    {
        Console.WriteLine("Введите название объекта: ");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Запись не была произведена");
            return null;
        }

        return new Node(input);
    }

    // Добавление элемента в конец очереди (аналог spstore)
    public void Enqueue()
    {
        Node newNode = CreateNode();
        if (newNode == null) return;

        if (head == null) // если очередь пуста
        {
            head = newNode;
            last = newNode;
        }
        else // если в очереди уже есть элементы
        {
            last.Next = newNode;
            last = newNode;
        }
        length++;
        Console.WriteLine($"Элемент '{newNode.Data}' добавлен в очередь");
    }

    // Извлечение элемента из начала очереди
    public void Dequeue()
    {
        if (head == null)
        {
            Console.WriteLine("Очередь пуста");
            return;
        }

        Node temp = head;
        head = head.Next; // перемещаем голову на следующий элемент
        length--;

        // Если очередь стала пустой
        if (head == null)
        {
            last = null;
        }

        Console.WriteLine($"Извлечен элемент: '{temp.Data}'");
    }

    // Просмотр первого элемента без извлечения
    public void Peek()
    {
        if (head == null)
        {
            Console.WriteLine("Очередь пуста");
            return;
        }

        Console.WriteLine($"Первый элемент: '{head.Data}'");
    }

    // Просмотр всей очереди (аналог review)
    public void Display()
    {
        if (head == null)
        {
            Console.WriteLine("Очередь пуста");
            return;
        }

        Console.WriteLine("\n=== Содержимое очереди ===");
        Node current = head;
        int position = 1;

        while (current != null)
        {
            Console.WriteLine($"{position}. {current.Data}");
            current = current.Next;
            position++;
        }
        Console.WriteLine($"Всего элементов: {length}\n");
    }

    // Поиск элемента в очереди (аналог find)
    public Node Find(string data)
    {
        if (head == null)
        {
            Console.WriteLine("Очередь пуста");
            return null;
        }

        Node current = head;

        while (current != null)
        {
            if (current.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Элемент '{data}' найден в очереди");
                return current;
            }
            current = current.Next;
        }

        Console.WriteLine($"Элемент '{data}' не найден в очереди");
        return null;
    }
   public Node FindandHead(string data)
{
    if (head == null)
    {
        Console.WriteLine("Очередь пуста");
        return null;
    }

    Node current = head;
    Node previous = null;
    Node foundNode = null;


    while (current != null)
    {
        if (current.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
        {
            foundNode = current;
            break;
        }
        previous = current;
        current = current.Next;
    }


    if (foundNode == null)
    {
        Console.WriteLine($"Элемент '{data}' не найден в очереди");
        return null;
    }


    if (foundNode == head)
    {
        Console.WriteLine($"Элемент '{data}' уже находится в начале очереди");
        return foundNode;
    }


    if (previous != null)
        previous.Next = foundNode.Next;


    if (foundNode == last)
        last = previous;


    foundNode.Next = head;
    head = foundNode;

    Console.WriteLine($"Элемент '{data}' перемещен в начало очереди");
    return foundNode;
}
    // Удаление конкретного элемента из очереди (аналог del)
    public void Remove(string data)
    {
        if (head == null)
        {
            Console.WriteLine("Очередь пуста");
            return;
        }

        Node current = head;
        Node previous = null;
        bool found = false;

        // Если удаляемый элемент - первый
        if (head.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
        {
            found = true;
            head = head.Next;
            length--;

            // Если очередь стала пустой
            if (head == null)
            {
                last = null;
            }

            Console.WriteLine($"Элемент '{data}' удален из очереди");
            return;
        }

        previous = current;
        current = current.Next;

        // Поиск элемента в середине или конце очереди
        while (current != null)
        {
            if (current.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
            {
                found = true;

                // Перестраиваем связи
                previous.Next = current.Next;
                length--;

                // Если удалили последний элемент
                if (current == last)
                {
                    last = previous;
                }

                Console.WriteLine($"Элемент '{data}' удален из очереди");
                break;
            }

            previous = current;
            current = current.Next;
        }

        if (!found)
        {
            Console.WriteLine($"Элемент '{data}' не найден в очереди");
        }
    }

    // Очистка очереди
    public void Clear()
    {
        head = null;
        last = null;
        length = 0;
        Console.WriteLine("Очередь очищена");
    }

    // Проверка на пустоту
    public bool IsEmpty()
    {
        return head == null;
    }
}
class Program
{
    static void Main()
    {
        Queue queue = new Queue();

        while (true)
        {
            Console.WriteLine("\n=== ОЧЕРЕДЬ (FIFO) ===");
            Console.WriteLine("1. Добавить элемент (Enqueue)");
            Console.WriteLine("2. Извлечь элемент (Dequeue)");
            Console.WriteLine("3. Посмотреть первый элемент (Peek)");
            Console.WriteLine("4. Показать всю очередь");
            Console.WriteLine("5. Найти элемент");
            Console.WriteLine("6. Удалить элемент");
            Console.WriteLine("7. Очистить очередь");
            Console.WriteLine("8. Проверить пустоту");
            Console.WriteLine("0. Выйти");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    queue.Enqueue();
                    break;

                case "2":
                    queue.Dequeue();
                    break;

                case "3":
                    queue.Peek();
                    break;

                case "4":
                    queue.Display();
                    break;

                case "5":
                    Console.Write("Введите значение для поиска: ");
                    string searchData = Console.ReadLine();
                    queue.Find(searchData);
                    break;


                case "6":
                    Console.Write("Введите значение для удаления: ");
                    string removeData = Console.ReadLine();
                    queue.Remove(removeData);
                    break;

                case "7":
                    queue.Clear();
                    break;

                case "8":
                    Console.WriteLine(queue.IsEmpty() ? "Очередь пуста" : "Очередь не пуста");
                    break;
                case "9":
                    Console.Write("Введите значение для поиска: ");
                    string NewFirst = Console.ReadLine();
                    queue.FindandHead(NewFirst);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }
}
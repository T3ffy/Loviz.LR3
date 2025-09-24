public class PriorityNode
{
    public string Data { get; set; }
    public int Priority { get; set; }
    public PriorityNode Next { get; set; }

    public PriorityNode(string data, int priority)
    {
        Data = data;
        Priority = priority;
        Next = null;
    }
}

public class PriorityQueue
{
    private PriorityNode head = null;
    private PriorityNode last = null;
    private int length = 0;

    public int Length => length;

    // Создание нового элемента с приоритетом
    private PriorityNode CreateNode()
    {
        Console.WriteLine("Введите название объекта: ");
        string data = Console.ReadLine();

        if (string.IsNullOrEmpty(data))
        {
            Console.WriteLine("Запись не была произведена");
            return null;
        }

        Console.WriteLine("Введите приоритет (целое число, меньшее = выше приоритет): ");
        if (!int.TryParse(Console.ReadLine(), out int priority))
        {
            Console.WriteLine("Неверный формат приоритета");
            return null;
        }

        return new PriorityNode(data, priority);
    }

    // Добавление элемента с учетом приоритета (в порядке возрастания приоритета)
    public void Enqueue()
    {
        PriorityNode newNode = CreateNode();
        if (newNode == null) return;

        // Если очередь пуста
        if (head == null)
        {
            head = newNode;
            last = newNode;
        }
        // Если новый элемент имеет высший приоритет (меньшее число)
        else if (newNode.Priority < head.Priority)
        {
            newNode.Next = head;
            head = newNode;
        }
        else
        {
            // Поиск места для вставки
            PriorityNode current = head;
            PriorityNode previous = null;

            while (current != null && current.Priority <= newNode.Priority)
            {
                previous = current;
                current = current.Next;
            }

            // Вставка элемента
            if (current == null) // В конец
            {
                last.Next = newNode;
                last = newNode;
            }
            else // В середину
            {
                previous.Next = newNode;
                newNode.Next = current;
            }
        }
        length++;
        Console.WriteLine($"Элемент '{newNode.Data}' с приоритетом {newNode.Priority} добавлен");
    }

    // Извлечение элемента с высшим приоритетом (из начала)
    public string Dequeue()
    {
        if (head == null)
        {
            Console.WriteLine("Очередь пуста");
            return null;
        }

        string data = head.Data;
        head = head.Next;
        length--;

        // Если очередь стала пустой
        if (head == null)
        {
            last = null;
        }

        Console.WriteLine($"Извлечен элемент: '{data}'");
        return data;
    }

    // Просмотр элемента с высшим приоритетом без извлечения
    public string Peek()
    {
        if (head == null)
        {
            Console.WriteLine("Очередь пуста");
            return null;
        }

        Console.WriteLine($"Первый элемент: '{head.Data}' (приоритет: {head.Priority})");
        return head.Data;
    }

    // Просмотр всей очереди
    public void Display()
    {
        if (head == null)
        {
            Console.WriteLine("Очередь пуста");
            return;
        }

        Console.WriteLine("\n=== Содержимое приоритетной очереди ===");
        PriorityNode current = head;
        int position = 1;

        while (current != null)
        {
            Console.WriteLine($"{position}. '{current.Data}' - приоритет: {current.Priority}");
            current = current.Next;
            position++;
        }
        Console.WriteLine($"Всего элементов: {length}\n");
    }

    // Поиск элемента по значению
    public PriorityNode Find(string data)
    {
        if (head == null)
        {
            Console.WriteLine("Очередь пуста");
            return null;
        }

        PriorityNode current = head;

        while (current != null)
        {
            if (current.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Найден элемент: '{current.Data}' с приоритетом {current.Priority}");
                return current;
            }
            current = current.Next;
        }

        Console.WriteLine($"Элемент '{data}' не найден");
        return null;
    }

    // Поиск элементов по приоритету
    public void FindByPriority(int priority)
    {
        if (head == null)
        {
            Console.WriteLine("Очередь пуста");
            return;
        }

        Console.WriteLine($"\nЭлементы с приоритетом {priority}:");
        PriorityNode current = head;
        bool found = false;

        while (current != null)
        {
            if (current.Priority == priority)
            {
                Console.WriteLine($"- '{current.Data}'");
                found = true;
            }
            current = current.Next;
        }

        if (!found)
        {
            Console.WriteLine("Элементы не найдены");
        }
    }

    // Удаление конкретного элемента по значению
    public void Remove(string data)
    {
        if (head == null)
        {
            Console.WriteLine("Очередь пуста");
            return;
        }

        // Если удаляем первый элемент
        if (head.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
        {
            head = head.Next;
            length--;

            if (head == null)
            {
                last = null;
            }

            Console.WriteLine($"Элемент '{data}' удален");
            return;
        }

        // Поиск элемента в середине или конце
        PriorityNode current = head;
        PriorityNode previous = null;
        bool found = false;

        while (current != null)
        {
            if (current.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
            {
                found = true;
                previous.Next = current.Next;

                // Если удалили последний элемент
                if (current == last)
                {
                    last = previous;
                }

                length--;
                Console.WriteLine($"Элемент '{data}' удален");
                break;
            }

            previous = current;
            current = current.Next;
        }

        if (!found)
        {
            Console.WriteLine($"Элемент '{data}' не найден");
        }
    }

    // Очистка всей очереди
    public void Clear()
    {
        head = null;
        last = null;
        length = 0;
        Console.WriteLine("Очередь очищена");
    }

    // Проверка, пуста ли очередь
    public bool IsEmpty()
    {
        return head == null;
    }
}
class Program
{
    static void Main()
    {
        PriorityQueue queue = new PriorityQueue();

        while (true)
        {
            Console.WriteLine("\n=== ПРИОРИТЕТНАЯ ОЧЕРЕДЬ ===");
            Console.WriteLine("1. Добавить элемент (Enqueue)");
            Console.WriteLine("2. Извлечь элемент (Dequeue)");
            Console.WriteLine("3. Просмотреть первый элемент (Peek)");
            Console.WriteLine("4. Показать всю очередь");
            Console.WriteLine("5. Найти элемент по значению");
            Console.WriteLine("6. Найти элементы по приоритету");
            Console.WriteLine("7. Удалить конкретный элемент");
            Console.WriteLine("8. Очистить очередь");
            Console.WriteLine("9. Проверить пустоту очереди");
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
                    Console.Write("Введите приоритет для поиска: ");
                    if (int.TryParse(Console.ReadLine(), out int priority))
                    {
                        queue.FindByPriority(priority);
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат приоритета");
                    }
                    break;

                case "7":
                    Console.Write("Введите значение для удаления: ");
                    string removeData = Console.ReadLine();
                    queue.Remove(removeData);
                    break;

                case "8":
                    queue.Clear();
                    break;

                case "9":
                    Console.WriteLine(queue.IsEmpty() ? "Очередь пуста" : "Очередь не пуста");
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
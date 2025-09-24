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

public class Stack
{
    private Node top = null; // вместо head
    private int length = 0;

    public int Length => length;

    // Функция создания элемента (аналогично вашему get_struct)
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

    // Добавление элемента в стек (Push) - аналог spstore
    public void Push()
    {
        Node newNode = CreateNode();
        if (newNode == null) return;

        if (top == null) // если стек пуст
        {
            top = newNode;
        }
        else // если в стеке уже есть элементы
        {
            newNode.Next = top; // новый элемент указывает на текущую вершину
            top = newNode;      // новый элемент становится вершиной
        }
        length++;
        Console.WriteLine($"Элемент '{newNode.Data}' добавлен в стек");
    }

    // Извлечение элемента из стека (Pop)
    public void Pop()
    {
        if (top == null)
        {
            Console.WriteLine("Стек пуст");
            return;
        }

        Node temp = top;
        top = top.Next; // перемещаем вершину на следующий элемент
        length--;
        Console.WriteLine($"Извлечен элемент: '{temp.Data}'");
    }

    // Просмотр вершины стека без извлечения (Peek)
    public void Peek()
    {
        if (top == null)
        {
            Console.WriteLine("Стек пуст");
            return;
        }

        Console.WriteLine($"Вершина стека: '{top.Data}'");
    }

    // Просмотр всего стека (аналог вашего review)
    public void Display()
    {
        if (top == null)
        {
            Console.WriteLine("Стек пуст");
            return;
        }

        Console.WriteLine("\n=== Содержимое стека ===");
        Node current = top;
        int position = 1;

        while (current != null)
        {
            Console.WriteLine($"{position}. {current.Data}");
            current = current.Next;
            position++;
        }
        Console.WriteLine($"Всего элементов: {length}\n");
    }

    // Поиск элемента в стеке (аналог вашего find)
    public Node Find(string data)
    {
        if (top == null)
        {
            Console.WriteLine("Стек пуст");
            return null;
        }

        Node current = top;

        while (current != null)
        {
            if (current.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Элемент '{data}' найден в стеке");
                return current;
            }
            current = current.Next;
        }

        Console.WriteLine($"Элемент '{data}' не найден в стеке");
        return null;
    }

    // Удаление конкретного элемента из стека (аналог вашего del)
    public void Remove(string data)
    {
        if (top == null)
        {
            Console.WriteLine("Стек пуст");
            return;
        }

        // Если удаляемый элемент - вершина стека
        if (top.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
        {
            top = top.Next;
            length--;
            Console.WriteLine($"Элемент '{data}' удален из стека");
            return;
        }

        // Поиск элемента в глубине стека
        Node current = top;
        Node previous = null;
        bool found = false;

        while (current != null)
        {
            if (current.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
            {
                found = true;
                previous.Next = current.Next; // перебрасываем ссылку
                length--;
                Console.WriteLine($"Элемент '{data}' удален из стека");
                break;
            }
            previous = current;
            current = current.Next;
        }

        if (!found)
        {
            Console.WriteLine($"Элемент '{data}' не найден в стеке");
        }
    }

    // Очистка стека
    public void Clear()
    {
        top = null;
        length = 0;
        Console.WriteLine("Стек очищен");
    }

    // Проверка на пустоту
    public bool IsEmpty()
    {
        return top == null;
    }
}
class Program
{
    static void Main()
    {
        Stack stack = new Stack();

        while (true)
        {
            Console.WriteLine("\n=== СТЕК (LIFO) ===");
            Console.WriteLine("1. Добавить элемент (Push)");
            Console.WriteLine("2. Извлечь элемент (Pop)");
            Console.WriteLine("3. Посмотреть вершину (Peek)");
            Console.WriteLine("4. Показать весь стек");
            Console.WriteLine("5. Найти элемент");
            Console.WriteLine("6. Удалить элемент");
            Console.WriteLine("7. Очистить стек");
            Console.WriteLine("8. Проверить пустоту");
            Console.WriteLine("0. Выйти");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    stack.Push();
                    break;

                case "2":
                    stack.Pop();
                    break;

                case "3":
                    stack.Peek();
                    break;

                case "4":
                    stack.Display();
                    break;

                case "5":
                    Console.Write("Введите значение для поиска: ");
                    string searchData = Console.ReadLine();
                    stack.Find(searchData);
                    break;

                case "6":
                    Console.Write("Введите значение для удаления: ");
                    string removeData = Console.ReadLine();
                    stack.Remove(removeData);
                    break;

                case "7":
                    stack.Clear();
                    break;

                case "8":
                    Console.WriteLine(stack.IsEmpty() ? "Стек пуст" : "Стек не пуст");
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
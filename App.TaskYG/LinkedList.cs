namespace TaskYG
{
	/// <summary>
	/// Класс реализующий односвязный список
	/// </summary>
	public class LinkedList<T>
	{
		//Модификатор public, а не private только для демонстрации нахождения петли в тестах.
		public Node head;
		private int count;
		//
		private readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

		public LinkedList()
		{
			head = null;
			count = 0;
		}

		public int Count { get { return count; } }

		//Модификатор public, а не private только для демонстрации нахождения петли в тестах.
		public class Node
		{
			public T Value { get; set; }
			public Node Next { get; set; }

			public Node(T value)
			{
				Value = value;
				Next = null;
			}
		}

		/// <summary>
		/// Добавление элемента в начало списка
		/// </summary>
		public void AddFirst(T value)
		{
			Node newNode = new Node(value)
			{
				Next = head
			};
			head = newNode;
			count++;
		}

		/// <summary>
		/// Добавление элемента в конец списка
		/// </summary>
		public void AddLast(T value)
		{
			Node newNode = new Node(value);
			if (head == null)
			{
				head = newNode;
			}
			else
			{
				Node current = head;
				while (current.Next != null)
				{
					current = current.Next;
				}
				current.Next = newNode;
			}
			count++;
		}

		/// <summary>
		/// Удаление элемента из списка
		/// </summary>
		public bool Remove(T value)
		{
			Node current = head;
			Node previous = null;
			while (current != null)
			{
				if (current.Value.Equals(value))
				{
					if (previous != null)
					{
						previous.Next = current.Next;
					}
					else
					{
						head = current.Next;
					}
					count--;
					return true;
				}
				previous = current;
				current = current.Next;
			}
			return false;
		}

		/// <summary>
		/// Метод очистки списка
		/// </summary>
		public void Clear()
		{
			head = null;
			count = 0;
		}

		public IEnumerator<T> GetEnumerator()
		{
			Node current = head;
			while (current != null)
			{
				yield return current.Value;
				current = current.Next;
			}
		}

		/// <summary>
		/// Метод нахождения петли в списке используя алгоритм "Floyd's cycle-finding"
		/// </summary>
		public bool HasLoop()
		{
			var slow = head;
			var fast = head;

			while (fast != null && fast.Next != null)
			{
				slow = slow.Next;
				fast = fast.Next.Next;

				if (slow == fast)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Метод определения существования элемента в списке
		/// </summary>
		public bool Contains(T value)
		{
			Node current = head;
			while (current != null)
			{
				if (current.Value.Equals(value))
				{
					return true;
				}
				current = current.Next;
			}
			return false;
		}

		/// <summary>
		/// Потокобезопасный метод поиска элемента в списка
		/// </summary>
		public T Find(Predicate<T> predicate)
		{
			rwLock.EnterReadLock();
			try
			{
				Node current = head;
				while (current != null)
				{
					if (predicate(current.Value))
					{
						return current.Value;
					}
					current = current.Next;
				}
				return default(T);
			}
			finally
			{
				rwLock.ExitReadLock();
			}
		}
	}
}

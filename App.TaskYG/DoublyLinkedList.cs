namespace TaskYG
{
	/// <summary>
	/// Класс реализующий односвязный список
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DoublyLinkedList<T>
	{
		public Node head;
		public Node tail;
		private int count;
		private readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
		public int Count { get { return count; } }

		public class Node
		{
			public T Value { get; set; }
			public Node Prev { get; set; }
			public Node Next { get; set; }

			public Node(T value)
			{
				Value = value;
				Prev = null;
				Next = null;
			}
		}

		/// <summary>
		/// Добавление элемента в начало списка
		/// </summary>
		public void AddFirst(T value)
		{
			Node newNode = new Node(value);
			if (head == null)
			{
				head = newNode;
				tail = newNode;
			}
			else
			{
				newNode.Next = head;
				head.Prev = newNode;
				head = newNode;
			}
			count++;
		}

		/// <summary>
		/// Добавление элемента в конец списка
		/// </summary>
		public void AddLast(T value)
		{
			Node newNode = new Node(value);
			if (tail == null)
			{
				head = newNode;
				tail = newNode;
			}
			else
			{
				newNode.Prev = tail;
				tail.Next = newNode;
				tail = newNode;
			}
			count++;
		}

		/// <summary>
		/// Метод очистки списка
		/// </summary>
		public void Clear()
		{
			head = null;
			tail = null;
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
		/// Метод нахождения петли в списке используюя хэш таблицу
		/// </summary>
		public bool HasLoop()
		{
			var nodeSet = new HashSet<Node>();
			var current = head;

			while (current != null)
			{
				if (nodeSet.Contains(current))
				{
					return true;
				}
				else
				{
					nodeSet.Add(current);
					current = current.Next;
				}
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

					if (current.Next != null)
					{
						current.Next.Prev = current.Prev;
					}
					else
					{
						tail = current.Prev;
					}

					count--;
					return true;
				}
				previous = current;
				current = current.Next;
			}
			return false;
		}
	}
}
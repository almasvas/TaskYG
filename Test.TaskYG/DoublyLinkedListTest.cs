using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskYG;

namespace Test.TaskYG
{
	internal class DoublyLinkedListTest
	{
		private DoublyLinkedList<string> _doubleLinkedList;

		[SetUp]
		public void Setup()
		{
			_doubleLinkedList = new DoublyLinkedList<string>();
		}

		[Test]
		public void CountContainFindRemoveTest()
		{
			_doubleLinkedList.AddFirst("node1");
			_doubleLinkedList.AddFirst("node2");
			_doubleLinkedList.AddFirst("node3");
			_doubleLinkedList.AddFirst("node4");
			_doubleLinkedList.Remove("node4");
			var result1 = _doubleLinkedList.Count;
			var result2 = _doubleLinkedList.Contains("node2");
			var result3 = _doubleLinkedList.Find(x => x.EndsWith("3"));
			var result4 = _doubleLinkedList.Contains("node4");


			Assert.AreEqual(3, result1);
			Assert.IsTrue(result2);
			Assert.AreEqual("node3", result3);
			Assert.IsFalse(result4);
		}

		[Test]
		public void ForeachTest()
		{
			_doubleLinkedList.AddLast("node1");
			_doubleLinkedList.AddLast("node2");
			_doubleLinkedList.AddLast("node3");

			StringBuilder sb = new();

			foreach (var el in _doubleLinkedList)
			{
				sb.Append(el);
			}

			Assert.AreEqual("node1node2node3", sb.ToString());
		}

		[Test]
		public void ClearTest()
		{
			_doubleLinkedList.AddLast("node1");
			_doubleLinkedList.AddLast("node2");
			_doubleLinkedList.AddLast("node3");

			_doubleLinkedList.Clear();

			Assert.AreEqual(_doubleLinkedList.Count, 0);
			Assert.IsNull(_doubleLinkedList.head);
			Assert.IsNull(_doubleLinkedList.tail);
		}

		[Test]
		public void HasLoopTest()
		{
			_doubleLinkedList.AddFirst("node1");
			_doubleLinkedList.AddFirst("node2");
			_doubleLinkedList.AddFirst("node3");

			_doubleLinkedList.head.Next.Next = _doubleLinkedList.head;

			var result = _doubleLinkedList.HasLoop();

			Assert.IsTrue(result);
		}
	}
}

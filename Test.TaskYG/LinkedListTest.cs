using NUnit.Framework;
using System.Text;
using TaskYG;

namespace Test.TaskYG
{
	public class LinkedListTest
	{
		private LinkedList<string> _linkedList;

		[SetUp]
		public void Setup()
		{
			_linkedList = new LinkedList<string>();
		}

		[Test]
		public void CountContainFindRemoveTest()
		{
			_linkedList.AddFirst("node1");
			_linkedList.AddFirst("node2");
			_linkedList.AddFirst("node3");
			_linkedList.AddFirst("node4");
			_linkedList.Remove("node4");
			var result1 = _linkedList.Count;
			var result2 = _linkedList.Contains("node2");
			var result3 = _linkedList.Find(x => x.EndsWith("3"));
			var result4 = _linkedList.Contains("node4");


			Assert.AreEqual(3, result1);
			Assert.IsTrue(result2);
			Assert.AreEqual("node3", result3);
			Assert.IsFalse(result4);
		}

		[Test]
		public void ForeachTest()
		{
			_linkedList.AddLast("node1");
			_linkedList.AddLast("node2");
			_linkedList.AddLast("node3");

			StringBuilder sb = new();

			foreach (var el in _linkedList)
			{
				sb.Append(el);
			}

			Assert.AreEqual("node1node2node3", sb.ToString());
		}

		[Test]
		public void ClearTest()
		{
			_linkedList.AddLast("node1");
			_linkedList.AddLast("node2");
			_linkedList.AddLast("node3");

			_linkedList.Clear();

			Assert.AreEqual(_linkedList.Count, 0);
			Assert.IsNull(_linkedList.head);
		}

		[Test]
		public void HasLoopTest()
		{
			_linkedList.AddFirst("node1");
			_linkedList.AddFirst("node2");
			_linkedList.AddFirst("node3");

			_linkedList.head.Next.Next = _linkedList.head;

			var result = _linkedList.HasLoop();

			Assert.IsTrue(result);
		}
	}
}
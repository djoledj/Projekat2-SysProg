﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysProg2
{
	class LRUCache
	{
		class ListNode
		{
			public string key;
			public byte[] val;
			public ListNode prev, next;

			public ListNode(string k, byte[] v)
			{
				key = k;
				val = v;
				prev = null;
				next = null;
			}
		}

		private int capacity, size;
		private ListNode dummyHead, dummyTail;

		private Dictionary<string, ListNode> map;

		public bool argumentOk = true;

		public LRUCache(int capacity)
		{
			if (capacity <= 0)
			{

				argumentOk = false;
				return;
			}

			this.capacity = capacity;
			size = 0;
			dummyHead = new ListNode(null, null);
			dummyTail = new ListNode(null, null);
			dummyTail.prev = dummyHead;
			dummyHead.next = dummyTail;
			map = new Dictionary<string, ListNode>();
		}
		public bool checkget(string key)
		{
			return map.ContainsKey(key);
		}
		public byte[] get(string key)
		{
			ListNode target = map[key];
			remove(target);
			addToLast(target);
			return target.val;
		}

		public void set(string key, byte[] value)
		{
			if (map.ContainsKey(key))
			{
				ListNode target = map[key];
				target.val = value;
				remove(target);
				addToLast(target);
			}
			else
			{
				if (size == capacity)
				{
					map.Remove(dummyHead.next.key);
					remove(dummyHead.next);
					--size;
				}

				ListNode newNode = new ListNode(key, value);
				map.Add(key, newNode);
				addToLast(newNode);
				++size;
			}
		}

		private void addToLast(ListNode target)
		{
			target.next = dummyTail;
			target.prev = dummyTail.prev;
			dummyTail.prev.next = target;
			dummyTail.prev = target;
		}

		private void remove(ListNode target)
		{
			target.next.prev = target.prev;
			target.prev.next = target.next;
			Console.WriteLine($"Obrisano {target.key} iz kesa!");
		}

	}
}


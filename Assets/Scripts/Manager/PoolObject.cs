using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
	public class PoolObject : MonoBehaviour
	{
		public string poolName;
		//defines whether the object is waiting in pool or is in use
		public bool isPooled;
	}
}

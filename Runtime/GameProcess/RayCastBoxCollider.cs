using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using YusamPackage;

namespace YusamPackage
{

	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(YusamDebugProperties))]
	[DisallowMultipleComponent]
	public class RayCastBoxCollider : MonoBehaviour
	{
		public enum RayCastDirection
		{
			UpDown,
			LeftRight,
			ForwardBack
		}

		[SerializeField] private RayCastDirection rayCastDirection;
		[SerializeField] private int numberOfRays = 5;
		[SerializeField] private float offset = .001f;
		[SerializeField] private LayerMask rayCastLayerMask;

		[Serializable] public class RayCastBoxCollisionEvent : UnityEvent <RaycastHit[]> {}

		[Space(20)]
		[Header("Events")] 
		[Space(10)]
		[YusamHelpBox("public void MethodName(RaycastHit[] hit)")]
		
		[Space(10)]
		[SerializeField] private RayCastBoxCollisionEvent rayCastBoxCollisionEnter = new RayCastBoxCollisionEvent();
		public RayCastBoxCollisionEvent OnRayCastBoxCollisionEnter { get { return rayCastBoxCollisionEnter; } set { rayCastBoxCollisionEnter = value; } }

		[Space(10)]
		[SerializeField] private RayCastBoxCollisionEvent rayCastBoxCollisionExit = new RayCastBoxCollisionEvent();
		public RayCastBoxCollisionEvent OnRayCastBoxCollisionExit { get { return rayCastBoxCollisionExit; } set { rayCastBoxCollisionExit = value; } }

		[Space(10)]
		[SerializeField] private RayCastBoxCollisionEvent rayCastBoxCollisionStay = new RayCastBoxCollisionEvent();
		public RayCastBoxCollisionEvent OnRayCastBoxCollisionStay { get { return rayCastBoxCollisionStay; } set { rayCastBoxCollisionStay = value; } }

		private Collider _collider;
		private YusamDebugProperties _debugProperties;
		private bool _collisionStay;
		private RaycastHit? _lastHit; 

		private void Awake()
		{
			_debugProperties = GetComponent<YusamDebugProperties>();
			_collider = GetComponent<Collider>();
		}

		public void MethodNameEnter(RaycastHit[] hits)
		{
			Debug.Log("MethodNameEnter");
		}

		public void MethodNameExit(RaycastHit[] hits)
		{
			Debug.Log("MethodNameExit");
		}
		
		public void MethodNameStay(RaycastHit[] hits)
		{
			Debug.Log("MethodNameStay");
		}
		
		private void Update()
		{
			RaycastHit? hit = IsCollidingByRayDirection();

			if (hit.HasValue)
			{
				if (_collisionStay == false)
				{
					_collisionStay = true;
					OnRayCastBoxCollisionEnter?.Invoke(new []{hit.Value});
				}
			}
			else
			{
				if (_lastHit != null)
				{
					_collisionStay = false;
					OnRayCastBoxCollisionExit?.Invoke(new []{_lastHit.Value});
				}
			}

			if (hit != null)
			{
				OnRayCastBoxCollisionStay?.Invoke(new []{hit.Value});
			}

			_lastHit = hit;
		}

		private Vector3 GetStartPointByRayDirection()
		{
			if (rayCastDirection == RayCastDirection.LeftRight)
			{
				return new Vector3(_collider.bounds.min.x, _collider.bounds.min.y + offset,
					_collider.bounds.min.z + offset);
			}

			if (rayCastDirection == RayCastDirection.ForwardBack)
			{
				return new Vector3(_collider.bounds.min.x + offset, _collider.bounds.min.y + offset,
					_collider.bounds.min.z);
			}

			return new Vector3(_collider.bounds.min.x + offset, _collider.bounds.min.y,
				_collider.bounds.min.z + offset);
		}

		private float GetRayMaxDistanceByRayDirection()
		{
			if (rayCastDirection == RayCastDirection.LeftRight)
			{
				return _collider.bounds.extents.x;
			}

			if (rayCastDirection == RayCastDirection.ForwardBack)
			{
				return _collider.bounds.extents.z;
			}

			return _collider.bounds.extents.y;
		}

		private Vector3 GetOffsetDirByDirectionFirstAxis()
		{
			if (rayCastDirection == RayCastDirection.ForwardBack || rayCastDirection == RayCastDirection.LeftRight)
			{
				//forward/back = Z FIRST AXIS = Y
				return new Vector3(0, (_collider.bounds.size.y - 2 * offset) / (numberOfRays - 1), 0);
			}

			//UP/DOWN = Y FIRST AXIS = X
			return new Vector3((_collider.bounds.size.x - 2 * offset) / (numberOfRays - 1), 0, 0);
		}

		private Vector3 GetOffsetDirByDirectionSecondAxis()
		{
			if (rayCastDirection == RayCastDirection.ForwardBack)
			{
				//forward/back = Z SECOND AXIS = X
				return new Vector3((_collider.bounds.size.x - 2 * offset) / (numberOfRays - 1), 0, 0);
			}

			//UP/DOWN = Y SECOND AXIS = Z
			return new Vector3(0, 0, (_collider.bounds.size.z - 2 * offset) / (numberOfRays - 1));
		}

		private RaycastHit? IsCollidingByRayDirection()
		{
			Vector3 startPoint = GetStartPointByRayDirection();

			for (var i = 0; i < numberOfRays; i++)
			{
				RaycastHit? hitTest = TryRayCast(
					startPoint,
					GetOffsetDirByDirectionFirstAxis(),
					GetRayMaxDistanceByRayDirection()
				);
				if (hitTest.HasValue)
				{
					return hitTest;
				}

				startPoint += GetOffsetDirByDirectionSecondAxis();
			}

			return null;
		}

		private RaycastHit? TryRayCast(
			Vector3 startPoint,
			Vector3 offsetDir,
			float maxDistance
		)
		{
			Vector3 tmpPoint = startPoint;
			Vector3 rayDir;
			if (rayCastDirection == RayCastDirection.ForwardBack)
			{
				rayDir = Vector3.forward;
			}
			else if (rayCastDirection == RayCastDirection.LeftRight)
			{
				rayDir = Vector3.right;
			}
			else
			{
				rayDir = Vector3.up;
			}


			for (var i = 0; i < numberOfRays; i++)
			{

				Ray ray = new Ray(tmpPoint, rayDir);

				if (_debugProperties.debugEnabled)
				{
					Debug.DrawRay(tmpPoint, rayDir, _debugProperties.debugDefaultColor);
				}

				/*
				 * todo: нужно собрать все колизии 
				 */
				if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, rayCastLayerMask))
				{
					return hit;
				}

				tmpPoint += offsetDir;
			}

			return null;
		}
	}
}



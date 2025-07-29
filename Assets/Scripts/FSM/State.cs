// COMMENT TO SILENCE
//#define SYSTEM_DEBUG


using UnityEngine;
using System.Collections;

	[System.Serializable]
	public abstract class State : IStateInterface
	{
		public virtual void StateUpdate() { }
		public virtual void StateFixedUpdate() { }
		public virtual void StateLateUpdate() { }

		public virtual void OnCollisionEnter(UnityEngine.Collision collision)
		{
			#if (SYSTEM_DEBUG)
			UnityEngine.Debug.Log(machine.name + "." + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
			#endif // SYSTEM_DEBUG
		}
		public virtual void OnCollisionStay(UnityEngine.Collision collision) {}
		public virtual void OnCollisionExit(UnityEngine.Collision collision)
		{
			#if (SYSTEM_DEBUG)
			UnityEngine.Debug.Log(machine.name + "." + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
			#endif // SYSTEM_DEBUG
		}

		public virtual void OnTriggerEnter(UnityEngine.Collider collider)
		{
			#if (SYSTEM_DEBUG)
			UnityEngine.Debug.Log(machine.name + "." + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
			#endif // SYSTEM_DEBUG
		}

		public virtual void OnHitRevolvingDoor(UnityEngine.Collider collider) {
		}

		public virtual void OnTriggerStay(UnityEngine.Collider collider) {}
		public virtual void OnTriggerExit(UnityEngine.Collider collider)
		{
			#if (SYSTEM_DEBUG)
			UnityEngine.Debug.Log(machine.name + "." + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
			#endif // SYSTEM_DEBUG
		}

		public virtual void OnAnimatorIK(int layerIndex) { }

		public virtual void Initialize()
		{
			#if (SYSTEM_DEBUG)
			UnityEngine.Debug.Log(machine.name + "." + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
			#endif // SYSTEM_DEBUG
		}

		public virtual void Enter()
		{
			#if (SYSTEM_DEBUG)
			UnityEngine.Debug.Log(machine.name + "." + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
			#endif // SYSTEM_DEBUG
		}

		public virtual void Exit()
		{
			#if (SYSTEM_DEBUG)
			UnityEngine.Debug.Log(machine.name + "." + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
			#endif // SYSTEM_DEBUG
		}

		public T GetMachine<T>() where T : IStateMachineInterface
		{
			try
			{
				return (T)machine;
			}
			catch (System.InvalidCastException e)
			{
				if (typeof(T) == typeof(MachineState) || typeof(T).IsSubclassOf(typeof(MachineState)))
				{
					throw new System.Exception(machine.name + ".GetMachine() cannot return the type you requested!\tYour machine is derived from StateMachine not MachineState!" + e.Message);
				}
				else if (typeof(T) == typeof(StateMachine) || typeof(T).IsSubclassOf(typeof(StateMachine)))
				{
					throw new System.Exception(machine.name + ".GetMachine() cannot return the type you requested!\tYour machine is derived from MachineState not StateMachine!" + e.Message);
				}
				else
				{
					throw new System.Exception(machine.name + ".GetMachine() cannot return the type you requested!\n" + e.Message);
				}
			}
		}

		public IStateMachineInterface machine { get; internal set; }

		public bool isActive { get { return machine.IsCurrentState(GetType()); } }
	}


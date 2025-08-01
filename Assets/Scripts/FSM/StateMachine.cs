﻿


	public abstract class StateMachine :  UnityEngine.MonoBehaviour, IStateMachineInterface
	{
		/// <summary>
		/// REQUIRES Implementaion in Child class
		///     
		///     Add states to the machine with calls to AddState<>()
		///     
		///     When all states have been added notify the machine
		///         which state to start in with SetInitialState<>()
		/// </summary>
		public abstract void AddStates();

		public virtual void Start()
		{
			Initialize();

		}

		public virtual void Initialize()
		{
			AddStates();

			currentState = initialState;
			if (null == currentState)
			{
				throw new System.Exception("\n" + name + ".nextState is null on Initialize()!\tDid you forget to call SetInitialState()?\n");
			}

			foreach (System.Collections.Generic.KeyValuePair<System.Type, State> pair in states)
			{
				pair.Value.Initialize();
			}

			onEnter = true;
			onExit = false;
		}

		public virtual void Update()
		{
			if (onExit)
			{
				currentState.Exit();
				prevState = currentState;
				currentState = nextState;
				nextState = null;

				onEnter = true;
				onExit = false;
			}

			if (onEnter)
			{
				currentState.Enter();

				onEnter = false;
			}

			try
			{
				currentState.StateUpdate();
			}
			catch (System.NullReferenceException e)
			{
//				if (null == initialState)
//				{
//					throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you set initial state?\n" + e.Message);
//				}
//				else
//				{
//					throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you change state to a valid state?\n" + e.Message);
//				}
			}
		}

		public virtual void FixedUpdate()
		{
			if (!(onEnter && onExit))
			{
				try
				{
					currentState.StateFixedUpdate();
				}
				catch (System.NullReferenceException e)
				{
//					if (null == initialState)
//					{
//						throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you set initial state?\n" + e.Message);
//					}
//					else
//					{
//						throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you change state to a valid state?\n" + e.Message);
//					}
				}
			}
		}
			
		public virtual void LateUpdate()
		{
			if (!(onEnter && onExit))
			{
				try
				{
					currentState.StateLateUpdate();
				}
				catch (System.NullReferenceException e)
				{
					if (null == initialState)
					{
//						throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you set initial state?\n" + e.Message);
					}
					else
					{
						throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you change state to a valid state?\n" + e.Message);
					}
				}
			}
		}

		public virtual void OnCollisionEnter(UnityEngine.Collision collision) { currentState.OnCollisionEnter(collision); }
		public virtual void OnCollisionStay(UnityEngine.Collision collision) { currentState.OnCollisionStay(collision); }
		public virtual void OnCollisionExit(UnityEngine.Collision collision) { currentState.OnCollisionExit(collision); }

		public virtual void OnTriggerEnter(UnityEngine.Collider collider) { currentState.OnTriggerEnter(collider); }
		public virtual void  OnTriggerStay(UnityEngine.Collider collider) { currentState.OnTriggerStay(collider); }
		public virtual void OnTriggerExit(UnityEngine.Collider collider) { currentState.OnTriggerExit(collider); }

		public virtual void OnHitRevolvingDoor(UnityEngine.Collider collider)
		{
			currentState.OnHitRevolvingDoor (collider);
		}

		public void OnAnimatorIK(int layerIndex)
		{
			if (!(onEnter && onExit))
			{
				try
				{
					currentState.OnAnimatorIK(layerIndex);
				}
				catch (System.NullReferenceException e)
				{
					if (null == initialState)
					{
						throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you set initial state?\n" + e.Message);
					}
					else
					{
						throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you change state to a valid state?\n" + e.Message);
					}
				}
			}
		}

		public void SetInitialState<T>() where T : State { initialState = states[typeof(T)]; }
		public void SetInitialState(System.Type T) { initialState = states[T]; }

		public void ChangeState<T>() where T : State { ChangeState(typeof(T)); }
		public void ChangeState(System.Type T)
		{
			if (null != nextState)
			{
				throw new System.Exception(name + " is already changing states, you must wait to call ChangeState()!\n");
			}

			try
			{
				nextState = states[T];
			}
			catch (System.Collections.Generic.KeyNotFoundException e)
			{
				throw new System.Exception("\n" + name + "State: " + T +  ".ChangeState() cannot find the state in the machine!\tDid you add the state you are trying to change to?\n" + e.Message);
			}

			onExit = true;
		}
			

		public bool IsCurrentState<T>() where T : State { return (currentState.GetType() == typeof(T)) ? true : false; }
		public bool IsCurrentState(System.Type T) { return (currentState.GetType() == T) ? true : false; }

		public void AddState<T>() where T : State, new()
		{
			if (!ContainsState<T>())
			{
				State item = new T();
				item.machine = this;

				states.Add(typeof(T), item);
			}
		}
		public void AddState(System.Type T)
		{
			if (!ContainsState(T))
			{
				State item = (State)System.Activator.CreateInstance(T);
				item.machine = this;

				states.Add(T, item);
			}
		}

		public void RemoveState<T>() where T : State { states.Remove(typeof(T)); }
		public void RemoveState(System.Type T) { states.Remove(T); }

		public bool ContainsState<T>() where T : State { return states.ContainsKey(typeof(T)); }
		public bool ContainsState(System.Type T) { return states.ContainsKey(T); }

		public void RemoveAllStates() { states.Clear(); }

		public T CurrentState<T>() where T : State { return (T)currentState; }

		public T PreviousState<T>() where T : State { return (T)prevState; }

		public T GetState<T>() where T : State { return (T)states[typeof(T)]; }


		protected State currentState { get; set; }
		public State nextState { get; set; }
		public State prevState { get; set; }
		protected State initialState { get; set; }
		protected bool onEnter { get; set; }
		protected bool onExit { get; set; }

		protected System.Collections.Generic.Dictionary<System.Type, State> states = new System.Collections.Generic.Dictionary<System.Type, State>();
	}




	public interface IStateInterface
	{
		void Initialize();

		void Enter();

		void StateUpdate();
		void StateFixedUpdate();
		void StateLateUpdate();

		void Exit();

		void OnCollisionEnter(UnityEngine.Collision collision);
		void OnCollisionStay(UnityEngine.Collision collision);
		void OnCollisionExit(UnityEngine.Collision collision);

		void OnTriggerEnter(UnityEngine.Collider collider);
		void OnTriggerStay(UnityEngine.Collider collider);
		void OnTriggerExit(UnityEngine.Collider collider);

		void OnAnimatorIK(int layerIndex);

		bool isActive { get; }

		IStateMachineInterface machine { get; }

		T GetMachine<T>() where T : IStateMachineInterface;
	}


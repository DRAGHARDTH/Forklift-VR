
	public interface IStateMachineInterface
	{
		void SetInitialState<T>() where T : State;
		void SetInitialState(System.Type T);

		void ChangeState<T>() where T : State;
		void ChangeState(System.Type T);

		bool IsCurrentState<T>() where T : State;
		bool IsCurrentState(System.Type T);

		T CurrentState<T>() where T : State;
		T GetState<T>() where T : State;

		void AddState<T>() where T : State, new();
		void AddState(System.Type T);

		void RemoveState<T>() where T : State;
		void RemoveState(System.Type T);

		bool ContainsState<T>() where T : State;
		bool ContainsState(System.Type T);

		void RemoveAllStates();

		string name { get; set; }
	}	


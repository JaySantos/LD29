using UnityEngine;

public class Timer {

	/// <summary>
	/// Initializes a new instance of the <see cref="Timer"/> class.
	/// </summary>
	/// <param name="endTime">End time.</param>
	public Timer() {
		TimeEllapsed = 0;
	}

	public void Reset() {
		TimeEllapsed = 0;
	}

	public float TimeEllapsed {
		get;
		private set;
	}

	/// <summary>
	/// Updates the time.
	/// </summary>
	/// <param name="deltaTime">Delta time.</param>
	public void UpdateTime(float deltaTime) {
		TimeEllapsed += deltaTime;
	}

	public bool IsOver(float endTime, bool reset = false) {

		bool isOver = TimeEllapsed >= endTime;

		if(isOver && reset) 
			Reset();

		return isOver;
	}
}

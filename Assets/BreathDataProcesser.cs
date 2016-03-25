using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BreathDataProcesser : MonoBehaviour {
	
	private enum States {
		WAITING_ACTUALLY_SENSOR_INPUT,
		READY,
		RUNNING
	}
	
	public delegate void del();
	public static event del breathSensorReady;
	public static event del deepBreathHappened;

	//state control
	States STATE;
	bool isStable = false;
	bool firstLogOut = false, thirdLogOut = false; //log control, make sure log will only be printed once
	float waitingDataTime = 8.0f;
	
	//data process
	List<float> processedDataList;
	float dampRate = 0.03f;
	float inhaleExhalePurposeAverage =0.0f;
	int dataSetSize = 500;
	
	//detect inhale/exhale
	public bool isInhaling = false, isExhaling = false;
	
	bool isDecreasing = false, isIncreasing = false;
	int decreaseCounter = 0;
	int increaseCounter = 0;
	//how many times we ignore before swtich states between increasing and decrease
	int falutTolerantThreshold = 5; 
	int increaseFaultTolerantCounter = 0;
	int decreaseFaultTolerantCounter = 0;
	int exhaleDetermineThreshold = 10;
	
	//average inhale deepth
	public bool printEveryInhaleDepth = false;
	
	//avg calucation
	float runningAvg = 0.0f;
	List<float> runningAvgList;
	
	
	//inhale cool down stuff
	float timeStamp4Inhale = 0.0f;
	bool inhaleIsInCoolDown = false;
	float inhaleCoolDownTime = 2;
	
	//deep breath
	public int deepBreathCounter = 0;
	float deepBreathLength = 3.0f; //seconds
	float deepBreathThreshold;
	float timeStamp4DeepBreath = 0.0f;
	bool deepBreathInCoolDown = false;
	float deepBreathCoolDownTime = 5;


	//data viz
	public bool drawCurve = true;
	
	int myCounter = 0;
	
	//=====================================================================================================================
	
	void Start () {
		processedDataList = new List<float>();
		runningAvgList = new List<float>();

		deepBreathThreshold = deepBreathLength * 50;
	}
	
	//=====================================================================================================================
	
	void FixedUpdate () {

		stateController();
		if(isStable){
			float rawData = GameObject.Find("OSC").GetComponent<oscReceiver>().rawBreathData;
			float processedData = 0.0f;
			processedData = SensorProcessingMethods.smoothDataAddToList(processedDataList, rawData, dampRate ,dataSetSize);
			float tempSum = 0.0f;
			float tempLongSum = 0.0f;

			int averageFrame = 30;

			if(processedDataList.Count > averageFrame){
				for (int i=1; i<averageFrame+1; i++) {
					tempSum += processedDataList[processedDataList.Count-i];
				}
				runningAvg = tempSum / averageFrame;
				runningAvgList.Add(runningAvg);
			}
			if(drawCurve){
				if (runningAvgList.Count > 2) {
					SensorProcessingMethods.drawCurves(runningAvgList);
				}
			}
			if (runningAvgList.Count > 500) {
				runningAvgList.RemoveAt(0);
			}
			if(runningAvgList.Count > 3){
				detectInExHale();
			}
		}
	}
	//=====================================================================================================================
	void stateController(){
		switch(STATE){
			
		case States.WAITING_ACTUALLY_SENSOR_INPUT:
			if(!firstLogOut){
				Debug.Log("Waiting for the actual sensor data come in......");
				firstLogOut = true;
			}
			if(Time.time > waitingDataTime){
				STATE++;
			}
			break;

		case States.READY:
			isStable = true;
			if(breathSensorReady != null){
				breathSensorReady();
			}
			if(!thirdLogOut){
				Debug.Log("Ready");
				thirdLogOut = true;
			}
			STATE++;
			break;
		}
	}
	//=====================================================================================================================
	void detectInExHale(){
		if(!isIncreasing && !isDecreasing){
			if(runningAvg > runningAvgList[runningAvgList.Count-2]){
				isIncreasing = true;
			}else if(runningAvg < runningAvgList[runningAvgList.Count-2]){
				isDecreasing = true;
			}
		}
		
		if(isIncreasing){
			if(runningAvg > runningAvgList[runningAvgList.Count-2]){
				increaseCounter++;
				
			}else if(runningAvg < runningAvgList[runningAvgList.Count-2]){ 
				if(increaseFaultTolerantCounter < falutTolerantThreshold){
					increaseFaultTolerantCounter++;
				}else{
					increaseCounter = 0;
					isIncreasing = false;
					increaseFaultTolerantCounter = 0;
				}
			}
		}
		
		if(isDecreasing){
			if(runningAvg < runningAvgList[runningAvgList.Count-2]){
				decreaseCounter++;
//				Debug.Log(decreaseCounter);
			}else if(runningAvg > runningAvgList[runningAvgList.Count-2]){
				if(decreaseFaultTolerantCounter < falutTolerantThreshold){
					decreaseFaultTolerantCounter++;
				}else{
					decreaseCounter = 0;
					isDecreasing = false;
					decreaseFaultTolerantCounter = 0;
				}
			}
		}
		
		if(decreaseCounter >= exhaleDetermineThreshold){
			if(inhaleIsInCoolDown){
				if(Time.time - timeStamp4Inhale > inhaleCoolDownTime){
					inhaleIsInCoolDown = false;
					timeStamp4Inhale = 0.0f;
				}
			}else{
				isInhaling = true;
				inhaleIsInCoolDown = true;
				timeStamp4Inhale = Time.time;
			}
			//cool down stuff ends			
		}else if(decreaseCounter < exhaleDetermineThreshold){
			isInhaling = false;
		}

		//deep breath cool down stuff
		if(deepBreathInCoolDown){
			if(Time.time - timeStamp4DeepBreath > deepBreathCoolDownTime){
				deepBreathInCoolDown = false;
				timeStamp4DeepBreath = 0.0f;
			}
		}else{
			//if the decreasing is deeper than a certain threshold, then we add one to the deep breath counter.
			if(decreaseCounter >= deepBreathThreshold){
				deepBreathCounter++;
				Debug.LogWarning ("breath data processer: deep breath detected.");
				if(deepBreathHappened!=null){
					deepBreathHappened();
				}
				deepBreathInCoolDown = true;
				timeStamp4DeepBreath = Time.time;
			}
		}
	}
	

	//=====================================================================================================================
	
}

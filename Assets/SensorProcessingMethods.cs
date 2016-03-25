using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SensorProcessingMethods {



	//smooth! add to list!
	public static float smoothDataAddToList(List < float > _processedDataList,
		float _newInputRawData,
		float _dampRate,
		int _dataSetSize) {
		float processedDataVariable = 0.0f;

		if (_processedDataList.Count == 0) {
			_processedDataList.Add(_newInputRawData);
		} else {
			//smoother
			float difference = _newInputRawData - _processedDataList[_processedDataList.Count - 1];
			processedDataVariable = _processedDataList[_processedDataList.Count - 1] + (difference * _dampRate);
			//			detectInExHale(processedDataVariable);
			_processedDataList.Add(processedDataVariable);

			//limit list's size to dataSetSize
			if (_processedDataList.Count > _dataSetSize) {
				_processedDataList.RemoveAt(0);
			}
		}

		return processedDataVariable;
	}


	//data viz
	public static void drawCurves(List < float > _processedDataList, float _averageLineValue, bool _drawAverageFlag) {
		if (_processedDataList.Count > 2) {
			for (int i = 1; i < _processedDataList.Count; i++) {
				Vector2 point_one = new Vector2(i - 1, _processedDataList[i - 1]);
				Vector2 point_two = new Vector2(i, _processedDataList[i]);
				Debug.DrawLine(point_one, point_two);
			}
		}
		if (_drawAverageFlag) {
			Debug.DrawLine(new Vector2(0, _averageLineValue), new Vector2(_processedDataList.Count, _averageLineValue));
		}
	}

	public static void drawCurves(List < float > _processedDataList) {
		if (_processedDataList.Count > 2) {
			for (int i = 1; i < _processedDataList.Count; i++) {
				Vector2 point_one = new Vector2(i - 1, _processedDataList[i - 1]);
				Vector2 point_two = new Vector2(i, _processedDataList[i]);
				Debug.DrawLine(point_one, point_two);
			}
		}
	}



}
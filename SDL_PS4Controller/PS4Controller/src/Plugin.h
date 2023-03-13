#ifndef PLUGIN_H
#define PLUGIN_H

#include "SDL.h"
#include <iostream>
#include <fstream>

#define ACCEL_PLUGIN __declspec (dllexport)

extern "C" {
	//int  module_start(size_t sz, const void* arg);
	int ACCEL_PLUGIN InitPlugin();
	void ACCEL_PLUGIN getAccelerometerV1(float& x, float& y, float& z);
	int ACCEL_PLUGIN getAccelerometerV2();
	int ACCEL_PLUGIN getAccelerometerV4();

	void ACCEL_PLUGIN closePlugin();
}

#endif
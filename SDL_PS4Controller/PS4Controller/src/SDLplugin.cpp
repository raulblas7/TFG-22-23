#include "SDL.h"
#include <iostream>



// Elegante
#define ACCEL_PLUGIN extern "C" __declspec (dllexport) // Para seguir el convenio de C (nombre con un '_' delante) Si no se pone por defecto C++

// We need to provide an export to force the expected stub library to be generated
//PRX_EXPORT float media(float a, float b)
//{
//	std::cout << "Funcion media nativa llamada" << std::endl;
//	return (a + b) / 2;
//}

// "MAIN de la dll" que se llama al cargar por su nombre mágico module_start
extern "C" int module_start(size_t sz, const void* arg) {
	std::cout << "=========================" << std::endl;
	std::cout << "Se llama al cargar la dll" << std::endl;
	std::cout << "=========================" << std::endl;

	if (SDL_Init(SDL_INIT_JOYSTICK) < 0) {
		// Error al inicializar SDL
		return 1;
	}
	// Crear una instancia de SDL_Joystick para el mando de PS4
	/*SDL_Joystick* joystick = SDL_JoystickOpen(0);*/



	return 0;
}



ACCEL_PLUGIN void getAccelerometerV1(float& x, float& y, float& z) {

	SDL_Joystick* joystick = SDL_JoystickOpen(0);

	// Leer los valores del acelerómetro del joystick
	Sint16 accelX = SDL_JoystickGetAxis(joystick, 0);
	Sint16 accelY = SDL_JoystickGetAxis(joystick, 1);
	Sint16 accelZ = SDL_JoystickGetAxis(joystick, 2);

	// Convertir los valores a flotantes y normalizarlos en el rango [-1, 1]
	x = (float)accelX / 32767.0f;
	y = (float)accelY / 32767.0f;
	z = (float)accelZ / 32767.0f;

	// Cerrar el joystick
	SDL_JoystickClose(joystick);

}
ACCEL_PLUGIN int getAccelerometerV2() {
	SDL_Joystick* joystick = SDL_JoystickOpen(0);

	// Identificar el número de ejes del mando de PS4
	int numAxes = SDL_JoystickNumAxes(joystick);

	// Identificar el índice de eje del acelerómetro
	int accelAxis = 3; // Esto puede variar según la implementación específica del mando de PS4

		// Obtener el valor del eje correspondiente al acelerómetro
	int accelValue = SDL_JoystickGetAxis(joystick, accelAxis);

	// Cerrar el joystick
	SDL_JoystickClose(joystick);

	return accelValue;
}

//---------------------ESTE EJEMPLO NO FUNCIONA---------------------------------------------------
//PRX_EXPORT float getAccelerometerV3(float a, float b) {
//	// Configurar el subsistema de joystick
//	SDL_JoystickEventState(SDL_ENABLE);
//	SDL_JoystickOpen(0);
//
//	// Leer los valores del acelerómetro del mando de PS4
//	float accelX, accelY, accelZ;
//
//	//ESTO NO EXISTE
//	//SDL_SensorData sensorData;
//	//SDL_JoystickGetSensorData(SDL_JoystickOpen(0), SDL_SENSOR_ACCEL, &sensorData, 1);
//
//	// Convertir los valores a flotantes y normalizarlos en el rango [-1, 1]
//	accelX = sensorData.data[0].xyz[0] / 32768.0f;
//	accelY = sensorData.data[0].xyz[1] / 32768.0f;
//	accelZ = sensorData.data[0].xyz[2] / 32768.0f;
//
//	// Cerrar el joystick y detener el subsistema de joystick
//	SDL_JoystickClose(SDL_JoystickOpen(0));
//	//SDL_QuitSubSystem(SDL_INIT_JOYSTICK);
//
//	// Imprimir los valores del acelerómetro
//	printf("Acelerómetro X: %f\n", accelX);
//	printf("Acelerómetro Y: %f\n", accelY);
//	printf("Acelerómetro Z: %f\n", accelZ);
//}


int SDL_main(int argc, char* argv[]) {
	return 0;
}

ACCEL_PLUGIN void getAccelerometerV4(float& x, float& y, float& z) {
	// Configurar el subsistema de joystick
	SDL_GameControllerEventState(SDL_ENABLE);
	SDL_GameController* gameController = SDL_GameControllerOpen(0);

	// Leer los valores del acelerómetro del mando de PS4
	float *accel = (float *) malloc(sizeof(float)* 3);
	//float accelX, accelY, accelZ;

	if (SDL_GameControllerHasSensor(gameController, SDL_SENSOR_ACCEL) == SDL_TRUE) {
		std::cout << "El controller tiene Accelerometro" << std::endl;
	}
	if (SDL_GameControllerIsSensorEnabled(gameController, SDL_SENSOR_ACCEL) == SDL_TRUE) {
		std::cout << "Sensor Enabled" << std::endl;

	}
	//TODO: enviar y procesar los datos

	if (SDL_GameControllerGetSensorData(gameController, SDL_SENSOR_ACCEL, accel, 3) > -1) {
		x = accel[0];
		y = accel[1];
		z = accel[2];
	}



	

	// Cerrar el joystick y detener el subsistema de joystick
	//SDL_JoystickClose(SDL_JoystickOpen(0));
	SDL_GameControllerClose(gameController);
	//SDL_QuitSubSystem(SDL_INIT_JOYSTICK);

	// Imprimir los valores del acelerómetro
	//printf("Acelerómetro X: %f\n", accelX);
	//printf("Acelerómetro Y: %f\n", accelY);
	//printf("Acelerómetro Z: %f\n", accelZ);
}


//PRX_EXPORT float getAccelerometerV2(float a, float b) {
//	int dummy;
//
//	if (dummy & 1)
//		return a;
//	else
//		return b;
//
//	//TODO hacerlo de verdad
//}

ACCEL_PLUGIN void closePlugin() {

	SDL_QuitSubSystem(SDL_INIT_JOYSTICK);
	SDL_Quit();
}



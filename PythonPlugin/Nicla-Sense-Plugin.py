# Bleak es un software de cliente GATT, capaz de conectarse a dispositivos BLE que actúan como servidores GATT. 
# Está diseñado para proporcionar una API de Python asíncrona y multiplataforma para conectarse y comunicarse, p. sensores

import asyncio
from bleak import BleakClient
from bleak import BleakGATTCharacteristic

import struct
import threading
import keyboard

# Dirección MAC de la placa Nicla Sense ME a la que nos vamos a conectar via BLE (Bluetooth Low Energy)
NICLA_SENSE_ME_ADDRESS = "B6:FE:22:11:AA:D4"

# IDs de los valores que vamos a recoger de la placa, dichos IDs son definidos en el .ino que tiene instalado la placa
NICLA_SENSE_VALUES_TO_COLECT = {
    "orientation": "19b10000-9001-537e-4f6c-d104768a1214",
    "accelerometer": "19b10000-5001-537e-4f6c-d104768a1214",
    "gyroscope": "19b10000-6001-537e-4f6c-d104768a1214"
}

orientation = []
accelerometer = []
gyroscope = []

endConection = False

async def getValuesFromBoard(client):
    orien = await client.read_gatt_char(NICLA_SENSE_VALUES_TO_COLECT["orientation"])
    orientation = struct.unpack('<3h', orien)
    accel = await client.read_gatt_char(NICLA_SENSE_VALUES_TO_COLECT["accelerometer"])
    accelerometer = struct.unpack('<3f', accel)
    gyros = await client.read_gatt_char(NICLA_SENSE_VALUES_TO_COLECT["gyroscope"])
    gyroscope = struct.unpack('<3f', gyros)


    print('quaternion es ', orientation)
    print('accelerometer es ', accelerometer)
    print('gyroscope es ', gyroscope)


def callback(sender: BleakGATTCharacteristic, data: bytearray):
    pass

async def makeBLEConection():
    client = BleakClient(NICLA_SENSE_ME_ADDRESS)
    try:
        await client.connect() # Espera a que la placa se conecte, si no no avanza, dado que es una corrutina
        print("La conexión con la placa ha sido un éxito!")
        await client.start_notify(NICLA_SENSE_VALUES_TO_COLECT["orientation"], callback)
        await client.start_notify(NICLA_SENSE_VALUES_TO_COLECT["accelerometer"], callback)
        await client.start_notify(NICLA_SENSE_VALUES_TO_COLECT["gyroscope"], callback)
        endConection = keyboard.is_pressed('a')
        while not endConection:
            await getValuesFromBoard(client)
            endConection = keyboard.is_pressed('a')

    except Exception as exception:
        print(exception)
    finally:
        print("La Nicla va a ser desconectada...")
        await client.disconnect()

def targetThread():
    asyncio.run(makeBLEConection())

thread = threading.Thread(target=targetThread)
thread.start()
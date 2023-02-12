# Bleak es un software de cliente GATT, capaz de conectarse a dispositivos BLE que actúan como servidores GATT. 
# Está diseñado para proporcionar una API de Python asíncrona y multiplataforma para conectarse y comunicarse, p. sensores

import asyncio
from bleak import BleakClient
from bleak import BleakGATTCharacteristic

import struct
import threading
import keyboard

import socket
import pickle


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

# Variables para la gestion de la conexion TCP entre cliente y servidor
# El servidor sera inicializado en Python a partir de este script
# Sera el encargado de mandar la información recogida de la placa al cliente
# el cual escucha desde Unity
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)  # Creacion del socket
port = 8888
    
async def getValuesFromBoard(client, conn):
    orien = await client.read_gatt_char(NICLA_SENSE_VALUES_TO_COLECT["orientation"])
    orientation = struct.unpack('<3h', orien)
    accel = await client.read_gatt_char(NICLA_SENSE_VALUES_TO_COLECT["accelerometer"])
    accelerometer = struct.unpack('<3f', accel)
    gyros = await client.read_gatt_char(NICLA_SENSE_VALUES_TO_COLECT["gyroscope"])
    gyroscope = struct.unpack('<3f', gyros)

    listData = orientation + accelerometer + gyroscope
    data=pickle.dumps(listData)
    conn.sendall(data)
    
    print('orientation es ', orientation)
    print('accelerometer es ', accelerometer)
    print('gyroscope es ', gyroscope)


def callback(sender: BleakGATTCharacteristic, data: bytearray):
    pass

async def makeBLEConection(conn):
    client = BleakClient(NICLA_SENSE_ME_ADDRESS)
    try:
        await client.connect() # Espera a que la placa se conecte, si no no avanza, dado que es una corrutina
        print("La conexión con la placa ha sido un éxito!")
        await client.start_notify(NICLA_SENSE_VALUES_TO_COLECT["orientation"], callback)
        await client.start_notify(NICLA_SENSE_VALUES_TO_COLECT["accelerometer"], callback)
        await client.start_notify(NICLA_SENSE_VALUES_TO_COLECT["gyroscope"], callback)
        endConection = keyboard.is_pressed('a')
        while not endConection:
            await getValuesFromBoard(client, conn)
            endConection = keyboard.is_pressed('a')

    except Exception as exception:
        print(exception)
    finally:
        print("La Nicla va a ser desconectada...")
        await client.disconnect()

def targetThread(conn):
    asyncio.run(makeBLEConection(conn))


def initThread(conn):
    thread = threading.Thread(target=targetThread, args=(conn))
    thread.start()

def initServer():
    s.bind(('localhost', port))
    s.listen(1) #espera la conexión del cliente
    conn, addr = s.accept()
    initThread(conn)
    conn.close()

initServer()

    

# coding=utf-8

# Bleak es un software de cliente GATT, capaz de conectarse a dispositivos BLE que actúan como servidores GATT. 
# Está diseñado para proporcionar una API de Python asíncrona y multiplataforma para conectarse y comunicarse, p. sensores

import asyncio
from bleak import BleakClient
from bleak import BleakGATTCharacteristic

import struct
import threading

import socket
import select

#import os
#import sys

#rutaApp = os.path.dirname(sys.executable)


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

async def checkIfServerSendMessage(conn):
    sockets_list = [conn]
    read_sockets, _, _ = select.select(sockets_list, [], [], 0.1)

    # si hay un mensaje disponible, lo leemos
    for sock in read_sockets:
        if sock == conn:
            data = sock.recv(1024)
            print('Mensaje recibido:', data.decode())
            return True
    return False

async def getValuesFromBoard(client, conn):
    orien = await client.read_gatt_char(NICLA_SENSE_VALUES_TO_COLECT["orientation"])
    orientation = struct.unpack('<3h', orien)
    accel = await client.read_gatt_char(NICLA_SENSE_VALUES_TO_COLECT["accelerometer"])
    accelerometer = struct.unpack('<3f', accel)
    gyros = await client.read_gatt_char(NICLA_SENSE_VALUES_TO_COLECT["gyroscope"])
    gyroscope = struct.unpack('<3f', gyros)

    separador = '_'
    separadorLists = '/'

    listDataOrien = ''
    listDataAccel = ''
    listDataGyros = ''
    for num in orientation:
        listDataOrien += str(num) + separador
    
    for num in accelerometer:
        listDataAccel += str(num) + separador
    
    for num in gyroscope:
        listDataGyros += str(num) + separador
    
    listData = str(listDataOrien + separadorLists + listDataAccel + separadorLists + listDataGyros)
    conn.sendall(listData.encode())
    
    # print('orientation es ', orientation)
    #print('accelerometer es ', accelerometer)
    #print('gyroscope es ', gyroscope)


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
        #endConection = keyboard.is_pressed('a')
        var = await checkIfServerSendMessage(conn)
        while not var:
            await getValuesFromBoard(client, conn)
            var = await checkIfServerSendMessage(conn)

    except Exception as exception:
        print(exception)
    finally:
        print("El cliente va a ser desconectado...")
        conn.close()
        print("La Nicla va a ser desconectada...")

def targetThread(conn):
    asyncio.run(makeBLEConection(conn))


def initThread(conn):
    thread = threading.Thread(target=targetThread, args=(conn,))
    thread.start()

def initClient():
    s = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
    s.connect(('127.0.0.1',8888))
    return s

#initClient()
initThread(initClient())
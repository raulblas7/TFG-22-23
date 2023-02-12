import socket
import pickle

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)  # Creacion del socket
port = 8888

def initServer():
    s.bind(('localhost', port))
    s.listen(1) #espera la conexión del cliente
    conn, addr = s.accept()
    conn.sendall(b'Hello, world')
    conn.close()

initServer()
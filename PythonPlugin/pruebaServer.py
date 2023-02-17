import socket

s = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
s.bind(('', 8888))
s.listen()
print('El servidor está comenzando ...')

conn,address = s.accept()
print(address)

data = conn.recv(1024)
print('Recibir mensaje del cliente: {0}'.format(data.decode()))

conn.send('Hola a todos'.encode())

conn.close()
s.close()
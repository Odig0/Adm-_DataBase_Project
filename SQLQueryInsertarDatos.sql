-- Insertando datos en la tabla Hotel
INSERT INTO Hotel (HotelID, Nombre, Dirección, Teléfono, CorreoElectrónico, Estrellas, HoraDeEntrada, HoraDeSalida)
VALUES
(1, 'Hotel Ejemplo', 'Calle Ejemplo 123', '123456789', 'info@hotelejemplo.com', 5, '08:00:00', '18:00:00');

-- Insertando datos en la tabla TipoHabitacion
INSERT INTO TipoHabitacion (TipoID, Nombre, Descripcion, PrecioPorNoche, Capacidad)
VALUES
(1, 'Individual', 'Habitación para una sola persona', 50.00, 1),
(2, 'Doble', 'Habitación para dos personas', 80.00, 2),
(3, 'Suite', 'Habitación de lujo con servicios adicionales', 150.00, 2);

-- Insertando datos en la tabla Habitacion
INSERT INTO Habitacion (NumeroHabitacion, HotelID, TipoID, Estado)
VALUES
(101, 1, 1, 'Disponible'),
(102, 1, 1, 'Ocupada'),
(201, 1, 2, 'Disponible'),
(202, 1, 2, 'Disponible'),
(301, 1, 3, 'Disponible');

-- Insertando datos en la tabla Huesped
INSERT INTO Huesped (IDHuesped, Nombre, Apellido, FechaDeNacimiento, Direccion, Telefono, CorreoElectronico)
VALUES
(1, 'Juan', 'Pérez', '1990-05-15', 'Calle Principal 456', '987654321', 'juanperez@example.com'),
(2, 'María', 'Gómez', '1985-10-20', 'Avenida Central 789', '654321987', 'mariagomez@example.com');

-- Insertando datos en la tabla Reserva
INSERT INTO Reserva (IDReserva, IDHuesped, NumeroHabitacion, FechaDeEntrada, FechaDeSalida, PrecioTotal)
VALUES
(1, 1, 101, '2024-05-10', '2024-05-15', 250.00),
(2, 2, 201, '2024-06-01', '2024-06-05', 320.00);

-- Insertando datos en la tabla Pago
INSERT INTO Pago (IDPago, IDReserva, Monto, FechaDePago, MetodoPago)
VALUES
(1, 1, 250.00, '2024-05-10', 'Tarjeta de crédito'),
(2, 2, 320.00, '2024-06-01', 'Efectivo');

-- Insertando datos en la tabla Personal
INSERT INTO Personal (IDPersonal, HotelID, Nombre, Apellido, Posicion, Salario, FechaDeNacimiento, Telefono, FechaDeContratacion)
VALUES
(1, 1, 'Pedro', 'López', 'Recepcionista', 1500.00, '1988-02-20', '123456789', '2020-01-10'),
(2, 1, 'Ana', 'Martínez', 'Camarera', 1200.00, '1995-08-10', '987654321', '2021-03-15');

-- Insertando datos en la tabla Inventario
INSERT INTO Inventario (IDInventario, NumeroHabitacion, NombreItem, Cantidad)
VALUES
(1, 101, 'Toallas', 20),
(2, 102, 'Sábanas', 15),
(3, 201, 'Jabón', 30);

-- Insertando datos en la tabla Servicio
INSERT INTO Servicio (IDServicio, Nombre, Descripcion, Precio)
VALUES
(1, 'Desayuno', 'Desayuno buffet', 10.00),
(2, 'Wi-Fi', 'Conexión de alta velocidad', 5.00);

-- Insertando datos en la tabla ReservaServicio
INSERT INTO ReservaServicio (IDReservaServicio, IDServicio, IDReserva)
VALUES
(1, 1, 1),
(2, 2, 1),
(3, 1, 2);

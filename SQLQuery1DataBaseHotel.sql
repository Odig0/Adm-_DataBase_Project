DROP TABLE IF EXISTS TipoHabitación;
DROP TABLE IF EXISTS ReservaServicio;
DROP TABLE IF EXISTS Servicio;
DROP TABLE IF EXISTS Inventario;
DROP TABLE IF EXISTS Personal;
DROP TABLE IF EXISTS Pago;
DROP TABLE IF EXISTS Reserva;
DROP TABLE IF EXISTS Huésped;
DROP TABLE IF EXISTS Habitación;
CREATE TABLE TipoHabitacion (
    TipoID INT PRIMARY KEY,
    Nombre VARCHAR(50),
    Descripcion VARCHAR(255),
    PrecioPorNoche DECIMAL(10, 2),
    Capacidad INT
);

CREATE TABLE Habitacion (
    NumeroHabitacion INT PRIMARY KEY,
    HotelID INT,
    TipoID INT,
    Estado VARCHAR(20),
    FOREIGN KEY (HotelID) REFERENCES Hotel(HotelID),
    FOREIGN KEY (TipoID) REFERENCES TipoHabitacion(TipoID)
);

CREATE TABLE Huesped (
    IDHuesped INT PRIMARY KEY,
    Nombre VARCHAR(50),
    Apellido VARCHAR(50),
    FechaDeNacimiento DATE,
    Direccion VARCHAR(255),
    Telefono VARCHAR(15),
    CorreoElectronico VARCHAR(255)
);

CREATE TABLE Reserva (
    IDReserva INT PRIMARY KEY,
    IDHuesped INT,
    NumeroHabitacion INT,
    FechaDeEntrada DATE,
    FechaDeSalida DATE,
    PrecioTotal DECIMAL(10, 2),
    FOREIGN KEY (IDHuesped) REFERENCES Huesped(IDHuesped),
    FOREIGN KEY (NumeroHabitacion) REFERENCES Habitacion(NumeroHabitacion)
);

CREATE TABLE Pago (
    IDPago INT PRIMARY KEY,
    IDReserva INT,
    Monto DECIMAL(10, 2),
    FechaDePago DATE,
    MetodoPago VARCHAR(50),
    FOREIGN KEY (IDReserva) REFERENCES Reserva(IDReserva)
);

CREATE TABLE Personal (
    IDPersonal INT PRIMARY KEY,
    HotelID INT,
    Nombre VARCHAR(50),
    Apellido VARCHAR(50),
    Posicion VARCHAR(50),
    Salario DECIMAL(10, 2),
    FechaDeNacimiento DATE,
    Telefono VARCHAR(20),
    FechaDeContratacion DATE,
    FOREIGN KEY (HotelID) REFERENCES Hotel(HotelID)
);

CREATE TABLE Inventario (
    IDInventario INT PRIMARY KEY,
    NumeroHabitacion INT,
    NombreItem VARCHAR(100),
    Cantidad INT,
    FOREIGN KEY (NumeroHabitacion) REFERENCES Habitacion(NumeroHabitacion)
);

CREATE TABLE Servicio (
    IDServicio INT PRIMARY KEY,
    Nombre VARCHAR(50),
    Descripcion VARCHAR(255),
    Precio DECIMAL(10, 2)
);

CREATE TABLE ReservaServicio (
    IDReservaServicio INT PRIMARY KEY,
    IDServicio INT,
    IDReserva INT,
    FOREIGN KEY (IDServicio) REFERENCES Servicio(IDServicio),
    FOREIGN KEY (IDReserva) REFERENCES Reserva(IDReserva)
);


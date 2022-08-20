use Requerimiento 
go 

if exists(select top 1 1 from sys.objects where name = 'AdjuntoRequerimiento')
begin 
	drop table AdjuntoRequerimiento
end 
go

if exists(select top 1 1 from sys.objects where name = 'ComentarioRequerimiento')
begin 
	drop table ComentarioRequerimiento
end 
go

if exists(select top 1 1 from sys.objects where name = 'Requerimiento')
begin 
	drop table Requerimiento
end 
go

if exists(select top 1 1 from sys.objects where name = 'Cliente')
begin 
	drop table Cliente
end 
go

if exists(select top 1 1 from sys.objects where name = 'EstadoRequerimiento')
begin 
	drop table EstadoRequerimiento
end 
go

if exists(select top 1 1 from sys.objects where name = 'TipoRequerimiento')
begin 
	drop table TipoRequerimiento
end 
go

if exists(select top 1 1 from sys.objects where name = 'ParametrizacionRequerimiento')
begin 
	drop table ParametrizacionRequerimiento
end 
go

if exists(select top 1 1 from sys.objects where name = 'Gestor')
begin 
	drop table Gestor
end 
go

if exists(select top 1 1 from sys.objects where name = 'i_ResponsableArea_Usuario')
begin 
	DROP INDEX ResponsableArea.i_ResponsableArea_Usuario
end 
go

if exists(select top 1 1 from sys.objects where name = 'ResponsableArea')
begin 
	drop table ResponsableArea
end 
go

if exists(select top 1 1 from sys.objects where name = 'Usuario')
begin 
	drop table Usuario
end 
go

if exists(select top 1 1 from sys.objects where name = 'Area')
begin 
	drop table Area
end 
go

if exists(select top 1 1 from sys.objects where name = 'Producto')
begin 
	drop table Producto
end 
go

if exists(select top 1 1 from sys.objects where name = 'Problema')
begin 
	drop table Problema
end 
go

if exists(select top 1 1 from sys.objects where name = 'TiempoSolucion')
begin 
	drop table TiempoSolucion
end 
go


Create table Area (
IdArea int identity(1,1),
Codigo varchar(200),
Nombre varchar(200),
FechaCreacion datetime, 
FechaActualizacion datetime, 
Estado bit, 
Primary Key(IdArea)
)
go

create table Usuario(
IdUsuario int identity(1,1),
NombreUsuario varchar(200),
Contrasenia varchar(200),
FechaCreacion datetime, 
FechaActualizacion datetime, 
Estado bit, 
Primary Key(IdUsuario)
)
go

create table ResponsableArea(
IdResponsableArea int identity(1,1),
IdUsuario int, 
IdArea int, 
FechaCreacion datetime, 
FechaActualizacion datetime, 
Estado bit,
foreign key (IdUsuario) references Usuario(IdUsuario),
foreign key (IdArea) references Area(IdArea)
)
go

CREATE UNIQUE INDEX i_ResponsableArea_Usuario ON ResponsableArea (IdUsuario, IdArea)
go

Create table Producto (
IdProducto int identity(1,1),
Codigo varchar(200),
Nombre varchar(200),
FechaCreacion datetime, 
FechaActualizacion datetime, 
Estado bit, 
Primary Key(IdProducto)
)
go

Create table Problema (
IdProblema int identity(1,1),
Codigo varchar(200),
Nombre varchar(200),
FechaCreacion datetime, 
FechaActualizacion datetime, 
Estado bit, 
Primary Key(IdProblema)
)
go

Create table TiempoSolucion (
IdTiempoSolucion int identity(1,1),
Codigo varchar(200),
Nombre varchar(200),
Valor int,
FechaCreacion datetime, 
FechaActualizacion datetime, 
Estado bit, 
Primary Key(IdTiempoSolucion)
)
go

create table ParametrizacionRequerimiento(
IdParametrizacionRequerimiento int identity(1,1),
IdArea int,
IdProducto int,
IdProblema int,
IdTiempoSolucion int,
FechaCreacion datetime, 
FechaActualizacion datetime, 
Estado bit, 
Primary Key(IdParametrizacionRequerimiento),
foreign key (IdArea) references Area(IdArea),
foreign key (IdProducto) references Producto(IdProducto),
foreign key (IdProblema) references Problema(IdProblema),
foreign key (IdTiempoSolucion) references TiempoSolucion(IdTiempoSolucion),
)
go 

Create table Gestor (
IdGestor  int identity(1,1),
IdArea int, 
IdUsuario int, 
FechaCreacion datetime, 
FechaActualizacion datetime, 
Estado bit, 
Primary Key(IdGestor),
foreign key (IdUsuario) references Usuario(IdUsuario),
foreign key (IdArea) references Area(IdArea)
)
go

Create table Cliente(
IdCliente int identity(1,1),
Identificacion varchar(20),
Nombres varchar(400),
Apellidos varchar(400),
Celular varchar(400),
Telefono varchar(100),
Email varchar(400),
FechaCreacion datetime, 
FechaActualizacion datetime, 
Primary Key(IdCliente),
)
go

create table EstadoRequerimiento
(
IdEstadoRequerimiento int identity(1,1),
Codigo varchar(200),
Nombre varchar(200),
FechaCreacion datetime, 
FechaActualizacion datetime, 
Estado bit, 
Primary Key(IdEstadoRequerimiento)
)
go

create table TipoRequerimiento
(
IdTipoRequerimiento int identity(1,1),
Codigo varchar(200),
Nombre varchar(200),
FechaCreacion datetime, 
FechaActualizacion datetime, 
Estado bit, 
Primary Key(IdTipoRequerimiento)
)
go

create table Requerimiento
(
IdRequerimiento int identity(1,1),
IdParametrizacionRequerimiento int,
IdTipoRequerimiento int,
IdEstadoRequerimiento int, 
IdCliente int,
Codigo varchar(100),
FechaCreacion datetime, 
FechaActualizacion datetime, 
Primary Key(IdRequerimiento),
foreign key (IdParametrizacionRequerimiento) references ParametrizacionRequerimiento(IdParametrizacionRequerimiento),
foreign key (IdTipoRequerimiento) references TipoRequerimiento(IdTipoRequerimiento),
foreign key (IdEstadoRequerimiento) references EstadoRequerimiento(IdEstadoRequerimiento),
foreign key (IdCliente) references Cliente(IdCliente),
)
go

create table AdjuntoRequerimiento(
IdAdjuntoRequerimiento int identity(1,1),
NombreArchivo varchar(400),
Tipo varchar(200),
IdGestor int, 
IdRequerimiento int, 
FechaCreacion datetime, 
FechaActualizacion datetime, 
Primary Key(IdAdjuntoRequerimiento),
foreign key (IdGestor) references Gestor(IdGestor),
foreign key (IdRequerimiento) references Requerimiento(IdRequerimiento)
)
go

create table ComentarioRequerimiento
(
IdComentarioRequerimiento int identity(1,1),
IdGestor int, 
IdRequerimiento int, 
Comentario varchar(400),
FechaCreacion datetime, 
FechaActualizacion datetime,
foreign key (IdGestor) references Gestor(IdGestor),
foreign key (IdRequerimiento) references Requerimiento(IdRequerimiento),
Primary Key(IdComentarioRequerimiento),
)
go
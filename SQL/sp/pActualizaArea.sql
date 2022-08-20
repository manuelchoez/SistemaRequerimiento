use Requerimiento
go 

if exists (select top  1 1 from sys.objects where name = 'pActualizaArea' and type = 'P')
begin 
	drop procedure pActualizaArea
end 
go 

create procedure pActualizaArea (
@IdArea int, 
@Codigo varchar(200),
@Nombre varchar(200),
@Estado bit
)
as 

update area
set Codigo = @Codigo,
Nombre = @Nombre,
Estado = @Estado,
FechaActualizacion =  getdate()
where IdArea = @IdArea

go

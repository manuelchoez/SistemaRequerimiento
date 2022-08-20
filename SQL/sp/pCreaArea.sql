use Requerimiento
go 

if exists (select top  1 1 from sys.objects where name = 'pCreaArea' and type = 'P')
begin 
	drop procedure pCreaArea
end 
go 

create procedure pCreaArea (
@Codigo varchar(200),
@Nombre varchar(200)
)
as 

insert into area (
Codigo,
Nombre,
FechaCreacion,
Estado)
values (@Codigo,@Nombre,getdate(),1)


go

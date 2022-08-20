use Requerimiento
go 

if exists (select top  1 1 from sys.objects where name = 'pConsultaArea' and type = 'P')
begin 
	drop procedure pConsultaArea
end 
go 

create procedure pConsultaArea
as 

select 
IdArea,
Codigo,
Nombre,
FechaCreacion,
FechaActualizacion,
Estado
from area with(nolock)
where estado =1
go

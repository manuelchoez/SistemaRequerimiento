use Requerimiento
go 

if exists (select top  1 1 from sys.objects where name = 'pConsultaIdArea' and type = 'P')
begin 
	drop procedure pConsultaIdArea
end 
go 

create procedure pConsultaIdArea (@IdArea int)
as 

select 
IdArea,
Codigo,
Nombre,
FechaCreacion,
FechaActualizacion,
Estado
from area with(nolock)
where IdArea = @IdArea
go

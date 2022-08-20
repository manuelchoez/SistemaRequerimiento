use Requerimiento
go 

if exists (select top  1 1 from sys.objects where name = 'pEliminaArea' and type = 'P')
begin 
	drop procedure pEliminaArea
end 
go 

create procedure pEliminaArea (
@IdArea int
)
as 

update area set estado = 0 where idarea = @IdArea


go

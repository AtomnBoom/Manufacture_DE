declare @json NVARCHAR(MAX)

select @json = BulkColumn
from openrowset(
bulk 'C:\Заказчики.json',
single_nclob) -- что бы заработало следуйте инструкции из папки Temp в проекте
as fileDate;

insert into Customer (Id, Name, INN, Addres, Phone, IsSalesman, IsBuyer)
select id, name, inn, addres, phone, salesman, buyer
from openjson(@json)
with (
id int '$.id',
name	nvarchar(100)	'$.name',
inn	nchar(12)	'$.inn',
addres	nvarchar(100)	'$.addres',
phone	nvarchar(11)	'$.phone',
salesman	bit	'$.salesman',
buyer	bit	'$.buyer');

select * from Customer
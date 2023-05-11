Create Database ExpenseTracker

use ExpenseTracker

create Table ETracker
(
Transaction_Id int identity primary key,
Title varchar(50),
Descriptionn varchar(100),
Amount int,
Datee date
)

select * from ETracker
select sum(Amount) as AvailableBalance from ETracker
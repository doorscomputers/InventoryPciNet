select * from Stock where ItemName like 'EPSON L121'
select * from BranchStock where Stock=422

select * from Stock where ItemName like 'EPSON L1210'
select * from BranchStock where Stock=443

update stock set Cost = 5000, LastCost=5000, LatestCost=5000 where OID=443
update BranchStock set Cost = 5000 where Stock=443
update BranchStock set Quantity = 2 where OID=443
update BranchStock set Quantity = 5 where OID=1667
update BranchStock set Quantity = 13 where OID=2891



update stock set Cost = 5000, LastCost=5000, LatestCost=5000 where OID=422
update BranchStock set Cost = 5000 where Stock=422
update BranchStock set Quantity = 2 where OID=422
update BranchStock set Quantity = 5 where OID=1646




Delete from DeliveryDetail where OID>800
Delete from DeliveryHeader where OID>167

select * from DeliveryDetail
select * from DeliveryHeader
use db_aa6a53_pcnet
select * from Branch
select * from BranchStock
select * from DeliveryHeader
select * from DeliveryDetail
select * from itemledger
select * from Stock
select * from SaleHeader
select * from SaleDetail
select * from Transfer
select * from TransferDetail

delete from SaleSerial
delete from SaleDetail
delete from SaleRefund
delete from CustomerPayment
delete from SaleHeader
delete from DeliverySerial
delete from DeliveryDetail
delete from DeliveryHeader
delete from TransferDetail
delete from Transfer
delete from WarrantySerial
delete from WarrantyDetail
delete from Warranty
delete from BoSerial
delete from BoDetail
delete from Bo
delete from ManualInventoryDetail
delete from ManualInventory
delete from itemledger
delete from TransferDetail
delete from Transfer
--delete from supplier
--delete from BranchStock
--delete from Stock
--Update BranchStock set Quantity=0


DBCC CHECKIDENT('SaleSerial' , RESEED, 0)
DBCC CHECKIDENT('SaleDetail' , RESEED, 0)
DBCC CHECKIDENT('SaleHeader' , RESEED, 0)
DBCC CHECKIDENT('SaleRefund' , RESEED, 0)
DBCC CHECKIDENT('CustomerPayment' , RESEED, 0)
DBCC CHECKIDENT('DeliveryHeader' , RESEED, 0)
DBCC CHECKIDENT('DeliveryDetail' , RESEED, 0)
DBCC CHECKIDENT('DeliverySerial' , RESEED, 0)
DBCC CHECKIDENT('Transfer' , RESEED, 0)
DBCC CHECKIDENT('TransferDetail' , RESEED, 0)
DBCC CHECKIDENT('WarrantySerial' , RESEED, 0)
DBCC CHECKIDENT('WarrantyDetail' , RESEED, 0)
DBCC CHECKIDENT('ManualInventoryDetail' , RESEED, 0)
DBCC CHECKIDENT('ManualInventory' , RESEED, 0)
DBCC CHECKIDENT('Warranty' , RESEED, 0)
DBCC CHECKIDENT('BoSerial' , RESEED, 0)
DBCC CHECKIDENT('BoDetail' , RESEED, 0)
DBCC CHECKIDENT('Bo' , RESEED, 0)
DBCC CHECKIDENT('ItemLedger' , RESEED, 0)
DBCC CHECKIDENT('Transfer' , RESEED, 0)
DBCC CHECKIDENT('TransferDetail' , RESEED, 0)
--DBCC CHECKIDENT('Stock' , RESEED, 0)
--DBCC CHECKIDENT('BranchStock' , RESEED, 0)

--DBCC CHECKIDENT('BranchStock' , RESEED, 0)
--DBCC CHECKIDENT('Stock' , RESEED, 0)
--DBCC CHECKIDENT('Customer' , RESEED, 0)
--DBCC CHECKIDENT('Brand' , RESEED, 0)


--update SaleHeader set Branch=1


-- Example insert for testing (adjust values according to your data setup)
--INSERT INTO dbo.DeliveryDetail (DeliveryHeader, Stock, QtyDelivered, Cost, SubTotal, Discount, Amount)
--VALUES (1, 1, 10, 100.00, 1000.00, 0.00, 1000.00);




USE [db_aa6a53_pcnet]
GO

-- Insert distinct categories from MasterList into the Category table
INSERT INTO dbo.Category (CategoryName)
SELECT DISTINCT Category
FROM dbo.MasterList
WHERE Category IS NOT NULL AND NOT EXISTS (
    SELECT 1
    FROM dbo.Category c
    WHERE c.CategoryName = dbo.MasterList.Category
);

GO



USE [db_aa6a53_pcnet]
GO

-- Insert distinct brands from MasterList into the Brand table
INSERT INTO dbo.Brand (BrandName)
SELECT DISTINCT Brand
FROM dbo.MasterList
WHERE Brand IS NOT NULL AND NOT EXISTS (
    SELECT 1
    FROM dbo.Brand b
    WHERE b.BrandName = dbo.MasterList.Brand
);

GO

USE [db_aa6a53_pcnet]
GO

-- Update CategoryID in MasterList based on matching Category names in the Category table
UPDATE ml
SET ml.CategoryID = c.OID
FROM dbo.MasterList ml
INNER JOIN dbo.Category c ON ml.Category = c.CategoryName;

GO



USE [db_aa6a53_pcnet]
GO

-- Update BrandID in MasterList based on matching Brand names in the Brand table
UPDATE ml
SET ml.BrandID = b.OID
FROM dbo.MasterList ml
INNER JOIN dbo.Brand b ON ml.Brand = b.BrandName;

GO

USE [db_aa6a53_pcnet]
GO

-- Update supplierid in MasterList based on matching supplier names in the supplier table
UPDATE ml
SET ml.supplierid = b.OID
FROM dbo.MasterList ml
INNER JOIN dbo.Supplier b ON ml.Supplier = b.SupplierName;

GO


-- Update OID on FinaList based on matching Itemcode on both tables
UPDATE fl
SET fl.OID = s.OID
FROM dbo.FinalList fl
INNER JOIN dbo.Stock s ON fl.Barcode = s.ItemCode

GO


select * from MasterList
select * from Stock


USE [db_aa6a53_pcnet]
GO

-- Insert records from MasterList into Stock table with specific field mappings
INSERT INTO dbo.Stock (ItemCode, ItemName, Category, Brand, Cost, Price)
SELECT 
    Barcode,
    [Item Desc],
    categoryid,
    brandid,
    Cost,
    Retail
FROM dbo.MasterList;

GO





-- Check for existing stock-branch combinations to avoid duplicates
INSERT INTO BranchStock (Branch, Stock, Quantity,Cost,Price)
SELECT b.OID AS BranchID, s.OID AS StockID, 0 AS Quantity,Cost,Price -- Initialize Quantity to 0
FROM Branch b
CROSS JOIN Stock s
WHERE NOT EXISTS (
    SELECT 1
    FROM BranchStock bs
    WHERE bs.Branch = b.OID AND bs.Stock = s.OID
);

update Stock set Active=1
update BranchStock set Active=1

USE db_aa6a53_pcnet;
	GO
	-- Truncate the log by changing the database recovery model to SIMPLE.
	ALTER DATABASE db_aa6a53_pcnet
	SET RECOVERY SIMPLE;
	GO
	-- Shrink the truncated log file to 1 MB.
	DBCC SHRINKFILE (db_aa6a53_pcnet_Log, 1);
	GO
	-- Reset the database recovery model.
	ALTER DATABASE db_aa6a53_pcnet
	SET RECOVERY FULL;
	GO



	INSERT INTO dbo.Supplier(SupplierName)
SELECT DISTINCT Supplier
FROM dbo.MasterList
WHERE Supplier IS NOT NULL AND NOT EXISTS (
    SELECT 1
    FROM dbo.Supplier c
    WHERE c.SupplierName = dbo.MasterList.Supplier
);

GO

select * from Supplier ORDER by SupplierName
select * from Masterlist
Select * from stock where itemname='13ITL5-82EV002KPH | 13.3QHD | I5-1135G7 | 8GD4 | 512G | IRIS | W10 | HS2019'


USE [db_aa6a53_pcnet]
GO

UPDATE BS
SET BS.Quantity = ML.Warehouse
FROM BranchStock BS
INNER JOIN Masterlist ML ON BS.Stock = ML.OID
WHERE BS.Branch = 1;
GO


UPDATE BS
SET BS.Quantity = ML.Main
FROM BranchStock BS
INNER JOIN Masterlist ML ON BS.Stock = ML.OID
WHERE BS.Branch = 2;
GO


UPDATE BS
SET BS.Quantity = ML.Bambang
FROM BranchStock BS
INNER JOIN Masterlist ML ON BS.Stock = ML.OID
WHERE BS.Branch = 27;
GO




select * from Stock where lastcost is not null




USE [db_aa6a53_pcnet]
GO

CREATE TABLE [dbo].[FinalInventory](
    [Branch] [int] NOT NULL,
    [Stock] [int] NOT NULL,
    [FinalQuantity] [float] NOT NULL,
    PRIMARY KEY (Branch, Stock)
) ON [PRIMARY]
GO


USE [db_aa6a53_pcnet]
GO

CREATE TABLE [dbo].[InitialInventory](
    [Branch] [int] NOT NULL,
    [Stock] [int] NOT NULL,
    [InitialQuantity] [float] NOT NULL,
    PRIMARY KEY (Branch, Stock)
) ON [PRIMARY]
GO


-- Check for existing stock-branch combinations to avoid duplicates
INSERT INTO InitialInventory (Branch, Stock, InitialQuantity)
SELECT b.OID AS BranchID, s.OID AS StockID, 0 AS Quantity -- Initialize Quantity to 0
FROM Branch b
CROSS JOIN Stock s
WHERE NOT EXISTS (
    SELECT 1
    FROM InitialInventory bs
    WHERE bs.Branch = b.OID AND bs.Stock = s.OID
);


UPDATE BS
SET BS.InitialQuantity = ML.Warehouse
FROM InitialInventory BS
INNER JOIN Masterlist ML ON BS.Stock = ML.OID
WHERE BS.Branch = 1;
GO

UPDATE BS
SET BS.InitialQuantity = ML.Main
FROM InitialInventory BS
INNER JOIN Masterlist ML ON BS.Stock = ML.OID
WHERE BS.Branch = 2;
GO


UPDATE BS
SET BS.InitialQuantity = ML.Bambang
FROM InitialInventory BS
INNER JOIN Masterlist ML ON BS.Stock = ML.OID
WHERE BS.Branch = 27;
GO




USE [db_aa6a53_pcnet]
GO

CREATE PROCEDURE dbo.CalculateFinalInventory
AS
BEGIN
    SET NOCOUNT ON;

    -- Clear the FinalInventory table before inserting new data
    TRUNCATE TABLE dbo.FinalInventory;

    WITH DeliveryTotals AS (
        SELECT 
            DD.Stock,
            DD.Branch,
            SUM(DD.QtyDelivered) AS TotalDelivered
        FROM DeliveryDetail DD
        GROUP BY DD.Stock, DD.Branch
    ),
    SaleTotals AS (
        SELECT 
            SD.Stock,
            SD.Branch,
            SUM(SD.QtySold) AS TotalSold
        FROM SaleDetail SD
        GROUP BY SD.Stock, SD.Branch
    ),
    BoTotals AS (
        SELECT 
            BD.Stock,
            BD.Branch,
            SUM(BD.Quantity) AS TotalReturned
        FROM BoDetail BD
        GROUP BY BD.Stock, BD.Branch
    ),
    TransferOutTotals AS (
        SELECT 
            TD.Stock,
            T.FromBranch AS Branch,
            SUM(TD.Quantity) AS TotalTransferredOut
        FROM TransferDetail TD
        INNER JOIN Transfer T ON TD.Transfer = T.OID
        GROUP BY TD.Stock, T.FromBranch
    ),
    TransferInTotals AS (
        SELECT 
            TD.Stock,
            T.ToBranch AS Branch,
            SUM(TD.Quantity) AS TotalTransferredIn
        FROM TransferDetail TD
        INNER JOIN Transfer T ON TD.Transfer = T.OID
        GROUP BY TD.Stock, T.ToBranch
    ),
    WarrantyTotals AS (
        SELECT 
            WD.Stock,
            WD.Branch,
            SUM(WD.Quantity) AS TotalWarranty
        FROM WarrantyDetail WD
        GROUP BY WD.Stock, WD.Branch
    )
    INSERT INTO dbo.FinalInventory (Branch, Stock, FinalQuantity)
    SELECT 
        II.Branch,
        II.Stock,
        II.InitialQuantity 
        + COALESCE(DT.TotalDelivered, 0)
        - COALESCE(ST.TotalSold, 0)
        - COALESCE(BT.TotalReturned, 0)
        - COALESCE(TOT.TotalTransferredOut, 0)
        + COALESCE(TIT.TotalTransferredIn, 0)
        + COALESCE(WT.TotalWarranty, 0) AS FinalQuantity
    FROM 
        InitialInventory II
    LEFT JOIN DeliveryTotals DT ON II.Stock = DT.Stock AND II.Branch = DT.Branch
    LEFT JOIN SaleTotals ST ON II.Stock = ST.Stock AND II.Branch = ST.Branch
    LEFT JOIN BoTotals BT ON II.Stock = BT.Stock AND II.Branch = BT.Branch
    LEFT JOIN TransferOutTotals TOT ON II.Stock = TOT.Stock AND II.Branch = TOT.Branch
    LEFT JOIN TransferInTotals TIT ON II.Stock = TIT.Stock AND II.Branch = TIT.Branch
    LEFT JOIN WarrantyTotals WT ON II.Stock = WT.Stock AND II.Branch = WT.Branch
    ORDER BY II.Branch, II.Stock;
END
GO

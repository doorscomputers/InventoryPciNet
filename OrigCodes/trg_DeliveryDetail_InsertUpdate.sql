USE [db_aa6a53_pcnet]
GO
/****** Object:  Trigger [dbo].[trg_DeliveryDetail_InsertUpdate]    Script Date: 11/13/2024 9:51:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [dbo].[trg_DeliveryDetail_InsertUpdate]
ON [dbo].[DeliveryDetail]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Temporary table to store previous quantities
    DECLARE @PreviousQuantities TABLE (StockID INT, BranchID INT, PreviousQty INT);

    -- Populate temporary table with current quantities from BranchStock
    INSERT INTO @PreviousQuantities (StockID, BranchID, PreviousQty)
    SELECT bs.Stock, ins.Branch, bs.Quantity
    FROM dbo.BranchStock bs
    INNER JOIN inserted ins ON bs.Stock = ins.Stock AND bs.Branch = ins.Branch;

    -- Conditionally update BranchStock.Quantity based on the QtyDelivered for the given Branch and Stock, only if DeliveryHeader is not NULL
    UPDATE bs
    SET Quantity = Quantity + ins.QtyDelivered
    FROM inserted ins
    INNER JOIN dbo.BranchStock bs ON bs.Stock = ins.Stock AND bs.Branch = ins.Branch
    WHERE ins.QtyDelivered IS NOT NULL AND ins.DeliveryHeader IS NOT NULL;

    -- Insert audit records into ItemLedger, only if DeliveryHeader is not NULL
    INSERT INTO dbo.ItemLedger (TransactionType, TransactionDate, UserID, BranchID, BranchName, InvoiceNumber, TransactionOid, StockID, ItemName, PreviousQty, CurrentQty, QtyChange, Remarks)
    SELECT 
        'Delivery', 
        GETDATE(), 
        ins.CreatedBy, 
        ins.Branch, 
        b.BranchName, 
        NULL AS InvoiceNumber,
        ins.OID AS TransactionOid, 
        ins.Stock, 
        s.ItemName, 
        pq.PreviousQty,
        pq.PreviousQty + ins.QtyDelivered AS FinalQty, 
        ins.QtyDelivered AS QtyChanged, 
        'Delivery Inserted' AS Remarks
    FROM 
        inserted ins
    INNER JOIN dbo.Branch b ON ins.Branch = b.OID
    INNER JOIN dbo.Stock s ON ins.Stock = s.OID
    INNER JOIN @PreviousQuantities pq ON pq.StockID = ins.Stock AND pq.BranchID = ins.Branch
    WHERE ins.DeliveryHeader IS NOT NULL;

	--------New Code
	-- Update the Stock table based on the inserted or updated DeliveryDetail
    UPDATE S
    SET 
        S.LastDelivery = I.LastModifiedDate,
        S.LastSupplier = SU.[SupplierName],
        S.LastQtyDelivered = I.QtyDelivered,
        S.LastCost = S.Cost,
        S.LatestCost = I.Cost
    FROM 
        [dbo].[Stock] S
    INNER JOIN 
        inserted I ON S.OID = I.Stock
    INNER JOIN 
        [dbo].[DeliveryHeader] DH ON I.DeliveryHeader = DH.OID
    INNER JOIN 
        [dbo].[Supplier] SU ON S.Supplier = SU.OID
    WHERE 
        I.DeliveryHeader IS NOT NULL;



END;

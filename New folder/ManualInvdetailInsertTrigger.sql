USE [db_aa6a53_pcnet]
GO
/****** Object:  Trigger [dbo].[trg_ManualInventoryDetail_Insert]    Script Date: 04/14/2024 3:29:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [dbo].[trg_ManualInventoryDetail_Insert]
ON [dbo].[ManualInventoryDetail]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Create a temporary table to hold pre-update quantities.
    DECLARE @PreUpdateQuantities TABLE (
        StockID INT,
        BranchID INT,
        PreUpdateQuantity INT
    );

    -- Populate the temporary table with current BranchStock quantities for the inserted ManualInventoryDetails.
    INSERT INTO @PreUpdateQuantities (StockID, BranchID, PreUpdateQuantity)
    SELECT bs.Stock, bs.Branch, bs.Quantity
    FROM BranchStock bs
    INNER JOIN inserted i ON bs.Stock = i.Stock AND bs.Branch = i.Branch;

    -- Update BranchStock quantities based on ManualInventoryDetail.
    UPDATE BranchStock
    SET Quantity = i.Quantity
    FROM inserted i
    INNER JOIN BranchStock bs ON bs.Stock = i.Stock AND bs.Branch = i.Branch;

    -- Insert audit records into ItemLedger.
    INSERT INTO ItemLedger (
        TransactionType,
        TransactionDate,
        UserID,
        BranchID,
        BranchName,
        InvoiceNumber,
        TransactionOid,
        StockID,
        ItemName,
        PreviousQty,
        CurrentQty,
        QtyChange,
        Remarks
    )
    SELECT 
        'Manual Inventory Count' AS TransactionType,
        GETDATE() AS TransactionDate,
        i.CreatedBy AS UserID,
        i.Branch AS BranchID,
        (SELECT BranchName FROM Branch WHERE OID = i.Branch) AS BranchName,
        NULL AS InvoiceNumber,
        i.OID AS TransactionOid,
        i.Stock AS StockID,
        (SELECT ItemName FROM Stock WHERE OID = i.Stock) AS ItemName,
        pu.PreUpdateQuantity AS PreviousQty,
        i.Quantity AS CurrentQty,
        i.Quantity - pu.PreUpdateQuantity AS QtyChange,
        CASE 
            WHEN i.Quantity > pu.PreUpdateQuantity THEN 'Manual count exceeds system count'
            WHEN i.Quantity < pu.PreUpdateQuantity THEN 'Manual count is lower than system count'
            ELSE 'Manual count matches system count'
        END AS Remarks
    FROM inserted i
    INNER JOIN @PreUpdateQuantities pu ON i.Stock = pu.StockID AND i.Branch = pu.BranchID;
END;

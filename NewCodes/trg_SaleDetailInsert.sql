USE [db_aa6a53_pcnet]
GO
/****** Object:  Trigger [dbo].[trg_SaleDetail_Insert]    Script Date: 11/13/2024 2:34:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [dbo].[trg_SaleDetail_Insert]
ON [dbo].[SaleDetail]
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Temporary table to store previous quantities for auditing
        DECLARE @PreviousQuantities TABLE (StockID INT, BranchID INT, PreviousQty INT);

        -- Populate temporary table with current quantities from BranchStock
        INSERT INTO @PreviousQuantities (StockID, BranchID, PreviousQty)
        SELECT bs.Stock, ins.Branch, bs.Quantity
        FROM dbo.BranchStock bs
        INNER JOIN inserted ins ON bs.Stock = ins.Stock AND bs.Branch = ins.Branch;

        -- Conditionally subtract sold quantity from BranchStock only if SaleHeader is not NULL and QtySold is greater than zero
        UPDATE bs
        SET bs.Quantity = bs.Quantity - ins.QtySold
        FROM inserted ins
        INNER JOIN dbo.BranchStock bs WITH (ROWLOCK) ON bs.Stock = ins.Stock AND bs.Branch = ins.Branch
        WHERE ins.QtySold IS NOT NULL AND ins.QtySold > 0 AND ins.SaleHeader IS NOT NULL;

        -- Conditionally insert audit trail only if SaleHeader is not NULL
        INSERT INTO dbo.ItemLedger (TransactionType, TransactionDate, UserID, BranchID, BranchName, InvoiceNumber, TransactionOid, StockID, ItemName, PreviousQty, CurrentQty, QtyChange, Remarks)
        SELECT
            'Sale',
            GETDATE(),
            ins.CreatedBy,
            ins.Branch,
            b.BranchName,
            NULL AS InvoiceNumber, -- Assuming there might not be an invoice number directly available
            ins.OID AS TransactionOid,
            ins.Stock,
            stk.ItemName,
            pq.PreviousQty AS PreviousQty,
            pq.PreviousQty - ins.QtySold AS CurrentQty,
            -ins.QtySold AS QtyChange,
            'Sale Inserted'
        FROM inserted ins
        INNER JOIN dbo.Branch b WITH (NOLOCK) ON ins.Branch = b.OID
        INNER JOIN dbo.Stock stk WITH (NOLOCK) ON ins.Stock = stk.OID
        INNER JOIN @PreviousQuantities pq ON pq.StockID = ins.Stock AND pq.BranchID = ins.Branch
        WHERE ins.SaleHeader IS NOT NULL;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        -- Log the error if necessary
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR (@ErrorMessage, 16, 1);
    END CATCH
END;

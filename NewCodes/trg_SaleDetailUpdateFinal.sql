USE [db_aa6a53_pcnet]
GO
/****** Object:  Trigger [dbo].[trg_SaleDetail_Update]    Script Date: 11/13/2024 2:40:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [dbo].[trg_SaleDetail_Update]
ON [dbo].[SaleDetail]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Update BranchStock.Quantity based on the changes in QtySold
        UPDATE bs
        SET bs.Quantity = bs.Quantity - (i.QtySold - d.QtySold)
        FROM inserted i
        INNER JOIN deleted d ON i.OID = d.OID
        INNER JOIN dbo.BranchStock bs WITH (ROWLOCK) ON i.Stock = bs.Stock
        INNER JOIN dbo.SaleHeader sh ON i.SaleHeader = sh.OID
        WHERE bs.Branch = sh.Branch AND i.QtySold <> d.QtySold AND i.QtySold > 0;

        -- Insert the audit records for changes, capturing PreviousEditedQty and CurrentEditedQty
        INSERT INTO dbo.ItemLedger (
            TransactionType, TransactionDate, UserID, BranchID, BranchName, InvoiceNumber, TransactionOid, 
            StockID, ItemName, PreviousQty, CurrentQty, QtyChange, Remarks, PreviousEditedQty, CurrentEditedQty)
        SELECT
            'Sale Update',
            GETDATE(),
            i.LastModifiedBy,
            sh.Branch,
            b.BranchName,
            sh.InvoiceNumber,
            i.SaleHeader AS TransactionOid,
            i.Stock,
            stk.ItemName,
            bs.Quantity + (d.QtySold - i.QtySold) AS PreviousQty, -- Calculate previous stock quantity before sale update
            bs.Quantity AS CurrentQty,
            -(i.QtySold - d.QtySold) AS QtyChange,
            CASE 
                WHEN i.QtySold <> d.QtySold THEN 'QtySold Edited'
                ELSE 'Edited without QtySold Change'
            END AS Remarks,
            d.QtySold AS PreviousEditedQty, -- Previous QtySold before this transaction
            i.QtySold AS CurrentEditedQty -- New QtySold for this transaction
        FROM inserted i
        INNER JOIN deleted d ON i.OID = d.OID
        INNER JOIN dbo.SaleHeader sh ON i.SaleHeader = sh.OID
        INNER JOIN dbo.Branch b ON sh.Branch = b.OID
        INNER JOIN dbo.Stock stk ON i.Stock = stk.OID
        INNER JOIN dbo.BranchStock bs ON bs.Stock = i.Stock AND sh.Branch = bs.Branch;

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
